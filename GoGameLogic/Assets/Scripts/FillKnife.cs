using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillKnife : MonoBehaviour
{

    IEnumerator FillButton()
    {
        for (int i = 0; i < 340; i++)
        {
          //  Debug.Log("a");
            gameObject.GetComponent<Image>().fillAmount += 0.001f;
            yield return null;
        }
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.GetComponent<Image>().fillAmount += 0.001f;
        if (gameObject.GetComponent<Image>().fillAmount == 1)
        {
           // Debug.Log("aa");
            gameObject.GetComponent<Button>().interactable = true;
        }
        else
            gameObject.GetComponent<Button>().interactable = false;
    }
}
