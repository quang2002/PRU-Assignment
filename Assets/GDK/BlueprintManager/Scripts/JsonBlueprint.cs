namespace GDK.BlueprintManager.Scripts
{

    using System;
    using System.Reflection;
    using GDK.AssetsManager.Scripts;
    using Newtonsoft.Json;
    using UnityEngine;

    public abstract class JsonBlueprint : IBlueprint
    {
        protected JsonBlueprint(IAssetsManager assetsManager)
        {
            this.AssetsManager = assetsManager;
        }
        public          IAssetsManager AssetsManager  { get; }
        public abstract string         AddressableKey { get; }

        public void LoadBlueprint()
        {
            var input = this.AssetsManager.Load<TextAsset>(this.AddressableKey).text;

            var data = JsonConvert.DeserializeObject(input, this.GetType());

            if (data is null) throw new Exception($"Failed to deserialize blueprint {this.GetType().FullName}");

            foreach (var fieldInfo in data.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                fieldInfo.SetValue(this, fieldInfo.GetValue(data));
            }
        }
    }

}