namespace GDK.AssetsManager.Scripts
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public class AssetsManager : IAssetsManager
    {
        private Dictionary<string, AsyncOperationHandle> LoadedAssets { get; } = new();

        public TAsset Load<TAsset>(string assetName) where TAsset : Object
        {
            if (!this.LoadedAssets.TryGetValue(assetName, out var op))
            {
                op = Addressables.LoadAssetAsync<TAsset>(assetName);
                this.LoadedAssets.Add(assetName, op);
            }

            return (TAsset)op.WaitForCompletion();
        }

        public void Unload(string assetName)
        {
            if (this.LoadedAssets.TryGetValue(assetName, out var op))
            {
                Addressables.Release(op);
                this.LoadedAssets.Remove(assetName);
            }
        }
    }
}