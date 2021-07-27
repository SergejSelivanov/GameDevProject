using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArrows : MonoBehaviour
{
    public GameObject Arrows;
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        Arrows.transform.rotation = Quaternion.Euler(0, 0, mainCamera.transform.rotation.eulerAngles.y); //rotate arrows after camera
    }
}
