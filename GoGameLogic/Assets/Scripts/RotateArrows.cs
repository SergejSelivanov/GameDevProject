using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArrows : MonoBehaviour
{
    public GameObject Arrows;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Arrows.transform.rotation = Quaternion.Euler(0, 0, mainCamera.transform.rotation.eulerAngles.y);
    }
}
