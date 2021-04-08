using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private float DistanceToLever = 0.4f;
    public GameObject ConnectedDoor;
    //public int Angle = 90;
    private bool IsOpen = false;
    private bool IsWaiting = false;

    /*private GameObject FindConnectedDoor()
    {
        RaycastHit Hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out Hit);
        if (Hit.collider != null)
            return Hit.collider.gameObject;
        return null;
    }*/

    IEnumerator CloseDoors(int WhichCase)
    {
        if (WhichCase == 0)
        {
            for (int i = 0; i < 30; i++)
            {
                // ConnectedDoor.transform.position += new Vector3(-0.01f, 0, 0);
                ConnectedDoor.transform.GetChild(0).position += new Vector3(0.016f, 0, 0);
                ConnectedDoor.transform.position += new Vector3(-0.008f, 0, 0);
                yield return new WaitForSeconds(0.01f);
            }
        }
        else if (WhichCase == 1)
        {
            for (int i = 0; i < 30; i++)
            {
                // ConnectedDoor.transform.position += new Vector3(-0.01f, 0, 0);
                ConnectedDoor.transform.GetChild(0).position += new Vector3(0, 0, -0.016f);
                ConnectedDoor.transform.position += new Vector3(0, 0, 0.008f);
                yield return new WaitForSeconds(0.01f);
            }
        }
        else if (WhichCase == 2)
        {
            for (int i = 0; i < 30; i++)
            {
                // ConnectedDoor.transform.position += new Vector3(-0.01f, 0, 0);
                ConnectedDoor.transform.GetChild(0).position += new Vector3(-0.016f, 0, 0);
                ConnectedDoor.transform.position += new Vector3(0.008f, 0, 0);
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            for (int i = 0; i < 30; i++)
            {
                // ConnectedDoor.transform.position += new Vector3(-0.01f, 0, 0);
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
        if (WhichCase == 0)
        {
            for (int i = 0; i < 30; i++)
            {
                // ConnectedDoor.transform.position += new Vector3(-0.01f, 0, 0);
                ConnectedDoor.transform.GetChild(0).position += new Vector3(-0.016f, 0, 0);
                ConnectedDoor.transform.position += new Vector3(0.008f, 0, 0);
                yield return new WaitForSeconds(0.01f);
            }
        }
        else if (WhichCase == 1)
        {
            for (int i = 0; i < 30; i++)
            {
                // ConnectedDoor.transform.position += new Vector3(-0.01f, 0, 0);
                ConnectedDoor.transform.GetChild(0).position += new Vector3(0, 0, 0.016f);
                ConnectedDoor.transform.position += new Vector3(0, 0, -0.008f);
                yield return new WaitForSeconds(0.01f);
            }
        }
        else if (WhichCase == 2)
        {
            for (int i = 0; i < 30; i++)
            {
                // ConnectedDoor.transform.position += new Vector3(-0.01f, 0, 0);
                ConnectedDoor.transform.GetChild(0).position += new Vector3(0.016f, 0, 0);
                ConnectedDoor.transform.position += new Vector3(-0.008f, 0, 0);
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            for (int i = 0; i < 30; i++)
            {
                // ConnectedDoor.transform.position += new Vector3(-0.01f, 0, 0);
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
        /* GameObject[] AllGates = GameObject.FindGameObjectsWithTag("Gate");
         for (int i = 0; i < AllGates.Length; i++)
         {
             Debug.Log(player.transform.position.z);
             Debug.Log(AllGates[i].transform.position.z);
             if (
             (player.transform.position.x == AllGates[i].transform.position.x
             && (player.transform.position.z - DistanceToLever == AllGates[i].transform.position.z
             || player.transform.position.z + DistanceToLever == AllGates[i].transform.position.z))
             || player.transform.position.z == AllGates[i].transform.position.z
             && (player.transform.position.x - DistanceToLever == AllGates[i].transform.position.x
             || player.transform.position.x + DistanceToLever == AllGates[i].transform.position.x))
                 Debug.Log("YESS");
         }*/
        if (Mathf.Abs(player.transform.position.x - transform.position.x) <= DistanceToLever && Mathf.Abs(player.transform.position.z - transform.position.z) <= DistanceToLever)
        {
            if (ConnectedDoor.transform.rotation.eulerAngles.y == 0)
            {
                if (IsOpen == false && IsWaiting == false)
                {
                    IsWaiting = true;
                    IsOpen = true;
                    StartCoroutine("OpenDoors", 0);
                    
                    //ConnectedDoor.transform.rotation = Quaternion.Euler(0, ConnectedDoor.transform.rotation.eulerAngles.y + Angle, 0);

                }
                else if (IsOpen == true && IsWaiting == false)
                {
                    IsWaiting = true;
                    IsOpen = false;
                    //ConnectedDoor.transform.rotation = Quaternion.Euler(0, ConnectedDoor.transform.rotation.eulerAngles.y - Angle, 0);
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
                    //ConnectedDoor.transform.rotation = Quaternion.Euler(0, ConnectedDoor.transform.rotation.eulerAngles.y + Angle, 0);

                }
                else if (IsOpen == true && IsWaiting == false)
                {
                    IsWaiting = true;
                    IsOpen = false;
                    //ConnectedDoor.transform.rotation = Quaternion.Euler(0, ConnectedDoor.transform.rotation.eulerAngles.y - Angle, 0);
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
                    //ConnectedDoor.transform.rotation = Quaternion.Euler(0, ConnectedDoor.transform.rotation.eulerAngles.y + Angle, 0);

                }
                else if (IsOpen == true && IsWaiting == false)
                {
                    IsWaiting = true;
                    IsOpen = false;
                    //ConnectedDoor.transform.rotation = Quaternion.Euler(0, ConnectedDoor.transform.rotation.eulerAngles.y - Angle, 0);
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
                    //ConnectedDoor.transform.rotation = Quaternion.Euler(0, ConnectedDoor.transform.rotation.eulerAngles.y + Angle, 0);

                }
                else if (IsOpen == true && IsWaiting == false)
                {
                    IsWaiting = true;
                    IsOpen = false;
                    //ConnectedDoor.transform.rotation = Quaternion.Euler(0, ConnectedDoor.transform.rotation.eulerAngles.y - Angle, 0);
                    StartCoroutine("CloseDoors", 3);
                }
            }
        }

      /* if (player.transform.position.x == transform.position.x)
       {
            
            if (player.transform.position.z > transform.position.z)
            {
                //Debug.Log(player.transform.position.z);
                //Debug.Log(transform.position.z);
                if (player.transform.position.z - transform.position.z < DistanceToLever)
                {
                    Debug.Log("YESS1");
                    //ConnectedDoor.transform.rotation = Quaternion.Euler(0, ConnectedDoor.transform.rotation.eulerAngles.y + Angle, 0);
                    if (IsOpen == false)
                    {
                        IsOpen = true;
                       // ConnectedDoor.transform.rotation = Quaternion.Euler(0, ConnectedDoor.transform.rotation.eulerAngles.y + Angle, 0);
                    }
                    else
                    {
                        IsOpen = false;
                       // ConnectedDoor.transform.rotation = Quaternion.Euler(0, ConnectedDoor.transform.rotation.eulerAngles.y - Angle, 0);
                    }
                }
            }
            else
            {
                if (transform.position.z - player.transform.position.z < DistanceToLever)
                {
                    Debug.Log("YESS2");
                    if (IsOpen == false)
                    {
                        IsOpen = true;
                        ConnectedDoor.transform.rotation = Quaternion.Euler(0, ConnectedDoor.transform.rotation.eulerAngles.y + Angle, 0);
                    }
                    else
                    {
                        IsOpen = false;
                        ConnectedDoor.transform.rotation = Quaternion.Euler(0, ConnectedDoor.transform.rotation.eulerAngles.y - Angle, 0);
                    }
                }
            }
       }
       if (player.transform.position.z == transform.position.z)
       {
            // Debug.Log("LOL");
           // Debug.Log(transform.position.z);
            //Debug.Log(player.transform.position.z);
            if (player.transform.position.x > transform.position.x)
            {
                if (player.transform.position.x - transform.position.x < DistanceToLever)
                {
                    Debug.Log("YESS3");
                    //ConnectedDoor.transform.rotation = Quaternion.Euler(0, ConnectedDoor.transform.rotation.eulerAngles.y + Angle, 0);
                    if (IsOpen == false)
                    {
                        IsOpen = true;
                        ConnectedDoor.transform.rotation = Quaternion.Euler(0, ConnectedDoor.transform.rotation.eulerAngles.y + Angle, 0);
                    }
                    else
                    {
                        IsOpen = false;
                        ConnectedDoor.transform.rotation = Quaternion.Euler(0, ConnectedDoor.transform.rotation.eulerAngles.y - Angle, 0);
                    }
                }
            }
            else
            {
                if (transform.position.x - player.transform.position.x < DistanceToLever)
                {
                    Debug.Log("YESS4");
                    //ConnectedDoor.transform.rotation = Quaternion.Euler(0, ConnectedDoor.transform.rotation.eulerAngles.y + Angle, 0);
                    if (IsOpen == false)
                    {
                        IsOpen = true;
                        StartCoroutine("OpenDoors");
                        //ConnectedDoor.transform.rotation = Quaternion.Euler(0, ConnectedDoor.transform.rotation.eulerAngles.y + Angle, 0);
                        
                    }
                    else
                    {
                        IsOpen = false;
                        //ConnectedDoor.transform.rotation = Quaternion.Euler(0, ConnectedDoor.transform.rotation.eulerAngles.y - Angle, 0);
                        StartCoroutine("CloseDoors");
                    }
                }
            }
        }*/
            //Debug.Log("HERE");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
