namespace GDK.GDKUtils.Scripts
{
    using UnityEngine;
    using Zenject;

    public static class ZenjectUtils
    {
        public static DiContainer GetContainer()
        {
            return Object.FindObjectOfType<ProjectContext>()?.Container ??
                   Object.FindObjectOfType<SceneContext>()?.Container;
        }

        public static DiContainer GetContainer(this object _)
        {
            return GetContainer();
        }
    }
}