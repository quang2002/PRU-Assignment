namespace GDK.AssetsManager
{
    using UnityEngine;

    public interface IAssetsManager
    {
        TAsset Load<TAsset>(string assetName) where TAsset : Object;
        void   Unload(string       assetName);
    }
}