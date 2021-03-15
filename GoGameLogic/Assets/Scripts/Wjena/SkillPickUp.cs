using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPickUp : MonoBehaviour
{
    public GameObject mainHero;
    public GameObject Button;
    public float distance;
    float PositionPickUpX;
    float PositionPickUpY;
    float PositionPickUpZ;

    float PositionHeroX;
    float PositionHeroY;
    float PositionHeroZ;

    void Start()
    {
        PositionPickUpX = transform.position.x;
        PositionPickUpY = transform.position.y;
        PositionPickUpZ = transform.position.z;
    }

    void Update()
    {
        PositionHeroX = mainHero.transform.position.x;
        PositionHeroY = mainHero.transform.position.y;
        PositionHeroZ = mainHero.transform.position.z;

        if (Mathf.Sqrt(Mathf.Pow(PositionHeroX - PositionPickUpX, 2) + Mathf.Pow(PositionHeroZ - PositionPickUpZ, 2)) < distance)
        {
            Destroy(gameObject);
            Button.SetActive(true);
        }
    }
}
