using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject MenuPanel;
    GameObject audioManager;

    /*private void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("GraphicsDropdown", qualityIndex);
    }*/

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
            gameObject.transform.Find("MusicToggle").gameObject.GetComponent<Toggle>().isOn = false; //turn off music toggle at start of scene if it needs to
        else
            gameObject.transform.Find("MusicToggle").gameObject.GetComponent<Toggle>().isOn = true;
        if (PlayerPrefs.GetInt("SoundsVolume") == 0)
            gameObject.transform.Find("SoundsToggle").gameObject.GetComponent<Toggle>().isOn = false;
        else
            gameObject.transform.Find("SoundsToggle").gameObject.GetComponent<Toggle>().isOn = true;
        /*if (PlayerPrefs.GetInt("GraphicsDropdown") == 0)
        {
            gameObject.transform.Find("GraphicsDropdown").gameObject.GetComponent<Dropdown>().value = 0;
            SetQuality(0);
        }
        else if (PlayerPrefs.GetInt("GraphicsDropdown") == 1)
        {
            gameObject.transform.Find("GraphicsDropdown").gameObject.GetComponent<Dropdown>().value = 1;
            SetQuality(1);
        }
        else if (PlayerPrefs.GetInt("GraphicsDropdown") == 2)
        {
            gameObject.transform.Find("GraphicsDropdown").gameObject.GetComponent<Dropdown>().value = 2;
            SetQuality(2);
        }*/
        audioManager = FindObjectOfType<AudioManager>().gameObject;
    }

    void Update()
    {
        if (gameObject.transform.Find("SoundsToggle").gameObject.GetComponent<Toggle>().isOn == true && PlayerPrefs.GetInt("SoundsVolume") == 0) //if sounds toggle is switched
            audioManager.GetComponent<AudioManager>().SwapSoundVolume();
        if (gameObject.transform.Find("SoundsToggle").gameObject.GetComponent<Toggle>().isOn == false && PlayerPrefs.GetInt("SoundsVolume") == 1)
            audioManager.GetComponent<AudioManager>().SwapSoundVolume();
        if (gameObject.transform.Find("MusicToggle").gameObject.GetComponent<Toggle>().isOn == true && PlayerPrefs.GetInt("MusicVolume") == 0) //if music toggle is switched
            PlayerPrefs.SetInt("MusicVolume", 1);
        if (gameObject.transform.Find("MusicToggle").gameObject.GetComponent<Toggle>().isOn == false && PlayerPrefs.GetInt("MusicVolume") == 1)
            PlayerPrefs.SetInt("MusicVolume", 0);
        /*if (gameObject.transform.Find("GraphicsDropdown").gameObject.GetComponent<Dropdown>().value == 0 && QualitySettings.GetQualityLevel() != 0) //if graphics dropdown is switched
            SetQuality(0);
        if (gameObject.transform.Find("GraphicsDropdown").gameObject.GetComponent<Dropdown>().value == 1 && QualitySettings.GetQualityLevel() != 1)
            SetQuality(1);
        if (gameObject.transform.Find("GraphicsDropdown").gameObject.GetComponent<Dropdown>().value == 2 && QualitySettings.GetQualityLevel() != 2)
            SetQuality(2);*/
    }
}
