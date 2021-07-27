using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamReplaceScript : MonoBehaviour
{
    public GameObject mainHero;
    public GameObject Cam1;
    public GameObject Cam2;
    public float distance;
    float PositionPickUpZ;
    float PositionHeroZ;

    private bool CamRep = true;

    void Start()
    {
        PositionPickUpZ = transform.position.z;
    }

    void Update()
    {
        PositionHeroZ = mainHero.transform.position.z;
        if ((PositionPickUpZ - PositionHeroZ) < 0 && CamRep == false) //if player is near camera trigger
        {
            CamRep = true;
            Cam1.SetActive(false);
            Cam2.SetActive(true);
        }
        else if ((PositionPickUpZ - PositionHeroZ) >= 0 && CamRep == true)
        {
            CamRep = false;
            Cam1.SetActive(true);
            Cam2.SetActive(false);
        }
    }
}
