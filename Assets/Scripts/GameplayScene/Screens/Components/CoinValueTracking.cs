namespace GameplayScene.Screens.Components
{

    using System.Numerics;
    using GDK.GDKUtils.Scripts;
    using Models.LocalData;
    using TMPro;
    using UnityEngine;
    using Utilities;

    public class CoinValueTracking : MonoBehaviour
    {
        [field: SerializeField]
        public TMP_Text TextValue { get; private set; }

        public MainLocalData MainLocalData { get; private set; }
        public BigInteger    OldCoinValue  { get; private set; } = -1;

        private void Awake()
        {
            this.MainLocalData = this.GetContainer().Resolve<MainLocalData>();
        }

        private void Update()
        {
            if (this.OldCoinValue != this.MainLocalData.Coins)
            {
                this.OldCoinValue   = this.MainLocalData.Coins;
                this.TextValue.text = this.MainLocalData.Coins.ToShortString();
            }
        }
    }

}