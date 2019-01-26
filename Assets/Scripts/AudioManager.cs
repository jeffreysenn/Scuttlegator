using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource[] channels;
    public AudioSource musicChannel;
    public float volumeIncrement = 0.1f;

    public AudioClip[] m_SoundEffects;

    public static AudioManager instance;

    private void Start()
    {
        InitAudioSources();
        instance = this;
        musicChannel.Play();
    }

    public void PlaySound(string p_name, float volume = 1.0f, bool isPitchShifted = false, float pitchShift = 0.0f)
    {
        foreach (AudioClip clip in m_SoundEffects)
        {
            if (clip.name == p_name)
            {
                PlaySound(clip, volume, isPitchShifted, pitchShift);
                return;
            }
        }

        Debug.LogError("Can't find the audioclip");
    }

    public void PlaySound(AudioClip s, float volume = 1.0f, bool isPitchShifted = false, float pitchShift = 0.0f)
    {
        foreach (AudioSource c in channels)
        {
            if (!c.isPlaying)
            {
                c.clip = s;
                c.volume = volume;
                if (isPitchShifted) c.pitch += Random.Range(-pitchShift, pitchShift);
                c.Play();
                return;
            }
        }
    }

    private void InitAudioSources()
    {
        GameObject sources = transform.Find("Sources").gameObject;
        channels = sources.GetComponents<AudioSource>();

        foreach (AudioSource source in channels)
        {
            source.playOnAwake = false;
        }

        m_SoundEffects = Resources.LoadAll<AudioClip>("Audio/Effects/");
    }

    private void OnDestroy()
    {
        instance = null;
    }
}

