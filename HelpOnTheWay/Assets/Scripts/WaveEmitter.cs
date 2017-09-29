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

    public GameObject b;
    public float smooth = 10;
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
	void Update ()
    {
        float a = Analyser.GetFrequency(440.0f);
        {
            Append(a.ToString());
            if (b)
            {
                b.transform.localScale = Vector3.Lerp(new Vector3(0.5f,1 * (a * 100),0.5f), b.transform.localScale, Time.deltaTime * smooth);

            }

        }
	}
    void Append(string toAppend)
    {
        textRef.text = toAppend + " \n";
    }
   public void ChangeCoiso()
    {
        source.clip = clip2;
        source.Play();
    }
    
}
