using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int MovingClockwise = 1;

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
            //Debug.Log(Hit.collider.gameObject);
            if (Hit.collider.gameObject == player)
                return true;
        }
        return false;
    }

    IEnumerator RotateCamera()
    {
        for (int i = 0; i < 90; i++)
        {
            //gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y + 1, 0);
            gameObject.transform.rotation = Quaternion.Euler(0, gameObject.transform.rotation.eulerAngles.y + 1 * MovingClockwise, 0);
            yield return new WaitForSeconds(0.00444f);
            //yield return new WaitForSeconds(0.0133f);
        }
        gameObject.transform.rotation = Quaternion.Euler(0, Mathf.Round(gameObject.transform.rotation.eulerAngles.y), 0);
        yield return null;
    }

    public void MoveCamera()
    {
        StartCoroutine("RotateCamera");
        //gameObject.transform.rotation = Quaternion.Euler(0, gameObject.transform.rotation.eulerAngles.y + 90, 0);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.rotation.eulerAngles.y % 90 == 0 && CheckIfPlayerInfrontOfCamera())
            //SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
            SceneManager.LoadScene(0);
            //Debug.Log("YOU'RE DEAD");
    }
}
