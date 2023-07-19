namespace Common
{
    using DG.Tweening;
    using GDK.ObjectPool;
    using TMPro;
    using UnityEngine;

    [RequireComponent(typeof(CanvasGroup), typeof(TextMeshProUGUI))]
    public class FloatingDamageText : MonoBehaviour, IPooledObject
    {
        IObjectPool IPooledObject.ObjectPool { get; set; }

        public CanvasGroup     CanvasGroup     { get; private set; }
        public TextMeshProUGUI TextMeshProUGUI { get; private set; }

        [field: SerializeField]
        public Vector3 BigScale { get; private set; } = new(1.2f, 1.2f, 1.2f);

        [field: SerializeField]
        public float ScaleDuration { get; private set; } = 0.2f;

        [field: SerializeField]
        public float ScaleDelay { get; private set; } = 0.4f;

        [field: SerializeField]
        public float FadeOutDelay { get; private set; } = 0.4f;

        [field: SerializeField]
        public float FadeOutDuration { get; private set; } = 0.4f;

        [field: SerializeField]
        public float MoveAmount { get; private set; } = 50;

        private void Awake()
        {
            this.CanvasGroup     = this.GetComponent<CanvasGroup>();
            this.TextMeshProUGUI = this.GetComponent<TextMeshProUGUI>();
        }

        public void FadeOut(bool isCritical)
        {
            this.TextMeshProUGUI.color = isCritical ? Color.red : Color.white;

            this.CanvasGroup.alpha = 1;

            this.gameObject.transform
                .DOScale(this.BigScale, this.ScaleDuration)
                .SetEase(Ease.Linear)
                .SetLoops(2, LoopType.Yoyo)
                .OnComplete(this.FadeOutText);
        }

        private void FadeOutText()
        {
            DOTween.To(() => this.CanvasGroup.alpha, (value) => this.CanvasGroup.alpha = value, 0, this.FadeOutDuration)
                   .SetDelay(this.FadeOutDelay);

            this.gameObject.transform
                .DOLocalMoveY(this.transform.localPosition.y + this.MoveAmount, this.FadeOutDuration)
                .SetDelay(this.FadeOutDelay)
                .OnComplete(this.Release);
        }
    }
}