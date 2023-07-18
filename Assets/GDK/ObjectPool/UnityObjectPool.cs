namespace GDK.ObjectPool
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public abstract class UnityObjectPool<TModel, TObject> : MonoBehaviour, IObjectPool
        where TObject : Object, IPooledObject
    {
        protected HashSet<TObject> UsedObjects { get; } = new();
        protected HashSet<TObject> FreeObjects { get; } = new();

        object IObjectPool.Instantiate(object model)
        {
            return this.Instantiate((TModel)model);
        }

        void IObjectPool.Release(object obj)
        {
            this.Release((TObject)obj);
        }

        public TObject Instantiate(TModel model = default)
        {
            var obj = this.FreeObjects.FirstOrDefault();

            if (obj == null)
            {
                obj            = this.CreateObject(model);
                obj.ObjectPool = this;
            }
            else
            {
                this.FreeObjects.Remove(obj);
            }

            this.UsedObjects.Add(obj);
            this.OnInstantiate(obj, model);
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

        protected abstract TObject CreateObject(TModel   model);
        protected abstract void    OnInstantiate(TObject obj, TModel model);
        protected abstract void    OnRelease(TObject     obj);
    }

    public abstract class UnityObjectPool<TObject> : UnityObjectPool<object, TObject>
        where TObject : Object, IPooledObject
    {
        protected sealed override TObject CreateObject(object model) => this.CreateObject();

        protected sealed override void OnInstantiate(TObject obj, object model) => this.OnInstantiate(obj);

        protected abstract TObject CreateObject();
        protected abstract void    OnInstantiate(TObject obj);
    }
}