namespace GDK.AssetsManager.Scripts
{
    public interface IAssetsManager
    {
        TAsset Load<TAsset>(string assetName) where TAsset : UnityEngine.Object;
        void   Unload(string       assetName);
    }
}