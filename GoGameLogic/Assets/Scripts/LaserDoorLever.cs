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
    /*IEnumerator CloseDoors(int WhichCase)
    {
        if (PlaneToTurnOff != null)
            PlaneToTurnOff.SetActive(true);
        ConnectedDoor.transform.GetChild(0).position += new Vector3(0, 100, 0);
        IsWaiting = false;
        yield return null;
    }*/

    /*IEnumerator OpenDoors(int WhichCase)
    {
        if (PlaneToTurnOff != null)
            PlaneToTurnOff.SetActive(false);
        ConnectedDoor.transform.GetChild(0).position -= new Vector3(0, 100, 0);
        IsWaiting = false;
        yield return null;
    }*/

    private void OnMouseDown()
    {
        GameObject player;
        player = GameObject.FindGameObjectWithTag("Projection");
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
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
                Debug.Log("AUE");
                IsWaiting = true;
                IsOpen = false;
                CloseDoors();
            }
        }
    }
}