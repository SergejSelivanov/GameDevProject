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
        if (PlaneToTurnOff != null)
            PlaneToTurnOff.SetActive(true);
        ConnectedPart.SetActive(true);
        //ConnectedDoor.transform.GetChild(0).position += new Vector3(0, 100, 0);
        IsWaiting = false;
    }

    void OpenPart()
    {
        if (PlaneToTurnOff != null)
            PlaneToTurnOff.SetActive(false);
        //ConnectedDoor.transform.GetChild(0).position -= new Vector3(0, 100, 0);
        ConnectedPart.SetActive(false);
        IsWaiting = false;
    }

    private void OnMouseDown()
    {
        GameObject player;
        //player = GameObject.FindGameObjectWithTag("Projection");
        //if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
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
                //Debug.Log("AUE");
                IsWaiting = true;
                IsOpen = false;
                ClosePart();
            }
        }
    }




    // Start is called before the first frame update
    /* void Start()
     {

     }

     // Update is called once per frame
     void Update()
     {

     }*/
}
