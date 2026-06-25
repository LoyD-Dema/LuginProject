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

        //TODO: THIS should be tied with the movement component.
        private Vector2 moveInput;
        private float moveSpeed = 10f;
        private Vector3 currentVelocity;
        private float acceleration = 1f;

        private void OnEnable()
        {
            HealthComponent.Death += OnDeath;
        }

        private void OnDisable()
        {
            HealthComponent.Death -= OnDeath;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            shootComponent = GetComponent<ShootComponent>();
        }

        public void OnMove(InputValue value)
        {
            moveInput = value.Get<Vector2>();
            bIsMoving = moveInput.sqrMagnitude > 0.01f;
            animator.SetBool("IsMoving", bIsMoving);
        }

        public void OnShoot(InputValue value)
        {
            Debug.Log($"Can fire: {shootComponent.bCanFire}, fire input: {value.Get<float>() > 0.5f}");

            animator.SetFloat("ShootingSpeedMult",1f/shootComponent.FireRate);
            
            if(shootComponent.bCanFire)
                animator.SetTrigger("Shoot");
        }
        
        public void FixedUpdate()
        {
            //Find the desired direction from the input.
            Vector3 inputDir = new Vector3(moveInput.x, 0f, moveInput.y);
            inputDir = Vector3.ClampMagnitude(inputDir, 1f);

            //Calculate target velocity
            Vector3 targetVelocity = inputDir * moveSpeed;

            //Smooth the physics movement
            currentVelocity = Vector3.Lerp(
                rb.linearVelocity,
                targetVelocity,
                acceleration * Time.fixedDeltaTime
            );

            rb.linearVelocity = currentVelocity;
        }

        private void OnDeath(GameObject obj)
        {
            if (obj == gameObject)
            {
            animator.SetTrigger("IsDead");
            this.enabled = false;
            }
        }
        
        void Update()
        {
            //Use desired velocity for animation
            Vector3 velocity = currentVelocity;
            float speed = velocity.magnitude;

            //Use a dead zone to kill sliding on horizontal and vertical components
            if (speed < 0.05f)
                speed = 0f;

            animator.SetFloat("Speed", speed);

            if (speed > 0.05f)
            {
                Vector3 localVelocity =
                    transform.InverseTransformDirection(velocity);

                animator.SetFloat("Horizontal", localVelocity.x);
                animator.SetFloat("Vertical", localVelocity.z);
            }
            else
            {
                animator.SetFloat("Horizontal", 0f);
                animator.SetFloat("Vertical", 0f);
            }
        }
    }
}