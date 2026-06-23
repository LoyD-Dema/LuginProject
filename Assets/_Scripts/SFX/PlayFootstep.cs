using UnityEngine;

public class PlayFootstep : MonoBehaviour
{
    public void PlayFootstepSound(SoundType type)
    {
        AudioManager.PlaySound3D(type, transform.position);
    }
}
