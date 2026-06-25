using UnityEngine;
using UnityEngine.Events;

public class AnimationController : MonoBehaviour
{
    private Animation animation;
    private Animator animator;

    [SerializeField] public UnityEvent OnAnimationComplete;

    private void Awake()
    {
        animation = GetComponent<Animation>();
        animator = GetComponent<Animator>();
    }

    public void PlayUsingAnimation(string name)
    {
        animation.Rewind(name);
        animation.Play(name);
    }

    public void PlayUsingAnimator(bool isOpen)
    {
        animator.SetBool("isOpen", isOpen);
    }

    public void OnAnimationIsOver()
    {
        OnAnimationComplete?.Invoke();  
    }
}
