using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoorLever : MonoBehaviour
{
    private float DistanceToLever = 0.4f;
    public bool IsOpen = false;
    private bool IsWaiting = false;
    public GameObject PlaneToTurnOff;
    public GameObject ConnectedPart;


    void ClosePart()
    {
        FindObjectOfType<AudioManager>().Play("SwitchOff");
        FindObjectOfType<AudioManager>().Play("Lever");
        if (PlaneToTurnOff != null)
            PlaneToTurnOff.SetActive(true);
        ConnectedPart.SetActive(true);
        //ConnectedDoor.transform.GetChild(0).position += new Vector3(0, 100, 0);
        IsWaiting = false;
    }

    void OpenPart()
    {
        FindObjectOfType<AudioManager>().Play("SwitchOn");
        FindObjectOfType<AudioManager>().Play("Lever");
        if (PlaneToTurnOff != null)
            PlaneToTurnOff.SetActive(false);
        //ConnectedDoor.transform.GetChild(0).position -= new Vector3(0, 100, 0);
        ConnectedPart.SetActive(false);
        IsWaiting = false;
    }

    private void OnMouseDown()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (Mathf.Abs(player.transform.position.x - transform.position.x) <= DistanceToLever && Mathf.Abs(player.transform.position.z - transform.position.z) <= DistanceToLever)
        {
            if (IsOpen == false && IsWaiting == false)
            {
                IsWaiting = true;
                IsOpen = true;
                OpenPart();
            }
            else if (IsOpen == true && IsWaiting == false)
            {
                IsWaiting = true;
                IsOpen = false;
                ClosePart();
            }
        }
    }
}
