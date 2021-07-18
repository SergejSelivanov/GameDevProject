using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject MenuPanel;

    public void ChangeMusicVolume()
    {
        if (PlayerPrefs.GetInt("MusicVolume") == 1)
            PlayerPrefs.SetInt("MusicVolume", 0);
        else
            PlayerPrefs.SetInt("MusicVolume", 1);
    }

    public void ChangeSoundsVolume()
    {
        if (PlayerPrefs.GetInt("SoundsVolume") == 1)
            PlayerPrefs.SetInt("SoundsVolume", 0);
        else
            PlayerPrefs.SetInt("SoundsVolume", 1);

    }

    public void OpenSettings()
    {
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        MenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }

    private void Awake()
    {
        if (PlayerPrefs.GetInt("MusicVolume") == 0)
        {
            //Debug.Log("AUE");
            gameObject.transform.Find("MusicToggle").gameObject.GetComponent<Toggle>().isOn = false;
            PlayerPrefs.SetInt("MusicVolume", 0);
        }
        else
        {
            gameObject.transform.Find("MusicToggle").gameObject.GetComponent<Toggle>().isOn = true;
            PlayerPrefs.SetInt("MusicVolume", 1);
        }
        if (PlayerPrefs.GetInt("SoundsVolume") == 0)
        {
            //Debug.Log("AUE");
            gameObject.transform.Find("SoundsToggle").gameObject.GetComponent<Toggle>().isOn = false;
           // PlayerPrefs.SetInt("SoundsVolume", 0);
        }
        else
        {
            gameObject.transform.Find("SoundsToggle").gameObject.GetComponent<Toggle>().isOn = true;
           // PlayerPrefs.SetInt("SoundsVolume", 1);
        }
        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Debug.Log("a" + PlayerPrefs.GetInt("SoundsVolume"));
    }


}
