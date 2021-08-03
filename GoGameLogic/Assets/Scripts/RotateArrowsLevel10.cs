using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArrowsLevel10 : MonoBehaviour
{
    public GameObject arrows;
    private void OnTriggerEnter(Collider other)
    {
        arrows.transform.rotation = Quaternion.Euler(0, 0, -90);
    }
}
