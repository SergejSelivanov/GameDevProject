using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalCutscene : MonoBehaviour
{
    public GameObject firstCamera;
    public GameObject secondCamera;
    public GameObject thirdCamera;
    public GameObject fourthCamera;
    public GameObject fifthCamera;
    public GameObject sixthCamera;
    public GameObject seventhCamera;
    public GameObject dialog;
    private bool DialogHasStarted;

    IEnumerator MoveSecond()
    {
       yield return new WaitForSeconds(7f);
        sixthCamera.SetActive(false);
        seventhCamera.SetActive(true);
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(0);
    }


    IEnumerator MoveFirstCamera()
    {
        float StartFOV = firstCamera.GetComponent<Camera>().fieldOfView;
        Quaternion StartRotation = firstCamera.transform.rotation;
        Vector3 StartPosition = firstCamera.transform.position;
        float MainFOV = secondCamera.GetComponent<Camera>().fieldOfView;
        Quaternion MainRotation = secondCamera.transform.rotation;
        Vector3 MainPosition = secondCamera.transform.position;
        float FovDiff = (MainFOV - StartFOV) / 900;
        float RotationDiffX = (StartRotation.eulerAngles.x - MainRotation.eulerAngles.x) / 900;
        float RotationDiffY = (StartRotation.eulerAngles.y - MainRotation.eulerAngles.y) / 900;
        Vector3 PositionDiff = (MainPosition - StartPosition) / 900;
        for (float i = 0; i < 9; i += 0.01f)//changing FOV, rotation and position of camera
        {
            firstCamera.GetComponent<Camera>().fieldOfView += FovDiff; //changing FOV
            firstCamera.transform.rotation = Quaternion.Euler(firstCamera.transform.rotation.eulerAngles.x - RotationDiffX, firstCamera.transform.rotation.eulerAngles.y - RotationDiffY, 0);
            firstCamera.transform.position += PositionDiff;
            yield return new WaitForSeconds(0.01f);
        }
        firstCamera.SetActive(false);
        thirdCamera.SetActive(true);
        //yield return new WaitForSeconds(7.5f);
        StartFOV = thirdCamera.GetComponent<Camera>().fieldOfView;
        StartRotation = thirdCamera.transform.rotation;
        StartPosition = thirdCamera.transform.position;
        MainFOV = fourthCamera.GetComponent<Camera>().fieldOfView;
        MainRotation = fourthCamera.transform.rotation;
        MainPosition = fourthCamera.transform.position;
        FovDiff = (MainFOV - StartFOV) / 740;
        RotationDiffX = (StartRotation.eulerAngles.x - MainRotation.eulerAngles.x) / 740;
        RotationDiffY = (StartRotation.eulerAngles.y - MainRotation.eulerAngles.y) / 740;
        PositionDiff = (MainPosition - StartPosition) / 740;
        for (float i = 0; i < 7.4; i += 0.01f)//changing FOV, rotation and position of camera
        {
            thirdCamera.GetComponent<Camera>().fieldOfView += FovDiff; //changing FOV
            thirdCamera.transform.rotation = Quaternion.Euler(thirdCamera.transform.rotation.eulerAngles.x - RotationDiffX, thirdCamera.transform.rotation.eulerAngles.y - RotationDiffY, 0);
            thirdCamera.transform.position += PositionDiff;
            yield return new WaitForSeconds(0.01f);
        }
        thirdCamera.SetActive(false);
        fifthCamera.SetActive(true);
        dialog.SetActive(true);
        DialogHasStarted = true;
        yield return null;
    }

    void Start()
    {
        StartCoroutine(MoveFirstCamera());
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogHasStarted == true && dialog.activeSelf == false)
        {
            DialogHasStarted = false;
            fifthCamera.SetActive(false);
            sixthCamera.SetActive(true);
            StartCoroutine(MoveSecond());
        }

    }
}
