using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public void LoadLevelSelector()
    {
        StartCoroutine(LoadLevel(0));
    }

    public void LoadSameLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadNextLevel()
    {
        if (PlayerPrefs.GetInt("levelReached") < SceneManager.GetActiveScene().buildIndex + 1)
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 > 6)
                PlayerPrefs.SetInt("levelReached", SceneManager.GetActiveScene().buildIndex - 1);
            else if (PlayerPrefs.GetInt("levelReached") == 5 || PlayerPrefs.GetInt("levelReached") == 6)
                PlayerPrefs.SetInt("levelReached", 5);
            else
                PlayerPrefs.SetInt("levelReached", SceneManager.GetActiveScene().buildIndex + 1);
        }
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start"); //start crossfade
        yield return new WaitForSeconds(1f); //wait for crossfade animation
        SceneManager.LoadScene(levelIndex);
    }
}
