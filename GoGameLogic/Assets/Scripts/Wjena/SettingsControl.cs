using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.UI;

public class SettingsControl : MonoBehaviour
{
    private bool isFullScreen = false;
    public AudioMixer am;
    public AudioMixer am1;
    Resolution[] rsl;
    List<string> resolutions;
    public Dropdown dropdown;

    public void FullScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    public void AudioVolume(float sliderValue)
    {
        am.SetFloat("MasterVolume", sliderValue);
    }

    public void AudioVolume1(float sliderValue)
    {
        am1.SetFloat("MasterVolume", sliderValue);
    }

    public void Resolution(int r)
    {
        Screen.SetResolution(rsl[r].width, rsl[r].height, isFullScreen);
    }

    public void Awake()
    {
        resolutions = new List<string>();
        rsl = Screen.resolutions;
        foreach (var i in rsl)
        {
            resolutions.Add(i.width + "x" + i.height);
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(resolutions);
    }
}
