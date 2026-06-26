using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

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
        private Renderer targetRenderer;
        private Material originalMaterial;
        private Texture baseTexture;
        private Coroutine hitRoutine;
        
        private HealthComponent healthComponent;

        private FireEffect fireEffect;
        private IceEffect iceEffect;

        [Header("Material Settings")]
        [SerializeField] private VisualEffectAsset hitParticlesAsset; // particles to play when hit
        [SerializeField] private Material hitMaterial; // a shader to apply to the Actor when hit
        private Material runtimeHitMaterial; //local copy
        [SerializeField] private Material healMaterial; // a shader to apply to the Actor when hit
        private Material runtimeHealMaterial; //local copy

        [SerializeField] private Material fireMaterial;
        private Material runtimeFireMaterial;
        [SerializeField] private Material iceMaterial;
        private Material runtimeIceMaterial;

        private bool isBurning;
        private bool isFrozen;

        #region Utilities
        private GameObject go;
        private VisualEffect HitParticlesVfx;
        [SerializeField] private float hitEffectDuration = 0.2f;
        private float lifetimeBuffer = .4f; 
        #endregion

        private void Awake()
        {
            healthComponent = GetComponent<HealthComponent>();
            fireEffect = GetComponent<FireEffect>();
            iceEffect = GetComponent<IceEffect>();
        }
        
        private void OnEnable()
        {
            healthComponent.Damage += OnDamage;
            healthComponent.Heal += OnHeal;

            if (fireEffect != null) fireEffect.OnFireStateChange += OnFireStateChange;
            if (iceEffect != null) iceEffect.OnIceStateChange += OnIceStateChange;
            ResetToOriginalMaterial();
        }

        private void OnDisable()
        {
            healthComponent.Damage -= OnDamage;
            healthComponent.Heal -= OnHeal;

            if (fireEffect != null) fireEffect.OnFireStateChange -= OnFireStateChange;
            if (iceEffect != null) iceEffect.OnIceStateChange -= OnIceStateChange;

            ResetToOriginalMaterial();
        }
        
        private void Start()
        {
            InitializeReferences();
        }
        
        private void OnHeal()
        {
            if (runtimeHealMaterial == null) return; 
            PlayTemporaryShaderEffect(runtimeHealMaterial);
        }

        private void OnDamage()
        {
            if (runtimeHitMaterial == null) return; 
            PlayTemporaryShaderEffect(runtimeHitMaterial);
        }

        private void OnFireStateChange(bool isActive)
        {
            isBurning = isActive;
            UpdatePersistentMaterial();
        }

        private void OnIceStateChange(bool isActive)
        {
            isFrozen = isActive;
            UpdatePersistentMaterial();
        }

        private void UpdatePersistentMaterial()
        {
            if (hitRoutine != null) return;

            if (isFrozen && runtimeIceMaterial != null)
            {
                targetRenderer.sharedMaterial = runtimeIceMaterial;
            }
            else if (isBurning && runtimeFireMaterial != null)
            {
                targetRenderer.sharedMaterial = runtimeFireMaterial;
            }
            else
            {
                targetRenderer.sharedMaterial = originalMaterial;
            }
        }

        public void PlayTemporaryShaderEffect(Material material = null)
        {
            if (hitRoutine != null)
                StopCoroutine(hitRoutine);

            hitRoutine = StartCoroutine(RunShaderEffect(material));
        }

        private IEnumerator RunShaderEffect(Material materialToRun)
        {
            targetRenderer.sharedMaterial = materialToRun;
            yield return new WaitForSeconds(hitEffectDuration);
            targetRenderer.sharedMaterial = originalMaterial;
            
            hitRoutine = null;
        }

        private void OnDestroy()
        {
            if (runtimeHitMaterial != null)
            {
                Destroy(runtimeHitMaterial);
            }
        }
        
        private void InitializeReferences()
        {
            // Prevent double initialization if called by both Start and Reset
            if (targetRenderer != null) return;

            targetRenderer = GetComponentInChildren<Renderer>();
            
            if (targetRenderer != null)
            {
                originalMaterial = targetRenderer.sharedMaterial;
                baseTexture = originalMaterial.mainTexture;
                
                if (hitMaterial != null)
                {
                    runtimeHitMaterial = new Material(hitMaterial);
                    if (baseTexture != null) runtimeHitMaterial.SetTexture("_Texture", baseTexture);
                }

                if (healMaterial != null)
                {
                    runtimeHealMaterial = new Material(healMaterial);
                    if (baseTexture != null) runtimeHealMaterial.SetTexture("_Texture", baseTexture);
                }
            }
        }
        
        /// <summary>
        /// Safely strips the hit/heal shader and reverts the enemy back to normal.
        /// </summary>
        private void ResetToOriginalMaterial()
        {
            if (hitRoutine != null)
            {
                StopCoroutine(hitRoutine);
                hitRoutine = null;
            }

            if (targetRenderer == null)
            {
                InitializeReferences();
            }

            if (targetRenderer != null && originalMaterial != null)
            {
                targetRenderer.sharedMaterial = originalMaterial;
            }
        }
    }
}