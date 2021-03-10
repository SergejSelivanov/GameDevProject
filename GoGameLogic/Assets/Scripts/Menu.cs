using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // public GameObject InvisibilityButton;
    // public GameObject ThrowKnifeButton;
    public GameObject Panel;
    public GameObject MenuButton;

    // Start is called before the first frame update

    public void OpenMenu()
    {
        Panel.SetActive(true);
        MenuButton.SetActive(false);
        Time.timeScale = 0;
       // InvisibilityButton.SetActive(false);
       // ThrowKnifeButton.SetActive(false);

    }

    public void Continue()
    {
        Panel.SetActive(false);
        MenuButton.SetActive(true);
        Time.timeScale = 1;
    }

    /*void Start()
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown("escape"))
        {
            OpenMenu();
        }*/
    }
}
