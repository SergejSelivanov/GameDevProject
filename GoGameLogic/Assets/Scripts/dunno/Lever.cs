using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private float DistanceToLever = 0.4f;
    public GameObject ConnectedDoor;
    public GameObject PlaneToTurnOff;
    private bool IsOpen = false;
    private bool IsWaiting = false;

    IEnumerator CloseDoors(int WhichCase)
    {
        if (PlaneToTurnOff != null)
            PlaneToTurnOff.SetActive(true);
        if (WhichCase == 0)
        {
            for (int i = 0; i < 30; i++)
            {
                ConnectedDoor.transform.GetChild(0).position += new Vector3(0.016f, 0, 0);
                ConnectedDoor.transform.position += new Vector3(-0.008f, 0, 0);
                yield return new WaitForSeconds(0.01f);
            }
        }
        else if (WhichCase == 1)
        {
            for (int i = 0; i < 30; i++)
            {
                ConnectedDoor.transform.GetChild(0).position += new Vector3(0, 0, -0.016f);
                ConnectedDoor.transform.position += new Vector3(0, 0, 0.008f);
                yield return new WaitForSeconds(0.01f);
            }
        }
        else if (WhichCase == 2)
        {
            for (int i = 0; i < 30; i++)
            {
                ConnectedDoor.transform.GetChild(0).position += new Vector3(-0.016f, 0, 0);
                ConnectedDoor.transform.position += new Vector3(0.008f, 0, 0);
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            for (int i = 0; i < 30; i++)
            {
                ConnectedDoor.transform.GetChild(0).position += new Vector3(0, 0, 0.016f);
                ConnectedDoor.transform.position += new Vector3(0, 0, -0.008f);
                yield return new WaitForSeconds(0.01f);
            }
        }
        IsWaiting = false;
        yield return null;
    }

    IEnumerator OpenDoors(int WhichCase)
    {
        if (PlaneToTurnOff != null)
            PlaneToTurnOff.SetActive(false);
        if (WhichCase == 0)
        {
            for (int i = 0; i < 30; i++)
            {
                ConnectedDoor.transform.GetChild(0).position += new Vector3(-0.016f, 0, 0);
                ConnectedDoor.transform.position += new Vector3(0.008f, 0, 0);
                yield return new WaitForSeconds(0.01f);
            }
        }
        else if (WhichCase == 1)
        {
            for (int i = 0; i < 30; i++)
            {
                ConnectedDoor.transform.GetChild(0).position += new Vector3(0, 0, 0.016f);
                ConnectedDoor.transform.position += new Vector3(0, 0, -0.008f);
                yield return new WaitForSeconds(0.01f);
            }
        }
        else if (WhichCase == 2)
        {
            for (int i = 0; i < 30; i++)
            {
                ConnectedDoor.transform.GetChild(0).position += new Vector3(0.016f, 0, 0);
                ConnectedDoor.transform.position += new Vector3(-0.008f, 0, 0);
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            for (int i = 0; i < 30; i++)
            {
                ConnectedDoor.transform.GetChild(0).position += new Vector3(0, 0, -0.016f);
                ConnectedDoor.transform.position += new Vector3(0, 0, 0.008f);
                yield return new WaitForSeconds(0.01f);
            }
        }
        IsWaiting = false;
        yield return null;
    }

    private void OnMouseDown()
    {
        GameObject player;
        player = GameObject.FindGameObjectWithTag("Projection");
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        if (Mathf.Abs(player.transform.position.x - transform.position.x) <= DistanceToLever && Mathf.Abs(player.transform.position.z - transform.position.z) <= DistanceToLever)
        {
            if (ConnectedDoor.transform.rotation.eulerAngles.y == 0)
            {
                if (IsOpen == false && IsWaiting == false)
                {
                    IsWaiting = true;
                    IsOpen = true;
                    StartCoroutine("OpenDoors", 0);
                }
                else if (IsOpen == true && IsWaiting == false)
                {
                    IsWaiting = true;
                    IsOpen = false;
                    StartCoroutine("CloseDoors", 0);
                }
            }
            if (ConnectedDoor.transform.rotation.eulerAngles.y == 90)
            {
                if (IsOpen == false && IsWaiting == false)
                {
                    IsWaiting = true;
                    IsOpen = true;
                    StartCoroutine("OpenDoors", 1);

                }
                else if (IsOpen == true && IsWaiting == false)
                {
                    IsWaiting = true;
                    IsOpen = false;
                    StartCoroutine("CloseDoors", 1);
                }
            }
            if (ConnectedDoor.transform.rotation.eulerAngles.y == 180)
            {
                if (IsOpen == false && IsWaiting == false)
                {
                    IsWaiting = true;
                    IsOpen = true;
                    StartCoroutine("OpenDoors", 2);

                }
                else if (IsOpen == true && IsWaiting == false)
                {
                    IsWaiting = true;
                    IsOpen = false;
                    StartCoroutine("CloseDoors", 2);
                }
            }
            if (ConnectedDoor.transform.rotation.eulerAngles.y == 270)
            {
                if (IsOpen == false && IsWaiting == false)
                {
                    IsWaiting = true;
                    IsOpen = true;
                    StartCoroutine("OpenDoors", 3);

                }
                else if (IsOpen == true && IsWaiting == false)
                {
                    IsWaiting = true;
                    IsOpen = false;
                    StartCoroutine("CloseDoors", 3);
                }
            }
        }
    }
}
