using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionlessEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    private bool CheckIfFacing(GameObject player)
    {
        if (gameObject.transform.rotation.eulerAngles.y == 0 
        && player.transform.rotation.eulerAngles.y == gameObject.transform.rotation.y + 180)
            return true;
        if (gameObject.transform.rotation.eulerAngles.y == 90 
        && player.transform.rotation.eulerAngles.y - 180 == gameObject.transform.rotation.eulerAngles.y)
            return true;
        if (gameObject.transform.rotation.eulerAngles.y == 180 
        && player.transform.rotation.eulerAngles.y + 180 == gameObject.transform.rotation.eulerAngles.y)
            return true;
        if (gameObject.transform.rotation.eulerAngles.y == 270 
        && player.transform.rotation.eulerAngles.y == gameObject.transform.rotation.eulerAngles.y - 180)
            return true;
        return false;
    }

    /*void Start()
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
        //Transform PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (transform.position.x == player.transform.position.x
        && transform.position.z == player.transform.position.z
        && !CheckIfFacing(player))
            Destroy(gameObject);
    }
}
