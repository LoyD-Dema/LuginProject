using UnityEngine;

public class AnimatorEventSound : MonoBehaviour
{
    private PlayFootstep footstep;

    void Awake()
    {
        footstep = GetComponent<PlayFootstep>();
    }

    public void ExecuteSound(SoundType type)
    {
        if (footstep != null)
        {
            footstep.PlayFootstepSound(type);
        }
    }
}
