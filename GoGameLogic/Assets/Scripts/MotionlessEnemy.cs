using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotionlessEnemy : MonoBehaviour
{
	public GameObject KnifeHandler; //canvas with ThrowKnife script
	private Player PlayerFuncs; // variable with players functions
	private ThrowKnife KnifeFuncs; // variable with ThrowKnife functions
	private bool IsKilling;

	private void OnMouseDown()
    {
		if (Time.timeScale == 1) // to protect multiple killing
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			if (PlayerFuncs.KnifeIsReady == true) //if button has been pressed
			{
				if (KnifeFuncs.CheckIfInRange(gameObject, player)) //if enemy is in range of killing
				{
					Time.timeScale = 0.99f; // to protect multiple killing
					PlayerFuncs.gameObject.GetComponent<Animator>().SetBool("IsTaunting", true); // activate players break-in animation
					PlayerFuncs.StartCoroutine("StopBreaking", gameObject); // rotate player and enemy death
					if (GameObject.FindObjectOfType<FillKnife>() != null)
						GameObject.FindObjectOfType<FillKnife>().GetComponent<Image>().fillAmount = 0;  //unfill button
					PlayerFuncs.IsMovable = true; //player can move now
				}
				PlayerFuncs.KnifeIsReady = false; // player cant break-in more
			}
		}
    }

    void Start()
	{
		PlayerFuncs = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		KnifeFuncs = KnifeHandler.GetComponent<ThrowKnife>();
	}

	void Update()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (Utilities.CheckifPlayerInfrontofEnemy(player, gameObject) && !Utilities.IsThereGate(gameObject.transform) 
		&& !Utilities.IsThereCamera(gameObject.transform) && PlayerFuncs.LightsOffTurns <= 0 && IsKilling == false) //check if nothing interfere
		{
			IsKilling = true;
			gameObject.GetComponent<Animator>().SetBool("IsKilling", true); //activate MotionlessEnemy killing animation
			PlayerFuncs.StartCoroutine("KillingAnimation", gameObject); //player death animation
		}
	}
}
