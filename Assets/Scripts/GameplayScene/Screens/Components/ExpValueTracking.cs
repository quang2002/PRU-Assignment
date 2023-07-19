namespace GameplayScene.Screens.Components
{
    using System;
    using Models.DataControllers;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    public class ExpValueTracking : MonoBehaviour
    {
        [field: SerializeField]
        public Slider Slider { get; private set; }

        [field: SerializeField]
        public TMP_Text LevelText { get; private set; }

        #region Inject

        private MainLocalDataController MainLocalDataController { get; set; }

        [Inject]
        private void Inject(MainLocalDataController mainLocalDataController)
        {
            this.MainLocalDataController = mainLocalDataController;
        }

        #endregion

        private void Update()
        {
            this.Slider.value   = (this.MainLocalDataController.Exp % 100) / 100f;
            this.LevelText.text = $"LEVEL {this.MainLocalDataController.Level}";
        }
    }
}