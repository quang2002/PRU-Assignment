namespace GameplayScene.Background
{
    using System;
    using GDK.ObjectPool.Scripts;
    using UnityEngine;

    [RequireComponent(typeof(SpriteRenderer))]
    public class RunningBack : MonoBehaviour, IPooledObject<RunningBack>
    {
        public IObjectPool<RunningBack> ObjectPool { get; set; }

        public float          TileWidth      { get; set; }
        public SpriteRenderer SpriteRenderer { get; set; }

        #region Serialized Fields

        [field: SerializeField]
        public float Speed { get; set; }

        #endregion

        private void Awake()
        {
            this.SpriteRenderer = this.GetComponent<SpriteRenderer>();
            this.TileWidth      = this.SpriteRenderer.bounds.size.x;
        }

        private void Update()
        {
            this.transform.Translate(Vector3.left * (this.Speed * Time.deltaTime));
        }

        public void UpdateSprite(Sprite sprite)
        {
            this.SpriteRenderer.sprite = sprite;
            this.TileWidth             = this.SpriteRenderer.bounds.size.x;
        }
    }
}