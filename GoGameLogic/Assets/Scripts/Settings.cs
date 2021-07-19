using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject MenuPanel;
    GameObject audioManager;

    /*public void ChangeMusicVolume()
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

    }*/

    private void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
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
           // PlayerPrefs.SetInt("MusicVolume", 0);
        }
        else
        {
            gameObject.transform.Find("MusicToggle").gameObject.GetComponent<Toggle>().isOn = true;
            //PlayerPrefs.SetInt("MusicVolume", 1);
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
        if (QualitySettings.GetQualityLevel() == 0)
            gameObject.transform.Find("GraphicsDropdown").gameObject.GetComponent<Dropdown>().value = 0;
        else if (QualitySettings.GetQualityLevel() == 1)
            gameObject.transform.Find("GraphicsDropdown").gameObject.GetComponent<Dropdown>().value = 1;
        else if (QualitySettings.GetQualityLevel() == 2)
            gameObject.transform.Find("GraphicsDropdown").gameObject.GetComponent<Dropdown>().value = 2;
        audioManager = FindObjectOfType<AudioManager>().gameObject;
        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("a" + PlayerPrefs.GetInt("SoundsVolume"));
        if (gameObject.transform.Find("SoundsToggle").gameObject.GetComponent<Toggle>().isOn == true && PlayerPrefs.GetInt("SoundsVolume") == 0)
        {
            audioManager.GetComponent<AudioManager>().SwapSoundVolume();
            //PlayerPrefs.SetInt("SoundsVolume", 1);
        }
        if (gameObject.transform.Find("SoundsToggle").gameObject.GetComponent<Toggle>().isOn == false && PlayerPrefs.GetInt("SoundsVolume") == 1)
        {
            audioManager.GetComponent<AudioManager>().SwapSoundVolume();
            //PlayerPrefs.SetInt("SoundsVolume", 0);
        }
        if (gameObject.transform.Find("MusicToggle").gameObject.GetComponent<Toggle>().isOn == true && PlayerPrefs.GetInt("MusicVolume") == 0)
        {
            PlayerPrefs.SetInt("MusicVolume", 1);
        }
        if (gameObject.transform.Find("MusicToggle").gameObject.GetComponent<Toggle>().isOn == false && PlayerPrefs.GetInt("MusicVolume") == 1)
            PlayerPrefs.SetInt("MusicVolume", 0);
        if (gameObject.transform.Find("GraphicsDropdown").gameObject.GetComponent<Dropdown>().value == 0 && QualitySettings.GetQualityLevel() != 0)
        {
           //PlayerPrefs.SetInt("GraphicsIndex", 0);
            SetQuality(0);
        }
        if (gameObject.transform.Find("GraphicsDropdown").gameObject.GetComponent<Dropdown>().value == 1 && QualitySettings.GetQualityLevel() != 1)
        {
            //PlayerPrefs.SetInt("GraphicsIndex", 1);
            SetQuality(1);
        }
        if (gameObject.transform.Find("GraphicsDropdown").gameObject.GetComponent<Dropdown>().value == 2 && QualitySettings.GetQualityLevel() != 2)
        {
            //PlayerPrefs.SetInt("GraphicsIndex", 2);
            SetQuality(2);
        }
    }
}
