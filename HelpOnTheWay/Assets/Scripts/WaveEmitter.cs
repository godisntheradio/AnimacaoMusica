﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
public class WaveEmitter : MonoBehaviour
{
    public List<AudioClip> audioList;
    int currentSong = 0;

    public Text textRef;
    public AudioSource source;
    public Text CurrentSample;

    
    WaveAnalyser Analyser;

    public GameObject b;
    List<GameObject> objs;
    public float smooth = 40;
    [SerializeField]
    float SpectrumScale = 100;
    private void Awake()
    {
       
    }
    void Start ()
    {
        source = gameObject.AddComponent<AudioSource>();
        objs = new List<GameObject>();
        Analyser = new WaveAnalyser();
        foreach (var item in audioList)
        {
            item.LoadAudioData();
        }
        source.clip = audioList[0];
        Analyser.Initialize(source);
        source.Play();
        objs.Add(b);
        for (int i = 0; i < 100; i++)
        {
            GameObject toAdd = Instantiate(b, b.transform.position + new Vector3(0.5f * (i+1),0,0),b.transform.rotation);
            objs.Add(toAdd);
        }
	}
	void Update ()
    {
        float freq = 32.70f;
        float fundamental = freq;
        int pitch = 1;
        foreach (GameObject item in objs)
        {
            float a = Analyser.GetFrequencyVolume(freq);
            item.transform.localScale = Vector3.Lerp(new Vector3(0.5f,1 * (a * SpectrumScale),0.5f), item.transform.localScale, Time.deltaTime * smooth);
            float aconst = Mathf.Pow(2.0f, 1.0f / 12.0f);
            float aN = (Mathf.Pow(aconst, (float) pitch));
            freq = fundamental * aN;
            pitch++;
            
            
        }
	}
    void Append(string toAppend)
    {
        textRef.text = toAppend + " \n";
    }
   public void Next()
    {
        if (currentSong + 1 > audioList.Count - 1)
        {
            source.clip = audioList[0];
            currentSong = 0;
        }
        else
        {
            source.clip = audioList[currentSong + 1];
            currentSong = currentSong + 1;

        }
        source.Play();
    }
    public void Prev()
    {
        if (currentSong - 1 < 0)
        {
            source.clip = audioList[audioList.Count - 1];
            currentSong = audioList.Count - 1;

        }
        else
        {
            source.clip = audioList[currentSong - 1];
            currentSong = currentSong - 1;

        }
        source.Play();

    }
    public AudioSource GetAudioSource()
    {
        return source;
    }

}
