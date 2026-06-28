using System;
using UnityEngine;

public class IceEffect : MonoBehaviour
{

    public event Action<bool> OnIceStateChange;
    private float duration;
    private float elapsedTime;
    
    private float speedReduction;

    private AIController AIController;

    private GameObject vfx;

    private void Awake()
    {
        speedReduction = EffectsManager.I.CharacterSpeedMultiplayerToDecrese;
        duration = EffectsManager.I.IceEffectDurarion;
        vfx = EffectsManager.I.IceHitVfxPrefab;
    }

    private void OnEnable()
    {
        if(AIController == null)
        {
            AIController = GetComponent<AIController>();
        }

        Reset();

        if (AIController != null)
        {
            AIController.SpeedMultiplayer -= speedReduction;
        }
        OnIceStateChange?.Invoke(true);
    }

    private void OnDisable()
    {
        if (AIController != null)
        {
            AIController.SpeedMultiplayer += speedReduction;
        }
        OnIceStateChange?.Invoke(false);
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