namespace GDK.LocalData.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Newtonsoft.Json;
    using UnityEngine;

    public class LocalDataManager
    {
        public void LoadAllLocalData()
        {
            foreach (var (localDataType, localData) in this.LocalDataCache)
            {
                var localDataKey  = GetLocalDataKeyByType(localDataType);
                var localDataJson = PlayerPrefs.GetString(localDataKey);

                if (string.IsNullOrEmpty(localDataJson))
                {
                    localData.Initialize();
                    return;
                }

                var localDataValue = JsonConvert.DeserializeObject(localDataJson, localDataType);

                foreach (var fieldInfo in localDataType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    if (fieldInfo.GetCustomAttributes(typeof(JsonIgnoreAttribute), true).Any())
                        continue;
                    fieldInfo.SetValue(localData, fieldInfo.GetValue(localDataValue));
                }

                this.Logger.Log($"<color=green>Load local data: {localDataType.Name}</color>");
            }
        }

        public void SaveAllLocalData()
        {
            foreach (var (localDataType, localData) in this.LocalDataCache)
            {
                var localDataKey  = GetLocalDataKeyByType(localDataType);
                var localDataJson = JsonConvert.SerializeObject(localData);

                PlayerPrefs.SetString(localDataKey, localDataJson);
            }

            PlayerPrefs.Save();

            this.Logger.Log("<color=green>Save all local data</color>");
        }

        public void SaveLocalData(Type localDataType)
        {
            var localDataKey  = GetLocalDataKeyByType(localDataType);
            var localDataJson = JsonConvert.SerializeObject(this.LocalDataCache[localDataType]);

            PlayerPrefs.SetString(localDataKey, localDataJson);
            PlayerPrefs.Save();

            this.Logger.Log($"<color=green>Save local data: {localDataType.Name}</color>");
        }

        public static string GetLocalDataKeyByType(Type localDataType)
        {
            return $"LD-{localDataType.Name}";
        }

        #region Inject

        public ILogger Logger { get; }

        private Dictionary<Type, ILocalData> LocalDataCache { get; } = new();

        public LocalDataManager(List<ILocalData> localDataList, ILogger logger)
        {
            this.Logger = logger;
            foreach (var localData in localDataList)
            {
                this.LocalDataCache.Add(localData.GetType(), localData);
            }

            this.LoadAllLocalData();
        }

        #endregion
    }
}