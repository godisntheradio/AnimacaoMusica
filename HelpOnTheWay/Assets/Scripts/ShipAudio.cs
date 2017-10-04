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

    bool hasDeathSound = false;
    void Start()
    {
        ShipSource = gameObject.AddComponent<AudioSource>();
        Death.LoadAudioData();
        Shot.LoadAudioData();
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
        //ShipSource.Play();
    }
    public void PlayDeathSound()
    {
        if (!hasDeathSound)
        {
            ShipSource.clip = Death;
            ShipSource.loop = false;
            ShipSource.Play();
            hasDeathSound = true;
        }
    }
    public bool IsPlayingSound()
    {
        return ShipSource.isPlaying;
    }


}
