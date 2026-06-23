using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animation))]
public class AnimationController : MonoBehaviour
{
    private Animation animation;

    [SerializeField] public UnityEvent OnAnimationComplete;
    [SerializeField] string checkAnimWhenFinish;

    private void Awake()
    {
        animation = GetComponent<Animation>();
    }

    private void Update()
    {
        
    }

    public void Play(string name)
    {
        animation.Rewind(name);
        animation.Play(name);
    }

    public void OnAnimationIsOver()
    {
        OnAnimationComplete?.Invoke();  
    }
}
