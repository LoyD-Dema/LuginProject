using UnityEngine;

// Is possible move the effecttype here in this class

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager I {  get; private set; }

    [Header("Params")]
    [Range(0.0f, 1f)]
    [Header("Params/Ice")]
    [SerializeField] private float amountToDecreseIceSpeedMutliplayer = 0.2f;
    [SerializeField] private float iceEffectDurarion = 1.0f;


    // Property
    public float AmountToDecreseIceSpeedMutliplayer => amountToDecreseIceSpeedMutliplayer;
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
