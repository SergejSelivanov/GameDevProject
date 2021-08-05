using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArrowsLevel10 : MonoBehaviour
{
    public GameObject arrows;
    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (player.transform.rotation.eulerAngles.y == gameObject.transform.rotation.eulerAngles.y
        || Mathf.Abs(player.transform.rotation.eulerAngles.y - gameObject.transform.rotation.eulerAngles.y) < 0.1f)
            arrows.transform.rotation = Quaternion.Euler(0, 0, -90);
        else
            arrows.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
