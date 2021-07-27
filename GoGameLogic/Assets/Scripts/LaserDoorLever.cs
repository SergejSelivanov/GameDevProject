using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDoorLever : MonoBehaviour
{
    public GameObject ConnectedDoor;
    public GameObject PlaneToTurnOff;
    public bool IsOpen = false;
    private bool IsWaiting = false;

    void CloseDoors()
    {
        FindObjectOfType<AudioManager>().Play("SwitchOn");
        if (PlaneToTurnOff != null)
            PlaneToTurnOff.SetActive(true);
        ConnectedDoor.transform.GetChild(0).position += new Vector3(0, 100, 0); //laser rays of doors return to their position
        IsWaiting = false;
    }

    void OpenDoors()
    {
        FindObjectOfType<AudioManager>().Play("SwitchOff");
        if (PlaneToTurnOff != null)
            PlaneToTurnOff.SetActive(false);
        ConnectedDoor.transform.GetChild(0).position -= new Vector3(0, 100, 0); //put away laser rays
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
}