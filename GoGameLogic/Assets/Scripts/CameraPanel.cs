using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanel : MonoBehaviour
{
    public GameObject ConnectedCamera;

    private void SwitchArrow()
    {
        if (ConnectedCamera.transform.GetChild(0).GetComponent<CameraEnemy>().IsClockwise == 1)
        {
            FindObjectOfType<AudioManager>().Play("SwitchOff");
            ConnectedCamera.transform.GetChild(0).GetChild(8).gameObject.SetActive(false); //set off clockwise arrow
            ConnectedCamera.transform.GetChild(0).GetChild(7).gameObject.SetActive(true);  //and turn on counter clockwise
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("SwitchOn");
            ConnectedCamera.transform.GetChild(0).GetChild(8).gameObject.SetActive(true);
            ConnectedCamera.transform.GetChild(0).GetChild(7).gameObject.SetActive(false);
        }
    }

    public void ChangeState() //camera starts rotate in different destination and arrows are changed
    {
        SwitchArrow();
        ConnectedCamera.transform.GetChild(0).GetComponent<CameraEnemy>().IsClockwise *= -1;
    }
}