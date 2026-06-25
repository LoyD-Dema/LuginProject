using System.Collections;
using UnityEngine;

namespace Controllers
{
    using System.Collections;
    using UnityEngine;

    public class MainMenuIdleAnimController : MonoBehaviour
    {
        [SerializeField] private float minWaitTime = 3f; // Increased default to feel more natural
        [SerializeField] private float maxWaitTime = 7f;
        [SerializeField] private bool canLookAround = true;
    
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
            StartCoroutine(IdleRoutine());
        }
    
        private IEnumerator IdleRoutine()
        {
            while (canLookAround)
            {
                float currentWaitTime = Random.Range(minWaitTime, maxWaitTime);
                yield return new WaitForSeconds(currentWaitTime);
                if (!canLookAround) break; 
                animator.SetBool("IsLookingAround", true);

                yield return null; //wait for animation transition

                while (animator.GetCurrentAnimatorStateInfo(0).IsName("Looking Around"))
                {
                    yield return null; 
                }
                animator.SetBool("IsLookingAround", false);
            }
        }
    }
}