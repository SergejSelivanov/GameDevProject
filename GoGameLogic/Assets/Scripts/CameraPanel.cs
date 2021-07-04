using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanel : MonoBehaviour
{
    public GameObject ConnectedCamera;
    public float DistanceToPanel = 0.4f;

    private void OnMouseDown()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (Mathf.Abs(player.transform.position.x - transform.position.x) <= DistanceToPanel && Mathf.Abs(player.transform.position.z - transform.position.z) <= DistanceToPanel
        && GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsWaiting == false)
        {
            //FindObjectOfType<AudioManager>().Play();
            ConnectedCamera.transform.GetChild(0).GetComponent<CameraEnemy>().IsClockwise *= -1;
        }
    }
}