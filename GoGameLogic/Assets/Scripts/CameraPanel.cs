using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanel : MonoBehaviour
{
    public GameObject ConnectedCamera;
    public float DistanceToPanel = 0.4f;



    private void OnMouseDown()
    {
        Debug.Log("aaa");
        GameObject player;
        player = GameObject.FindGameObjectWithTag("Projection");
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        if (Mathf.Abs(player.transform.position.x - transform.position.x) <= DistanceToPanel && Mathf.Abs(player.transform.position.z - transform.position.z) <= DistanceToPanel && GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Waiting == false)
        {
            /*if (IsOpen == false && IsWaiting == false)
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
            }*/
            ConnectedCamera.transform.GetChild(0).GetComponent<CameraEnemy>().IsClockwise *= -1;
        }
    }

    // Start is called before the first frame update
    /*void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }*/
}