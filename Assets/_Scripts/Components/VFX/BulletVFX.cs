using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Utilities;

namespace Components.VFX
{
    /// <summary>
    /// This component is responsible for handling the visual effects related to bullets,
    /// such as the trail effect while the bullet is traveling and the impact effect when the bullet hits a target.
    /// It can be attached to a bullet prefab and will manage the instantiation and cleanup of the visual effects
    /// associated with the bullet's lifecycle.
    /// </summary>
    [RequireComponent(typeof(BulletBehavior))]
    public class BulletVFX : MonoBehaviour
    {
        [SerializeField] private VisualEffectAsset hitEffectAsset; //particles to play when hit
        private VisualEffect hitVfx;

        private BulletBehavior bulletBehavior;
        private GameObject go;
        
        private void OnEnable()
        {
            bulletBehavior = GetComponent<BulletBehavior>();
            bulletBehavior.OnHit += OnHit;
        }

        private void OnDisable()
        {
            bulletBehavior.OnHit -= OnHit;
        }

        private void OnHit(object sender, HitInfo hitInfo)
        {
            go = new GameObject("HitEffectsVFX");
            go.transform.position = hitInfo.HitPoint;
            
            Debug.Log("VFX position: " + go.transform.position);
            
            hitVfx = go.AddComponent<VisualEffect>();
            hitVfx.visualEffectAsset = hitEffectAsset;
            
            hitVfx.Play();
            StartCoroutine(DestroyWhenFinished(go, hitVfx));
        }
        
        private IEnumerator DestroyWhenFinished(GameObject go, VisualEffect vfx)
        {
            // Wait until the effect actually starts emitting
            yield return null;

            while (vfx.aliveParticleCount == 0)
                yield return null;
            
            while (vfx.aliveParticleCount > 0)
                yield return null;

            yield return new WaitForSeconds(1f); //added a small buffer to ensure that the effect is completely finished before destroying the game object
            
            Destroy(go);
        }
    }
}