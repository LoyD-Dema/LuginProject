using UnityEngine;

public class IceEffect : MonoBehaviour
{
    [SerializeField] private float duration;
    private float elapsedTime;
    
    private float speedReduction;

    private MovementComponent movement;

    private void Awake()
    {
        speedReduction = EffectsManager.I.AmountToDecreseIceSpeedMutliplayer;
    }

    private void OnEnable()
    {
        elapsedTime = duration;
        if(movement == null)
        {
            movement = GetComponent<MovementComponent>();
        }
        movement.SpeedMultiplayer -= speedReduction;
    }

    private void OnDisable()
    {
        movement.SpeedMultiplayer += speedReduction; 
    }

    private void Update()
    {
        elapsedTime -= Time.deltaTime;

        if(elapsedTime < 0)
        {
            enabled = false;
        }
    }
};