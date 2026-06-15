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
        [SerializeField] private GameObject hitVfxPrefab;
        //[SerializeField] private VisualEffectAsset hitEffectAsset; //particles to play when hit
        private VisualEffect hitVfx;

        private BulletBehavior bulletBehavior;
        private GameObject go;
        
        private void OnEnable()
        {
            bulletBehavior = GetComponent<BulletBehavior>();
            bulletBehavior.OnHitTrigger += OnHit;
        }

        private void OnDisable()
        {
            bulletBehavior.OnHitTrigger -= OnHit;
        }

        private void OnHit(object sender, OnHitEventArgs e)
        {
            GameObject vfx = Instantiate(hitVfxPrefab, e.HitInfo.HitPoint, Quaternion.identity);
            Destroy(vfx, 1f);
        }
        
        private IEnumerator DestroyWhenFinished(GameObject go)
        {
            // Wait until the effect actually starts emitting
            yield return new WaitForSeconds(.5f); //added a small buffer to ensure that the effect is completely finished before destroying the game object
            Destroy(go);
        }
    }
}