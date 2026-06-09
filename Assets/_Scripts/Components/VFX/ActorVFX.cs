using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;
using Utilities;

namespace Components.VFX
{
    /// <summary>
    /// This component is responsible for handling the visual effects that occur when an Actor receives a hit.
    /// It listens for the HitReceived event from the Actor and triggers the appropriate visual effects,
    /// such as playing hit particles and applying a hit shader to the Actor's material.
    /// The component also ensures that the visual effects are properly cleaned up after they have finished playing.
    /// </summary>
    public class ActorVFX : MonoBehaviour
    {
        private Renderer renderer;
        private Material originalMaterial;
        private Coroutine hitRoutine;
        
        private HealthComponent healthComponent;
        
        [SerializeField] private VisualEffectAsset hitParticlesAsset; //particles to play when hit
        [SerializeField] private Material hitMaterial; //a shader to apply to the Actor when hit
        
        #region Utilities
        private GameObject go;
        private VisualEffect HitParticlesVfx;
        private float hitEffcetDuration = 1f; //duration of the hit effect, we can adjust this based on the actual duration of the particle system or shader effect
        private float lifetimeBuffer = .4f; //a small buffer to ensure that the effect is completely finished before destroying the game object
        #endregion

        private void Awake()
        {
            healthComponent = GetComponent<HealthComponent>();
        }
        
        private void OnEnable()
        {
            healthComponent.Damage += OnDamage;
            healthComponent.Heal += OnHeal;
        }

        private void OnDisable()
        {
            healthComponent.Damage -= OnDamage;
            healthComponent.Heal -= OnHeal;
        }
        
        private void Start()
        {
            renderer = GetComponentInChildren<Renderer>();
            originalMaterial = renderer.material;
        }
        
        private void OnHeal()
        {
            throw new NotImplementedException();
        }

        private void OnDamage()
        {
            PlayTemporaryShaderEffect(hitMaterial);
        }

        public void PlayTemporaryShaderEffect(Material material = null)
        {
            if(hitRoutine != null)
                StopCoroutine(hitRoutine);

            hitRoutine = StartCoroutine(RunShaderEffect(material));
        }

        IEnumerator RunShaderEffect(Material shaderMaterial = null)
        {
            renderer.material = shaderMaterial;
            yield return new WaitForSeconds(hitEffcetDuration);
            renderer.material = originalMaterial;
            hitRoutine = null;
        }
    }
}