namespace GDK.ObjectPool
{
    public interface IPooledObject
    {
        IObjectPool ObjectPool { get; protected internal set; }
    }
}