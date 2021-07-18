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
        /*if (Mathf.Abs(player.transform.position.x - button.transform.position.x) <= DistanceToPanel && Mathf.Abs(player.transform.position.z - transform.position.z) <= DistanceToPanel
        && GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsWaiting == false)
        {
            button.SetActive(true);
        }
        else
            button.SetActive(false);*/
        if (player.GetComponent<Player>().IsWaiting == false)
        {
            for (int i = 0; i < levers.Length; i++)
            {
                if (Mathf.Abs(player.transform.position.x - levers[i].transform.position.x) <= DistanceToPanel && Mathf.Abs(player.transform.position.z - levers[i].transform.position.z) <= DistanceToPanel)
                {
                    button.SetActive(true);
                    currentLever = levers[i];
                    break;
                }
                button.SetActive(false);
            }
        }
        else
            button.SetActive(false);
    }
}
