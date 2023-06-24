namespace LoadingScene.Screens
{
    using System;
    using GameplayScene.Screens;
    using GDK.UIManager.Scripts;
    using TMPro;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class LoadingScreen : BasePage
    {
        #region View

        [field: SerializeField]
        public Slider SliderProgress { get; set; }

        [field: SerializeField]
        public TMP_Text TxtProgress { get; set; }

        #endregion

        private float NewProgressValue { get; set; }

        private float CurProgressValue
        {
            get => this.SliderProgress.value;
            set
            {
                this.SliderProgress.value = value;
                this.TxtProgress.text     = $"{value:P}";
            }
        }


        protected override void OnShow()
        {
            base.OnShow();
            this.NewProgressValue = this.CurProgressValue = 0;

            // TODO: Fake loading, remove it when you will have real loading
            this.NewProgressValue = 1;
        }

        private void Update()
        {
            this.CurProgressValue = Mathf.Lerp(this.CurProgressValue, this.NewProgressValue, Time.deltaTime);

            if (Math.Abs(this.CurProgressValue - this.NewProgressValue) < 0.01f)
            {
                this.CurProgressValue = this.NewProgressValue;
            }

            if (Math.Abs(this.CurProgressValue - 1) < 0.01f)
            {
                SceneManager.LoadScene("GameplayScene");
            }
        }
    }
}