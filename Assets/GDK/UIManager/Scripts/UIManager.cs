namespace GDK.UIManager.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using GDK.AssetsManager.Scripts;
    using UnityEngine;
    using UnityEngine.Assertions;
    using UnityEngine.EventSystems;
    using Zenject;

    public class UIManager : MonoBehaviour
    {
        #region Serialized

        [field: Header("Containers")]

        #region

        [field: SerializeField]
        public RectTransform Temp { get; private set; }

        [field: SerializeField]
        public RectTransform Page { get; private set; }

        [field: SerializeField]
        public RectTransform Overlay { get; private set; }

        #endregion

        [field: Header("Misc")]

        #region

        [field: SerializeField]
        public Camera UICamera { get; private set; }

        [field: SerializeField]
        public EventSystem EventSystem { get; private set; }

        #endregion

        #endregion

        #region Fields & Properties

        [Inject]
        private DiContainer Container { get; }

        [Inject]
        private IAssetsManager AssetsManager { get; }

        private Dictionary<Type, BaseScreen> Screens   { get; } = new();
        private Stack<BasePage>              PageStack { get; } = new();

        #endregion

        #region Public API

        #region Generic API

        public void OpenScreen<T>(object data = null) where T : BaseScreen
        {
            this.OpenScreen(typeof(T), data);
        }

        public bool TryOpenScreen<T>(object data = null) where T : BaseScreen
        {
            return this.TryOpenScreen(typeof(T), data);
        }

        public void CloseScreen<T>() where T : BaseScreen
        {
            this.CloseScreen(typeof(T));
        }

        public bool TryCloseScreen<T>() where T : BaseScreen
        {
            return this.TryCloseScreen(typeof(T));
        }

        #endregion

        #region Non-Generic API

        public void OpenScreen(Type screenType, object data = null)
        {
            AssertScreenType(screenType);

            if (!this.Screens.TryGetValue(screenType, out var screen))
            {
                screen = this.InitScreen(screenType);
                this.Screens.TryAdd(screenType, screen);
            }

            screen.Data = data;

            switch (screen)
            {
                case BasePage page:
                    if (!this.PageStack.TryPeek(out var topPage) || topPage != page)
                    {
                        if (topPage) topPage.Hide();
                        page.Show();
                        this.PageStack.Push(page);
                    }

                    break;
                case BasePopup popup:
                    if (!popup.IsVisible) popup.Show();

                    break;
            }
        }

        public void CloseScreen(Type screenType)
        {
            AssertScreenType(screenType);

            if (!this.Screens.TryGetValue(screenType, out var screen)) return;

            switch (screen)
            {
                case BasePage page:
                    if (this.PageStack.TryPeek(out var topPage) && topPage == page)
                    {
                        this.PageStack.Pop();
                        page.Hide();
                    }

                    break;
                case BasePopup popup:
                    if (popup.IsVisible) popup.Hide();

                    break;
            }
        }

        public bool TryOpenScreen(Type screenType, object data = null)
        {
            try
            {
                this.OpenScreen(screenType, data);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return false;
            }

            return true;
        }

        public bool TryCloseScreen(Type screenType)
        {
            try
            {
                this.CloseScreen(screenType);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return false;
            }

            return true;
        }

        #endregion

        #endregion

        #region Private API

        private BaseScreen InitScreen(Type screenType)
        {
            AssertScreenType(screenType);

            var addressableKey =
                (screenType.GetCustomAttribute(typeof(ScreenInfoAttribute)) as ScreenInfoAttribute)?.ID ??
                screenType.Name;

            var prefab = this.AssetsManager.Load<GameObject>(addressableKey);

            var screen = this.Container.InstantiatePrefab(prefab, this.Temp).GetComponent<BaseScreen>();

            screen.Init();

            return screen;
        }

        private static void AssertScreenType(Type screenType)
        {
            Assert.IsTrue(typeof(BaseScreen).IsAssignableFrom(screenType), "screenType must be implements IScreen");
        }

        #endregion
    }
}