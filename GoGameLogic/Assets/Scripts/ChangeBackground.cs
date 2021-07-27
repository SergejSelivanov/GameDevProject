using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour
{
    float Timer = 0;
    int index = 0;
    public Sprite[] Images;

    private void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= 10f) //every ten seconds change background image
        {
            Timer = 0;
            index++;
            if (index >= Images.Length)
                index = 0;
            gameObject.GetComponent<Image>().sprite = Images[index];
        }
    }
}
