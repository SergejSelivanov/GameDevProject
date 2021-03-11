using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private float DistanceToLever = 0.25f;
    public GameObject ConnectedDoor;
    public int Angle = 90;
    private bool IsOpen = false;

    /*private GameObject FindConnectedDoor()
    {
        RaycastHit Hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out Hit);
        if (Hit.collider != null)
            return Hit.collider.gameObject;
        return null;
    }*/

    private void OnMouseDown()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
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
       if (player.transform.position.x == transform.position.x)
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
