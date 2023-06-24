namespace GDK.ObjectPool.Scripts
{
    public interface IObjectPool<TObject>
        where TObject : IPooledObject<TObject>
    {
        TObject Instantiate();
        void    Release(TObject obj);
    }
}