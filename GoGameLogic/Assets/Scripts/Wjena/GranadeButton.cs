using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeButton : MonoBehaviour
{
    public GameObject PlayerHandler;
    private Player PlayerFuncs;
    public void ft_pressed_granade_button()
    {
        if (PlayerFuncs.IsflagGranade == true)
        {
            Time.timeScale = 0;
            PlayerFuncs.IsflagGranade = false;
            return;
        }
        else if (PlayerFuncs.IsflagGranade == false)
        {
            Time.timeScale = 1;
            PlayerFuncs.IsflagGranade = true;
        }
    }

    void Start()
    {
        PlayerFuncs = PlayerHandler.GetComponent<Player>();
    }

    // Update is called once per frame
   /* void Update()
    {
        
    }*/
}
