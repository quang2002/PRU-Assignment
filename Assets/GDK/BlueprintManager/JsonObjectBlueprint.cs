namespace GDK.BlueprintManager
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using GDK.AssetsManager;
    using Newtonsoft.Json;
    using UnityEngine;

    public abstract class JsonObjectBlueprint<TValue> : IBlueprint, IEnumerable<KeyValuePair<string, TValue>>
    {
        #region Inject

        protected JsonObjectBlueprint(IAssetsManager assetsManager)
        {
            this.AssetsManager = assetsManager;
        }

        public IAssetsManager AssetsManager { get; }

        #endregion

        public abstract string AddressableKey { get; }

        protected Dictionary<string, TValue> Data { get; private set; }

        public void LoadBlueprint()
        {
            var input = this.AssetsManager.Load<TextAsset>(this.AddressableKey).text;
            var type  = typeof(Dictionary<,>).MakeGenericType(typeof(string), typeof(TValue));

            if (JsonConvert.DeserializeObject(input, type) is not Dictionary<string, TValue> data)
            {
                throw new Exception($"Failed to deserialize blueprint {this.GetType().FullName}");
            }

            this.Data = data;

            this.AssetsManager.Unload(this.AddressableKey);
        }

        public TValue this[string key] => this.Data.TryGetValue(key, out var value) ? value : default;

        public TValue this[Enum key] => this[key.ToString()];

        public TValue this[int key] => this[key.ToString()];

        public IEnumerable<TValue>                       Values          => this.Data.Values;
        public IEnumerable<string>                       Keys            => this.Data.Keys;
        public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator() => this.Data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}