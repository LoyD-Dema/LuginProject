using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Utilities;

namespace Components.VFX
{
    [RequireComponent(typeof(HealthComponent))]
    public class HitEffects : MonoBehaviour
    {
        private Actor owningActor;
        
        [SerializeField] private VisualEffectAsset hitParticlesAsset;
        private VisualEffect vfx;
        private GameObject go;
        [SerializeField] private float lifetimeBuffer = 1f;

        private void OnEnable()
        {
            owningActor = GetComponent<Actor>();
            owningActor.HitReceived += OnHit;
        }

        private void OnDisable()
        {
            owningActor.HitReceived -= OnHit;
        }

        private void OnHit(HitInfo hitInfo)
        {
            go = new GameObject("HitEffectsVFX");
            go.transform.position = hitInfo.HitPoint;
            
            Debug.Log("VFX position: " + go.transform.position);
            
            vfx = go.AddComponent<VisualEffect>();
            vfx.visualEffectAsset = hitParticlesAsset;
            
            vfx.Play();
            StartCoroutine(DestroyWhenFinished(go, vfx));
            //flash Material
            //play sound
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