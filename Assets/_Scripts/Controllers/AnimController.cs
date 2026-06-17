using UnityEngine.InputSystem;
using UnityEngine;

namespace Controllers
{
    public class AnimController : MonoBehaviour
    {
        private Animator animator;

        private bool bIsMoving = false;
        
        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void OnMove(InputValue value)
        {
            Vector2 moveInput = value.Get<Vector2>();
            animator.SetBool("IsMoving", moveInput.sqrMagnitude > 0.01f);
        }
    }
}