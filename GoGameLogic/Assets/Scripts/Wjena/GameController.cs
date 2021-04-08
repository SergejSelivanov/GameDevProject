using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private bool menu = false;
    public GameObject but1;
    public GameObject but2;
    public GameObject but3;
    public void ft_active_buttons()
    {
        if (menu == false)
        {
            Time.timeScale = 0;
            menu = true;
            but1.SetActive(true);
            but2.SetActive(true);
        }
        else if (menu == true)
        {
            Time.timeScale = 1;
            menu = false;
            but1.SetActive(false);
            but2.SetActive(false);
        }    
    }

    public void ft_active_buttons1()
    {
        if (menu == false)
        {
            Time.timeScale = 0;
            menu = true;
            but1.SetActive(true);
            but2.SetActive(true);
            but3.SetActive(true);
        }
        else if (menu == true)
        {
            Time.timeScale = 1;
            menu = false;
            but1.SetActive(false);
            but2.SetActive(false);
            but3.SetActive(false);
        }
    }

    public void ft_continue1()
    {
        Time.timeScale = 1;
        menu = false;
        but1.SetActive(false);
        but2.SetActive(false);
        but3.SetActive(false);
    }

    public void ft_exit_menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void ft_choose_menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MapScene");
    }

    public void ft_continue()
    {
        Time.timeScale = 1;
        menu = false;
        but1.SetActive(false);
        but2.SetActive(false);
    }
}
