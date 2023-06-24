namespace GDK.ObjectPool.Scripts
{
    public interface IObjectPool<TObject>
    {
        TObject    Instantiate();
        void Release(TObject obj);
    }
}