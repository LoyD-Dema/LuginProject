using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    CowboyFootstep,
    CowboyShoot,
    CowboyHurt,
    CowboyDead,
    OutlawFootstep,
    OutlawShoot,
    OutlawHurt,
    OutlawDead,
    HoverButton,
    SelectionButton,
    MenuOpen,
    MenuClose,
    BaseBulletImpact,
    IceBulletImpact
}
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [SerializeField] private SoundElement[] soundElements;
    [SerializeField] private GameObject spawnAudioPrefab;

    private Dictionary<SoundType, (AudioClip[] clips, AudioMixerGroup group)> soundDictionary;

    private AudioSource audioSource2D;
    private AudioSource musicAudioSource;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            InitializeDictionary();
            Setup2DAudioSource();
            SetupMusicAudioSource();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeDictionary()
    {
        soundDictionary = new Dictionary<SoundType, (AudioClip[] clips, AudioMixerGroup group)>();
        foreach (SoundElement element in soundElements)
        {
            if (!soundDictionary.ContainsKey(element.type))
            {
                soundDictionary.Add(element.type, (element.clips, element.mixerGroup));
            }
        }
    }

    private void Setup2DAudioSource()
    {
        audioSource2D = gameObject.AddComponent<AudioSource>();
        audioSource2D.spatialBlend = 0f;
        audioSource2D.playOnAwake = false;
    }

    private void SetupMusicAudioSource()
    {
        musicAudioSource = gameObject.AddComponent<AudioSource>();
        musicAudioSource.spatialBlend = 0f;
        musicAudioSource.playOnAwake = false;
        musicAudioSource.loop = true;
    }

    public static void PlaySound3D(SoundType type, Vector3 position, float volume = 1)
    {
        if (!instance.soundDictionary.TryGetValue(type, out var soundData)) return;
        AudioClip randomClip = soundData.clips[Random.Range(0, soundData.clips.Length)];

        GameObject audioObj = Instantiate(instance.spawnAudioPrefab, position, Quaternion.identity);
        AudioSource source = audioObj.GetComponent<AudioSource>();

        source.clip = randomClip;
        source.volume = volume;

        if (soundData.group != null)
        {
            source.outputAudioMixerGroup = soundData.group;
        }

        source.Play();
        Destroy(audioObj, randomClip.length);
    }

    public static void PlaySound2D(SoundType type, float volume = 1)
    {
        if (!instance.soundDictionary.TryGetValue(type, out var soundData)) return;

        AudioClip randomClip = soundData.clips[Random.Range(0, soundData.clips.Length)];

        if (soundData.group != null)
        {
            instance.audioSource2D.outputAudioMixerGroup = soundData.group;
        }

        instance.audioSource2D.PlayOneShot(randomClip, volume);
    }

    public static void PlayMusic(SoundType type, bool loop = true, float volume = 1)
    {
        if (!instance.soundDictionary.TryGetValue(type, out var soundData)) return;

        AudioClip randomClip = soundData.clips[Random.Range(0, soundData.clips.Length)];

        if (soundData.group != null)
        {
            instance.musicAudioSource.outputAudioMixerGroup = soundData.group;
        }

        instance.musicAudioSource.clip = randomClip;
        instance.musicAudioSource.volume = volume;
        instance.musicAudioSource.loop = loop;
        instance.musicAudioSource.Play();
    }

    public static void StopMusic()
    {
        if (instance.musicAudioSource.isPlaying)
        {
            instance.musicAudioSource.Stop();
        }
    }

}

[System.Serializable]
public struct SoundElement
{
    public SoundType type;
    public AudioClip[] clips;
    public AudioMixerGroup mixerGroup;
}
