namespace GDK.ObjectPool
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public abstract class UnityObjectPool<TObject> : MonoBehaviour, IObjectPool<TObject>
        where TObject : Object, IPooledObject<TObject>
    {
        protected HashSet<TObject> UsedObjects { get; } = new();
        protected HashSet<TObject> FreeObjects { get; } = new();

        public TObject Instantiate()
        {
            var obj = this.FreeObjects.FirstOrDefault();

            if (obj == null)
            {
                obj            = this.CreateObject();
                obj.ObjectPool = this;
            }
            else
            {
                this.FreeObjects.Remove(obj);
            }

            this.UsedObjects.Add(obj);
            this.OnInstantiate(obj);
            return obj;
        }

        public void Release(TObject obj)
        {
            if (this.UsedObjects.Remove(obj))
            {
                this.FreeObjects.Add(obj);
                this.OnRelease(obj);
                return;
            }

            Destroy(obj);
        }

        protected abstract TObject CreateObject();
        protected abstract void    OnInstantiate(TObject obj);
        protected abstract void    OnRelease(TObject     obj);
    }
}