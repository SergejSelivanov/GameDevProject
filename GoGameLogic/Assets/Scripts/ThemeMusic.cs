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

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<AudioSource>().GetComponent<AudioClip>() = Songs[0];
        source = gameObject.GetComponent<AudioSource>();
        source.clip = Songs[0];
        source.Play();
        //gameObject.GetComponent<AudioSource>().clip = Songs[0];
        //clip = Songs[0];
        //FindObjectsOfType<AudioSource>().
        //gameObject.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= source.clip.length)
        {
            Timer = 0;
            index++;
            if (index >= Songs.Length)
                index = 0;
            source.clip = Songs[index];
            source.Play();
        }
        //Debug.Log(source.clip.length);
    }
}
