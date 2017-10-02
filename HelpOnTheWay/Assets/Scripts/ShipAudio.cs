using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAudio : MonoBehaviour
{
    AudioSource ShipSource;
    [SerializeField]
    AudioClip Teste;

    void Start()
    {
        ShipSource = gameObject.AddComponent<AudioSource>();

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



}
