using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementLevel7 : MonoBehaviour
{
    public GameObject StartCamera;
    public GameObject MainCamera;
    public float switchTime = 5;

    IEnumerator MoveCamera()
    {
        float StartFOV = StartCamera.GetComponent<Camera>().fieldOfView;
        Quaternion StartRotation = StartCamera.transform.rotation;
        Vector3 StartPosition = StartCamera.transform.position;
        float MainFOV = MainCamera.GetComponent<Camera>().fieldOfView;
        Quaternion MainRotation = MainCamera.transform.rotation;
        Vector3 MainPosition = MainCamera.transform.position;
        float FovDiff = (MainFOV - StartFOV) / (switchTime * 100);
        float RotationDiffX = (StartRotation.eulerAngles.x - MainRotation.eulerAngles.x) / (switchTime * 100);
        float RotationDiffY = (StartRotation.eulerAngles.y - MainRotation.eulerAngles.y) / (switchTime * 100);
        Vector3 PositionDiff = (MainPosition - StartPosition) / (switchTime * 100);
        for (float i = 0; i < switchTime; i += 0.01f)//changing FOV, rotation and position of camera
        {
            StartCamera.GetComponent<Camera>().fieldOfView += FovDiff; //changing FOV
            StartCamera.transform.rotation = Quaternion.Euler(StartCamera.transform.rotation.eulerAngles.x - RotationDiffX, StartCamera.transform.rotation.eulerAngles.y - RotationDiffY, 0);
            StartCamera.transform.position += PositionDiff;
            yield return new WaitForSeconds(0.01f);
        }
        Time.timeScale = 1;
    }

    private void Awake()
    {
        Time.timeScale = 0.99f; //to avoid player move before camera get needed position
    }
    void Start()
    {
        float StartFOV = StartCamera.GetComponent<Camera>().fieldOfView;
        Quaternion StartRotation = StartCamera.transform.rotation;
        Vector3 StartPosition = StartCamera.transform.position;
        float MainFov = MainCamera.GetComponent<Camera>().fieldOfView;
        Quaternion MainRotation = MainCamera.transform.rotation;
        Vector3 MainPosition = MainCamera.transform.position;
        StartCoroutine("MoveCamera");
    }
}
