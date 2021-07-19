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

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PlayerPrefs.GetInt("MusicVolume"));
        source.volume = PlayerPrefs.GetInt("MusicVolume");
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
