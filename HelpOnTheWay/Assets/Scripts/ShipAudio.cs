using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAudio : MonoBehaviour
{
    AudioSource ShipSource;
    [SerializeField]
    AudioClip Teste;
    [SerializeField]
    AudioClip Death;
    [SerializeField]
    AudioClip Shot;
    [SerializeField]
    AudioClip GetShot;
    AudioSource DeathSource;
    [SerializeField]
    UnityEngine.Audio.AudioMixer mixer;

    public bool hasDeathSound = false;
    void Start()
    {

        if (!ShipSource)
        {
            ShipSource = gameObject.AddComponent<AudioSource>();
            ShipSource.playOnAwake = false;
            ShipSource.outputAudioMixerGroup = mixer.outputAudioMixerGroup;
        }
        if (!DeathSource)
        {
            DeathSource = gameObject.AddComponent<AudioSource>();
            DeathSource.playOnAwake = false;
            DeathSource.outputAudioMixerGroup = mixer.outputAudioMixerGroup;

        }

        Death.LoadAudioData();
        DeathSource.clip = Death;
        Shot.LoadAudioData();
    }
    public void Initialize()
    {
        hasDeathSound = false;

    }
    void Update()
    {

    }

    public void PlayTestSound()
    {
        ShipSource.clip = Teste;
        ShipSource.loop = false;
        ShipSource.Play();
    }
    public void PlayShootSound()
    {
        ShipSource.clip = Shot;
        ShipSource.loop = false;
        ShipSource.Play();
    }
    public void PlayDeathSound()
    {
        if (!hasDeathSound)
        {
            DeathSource.loop = false;
            DeathSource.Play();
            hasDeathSound = true;
        }
    }
    public bool IsPlayingSound()
    {
        return ShipSource.isPlaying;
    }
    public bool IsPlayingDeathSound()
    {
        return DeathSource.isPlaying;
    }
    public void PlayHitSound()
    {
        ShipSource.clip = GetShot;
        ShipSource.loop = false;
        ShipSource.Play();
    }

}
