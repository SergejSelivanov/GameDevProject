using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementLevel6 : MonoBehaviour
{
    public GameObject StartCamera;
    public GameObject MainCamera;
    public float switchTime = 5;

    IEnumerator MoveCamera()
    {
        Quaternion StartRotation = StartCamera.transform.rotation;
        Vector3 StartPosition = StartCamera.transform.position;
        Quaternion MainRotation = MainCamera.transform.rotation;
        Vector3 MainPosition = MainCamera.transform.position;
        /* float RotationDiffX = (StartRotation.eulerAngles.x - MainRotation.eulerAngles.x) / (switchTime * 100);
         float RotationDiffY =  (360 + (MainRotation.eulerAngles.y - StartRotation.eulerAngles.y)) / (switchTime * 100);
         float RotationDiffZ = (StartRotation.eulerAngles.z - MainRotation.eulerAngles.z) / (switchTime * 100);
         Vector3 PositionDiff = (MainPosition - StartPosition) / (switchTime * 100);*/
        float RotationDiffX = (StartRotation.eulerAngles.x - MainRotation.eulerAngles.x) / (switchTime * 100 / 2.5f);
        float RotationDiffY = (360 + (MainRotation.eulerAngles.y - StartRotation.eulerAngles.y)) / (switchTime * 100 / 2.5f);
        float RotationDiffZ = (StartRotation.eulerAngles.z - MainRotation.eulerAngles.z) / (switchTime * 100 / 2.5f);
        Vector3 PositionDiff = (MainPosition - StartPosition) / (switchTime * 100 / 2.5f);
        //for (float i = 0; i < switchTime; i += 0.01f)//changing rotation and position of camera
        for (float i = 0; i < switchTime; i += 0.025f)
        {
            StartCamera.transform.rotation = Quaternion.Euler(StartCamera.transform.rotation.eulerAngles.x - RotationDiffX, StartCamera.transform.rotation.eulerAngles.y + RotationDiffY, StartCamera.transform.rotation.eulerAngles.z - RotationDiffZ);
            StartCamera.transform.position += PositionDiff;
            //yield return new WaitForSeconds(0.01f);
            yield return new WaitForSeconds(0.025f);
        }
        Time.timeScale = 1;
    }

    private void Awake()
    {
        Time.timeScale = 0.99f; //to avoid player move before camera get needed position
    }
    void Start()
    {
        Quaternion StartRotation = StartCamera.transform.rotation;
        Vector3 StartPosition = StartCamera.transform.position;
        Quaternion MainRotation = MainCamera.transform.rotation;
        Vector3 MainPosition = MainCamera.transform.position;
        StartCoroutine("MoveCamera");
    }
}
