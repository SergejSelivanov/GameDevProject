using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeButton : MonoBehaviour
{
    // Start is called before the first frame update
    // public GameObject NodeHandler;
    // private Node NodeFuncs;
    public GameObject PlayerHandler;
    private Player PlayerFuncs;
    public void ft_pressed_granade_button()
    {
        //flagGranade = false;
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
        //Debug.Log(flagGranade);
    }

    void Start()
    {
        PlayerFuncs = PlayerHandler.GetComponent<Player>();
        // NodeFuncs = NodeHandler.GetComponent<Node>();
    }

    // Update is called once per frame
   /* void Update()
    {
        
    }*/
}
