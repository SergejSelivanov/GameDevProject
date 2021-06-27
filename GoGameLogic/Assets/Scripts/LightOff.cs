using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOff : MonoBehaviour
{
    public GameObject PlayerHandler;
    public GameObject ButtonUI;
    private Player PlayerFuncs;
    public int TurnsToTurnLightsOff = 3;

    public void TurnOffLight()
    {
        PlayerFuncs.LightsOffTurns = TurnsToTurnLightsOff;
        ButtonUI.SetActive(false);
        PlayerFuncs.ChangeLights();
    }

    void Start()
    {
        PlayerFuncs = PlayerHandler.GetComponent<Player>();
    }
}
