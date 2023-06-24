namespace GDK.ObjectPool.Scripts
{
    public interface IPooledObject<TObject>
        where TObject : IPooledObject<TObject>
    {
        IObjectPool<TObject> ObjectPool { get; set; }
    }
}