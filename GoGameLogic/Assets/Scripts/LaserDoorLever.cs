using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDoorLever : MonoBehaviour
{
    private float DistanceToLever = 0.4f;
    public GameObject ConnectedDoor;
    public GameObject PlaneToTurnOff;
    public bool IsOpen = false;
    private bool IsWaiting = false;

    void CloseDoors()
    {
        FindObjectOfType<AudioManager>().Play("SwitchOn");
        if (PlaneToTurnOff != null)
            PlaneToTurnOff.SetActive(true);
        ConnectedDoor.transform.GetChild(0).position += new Vector3(0, 100, 0);
        IsWaiting = false;
    }

    void OpenDoors()
    {
        FindObjectOfType<AudioManager>().Play("SwitchOff");
        if (PlaneToTurnOff != null)
            PlaneToTurnOff.SetActive(false);
        ConnectedDoor.transform.GetChild(0).position -= new Vector3(0, 100, 0);
        IsWaiting = false;
    }

    public void ChangeState()
    {
        if (IsOpen == false && IsWaiting == false)
        {
            IsWaiting = true;
            IsOpen = true;
            OpenDoors();
        }
        else if (IsOpen == true && IsWaiting == false)
        {
            IsWaiting = true;
            IsOpen = false;
            CloseDoors();
        }
    }

    /*private void OnMouseDown()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (Mathf.Abs(player.transform.position.x - transform.position.x) <= DistanceToLever && Mathf.Abs(player.transform.position.z - transform.position.z) <= DistanceToLever)
        {
            if (IsOpen == false && IsWaiting == false)
            {
                IsWaiting = true;
                IsOpen = true;
                OpenDoors();
            }
            else if (IsOpen == true && IsWaiting == false)
            {
                IsWaiting = true;
                IsOpen = false;
                CloseDoors();
            }
        }
    }*/
}