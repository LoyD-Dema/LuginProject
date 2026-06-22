using UnityEngine;

public class IceEffect : MonoBehaviour
{
    private float duration;
    private float elapsedTime;
    
    private float speedReduction;

    private AIController AIController;

    private void Awake()
    {
        speedReduction = EffectsManager.I.CharacterSpeedMultiplayerToDecrese;
        duration = EffectsManager.I.IceEffectDurarion;
    }

    private void OnEnable()
    {
        Reset();
        if(AIController == null)
        {
            AIController = GetComponent<AIController>();
        }
        AIController.SpeedMultiplayer -= speedReduction;
    }

    private void OnDisable()
    {
        AIController.SpeedMultiplayer += speedReduction; 
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