using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamReplaceScript : MonoBehaviour
{
    public GameObject mainHero;
    public GameObject Cam1;
    public GameObject Cam2;
    public float distance;
    float PositionPickUpX;
    float PositionPickUpY;
    float PositionPickUpZ;

    float PositionHeroX;
    float PositionHeroY;
    float PositionHeroZ;

    private bool CamRep = true;

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

        if ((PositionPickUpZ - PositionHeroZ) < 0 && CamRep == false)
        {
            CamRep = true;
            Cam1.active = false;
            Cam2.active = true;
        }
        else if ((PositionPickUpZ - PositionHeroZ) >= 0 && CamRep == true)
        {
            CamRep = false;
            Cam1.active = true;
            Cam2.active = false;
        }
    }
}
