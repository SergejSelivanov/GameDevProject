using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLever : MonoBehaviour
{
    GameObject player;
    public float DistanceToPanel = 0.4f;
    public GameObject button;
    GameObject[] levers;
    GameObject currentLever;

    public void ChangeLeverState()
    {
        try
        {
            currentLever.GetComponent<LaserDoorLever>().ChangeState();
        }
        catch
        {
            currentLever.GetComponent<CameraPanel>().ChangeState();
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        levers = GameObject.FindGameObjectsWithTag("Lever");
    }

    private void FixedUpdate()
    {
        if (player.GetComponent<Player>().IsWaiting == false) //works only if player is ready to move
        {
            for (int i = 0; i < levers.Length; i++)
            {
                if (Mathf.Abs(player.transform.position.x - levers[i].transform.position.x) <= DistanceToPanel //if player is close enough to lever
                && Mathf.Abs(player.transform.position.z - levers[i].transform.position.z) <= DistanceToPanel) 
                {
                    button.SetActive(true); //activate button to interact with levers
                    currentLever = levers[i]; //to understand which lever player needs to interact with
                    break;
                }
                button.SetActive(false); //unactivate button to interact with levers
            }
        }
        else
            button.SetActive(false);
    }
}
