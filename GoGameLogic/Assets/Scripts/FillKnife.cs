using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillKnife : MonoBehaviour
{
    IEnumerator FillButton()
    {
        for (int i = 0; i < 340; i++) //fill button by third part
        {
            gameObject.GetComponent<Image>().fillAmount += 0.001f;
            yield return null;
        }
        yield return null;
    }

    void Start()
    {
        gameObject.GetComponent<Button>().interactable = false;
    }

    void Update()
    {
        if (gameObject.GetComponent<Image>().fillAmount == 1) //if buttin is filled it is ready to be used
            gameObject.GetComponent<Button>().interactable = true;
        else
            gameObject.GetComponent<Button>().interactable = false;
    }
}
