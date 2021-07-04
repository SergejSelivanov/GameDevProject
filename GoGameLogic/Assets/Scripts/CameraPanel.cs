using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanel : MonoBehaviour
{
    public GameObject ConnectedCamera;
    public float DistanceToPanel = 0.4f;

    private void SwitchArrow()
    {
        if (ConnectedCamera.transform.GetChild(0).GetComponent<CameraEnemy>().IsClockwise == 1)
        {
            FindObjectOfType<AudioManager>().Play("SwitchOff");
            ConnectedCamera.transform.GetChild(7).gameObject.SetActive(false);
            ConnectedCamera.transform.GetChild(6).gameObject.SetActive(true);
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("SwitchOn");
            ConnectedCamera.transform.GetChild(7).gameObject.SetActive(true);
            ConnectedCamera.transform.GetChild(6).gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (Mathf.Abs(player.transform.position.x - transform.position.x) <= DistanceToPanel && Mathf.Abs(player.transform.position.z - transform.position.z) <= DistanceToPanel
        && GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IsWaiting == false)
        {
            /* if (ConnectedCamera.transform.GetChild(0).GetComponent<CameraEnemy>().IsClockwise == 1)
                 FindObjectOfType<AudioManager>().Play("SwitchOff");
             else
                 FindObjectOfType<AudioManager>().Play("SwitchOn");*/
            SwitchArrow();
            ConnectedCamera.transform.GetChild(0).GetComponent<CameraEnemy>().IsClockwise *= -1;
        }
    }
}