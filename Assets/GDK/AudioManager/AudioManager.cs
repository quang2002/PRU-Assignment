namespace GDK.AudioManager
{
    using System.Collections.Generic;
    using UnityEngine;

    [ExecuteAlways]
    public class AudioManager : MonoBehaviour
    {
        public Dictionary<string, AudioData> Audios { get; } = new();

        private void Awake()
        {
            var invalidAudios = new List<GameObject>();

            foreach (Transform child in this.transform)
            {
                var childAudioData = child.GetComponent<AudioData>();

                if (childAudioData is null || string.IsNullOrWhiteSpace(childAudioData.Addressable) || this.Audios.ContainsKey(childAudioData.Addressable))
                {
                    invalidAudios.Add(child.gameObject);
                    continue;
                }

                childAudioData.Initialize();

                this.Audios.Add(childAudioData.Addressable, childAudioData);
            }

            foreach (var invalidAudio in invalidAudios)
            {
                DestroyImmediate(invalidAudio);
            }
        }

        public AudioData GetAudioData(string key)
        {
            if (this.Audios.TryGetValue(key, out var audioData))
                return audioData.LoadAudioClipIfNull();
            throw new KeyNotFoundException($"{key} not found");
        }

        public void AddAudioData()
        {
            _ = new GameObject("", typeof(AudioData))
            {
                transform =
                {
                    parent = this.transform
                }
            };
        }
    }
}