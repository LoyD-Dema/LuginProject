using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// A simple class to debug events and interactions related to the player.
/// Remove this class once the player controller and components are fully implemented and tested.
/// </summary>
///
/// //require the components that need to be tested
[RequireComponent(typeof(HealthComponent))] 
public class PlayerDebug : MonoBehaviour
{
    public InputActionAsset inputActionAsset;
    private InputActionMap debugMap;
    
    private InputAction takeDmgAction;
    private InputAction gainHealthAction;
    
    private HealthComponent hc;

    private void Awake()
    {
        debugMap = inputActionAsset.FindActionMap("Debug");

        try
        {
            takeDmgAction = debugMap.FindAction("TakeDmg");
            gainHealthAction = debugMap.FindAction("GainHealth");
        }catch (Exception e)
        {
            Debug.LogError($"Error finding debug actions: {e.Message}");
        }
    }

    private void OnEnable()
    {
        takeDmgAction.performed += OnTakeDamage;
        gainHealthAction.performed += OnGainHealth;
    }

    private void Start()
    {
        debugMap.Enable();
        hc = GetComponent<HealthComponent>();
        Debug.Log("PlayerDebug initialized. Listening for input events...");
    }
    
    private void OnGainHealth(InputAction.CallbackContext obj)
    {
        hc.GainHealth(10);
    }

    private void OnTakeDamage(InputAction.CallbackContext obj)
    {
        hc.TakeDamage(10);
    }

    private void OnDisable()
    {
        takeDmgAction.performed -= OnTakeDamage;
        gainHealthAction.performed -= OnGainHealth;
    }
    
    public void OnDestroy()
    {
        debugMap.Disable();
    }
}
