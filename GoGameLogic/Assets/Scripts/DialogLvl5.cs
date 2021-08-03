using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogLvl5 : MonoBehaviour
{
    public GameObject Dialog;
    private SkillPickUp pickup;

    private void OpenDialog()
    {
        Dialog.SetActive(true);
    }

    private void FixedUpdate()
    {
        pickup = FindObjectOfType<SkillPickUp>();
        if (pickup == null)
        {
            OpenDialog();
            Destroy(this);
        }
    }
}
