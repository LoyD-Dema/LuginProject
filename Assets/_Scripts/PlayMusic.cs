using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    [SerializeField] SoundType musicAudioType;

    private void Start()
    {
        AudioManager.PlayMusic(musicAudioType);
    }
}
