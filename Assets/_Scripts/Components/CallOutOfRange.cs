using System;
using UnityEngine;

public class CallOutOfRange : MonoBehaviour
{
    [SerializeField] private float range;
    public event Action OnOutOfRange; 

    private void Update()
    {
        if (transform.position.magnitude > range)
        {
            OnOutOfRange?.Invoke();
            Debug.Log(transform.position);
        }
    }
}
