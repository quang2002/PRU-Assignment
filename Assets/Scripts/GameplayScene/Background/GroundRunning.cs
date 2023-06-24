namespace GameplayScene.Background
{
    using System.Collections.Generic;
    using UnityEngine;

    public class GroundRunning : MonoBehaviour
    {
        [field: SerializeField]
        public Camera Camera { get; private set; }

        public List<RunningBack> RunningBacks { get; } = new();

        public float LeftBound  { get; set; }
        public float RightBound { get; set; }

        private void Awake()
        {
            this.LeftBound  = this.Camera.ViewportToWorldPoint(Vector3.zero).x;
            this.RightBound = this.Camera.ViewportToWorldPoint(Vector3.one).x;

            this.RunningBacks.AddRange(this.GetComponentsInChildren<RunningBack>());
        }

        private void Update()
        {
            foreach (var children in this.RunningBacks)
            {
                if (children.transform.position.x < this.LeftBound - children.TileWidth / 2f)
                {
                    children.transform.position += Vector3.right * (this.RightBound - this.LeftBound + children.TileWidth);
                }
            }
        }
    }
}