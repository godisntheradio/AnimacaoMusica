using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveEmitter : MonoBehaviour
{
    public AudioClip clip;
    public AudioClip clip2;

    public Text analysis;
    public AudioSource source;
    public Text CurrentSample;

    private void Awake()
    {
        clip.LoadAudioData();
        clip2.LoadAudioData();
    }
    void Start ()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.Play();
	}
    float previousMedian = 0;
	void Update ()
    {
        if (source.isPlaying)
        {
            float[] spectrum = new float[256];
            source.GetSpectrumData(spectrum,0,FFTWindow.Rectangular);
            float median = 0;
            for (int i = 0; i < 256; i++)
            {
                median += spectrum[i];
            }
            if (median / spectrum.Length > previousMedian)
            {
                previousMedian = median / spectrum.Length;
                Append(previousMedian.ToString());
            }
            CurrentSample.text = (median / spectrum.Length).ToString();
        }
	}
    void Append(string toAppend)
    {
        analysis.text += toAppend + " \n";
    }
   public void ChangeCoiso()
    {
        source.clip = clip2;
        source.Play();
    }
    
}
