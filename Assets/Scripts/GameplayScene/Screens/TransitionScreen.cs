namespace GameplayScene.Screens
{
    using System;
    using DG.Tweening;
    using GDK.UIManager;
    using UnityEngine;

    public class TransitionScreen : BasePopup<TransitionScreen.Model>
    {
        public class Model
        {
            public Action BeforeTransition { get; init; }
            public Action WhileTransition  { get; init; }
            public Action AfterTransition  { get; init; }
            public float  Duration         { get; init; }
            public float  Delay            { get; init; }
        }

        [field: SerializeField]
        public RectTransform TweenObject { get; private set; }

        protected override bool IsOverlay => true;

        protected override void OnShow()
        {
            base.OnShow();

            this.Data.BeforeTransition?.Invoke();
            this.ScaleOut();
        }

        private void ScaleOut()
        {
            this.TweenObject
                .DOScale(Vector3.one * 100f, this.Data.Duration)
                .SetEase(Ease.Linear)
                .SetDelay(this.Data.Delay)
                .OnComplete(this.ScaleIn);
        }

        private void ScaleIn()
        {
            this.Data.WhileTransition?.Invoke();

            this.TweenObject
                .DOScale(Vector3.zero, this.Data.Duration)
                .SetEase(Ease.Linear)
                .SetDelay(this.Data.Delay)
                .OnComplete(this.Close);
        }

        protected override void OnHide()
        {
            base.OnHide();
            this.Data.AfterTransition?.Invoke();
        }
    }
}