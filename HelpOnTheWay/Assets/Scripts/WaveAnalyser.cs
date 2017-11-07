using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using AForge;
namespace Assets.Scripts
{
    class WaveAnalyser
    {

        public AudioSource Source;
        public int sampleResolution = 1024;
        [SerializeField]
        FFTWindow window = FFTWindow.Rectangular;

        

        public void Initialize(AudioSource src)
        {
            Source = src;

        }
        public float GetFrequencyVolume(float freq)
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
        public float GetMedianOfSpectrum()
        {
            float freq = 32.70f;
            float fundamental = freq;
            float sum = 0;
            float total = 96;
            float a = 0;
            int pitch = 1;
            for (int i = 0; i < total; i++)
            {

                a = GetFrequencyVolume(freq);
                sum += a;
                float aconst = Mathf.Pow(2.0f, 1.0f / 12.0f);
                float aN = (Mathf.Pow(aconst, (float)pitch));
                freq = fundamental * aN;
                pitch++;
                
            }
            return a;
        }
        int GetFrequencyIndexInSpectrum(float freq)
        {
            float interval = (AudioSettings.outputSampleRate / 2) / sampleResolution;
            return (int)(freq / interval);
        }
        //feed it with lowest note for better effect
        
        ////returns list of indexes(of spectrum) where the peak occurs
        //public List<int> FindPeaks(float freq)
        //{
        //    List<int> peaks = new List<int>();
        //    float xMinus = 0, xPlus = 0;
        //    for (int i = 0; i < Source.clip.samples - ((Source.clip.samples / (int)Source.clip.length)); i += (Source.clip.samples / (int)Source.clip.length))
        //    {
        //        float[] samples = new float[sampleResolution];
        //        float[] samplesPlus = new float[sampleResolution];

        //        Source.clip.GetData(samples, i);
        //        Source.clip.GetData(samplesPlus, i + 2);
        //        int index = GetFrequencyIndexInSpectrum(freq);
        //        xPlus = samplesPlus[index];
                
        //        if (samples[index] > xMinus && samples[index] > xPlus)
        //        {
        //            peaks.Add(i);
        //        }

        //        xMinus = samples[index];
                
                
        //    }
        //    return peaks;
        //}
        //public bool IsPeak(int index, float freq)
        //{
            
        //    if (Source.timeSamples == index) 
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}
