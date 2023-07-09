namespace GameplayScene.Background
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Image))]
    public class ScrollingImage : MonoBehaviour
    {
        [field: SerializeField]
        public Vector2 Direction { get; private set; }

        public Image Image { get; private set; }

        private void Awake()
        {
            this.Image          = this.GetComponent<Image>();
            this.Image.material = new Material(this.Image.material);
        }

        private void Update()
        {
            this.Image.material.mainTextureOffset += this.Direction * Time.deltaTime;
        }
    }
}