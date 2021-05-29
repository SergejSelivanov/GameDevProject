using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiLaserDoorLever : MonoBehaviour
{
    private float DistanceToLever = 0.4f;
    public GameObject ConnectedDoor;
    public GameObject PlaneToTurnOff;
   // public bool IsOpen = false;
    private bool IsWaiting = false;
    private MultiLaserDoor door;

    void CloseDoors()
    {
        if (PlaneToTurnOff != null)
            PlaneToTurnOff.SetActive(true);
        ConnectedDoor.transform.GetChild(0).position += new Vector3(0, 100, 0);
        IsWaiting = false;
    }

    void OpenDoors()
    {
        if (PlaneToTurnOff != null)
            PlaneToTurnOff.SetActive(false);
        ConnectedDoor.transform.GetChild(0).position -= new Vector3(0, 100, 0);
        IsWaiting = false;
    }

    private void OnMouseDown()
    {
        GameObject player;
        player = GameObject.FindGameObjectWithTag("Player");
        if (Mathf.Abs(player.transform.position.x - transform.position.x) <= DistanceToLever && Mathf.Abs(player.transform.position.z - transform.position.z) <= DistanceToLever)
        {
            if (door.IsOpened == false && IsWaiting == false)
            {
                IsWaiting = true;
                door.IsOpened = true;
                OpenDoors();
            }
            else if (door.IsOpened == true && IsWaiting == false)
            {
                IsWaiting = true;
                door.IsOpened = false;
                CloseDoors();
            }
        }
    }

    private void Start()
    {
        door = ConnectedDoor.GetComponent<MultiLaserDoor>();
    }
}
