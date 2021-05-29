using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoor : MonoBehaviour
{
    private bool IsOpened = false;

    // Start is called before the first frame update
   /* void Start()
    {
        
    }*/

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.Find("FirstPart").gameObject.activeInHierarchy == false && transform.Find("SecondPart").gameObject.activeInHierarchy == false && IsOpened == false)
        {
            IsOpened = true;
            transform.GetChild(2).position -= new Vector3(0, 100, 0);
        }
        if ((transform.Find("FirstPart").gameObject.activeInHierarchy == true || transform.Find("SecondPart").gameObject.activeInHierarchy == true) && IsOpened == true)
        {
            IsOpened = false;
            transform.GetChild(2).position += new Vector3(0, 100, 0);
        }
    }
}
