namespace GDK.AudioManager.Scripts
{
    using GDK.AssetsManager.Scripts;
    using UnityEngine;
    using Zenject;

    [ExecuteAlways]
    [RequireComponent(typeof(AudioSource))]
    public class AudioData : MonoBehaviour, IInitializable
    {
        [field: SerializeField]
        public string Addressable { get; internal set; }

        [field: SerializeField]
        public bool Preload { get; internal set; }

        [Inject]
        private IAssetsManager AssetsManager { get; }

        public AudioSource AudioSource { get; internal set; }

        public void Play(ulong delay = 0UL)
        {
            if (delay != 0UL)
                this.AudioSource.Play(delay);
            this.AudioSource.Play();
        }

        public void PlayOneShot()
        {
            this.AudioSource.PlayOneShot(this.AudioSource.clip);
        }

        public void Stop()
        {
            this.AudioSource.Stop();
        }

        public void Pause()
        {
            this.AudioSource.Pause();
        }

        public void UnPause()
        {
            this.AudioSource.UnPause();
        }

        private void OnValidate()
        {
            this.gameObject.name = string.IsNullOrWhiteSpace(this.Addressable) ? "AudioData" : this.Addressable;
        }

        public void Initialize()
        {
            this.AudioSource = this.GetComponent<AudioSource>();

            this.AudioSource.playOnAwake = false;
            this.AudioSource.loop        = false;

            if (this.Preload) this.LoadAudioClipIfNull();
        }

        internal AudioData LoadAudioClipIfNull()
        {
            if (!this.AudioSource) return this;

            this.AudioSource.clip ??= this.AssetsManager.Load<AudioClip>(this.Addressable);

            return this;
        }
    }
}