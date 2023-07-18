namespace GDK.ObjectPool
{
    public static class ObjectPoolExtensions
    {
        public static void Release(this IPooledObject pooledObject)
        {
            pooledObject.ObjectPool?.Release(pooledObject);
        }
    }
}