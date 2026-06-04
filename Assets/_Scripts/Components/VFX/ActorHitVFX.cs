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
    [RequireComponent(typeof(Actor))]
    public class ActorHitVFX : MonoBehaviour
    {
        private Actor owningActor;
        private Renderer renderer;
        private Material originalMaterial;
        private Coroutine hitRoutine;
        
        [SerializeField] private VisualEffectAsset hitParticlesAsset; //particles to play when hit
        [SerializeField] private Material hitMaterial; //a shader to apply to the Actor when hit
        
        #region Utilities
        private GameObject go;
        private VisualEffect HitParticlesVfx;
        private float hitEffcetDuration = 1f; //duration of the hit effect, we can adjust this based on the actual duration of the particle system or shader effect
        private float lifetimeBuffer = .4f; //a small buffer to ensure that the effect is completely finished before destroying the game object
        #endregion 
        
        private void OnEnable()
        {
            owningActor = GetComponent<Actor>();
            owningActor.HitReceived += OnHit;
        }

        private void OnDisable()
        {
            owningActor.HitReceived -= OnHit;
        }
        
        private void Start()
        {
            renderer = GetComponent<Renderer>();
            originalMaterial = renderer.material;
        }

        private void OnHit(HitInfo hitInfo)
        {
            go = new GameObject("HitEffectsVFX");
            go.transform.position = hitInfo.HitPoint;
            
            Debug.Log("VFX position: " + go.transform.position);
            
            HitParticlesVfx = go.AddComponent<VisualEffect>();
            HitParticlesVfx.visualEffectAsset = hitParticlesAsset;
            
            HitParticlesVfx.Play();
            StartCoroutine(DestroyWhenFinished(go, HitParticlesVfx));

            //flash Material
            PlayHitEffect();
            
            //play sound
        }

        public void PlayHitEffect()
        {
            if(hitRoutine != null)
                StopCoroutine(hitRoutine);

            hitRoutine = StartCoroutine(RunShaderEffect());
        }

        IEnumerator RunShaderEffect()
        {
            renderer.material = hitMaterial;
            yield return new WaitForSeconds(hitEffcetDuration);
            renderer.material = originalMaterial;
            hitRoutine = null;
        }

        private IEnumerator DestroyWhenFinished(GameObject go, VisualEffect vfx)
        {
            // Wait until the effect actually starts emitting
            yield return null;

            while (vfx.aliveParticleCount == 0)
                yield return null;
            
            while (vfx.aliveParticleCount > 0)
                yield return null;

            yield return new WaitForSeconds(lifetimeBuffer); //added a small buffer to ensure that the effect is completely finished before destroying the game object
            
            Destroy(go);
        }
        
    }
}