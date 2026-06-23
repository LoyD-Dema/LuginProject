using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public enum AudioType
{
    Master,
    SFX,
    Music,
}

public class AudioSettings : MonoBehaviour
{
    [Header("AudioMixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Sliders")]
    [SerializeField] Slider masterSlide;
    [SerializeField] Slider SFXSlider;
    [SerializeField] Slider musicSlider;

    [Header("AudioMixerExposedParameters")]
    [SerializeField] string masterVolumeName;
    [SerializeField] string SFXVolumeName;
    [SerializeField] string musicVolumeName;

    private float maxVolume = 20.0f;
    private float minVolume = -80.0f;

    void Start()
    {

        float masterAudio01 = PlayerPrefs.GetFloat("masterAudio", 0.5f);
        float SFXAudio01 = PlayerPrefs.GetFloat("SFXAudio", 0.75f);
        float musicAudio01 = PlayerPrefs.GetFloat("musicAudio", 0.75f);

        masterSlide.value = masterAudio01;
        SFXSlider.value = SFXAudio01;
        musicSlider.value = musicAudio01;   

        SetVolume(AudioType.Master, masterAudio01);
        SetVolume(AudioType.SFX, SFXAudio01);
        SetVolume(AudioType.Music, musicAudio01);
    }

    private float GetValueFrom01(float value01)
    {
            float v = Mathf.Lerp(minVolume, maxVolume, value01);
        Debug.Log(v);
        return v;
    }

    // Chiamato dallo slider
    public void SetVolumeMaster(float valume01)
    {
        SetVolume(AudioType.Master, valume01);
    }

    public void SetVolumeSFX(float valume01)
    {
        SetVolume(AudioType.SFX, valume01);
    }

    public void SetVolumeMusic(float valume01)
    {
        SetVolume(AudioType.Music, valume01);
    }

    public void SetVolume(AudioType type, float valume01)
    {
        switch (type)
        {
            case AudioType.Master:
                masterSlide.value = valume01;
                audioMixer.SetFloat(masterVolumeName, GetValueFrom01(valume01));
                PlayerPrefs.SetFloat("masterAudio", valume01);
                break;
            case AudioType.SFX:
                SFXSlider.value = valume01;
                audioMixer.SetFloat(SFXVolumeName, GetValueFrom01(valume01));
                PlayerPrefs.SetFloat("SFXAudio", valume01);
                break;
            case AudioType.Music:
                musicSlider.value = valume01;
                audioMixer.SetFloat(musicVolumeName, GetValueFrom01(valume01));
                PlayerPrefs.SetFloat("musicAudio", valume01);
                break;
        }
    }

    //public float GetVolume(AudioType type)
    //{
    //    float value = 0.0f;

    //    switch (type)
    //    {
    //        case AudioType.Master:
    //            masterMixer.GetFloat(masterVolumeName, out value);
    //            break;
    //        case AudioType.SFX:
    //            masterMixer.GetFloat(SFXVolumeName, out value);
    //            break;
    //        case AudioType.Music:
    //            masterMixer.GetFloat(musicVolumeName, out value);
    //            break;
    //    }

    //    return value;
    //}
}
