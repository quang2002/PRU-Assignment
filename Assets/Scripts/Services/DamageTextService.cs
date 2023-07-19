namespace Services
{
    using Common;
    using GDK.ObjectPool;
    using UnityEngine;

    [RequireComponent(typeof(Canvas))]
    public class DamageTextService : UnityObjectPool<DamageTextService.Model, FloatingDamageText>
    {
        [field: SerializeField]
        public GameObject TextPrefab { get; private set; }

        public class Model
        {
            public long    Damage   { get; init; }
            public Vector3 Position { get; init; }
            public bool    Critical { get; init; }
        }

        public Canvas Canvas { get; private set; }

        private void Awake()
        {
            this.Canvas = this.GetComponent<Canvas>();
        }

        protected override FloatingDamageText CreateObject(Model model)
        {
            return Instantiate(this.TextPrefab, this.transform).GetComponent<FloatingDamageText>();
        }

        protected override void OnInstantiate(FloatingDamageText obj, Model model)
        {
            obj.gameObject.SetActive(true);

            obj.transform.position   = model.Position;
            obj.TextMeshProUGUI.text = model.Damage.ToString();

            obj.FadeOut(model.Critical);
        }

        protected override void OnRelease(FloatingDamageText obj)
        {
            obj.gameObject.SetActive(false);
        }
    }
}