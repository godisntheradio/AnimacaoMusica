using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Scripts
{
    class WaveAnalyser
    {
        AudioSource Source;


        public void Initialize(AudioSource src)
        {
            Source = src;

        }
        public float GetFrequency(float freq)
        {
            if (Source.isPlaying)
            {
                float[] spectrum = new float[512];
                Source.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);

                //float interval = Source.clip;

                return spectrum[(int)(freq * P)];
               
            }
            else
            {
                Debug.Log("not playing");
                return 0;
            }
        }
    }
}
