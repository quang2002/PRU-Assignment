namespace GDK.AssetsManager
{
    using UnityEngine;

    public static class AssetsManagerExtensions
    {
        public static Sprite LoadSprite(this IAssetsManager assetsManager, string assetName)
        {
            try
            {
                return assetsManager.Load<Sprite>(assetName);
            }
            finally
            {
                assetsManager.Unload(assetName);
            }
        }
    }
}