using UnityEngine;

public class IceEffect : MonoBehaviour
{
    [SerializeField] private float duration;
    private float elapsedTime;

    [SerializeField] private float speedReduction;

    private MovementComponent movement;

    private void OnEnable()
    {
        elapsedTime = duration;
        if(movement == null)
        {
            movement = GetComponent<MovementComponent>();
        }
        //movement.SpeedMultiplayer -= speedReduction; // Get a speed multpliayer and decresed by the speedReduction
    }

    private void OnDisable()
    {
        //movement.SpeedMultiplayer += speedReduction; // Get a speed multpliayer and decresed by the speedReduction
    }

    private void Update()
    {
        elapsedTime -= Time.deltaTime;

        if(elapsedTime < 0)
        {
            enabled = false;
        }
    }
}
