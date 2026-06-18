using UnityEngine;

public class PlayFootstep : MonoBehaviour
{
    public void PlaySound(SoundType type)
    {
        AudioManager.PlaySound3D(type, transform.position);
    }
}
