using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject MenuPanel;

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

    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
