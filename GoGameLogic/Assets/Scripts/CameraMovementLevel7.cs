using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementLevel7 : MonoBehaviour
{
    public GameObject StartCamera;
    public GameObject MainCamera;
    // Start is called before the first frame update

    IEnumerator MoveCamera()
    {
        float StartFOV = StartCamera.GetComponent<Camera>().fieldOfView;
        Quaternion StartRotation = StartCamera.transform.rotation;
        Vector3 StartPosition = StartCamera.transform.position;
        float MainFOV = MainCamera.GetComponent<Camera>().fieldOfView;
        Quaternion MainRotation = MainCamera.transform.rotation;
        Vector3 MainPosition = MainCamera.transform.position;
        float FovDiff = (MainFOV - StartFOV) / 500;
        float RotationDiffX = (StartRotation.eulerAngles.x - MainRotation.eulerAngles.x) / 500;
        float RotationDiffY = (StartRotation.eulerAngles.y - MainRotation.eulerAngles.y) / 500;
        //float RotationDiffY = (StartRotation.eulerAngles.y - MainRotation.eulerAngles.y) / 500;
        //float RotationDiffZ = (StartRotation.eulerAngles.z - MainRotation.eulerAngles.z) / 500;
        //Quaternion RotationDiff = Quaternion.Euler(StartRotation.eulerAngles.x - MainRotation.eulerAngles.x / 500, StartRotation.eulerAngles.y - MainRotation.eulerAngles.y / 500, StartRotation.eulerAngles.z - MainRotation.eulerAngles.z / 500);
        Vector3 PositionDiff = (MainPosition - StartPosition) / 500;
        for (float i = 0; i < 5; i += 0.01f)
        {
            /* StartCamera.GetComponent<Camera>().fieldOfView += Mathf.Abs(MainFOV - StartFOV) / 500;
             StartCamera.transform.rotation = Quaternion.Euler(StartCamera.transform.rotation.eulerAngles.x - Mathf.Abs(MainRotation.x - StartRotation.x) / 500, 0, 0);
             StartPosition += MainPosition / 500;*/
            StartCamera.GetComponent<Camera>().fieldOfView += FovDiff;
            //StartCamera.transform.rotation = Quaternion.Euler(StartCamera.transform.rotation.eulerAngles.x - RotationDiffX, 0, 0);
            StartCamera.transform.rotation = Quaternion.Euler(StartCamera.transform.rotation.eulerAngles.x - RotationDiffX, StartCamera.transform.rotation.eulerAngles.y - RotationDiffY, 0);
            StartCamera.transform.position += PositionDiff;
            yield return new WaitForSeconds(0.01f);
        }
        Time.timeScale = 1;
    }

    private void Awake()
    {
        Time.timeScale = 0.99f;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
