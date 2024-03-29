namespace GDK.BlueprintManager
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using GDK.AssetsManager;
    using Newtonsoft.Json;
    using UnityEngine;

    public abstract class JsonArrayBlueprint<TValue> : IBlueprint, IEnumerable<TValue>
    {
        #region Inject

        protected JsonArrayBlueprint(IAssetsManager assetsManager)
        {
            this.AssetsManager = assetsManager;
        }

        public IAssetsManager AssetsManager { get; }

        #endregion

        public abstract string AddressableKey { get; }

        public List<TValue> Data { get; private set; }

        public void LoadBlueprint()
        {
            var input = this.AssetsManager.Load<TextAsset>(this.AddressableKey).text;
            var type  = typeof(List<>).MakeGenericType(typeof(TValue));

            if (JsonConvert.DeserializeObject(input, type) is not List<TValue> data)
            {
                throw new Exception($"Failed to deserialize blueprint {this.GetType().FullName}");
            }

            this.Data = data;

            this.AssetsManager.Unload(this.AddressableKey);
        }

        public IEnumerator<TValue> GetEnumerator() => this.Data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}