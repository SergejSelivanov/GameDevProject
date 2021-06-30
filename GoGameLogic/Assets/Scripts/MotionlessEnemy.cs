using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotionlessEnemy : MonoBehaviour
{
	public GameObject KnifeHandler;
	private Player PlayerFuncs;
	private ThrowKnife KnifeFuncs;

	private void OnMouseDown()
    {
		if (Time.timeScale == 1)
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			if (PlayerFuncs.KnifeIsReady == true)
			{
				if (KnifeFuncs.CheckIfInRange(gameObject, player))
				{
					Time.timeScale = 0.99f;
					PlayerFuncs.gameObject.GetComponent<Animator>().SetBool("IsTaunting", true);
					PlayerFuncs.StartCoroutine("StopBreaking", gameObject);
					if (GameObject.FindObjectOfType<FillKnife>() != null)
						GameObject.FindObjectOfType<FillKnife>().GetComponent<Image>().fillAmount = 0; 
					PlayerFuncs.IsMovable = true;
				}
				PlayerFuncs.KnifeIsReady = false;
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
		if (Utilities.CheckifPlayerInfrontofEnemy(player, gameObject) && !Utilities.IsThereGate(gameObject.transform) && !Utilities.IsThereCamera(gameObject.transform) && PlayerFuncs.LightsOffTurns <= 0)
		{
			gameObject.GetComponent<Animator>().SetBool("IsKilling", true);
			PlayerFuncs.StartCoroutine("KillingAnimation", gameObject);
		}
	}
}
