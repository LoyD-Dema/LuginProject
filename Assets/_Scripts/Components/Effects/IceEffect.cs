using UnityEngine;

public class IceEffect : MonoBehaviour
{
    private float duration;
    private float elapsedTime;
    
    private float speedReduction;

    private AIController AIController;

    private GameObject vfx;
    private GameObject trail;

    private void Awake()
    {
        speedReduction = EffectsManager.I.CharacterSpeedMultiplayerToDecrese;
        duration = EffectsManager.I.IceEffectDurarion;
        vfx = EffectsManager.I.IceHitVfxPrefab;
        trail = EffectsManager.I.IceTrailPrefab;
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