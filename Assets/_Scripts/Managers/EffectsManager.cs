using UnityEngine;

// Is possible move the effecttype here in this class

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager I {  get; private set; }

    [Header("Params")]
    [Range(0.0f, 1f)]
    [SerializeField] private float amountToDecreseIceSpeedMutliplayer = 0.2f;


    // Property
    public float AmountToDecreseIceSpeedMutliplayer => amountToDecreseIceSpeedMutliplayer;

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
