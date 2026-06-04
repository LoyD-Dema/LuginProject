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
    
    private InputAction takeDmgAction; //Keyboard[1]
    private InputAction gainHealthAction; //Keyboard[2]
    
    private HealthComponent hc;

    private void Awake()
    {
        debugMap = inputActionAsset.FindActionMap("Debug");
    }

    private void Start()
    {
        debugMap.Enable();
        hc = GetComponent<HealthComponent>();
        Debug.Log("PlayerDebug initialized. Listening for input events...");
    }
    
    public void OnGainHealth(InputValue value)
    {
        hc.GainHealth(10);
    }

    public void OnTakeDamage(InputValue value)
    {
        hc.TakeDamage(10);
    }

    
    public void OnDestroy()
    {
        debugMap.Disable();
    }
}
