using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Taken from https://youtu.be/N8whM1GjH4w
// Video by: Rehope Games
// Posted February 25, 2023

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] 
    private AudioSource musicSource;
    [SerializeField] 
    private AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip musicClip;
    public AudioClip swingClip;
    public AudioClip fruitHitClip;
    public AudioClip playerHitClip;
    public AudioClip playerDeathClip;
    public AudioClip fruitDeathClip;
    public AudioClip playerWalkClip;

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip, volume); 
        }
    }

    public void MuteAllExcept(AudioClip exceptionClip)
    {
        if (musicSource != null)
            musicSource.Stop();

        if (exceptionClip != null)
        {
            sfxSource.Stop();
            sfxSource.PlayOneShot(exceptionClip);
        }
    }
}

