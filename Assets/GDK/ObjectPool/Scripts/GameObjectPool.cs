namespace GDK.ObjectPool.Scripts
{
    using UnityEngine;

    public abstract class GameObjectPool<TObject> : UnityObjectPool<TObject>
        where TObject : Component
    {
        protected override void OnInstantiate(TObject obj)
        {
            obj.gameObject.SetActive(true);
        }

        protected override void OnRelease(TObject obj)
        {
            obj.gameObject.SetActive(false);
        }
    }
}