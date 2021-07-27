using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ThemeMusic : MonoBehaviour
{
    public AudioClip[] Songs;
    private AudioSource source;
    private double Timer;
    private static int index = 0;
    public int volume = 1;

    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        source.clip = Songs[0];
        source.Play();
    }

    void Update()
    {
        source.volume = PlayerPrefs.GetInt("MusicVolume");
        Timer += Time.deltaTime;
        if (Timer >= source.clip.length) //if song is over
        {
            Timer = 0;
            index++;
            if (index >= Songs.Length) //if there is no more songs
                index = 0;  //start from first song
            source.clip = Songs[index];
            source.Play();
        }
    }
}
