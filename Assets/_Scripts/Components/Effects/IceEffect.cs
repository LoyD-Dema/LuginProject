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
        Reset();
        if(AIController == null)
        {
            AIController = GetComponent<AIController>();
        }
        AIController.SpeedMultiplayer -= speedReduction;
        OnIceStateChange?.Invoke(true);
    }

    private void OnDisable()
    {
        AIController.SpeedMultiplayer += speedReduction;
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