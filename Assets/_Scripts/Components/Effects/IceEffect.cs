using UnityEngine;

public class IceEffect : MonoBehaviour
{
    private float duration;
    private float elapsedTime;
    
    private float speedReduction;

    private MovementComponent movement;

    private void Awake()
    {
        speedReduction = EffectsManager.I.CharacterSpeedMultiplayerToDecrese;
        duration = EffectsManager.I.IceEffectDurarion;
    }

    private void OnEnable()
    {
        Reset();
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

    public void Reset()
    {
        elapsedTime = duration;
    }
};