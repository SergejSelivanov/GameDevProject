using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOff : MonoBehaviour
{
    public GameObject PlayerHandler;
    public GameObject ButtonUI;
    //public GameObject Light;
    private Player PlayerFuncs;
    public int TurnsToTurnLightsOff = 2;

    public void TurnOffLight()
    {
        //PlayerFuncs.LightOffTurns = TurnsToTurnLightsOff;
        if (PlayerFuncs.ProjectionActive == false)
        {
            PlayerFuncs.LightOffTurns = 3;
            ButtonUI.SetActive(false);
            //Light.SetActive(false);
            PlayerFuncs.ChangeLights();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        PlayerFuncs = PlayerHandler.GetComponent<Player>();
    }

    // Update is called once per frame
   /* void Update()
    {
        
    }*/
}
