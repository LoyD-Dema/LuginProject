using UnityEngine;

[RequireComponent(typeof(Animation))]
public class AnimationController : MonoBehaviour
{
    private Animation animation;

    private void Awake()
    {
        animation = GetComponent<Animation>();
    }

    public void Play(string name)
    {
        animation.Rewind(name);
        animation.Play(name);
    }
}
