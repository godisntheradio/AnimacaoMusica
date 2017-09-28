using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
public class WaveEmitter : MonoBehaviour
{
    public AudioClip clip;
    public AudioClip clip2;

    public Text textRef;
    public AudioSource source;
    public Text CurrentSample;


    WaveAnalyser Analyser;

    private void Awake()
    {
        clip.LoadAudioData();
        clip2.LoadAudioData();
        Analyser = new WaveAnalyser();
    }
    void Start ()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        Analyser.Initialize(source);
        source.Play();
	}
    float previousMedian = 0;
	void Update ()
    {
        float a = Analyser.GetFrequency(440.0f);
        {
            Append(a.ToString());
        }
	}
    void Append(string toAppend)
    {
        textRef.text += toAppend + " \n";
    }
   public void ChangeCoiso()
    {
        source.clip = clip2;
        source.Play();
    }
    
}
