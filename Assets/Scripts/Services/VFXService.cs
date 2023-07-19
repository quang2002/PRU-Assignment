namespace Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using GDK.AssetsManager;
    using UnityEngine;
    using Zenject;

    public class VFXService : MonoBehaviour
    {
        private Dictionary<string, HashSet<ParticleSystem>> FreeParticleSystems { get; } = new();

        #region Inject

        private IAssetsManager AssetsManager { get; set; }
        private ILogger        Logger        { get; set; }

        [Inject]
        public void Inject(IAssetsManager assetsManager, ILogger logger)
        {
            this.AssetsManager = assetsManager;
            this.Logger        = logger;
        }

        #endregion

        public async Task SpawnVFX(string id, Vector3 position, Quaternion? rotation = null, Vector3? scale = null)
        {
            rotation ??= Quaternion.identity;
            scale    ??= Vector3.one;

            if (!this.FreeParticleSystems.TryGetValue(id, out var particleSystems))
            {
                particleSystems = new();
                this.FreeParticleSystems.Add(id, particleSystems);
            }

            var particle = particleSystems.FirstOrDefault();

            if (particle is null)
            {
                var prefab = this.AssetsManager.Load<GameObject>(id);

                if (!prefab)
                {
                    this.Logger.LogError("VFXSystem", $"Prefab with id {id} not found");
                    return;
                }

                particle = Instantiate(prefab, this.transform).GetComponent<ParticleSystem>();
                particle.gameObject.SetActive(false);

                if (!particle)
                {
                    this.Logger.LogError("VFXSystem", $"Particle system with id {id} not found");
                    return;
                }

                particleSystems.Add(particle);
            }

            lock (particleSystems)
            {
                particleSystems.Remove(particle);
            }

            particle.transform.position   = position;
            particle.transform.rotation   = rotation.Value;
            particle.transform.localScale = scale.Value;

            particle.gameObject.SetActive(true);
            particle.Stop(true);
            particle.Play(true);

            while (particle.isPlaying)
            {
                await Task.Yield();
            }

            particle.gameObject.SetActive(false);

            lock (particleSystems)
            {
                particleSystems.Add(particle);
            }
        }
    }
}