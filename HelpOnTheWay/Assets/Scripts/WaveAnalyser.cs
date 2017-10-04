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
        public int sampleResolution = 2048;
        [SerializeField]
        FFTWindow window = FFTWindow.Rectangular;

        public void Initialize(AudioSource src)
        {
            Source = src;

        }
        public float GetFrequency(float freq)
        {
            if (Source.isPlaying)
            {
                float[] spectrum = new float[sampleResolution];
                Source.GetSpectrumData(spectrum, 0, window);

                float interval = (AudioSettings.outputSampleRate/2) / sampleResolution;

                return spectrum[(int)(freq / interval )];
               
            }
            else
            {
                Debug.Log("not playing");
                return 0;
            }
        }
        void Analyse()
        {
            for (int i = 0; i < Source.clip.samples; i++)
            {
                float[] samples = new float[sampleResolution];
                Source.clip.GetData(samples, i);
                //fazer algo com isso
            }
        }
    }
}
