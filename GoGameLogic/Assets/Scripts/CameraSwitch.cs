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
       // Debug.Log("Aue");
        float StartFOV = StartCamera.GetComponent<Camera>().fieldOfView;
        Quaternion StartRotation = StartCamera.transform.rotation;
        Vector3 StartPosition = StartCamera.transform.position;
        float MainFOV = EndCamera.GetComponent<Camera>().fieldOfView;
        Quaternion MainRotation = EndCamera.transform.rotation;
        Vector3 MainPosition = EndCamera.transform.position;
        float FovDiff = (MainFOV - StartFOV) / 100;
        float RotationDiffX = (StartRotation.eulerAngles.x - MainRotation.eulerAngles.x) / 100;
        float RotationDiffY = (StartRotation.eulerAngles.y - MainRotation.eulerAngles.y) / 100;
        //float RotationDiffY = (StartRotation.eulerAngles.y - MainRotation.eulerAngles.y) / 500;
        //float RotationDiffZ = (StartRotation.eulerAngles.z - MainRotation.eulerAngles.z) / 500;
        //Quaternion RotationDiff = Quaternion.Euler(StartRotation.eulerAngles.x - MainRotation.eulerAngles.x / 500, StartRotation.eulerAngles.y - MainRotation.eulerAngles.y / 500, StartRotation.eulerAngles.z - MainRotation.eulerAngles.z / 500);
        Vector3 PositionDiff = (MainPosition - StartPosition) / 100;
        //Debug.Log((MainFOV - StartFOV) / 100);
        for (int i = 0; i < 100; i++)
        {
            //StartCamera.GetComponent<Camera>().fieldOfView += FovDiff;
            Camera.main.GetComponent<Camera>().fieldOfView += FovDiff;
            //StartCamera.transform.rotation = Quaternion.Euler(StartCamera.transform.rotation.eulerAngles.x - RotationDiffX, 0, 0);
            Camera.main.transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles.x - RotationDiffX, Camera.main.transform.rotation.eulerAngles.y - RotationDiffY, 0);
            Camera.main.transform.position += PositionDiff;
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other);
        //Debug.Log("HUH");
        //Debug.Log(Camera.main.gameObject);
        if (player.transform.rotation.eulerAngles.y == gameObject.transform.rotation.eulerAngles.y || Mathf.Abs(player.transform.rotation.eulerAngles.y - gameObject.transform.rotation.eulerAngles.y) < 0.1f)
        {
            //CameraNow.SetActive(false);
            // CameraToSwap.SetActive(true);
            //StartCoroutine("MoveCamera", Camera.main, CameraToSwap);
            //MoveCamera(Camera.main.gameObject, CameraToSwap);
           // Debug.Log("Yes");
            StartCoroutine(MoveCamera(Camera.main.gameObject, CameraToSwap));
        }
        else
        {
           // Debug.Log("BOCHKA");
            StartCoroutine(MoveCamera(Camera.main.gameObject, CameraToSwapBack));
            //CameraNow.SetActive(true);
            //CameraToSwap.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    /*void Update()
    {

    }*/
}
