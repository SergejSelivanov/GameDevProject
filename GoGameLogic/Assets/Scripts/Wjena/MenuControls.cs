using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public void ft_Play_Pressed()
    {
        SceneManager.LoadScene("MapScene");
    }

    public void ft_Exit_Pressed()
    {
        Debug.Log("Exit pressed!");
        Application.Quit();
    }
}
