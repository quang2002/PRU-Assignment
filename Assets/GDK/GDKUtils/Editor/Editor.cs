namespace GDK.GDKUtils.Editor
{
    using System.Linq;
    using UnityEditor;

    public class Editor<T> : Editor
        where T : UnityEngine.Object
    {
        // ReSharper disable once InconsistentNaming
        public new T target => base.target as T;

        // ReSharper disable once InconsistentNaming
        public new T[] targets => base.targets.Select(t => t as T).ToArray();
    }
}