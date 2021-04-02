using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    GameObject player;
    public int Level = 0;

    private void OnMouseDown()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) <= 2 && Mathf.Abs(transform.position.z - player.transform.position.z) <= 2)
            SceneManager.LoadScene(Level);
        /* if ((transform.position.x - player.transform.position.x <= 2
         || player.transform.position.x - transform.position.x <= 2)
         && (transform.position.z - player.transform.position.z <= 2
         && player.transform.position.z - transform.position.z <= 2))*/
        /*if (player.transform.position.x > transform.position.x)
        {
             if (player.transform.position.z >= transform.position.z)
             {
                 if (player.transform.position.z - transform.position.z <= 2)
                     SceneManager.LoadScene(0);
             }
             else
             {
                 if (transform.position.z - player.transform.position.z <= 2)
                     SceneManager.LoadScene(0);
             }
        }
        else
        {
             if (player.transform.position.z >= transform.position.z)
             {
                 if (player.transform.position.z - transform.position.z <= 2)
                     SceneManager.LoadScene(0);
             }
             else
             {
                 if (transform.position.z - player.transform.position.z <= 2)
                     SceneManager.LoadScene(0);
             }
         }*/
        // SceneManager.LoadScene(0);
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
