using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject CameraToSwap;
    public GameObject CameraToSwapBack;
    private GameObject player;

    IEnumerator MoveCamera(GameObject StartCamera, GameObject EndCamera)
    {
        float StartFOV = StartCamera.GetComponent<Camera>().fieldOfView;
        Quaternion StartRotation = StartCamera.transform.rotation;
        Vector3 StartPosition = StartCamera.transform.position;
        float MainFOV = EndCamera.GetComponent<Camera>().fieldOfView;
        Quaternion MainRotation = EndCamera.transform.rotation;
        Vector3 MainPosition = EndCamera.transform.position;
        float FovDiff = (MainFOV - StartFOV) / 100;
        float RotationDiffX = (StartRotation.eulerAngles.x - MainRotation.eulerAngles.x) / 100;
        float RotationDiffY = (StartRotation.eulerAngles.y - MainRotation.eulerAngles.y) / 100;
        Vector3 PositionDiff = (MainPosition - StartPosition) / 100;
        for (int i = 0; i < 100; i++) //change FOV, rotation and position of camera
        {
            Camera.main.GetComponent<Camera>().fieldOfView += FovDiff;
            Camera.main.transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles.x - RotationDiffX, Camera.main.transform.rotation.eulerAngles.y - RotationDiffY, 0);
            Camera.main.transform.position += PositionDiff;
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
    }

    private void OnTriggerEnter(Collider other) //if touched camera trigger 
    {
        if (player.transform.rotation.eulerAngles.y == gameObject.transform.rotation.eulerAngles.y 
        || Mathf.Abs(player.transform.rotation.eulerAngles.y - gameObject.transform.rotation.eulerAngles.y) < 0.1f) //in case camera trigger is broken in scene
        {
            StartCoroutine(MoveCamera(Camera.main.gameObject, CameraToSwap));
        }
        else
        {
            StartCoroutine(MoveCamera(Camera.main.gameObject, CameraToSwapBack));
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
