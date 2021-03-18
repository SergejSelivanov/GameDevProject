using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityButton : MonoBehaviour
{
    public GameObject PlayerHandler;
    public GameObject ButtonUI;
    private Player PlayerFuncs;
    public int StepsInInvisibilty = 2;

    public void GetInvisible()
    {
        if (PlayerFuncs.ProjectionActive == false)
        {
            PlayerFuncs.SkillSetter = 0;
            ButtonUI.SetActive(false);
            PlayerFuncs.Invisible = StepsInInvisibilty;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerFuncs = PlayerHandler.GetComponent<Player>();
        ButtonUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerFuncs.SkillSetter == 1)
            ButtonUI.SetActive(true);
        else
            ButtonUI.SetActive(false);
    }
}
