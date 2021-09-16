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
    public GameObject car;
    public GameObject statueLight;
    private AudioManager audioManager;
    private bool DialogHasStarted;
    private int qualLevel;

    IEnumerator MoveSecond()
    {
        audioManager.Play("Stun");
        yield return new WaitForSeconds(1f);
        statueLight.SetActive(true);
        //audioManager.Play("Stun");
        //audioManager.Play("Stun1");
        yield return new WaitForSeconds(2f);
        statueLight.SetActive(false);
        yield return new WaitForSeconds(1f);
        statueLight.SetActive(true);
       // audioManager.Play("Stun2");
        yield return new WaitForSeconds(1.5f);
        statueLight.SetActive(false);
        yield return new WaitForSeconds(1.5f);


        //yield return new WaitForSeconds(7f);
        sixthCamera.SetActive(false);
        seventhCamera.SetActive(true);
        car.GetComponent<Animator>().SetTrigger("IsFlying");
        audioManager.Play("Car");
        yield return new WaitForSeconds(6f);
        QualitySettings.SetQualityLevel(qualLevel);
        SceneManager.LoadScene(0);
    }


    IEnumerator MoveFirstCamera()
    {
        audioManager.Play("City");
        float StartFOV = firstCamera.GetComponent<Camera>().fieldOfView;
        Quaternion StartRotation = firstCamera.transform.rotation;
        Vector3 StartPosition = firstCamera.transform.position;
        float MainFOV = secondCamera.GetComponent<Camera>().fieldOfView;
        Quaternion MainRotation = secondCamera.transform.rotation;
        Vector3 MainPosition = secondCamera.transform.position;
        float FovDiff = (MainFOV - StartFOV) / 900 * 3;
        float RotationDiffX = (StartRotation.eulerAngles.x - MainRotation.eulerAngles.x) / 900 * 3;
        float RotationDiffY = (StartRotation.eulerAngles.y - MainRotation.eulerAngles.y) / 900 * 3;
        Vector3 PositionDiff = (MainPosition - StartPosition) / 900 * 3;
        //for (float i = 0; i < 9; i += 0.01f)//changing FOV, rotation and position of camera
        for (float i = 0; i < 9; i += 0.03f)//changing FOV, rotation and position of camera
        {
            firstCamera.GetComponent<Camera>().fieldOfView += FovDiff; //changing FOV
            firstCamera.transform.rotation = Quaternion.Euler(firstCamera.transform.rotation.eulerAngles.x - RotationDiffX, firstCamera.transform.rotation.eulerAngles.y - RotationDiffY, 0);
            firstCamera.transform.position += PositionDiff;
            //yield return new WaitForSeconds(0.01f);
            yield return new WaitForSeconds(0.03f);
        }
        audioManager.Play("Room");
        /*for (float i = 0; i < 1; i += 0.01f)//changing FOV, rotation and position of camera
        {
            firstCamera.GetComponent<Camera>().fieldOfView += FovDiff; //changing FOV
            firstCamera.transform.rotation = Quaternion.Euler(firstCamera.transform.rotation.eulerAngles.x - RotationDiffX, firstCamera.transform.rotation.eulerAngles.y - RotationDiffY, 0);
            firstCamera.transform.position += PositionDiff;
            yield return new WaitForSeconds(0.01f);
        }*/


        firstCamera.SetActive(false);
        thirdCamera.SetActive(true);
        //yield return new WaitForSeconds(7.5f);
        StartFOV = thirdCamera.GetComponent<Camera>().fieldOfView;
        StartRotation = thirdCamera.transform.rotation;
        StartPosition = thirdCamera.transform.position;
        MainFOV = fourthCamera.GetComponent<Camera>().fieldOfView;
        MainRotation = fourthCamera.transform.rotation;
        MainPosition = fourthCamera.transform.position;
        FovDiff = (MainFOV - StartFOV) / 740 * 3;
        RotationDiffX = (StartRotation.eulerAngles.x - MainRotation.eulerAngles.x) / 740 * 3;
        RotationDiffY = (StartRotation.eulerAngles.y - MainRotation.eulerAngles.y) / 740 * 3;
        PositionDiff = (MainPosition - StartPosition) / 740 * 3;
        //for (float i = 0; i < 7.4; i += 0.01f)//changing FOV, rotation and position of camera
        for (float i = 0; i < 7.4; i += 0.03f)//changing FOV, rotation and position of camera
        {
            thirdCamera.GetComponent<Camera>().fieldOfView += FovDiff; //changing FOV
            thirdCamera.transform.rotation = Quaternion.Euler(thirdCamera.transform.rotation.eulerAngles.x - RotationDiffX, thirdCamera.transform.rotation.eulerAngles.y - RotationDiffY, 0);
            thirdCamera.transform.position += PositionDiff;
            //yield return new WaitForSeconds(0.01f);
            yield return new WaitForSeconds(0.03f);
        }
        thirdCamera.SetActive(false);
        fifthCamera.SetActive(true);
        dialog.SetActive(true);
        DialogHasStarted = true;
        yield return null;
    }

    private void Awake()
    {
        qualLevel = QualitySettings.GetQualityLevel();
        QualitySettings.SetQualityLevel(2);
    }

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
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
