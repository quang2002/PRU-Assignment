namespace GameplayScene.Screens.Components
{
    using Models.DataControllers;
    using TMPro;
    using UnityEngine;
    using Utilities;
    using Zenject;

    public class CoinValueTracking : MonoBehaviour
    {
        [field: SerializeField]
        public TMP_Text TextValue { get; private set; }

        public long OldCoinValue { get; private set; } = -1;


        private void Update()
        {
            if (this.OldCoinValue != this.MainLocalDataController.Coins)
            {
                this.OldCoinValue   = this.MainLocalDataController.Coins;
                this.TextValue.text = this.MainLocalDataController.Coins.ToShortString();
            }
        }

        #region Inject

        public MainLocalDataController MainLocalDataController { get; private set; }

        [Inject]
        private void Inject(MainLocalDataController mainLocalDataController)
        {
            this.MainLocalDataController = mainLocalDataController;
        }

        #endregion
    }
}