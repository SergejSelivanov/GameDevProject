using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;


//[System.Serializable]
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private float[] volumes;

    private void Start()
    {
        int i = 0;
        volumes = new float[sounds.Length];
        foreach (Sound s in sounds)
        {
            volumes[i] = s.volume; //copying volumes of all sounds
            i++;
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            if (PlayerPrefs.GetInt("SoundsVolume") == 1) //if sounds enabled
                s.source.volume = s.volume;
            else
                s.source.volume = 0;
        }
    }

    public void Play (string name) //Play sound
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void SwapSoundVolume() //turn sound on and off
    {
        int i = 0;
        if (PlayerPrefs.GetInt("SoundsVolume") == 1)
        {
            foreach (Sound s in sounds)
            {
                s.source.volume = 0;
            }
            PlayerPrefs.SetInt("SoundsVolume", 0);
        }
        else
        {
            foreach (Sound s in sounds) //get needed volume
            {
                s.source.volume = volumes[i];
                i++;
            }
            PlayerPrefs.SetInt("SoundsVolume", 1);
        }
    }
}
