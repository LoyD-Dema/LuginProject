using UnityEngine;

// Is possible move the effecttype here in this class

public enum BulletType
{
    Normal,
    Fire,
    Ice,
    Shot,
    LAST
}

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager I { get; private set; }

    [Header("VFX/Normal")]
    [SerializeField] private GameObject normalHitVfxPrefab;
    
    [Header("Params/Ice")]
    // Bullet
    [Range(0.0f, 1f)]
    [SerializeField] private float bulletSpeedMultiplayerToDecrese = 0.2f;
    [Range(0.0f, 1f)]
    [SerializeField] private float bulletDmnMultiplayerToDecrese = 0.2f;
    // Effect
    [Range(0.0f, 1f)]
    [SerializeField] private float characterSpeedMultiplayerToDecrese = 0.2f;
    [SerializeField] private float iceEffectDurarion = 1.0f;
    
    [Header("VFX/Ice")]
    [SerializeField] private GameObject iceHitVfxPrefab;
    
    [Header("Params/Fire")]
    // Effect
    [SerializeField] private float fireEffectDuration = 5.0f;
    [SerializeField] private float fireDamageInterval = 1.0f;
    [SerializeField] private float fireDamageAmount = 10f;

    [Header("VFX/Fire")]
    [SerializeField] private GameObject fireHitVFXPrefab;

    // Property
    // Ice
    public float CharacterSpeedMultiplayerToDecrese => characterSpeedMultiplayerToDecrese;
    public float BulletSpeedMultiplayerToDecrese => bulletSpeedMultiplayerToDecrese;
    public float BulletDmnMultiplayerToDecrese => bulletDmnMultiplayerToDecrese;
    public float IceEffectDurarion => iceEffectDurarion;
    // Fire
    public float FireEffectDuration => fireEffectDuration;
    public float FireDamageInterval => fireDamageInterval;
    public float FireDamageAmount => fireDamageAmount;
    
    public GameObject IceHitVfxPrefab => iceHitVfxPrefab;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
            return;
        }

        Debug.Log($"Destroying this {gameObject.name}", gameObject);
        Debug.Break();
        Destroy(gameObject);
    }
}
