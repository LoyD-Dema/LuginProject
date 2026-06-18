using UnityEngine.InputSystem;
using UnityEngine;

namespace Controllers
{
    public class AnimController : MonoBehaviour
    {
        private Animator animator;
        private Rigidbody rb;

        private bool bIsMoving = false;
        private bool bIsShooting = false;
        
        private void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
        }

        public void OnMove(InputValue value)
        {
            Vector2 moveInput = value.Get<Vector2>();
            bIsMoving = moveInput.sqrMagnitude > 0.01f;
            animator.SetBool("IsMoving", bIsMoving);
        }

        public void OnShoot(InputValue value)
        {
            Debug.Log(value.Get<float>());
            float v = value.Get<float>();
            bIsShooting = v > 0.5f;
            animator.SetBool("IsShooting", bIsShooting);
        }

        
        public void FixedUpdate()
        {
            if (bIsMoving)
            {
                Vector3 velocity = rb.linearVelocity;
                Vector3 localVelocity = transform.InverseTransformDirection(velocity.normalized);

                Debug.Log($"Horizontal {localVelocity.x} Vertical {localVelocity.z}");
            
                animator.SetFloat("Horizontal", localVelocity.x);
                animator.SetFloat("Vertical", localVelocity.z);
            }
        }
    }
}