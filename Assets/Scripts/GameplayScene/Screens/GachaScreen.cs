using DG.Tweening;

namespace GameplayScene.Screens
{
    using GDK.UIManager;
    using UnityEngine;
    using UnityEngine.UI;

    public class GachaScreen : BasePopup
    {
        #region View

        [field: SerializeField] public Button ButtonX1 { get; private set; }

        [field: SerializeField] public Button ButtonX10 { get; private set; }

        [field: SerializeField] public Button ButtonClose { get; private set; }

        [field: SerializeField] public Transform ItemListTransform { get; private set; }

        [field: SerializeField] public Image ImageSpin { get; private set; }

        [field: SerializeField] public Image ImageChest { get; private set; }
        
        [field: SerializeField] public Image ImageRaiden { get; private set; }

        #endregion

        protected override bool IsOverlay => false;
        private Tween _tweenSpin, _tweenChestShake, _tweenChestScale, _tweenRaiden;

        protected override void OnInit()
        {
            base.OnInit();
            this.ButtonX1.onClick.AddListener(this.OnClickGachaX1);
            this.ButtonX10.onClick.AddListener(this.OnClickGachaX10);
            this.ButtonClose.onClick.AddListener(this.Close);
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
        }

        private void ClearAllItems()
        {
            foreach (Transform child in this.ItemListTransform)
            {
                Destroy(child.gameObject);
            }
        }

        private void OnClickGachaX10()
        {
        }

        private void OnClickGachaX1()
        {
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            this._tweenSpin.Kill();
            this._tweenChestShake.Kill();
            this._tweenChestScale.Kill();
            this._tweenRaiden.Kill();
        }
    }
}