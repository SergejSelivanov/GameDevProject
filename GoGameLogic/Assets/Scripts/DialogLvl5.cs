using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogLvl5 : MonoBehaviour
{
    public GameObject Dialog;
    public GameObject fx;
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
            fx.GetComponent<ParticleSystem>().Stop();
            Destroy(this);
        }
    }
}
