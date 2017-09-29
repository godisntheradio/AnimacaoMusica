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
        public int sampleResolution = 512;

        public void Initialize(AudioSource src)
        {
            Source = src;

        }
        public float GetFrequency(float freq)
        {
            if (Source.isPlaying)
            {
                float[] spectrum = new float[sampleResolution];
                Source.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);

                float interval = (AudioSettings.outputSampleRate/2) / sampleResolution;

                return spectrum[(int)(freq / interval )];
               
            }
            else
            {
                Debug.Log("not playing");
                return 0;
            }
        }
    }
}
