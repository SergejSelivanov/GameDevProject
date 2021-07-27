using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject Panel;
    public GameObject MenuButton;

    public void OpenMenu()
    {
        Panel.SetActive(true);
        MenuButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        Panel.SetActive(false);
        MenuButton.SetActive(true);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        Panel.SetActive(false);
        FindObjectOfType<LevelLoader>().LoadSameLevel();
    }

    public void ChooseLevel()
    {
        Time.timeScale = 1;
        Panel.SetActive(false);
        FindObjectOfType<LevelLoader>().LoadLevelSelector();
    }
}
