using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraEnemy : MonoBehaviour
{
    public int MovingClockwise = 1;
    private bool PlayerFound = false;
    private GameObject player;
    public int IsClockwise
    {
        get
        {
            return MovingClockwise;
        }
        set
        {
            MovingClockwise = value;
        }
    }


    private bool CheckIfPlayerInfrontOfCamera()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        RaycastHit Hit;
        Ray ray = new Ray(gameObject.transform.position, gameObject.transform.forward);
        Physics.Raycast(ray, out Hit, 1);
        if (Hit.collider != null)
        {
            if (Hit.collider.gameObject == player)
                return true;
        }
        return false;
    }

    IEnumerator RotateCamera()
    {
        for (int i = 0; i < 90; i++)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, gameObject.transform.rotation.eulerAngles.y + 1 * MovingClockwise, 0);
            yield return new WaitForSeconds(0.00444f);
        }
        gameObject.transform.rotation = Quaternion.Euler(0, Mathf.Round(gameObject.transform.rotation.eulerAngles.y), 0);
        yield return null;
    }

    public void MoveCamera()
    {
        StartCoroutine("RotateCamera");
    }

    IEnumerator redAlert() //animation of player being caught
    {
        FindObjectOfType<AudioManager>().Play("CameraAlarm");
        FindObjectOfType<Canvas>().transform.Find("RedAlert").gameObject.SetActive(true);
        for (int i = 0; i < 100; i++) //screen become red 
        {
            FindObjectOfType<Canvas>().transform.Find("RedAlert").GetComponent<Image>().color = new Color(255,0,0, FindObjectOfType<Canvas>().transform.Find("RedAlert").GetComponent<Image>().color.a + 0.0065f);
            yield return new WaitForSeconds(0.008f);
        }
        for (int i = 0; i < 100; i++) //transparent again
        {
            FindObjectOfType<Canvas>().transform.Find("RedAlert").GetComponent<Image>().color = new Color(255, 0, 0, FindObjectOfType<Canvas>().transform.Find("RedAlert").GetComponent<Image>().color.a - 0.0065f);
            yield return new WaitForSeconds(0.008f);
        }
        for (int i = 0; i < 100; i++)//red again
        {
            FindObjectOfType<Canvas>().transform.Find("RedAlert").GetComponent<Image>().color = new Color(255, 0, 0, FindObjectOfType<Canvas>().transform.Find("RedAlert").GetComponent<Image>().color.a + 0.0065f);
            yield return new WaitForSeconds(0.008f);
        }
        //player.GetComponent<Player>().AdCheckAndShow();
        FindObjectOfType<LevelLoader>().LoadSameLevel();
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (gameObject.transform.rotation.eulerAngles.y % 90 == 0 && CheckIfPlayerInfrontOfCamera() && PlayerFound == false) //if player got caught
        {
            PlayerFound = true;
            StartCoroutine("redAlert");
        }
    }
}
