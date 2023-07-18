namespace GDK.ObjectPool
{
    public interface IObjectPool
    {
        object Instantiate(object model);
        void   Release(object     obj);
    }
}