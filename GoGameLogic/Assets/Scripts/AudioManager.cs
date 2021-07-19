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
    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

    private void Start()
    {
        int i = 0;
        volumes = new float[sounds.Length];
        foreach (Sound s in sounds)
        {
            volumes[i] = s.volume;
            i++;
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            if (PlayerPrefs.GetInt("SoundsVolume") == 1)
            {
                //Debug.Log("Aue");
                s.source.volume = s.volume;
            }
            else
                s.source.volume = 0;
           // s.source.volume = s.volume = PlayerPrefs.GetInt("SoundsVolume");
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void SwapSoundVolume()
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
            foreach (Sound s in sounds)
            {
                s.source.volume = volumes[i];
                i++;
            }
            PlayerPrefs.SetInt("SoundsVolume", 1);
        }
    }
}
