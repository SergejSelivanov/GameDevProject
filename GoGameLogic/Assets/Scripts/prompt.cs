using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prompt : MonoBehaviour
{
    public GameObject dialog;

    public void ShowPrompt()
    {
        dialog.SetActive(true);
    }
}
