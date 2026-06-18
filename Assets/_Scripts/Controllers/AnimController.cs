using UnityEngine.InputSystem;
using UnityEngine;

namespace Controllers
{
    public class AnimController : MonoBehaviour
    {
        private Animator animator;
        private Rigidbody rb;
        private ShootComponent shootComponent;

        private bool bIsMoving = false;
        private bool bIsShooting = false;

        
        private void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            shootComponent = GetComponent<ShootComponent>();
        }

        public void OnMove(InputValue value)
        {
            Vector2 moveInput = value.Get<Vector2>();
            bIsMoving = moveInput.sqrMagnitude > 0.01f;
            animator.SetBool("IsMoving", bIsMoving);
        }

        public void OnShoot(InputValue value)
        {
            Debug.Log($"Can fire: {shootComponent.bCanFire}, fire input: {value.Get<float>() > 0.5f}");

            if(value.Get<float>() > 0.5f && shootComponent.bCanFire)
                animator.SetBool("CanShoot", true);
            else
                animator.SetBool("CanShoot", false);                
        }
        
        public void FixedUpdate()
        {
            Vector3 velocity = rb.linearVelocity;
            
            float speed = velocity.magnitude;
            animator.SetFloat("Speed", speed);
            
            if (bIsMoving)
            {
                Vector3 localVelocity = transform.InverseTransformDirection(velocity.normalized);

                Debug.Log($"Horizontal {localVelocity.x} Vertical {localVelocity.z}");
            
                animator.SetFloat("Horizontal", localVelocity.x);
                animator.SetFloat("Vertical", localVelocity.z);
            }
        }
    }
}