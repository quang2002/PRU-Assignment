namespace GameplayScene.Screens
{
    using System.Linq;
    using DG.Tweening;
    using GDK.AssetsManager;
    using GDK.UIManager;
    using Models.Blueprint;
    using Models.DataControllers;
    using Signals;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    public class GachaScreen : BasePopup
    {
        #region View

        [field: SerializeField]
        public Button ButtonX1 { get; private set; }

        [field: SerializeField]
        public Button ButtonX10 { get; private set; }

        [field: SerializeField]
        public Button ButtonClose { get; private set; }

        [field: SerializeField]
        public Transform ItemListTransform { get; private set; }

        [field: SerializeField]
        public Image ImageSpin { get; private set; }

        [field: SerializeField]
        public Image ImageChest { get; private set; }

        [field: SerializeField]
        public Image ImageRaiden { get; private set; }

        [field: SerializeField]
        public GameObject ItemTemplate { get; private set; }

        #endregion

        #region Inject

        private SkillBlueprint          SkillBlueprint          { get; set; }
        private IAssetsManager          AssetsManager           { get; set; }
        private InventoryDataController InventoryDataController { get; set; }
        private MainLocalDataController MainLocalDataController { get; set; }
        private SignalBus               SignalBus               { get; set; }

        [Inject]
        public void Inject(SkillBlueprint          skillBlueprint,
                           IAssetsManager          assetsManager,
                           InventoryDataController inventoryDataController,
                           MainLocalDataController mainLocalDataController,
                           SignalBus               signalBus)
        {
            this.SkillBlueprint          = skillBlueprint;
            this.AssetsManager           = assetsManager;
            this.InventoryDataController = inventoryDataController;
            this.MainLocalDataController = mainLocalDataController;
            this.SignalBus               = signalBus;
        }

        #endregion

        protected override bool  IsOverlay => false;
        private const      long  CoinToGacha = 10;
        private            Tween _tweenSpin, _tweenChestShake, _tweenChestScale, _tweenRaiden;

        protected override void OnInit()
        {
            base.OnInit();
            this.ButtonX1.onClick.AddListener(this.OnClickGachaX1);
            this.ButtonX10.onClick.AddListener(this.OnClickGachaX10);
            this.ButtonClose.onClick.AddListener(this.Close);
            this.SignalBus.Subscribe<CoinChangedSignal>(this.BindGacha);
        }

        protected override void OnShow()
        {
            base.OnShow();

            this.ClearAllItems();
            this._tweenSpin = this.ImageSpin.transform
                                  .DORotate(new Vector3(0, 0, -360), 2, RotateMode.FastBeyond360)
                                  .SetLoops(-1, LoopType.Restart)
                                  .SetEase(Ease.Linear);

            this._tweenChestShake = this.ImageChest.transform
                                        .DOShakeRotation(2, new Vector3(0, 0, 15), 5, 45)
                                        .SetLoops(-1, LoopType.Yoyo)
                                        .SetEase(Ease.Linear);

            this._tweenChestScale = this.ImageChest.transform
                                        .DOScale(Vector3.one * 1.3f, 0.8f)
                                        .SetLoops(-1, LoopType.Yoyo)
                                        .SetEase(Ease.InSine);

            this._tweenRaiden = this.ImageRaiden.transform
                                    .DOScale(Vector3.one * 1.1f, 0.8f)
                                    .SetLoops(-1, LoopType.Yoyo)
                                    .SetEase(Ease.InSine);

            this.BindGacha();
        }

        private void ClearAllItems()
        {
            foreach (Transform child in this.ItemListTransform)
            {
                if (child == this.ItemTemplate.transform) continue;
                Destroy(child.gameObject);
            }
        }

        private void OnClickGachaX10()
        {
            this.ClearAllItems();
            for (var i = 0; i < 10; i++)
            {
                this.GachaSkill();
            }
        }

        private void OnClickGachaX1()
        {
            this.ClearAllItems();
            this.GachaSkill();
        }

        private void GachaSkill()
        {
            var randomSkill = this.SkillBlueprint.ElementAt(Random.Range(0, this.SkillBlueprint.Count()));

            var skillIcon = Instantiate(this.ItemTemplate, this.ItemListTransform);
            skillIcon.GetComponent<Image>().sprite = this.AssetsManager.LoadSprite(randomSkill.Value.Icon);
            skillIcon.SetActive(true);

            this.MainLocalDataController.Coins -= CoinToGacha;
            this.InventoryDataController.AddSkillLevel(randomSkill.Key, 1);
        }

        private void BindGacha()
        {
            this.ButtonX1.interactable  = this.MainLocalDataController.Coins >= CoinToGacha;
            this.ButtonX10.interactable = this.MainLocalDataController.Coins >= CoinToGacha * 10;
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            this._tweenSpin.Kill();
            this._tweenChestShake.Kill();
            this._tweenChestScale.Kill();
            this._tweenRaiden.Kill();
            this.SignalBus.Unsubscribe<CoinChangedSignal>(this.BindGacha);
        }
    }
}