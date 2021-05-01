using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeFill : MonoBehaviour
{
    //public GameObject smth;
    public Image Button;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Image>().fillAmount = 0;
       //Button = smth.GetComponent<Image>();
        Button.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Button.fillAmount += 0.001f;
        //gameObject.GetComponent<Image>().fillAmount += 0.001f;
    }
}
