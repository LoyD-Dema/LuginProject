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
    public static EffectsManager I {  get; private set; }

    [Header("Params")]
    [Header("Params/Ice")]
    [Range(0.0f, 1f)]
    [SerializeField] private float characterSpeedMultiplayerToDecrese = 0.2f;
    [Range(0.0f, 1f)]
    [SerializeField] private float bulletSpeedMultiplayerToDecrese = 0.2f;
    [Range(0.0f, 1f)]
    [SerializeField] private float bulletDmnMultiplayerToDecrese = 0.2f;
    [SerializeField] private float iceEffectDurarion = 1.0f;


    // Property
    public float CharacterSpeedMultiplayerToDecrese => characterSpeedMultiplayerToDecrese;
    public float BulletSpeedMultiplayerToDecrese => bulletSpeedMultiplayerToDecrese;
    public float BulletDmnMultiplayerToDecrese => bulletDmnMultiplayerToDecrese;
    public float IceEffectDurarion => iceEffectDurarion;

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
