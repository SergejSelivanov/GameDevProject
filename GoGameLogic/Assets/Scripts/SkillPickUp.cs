using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPickUp : MonoBehaviour
{
    public GameObject mainHero;
    public GameObject Button;
    public float distance;
    float PositionPickUpX;
    float PositionPickUpZ;
    float PositionHeroX;
    float PositionHeroZ;

    void Start()
    {
        PositionPickUpX = transform.position.x;
        PositionPickUpZ = transform.position.z;
    }

    void Update()
    {
        PositionHeroX = mainHero.transform.position.x;
        PositionHeroZ = mainHero.transform.position.z;
        if (Mathf.Sqrt(Mathf.Pow(PositionHeroX - PositionPickUpX, 2) + Mathf.Pow(PositionHeroZ - PositionPickUpZ, 2)) < distance) //if player is near pickup
        {
            Destroy(gameObject);
            Button.SetActive(true);
        }
    }
}
