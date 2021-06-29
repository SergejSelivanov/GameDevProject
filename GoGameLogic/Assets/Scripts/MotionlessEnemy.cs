using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotionlessEnemy : MonoBehaviour
{
	public GameObject PlayerHandler;
	public GameObject KnifeHandler;
	private Player PlayerFuncs;
	private ThrowKnife KnifeFuncs;
	
	IEnumerator StopBreaking()
    {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		int RequiredAngle = 0;
		if (player.transform.position.x > gameObject.transform.position.x)
			RequiredAngle = 270;
		else if (player.transform.position.x < gameObject.transform.position.x)
			RequiredAngle = 90;
		else if (player.transform.position.z > gameObject.transform.position.z)
			RequiredAngle = 180;
		else
			RequiredAngle = 0;
		int playerangle = (int)player.transform.rotation.eulerAngles.y;
		int Diff = RequiredAngle - playerangle;
		if (Diff == 270 || Diff == -270)
			Diff = -Diff % 180;
		if (Diff != 0)
		{
			player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 1);
			for (int i = 0; i < 30; i++)
			{
				player.transform.rotation = Quaternion.Euler(0, (int)player.transform.rotation.eulerAngles.y + Diff / 30, 0);
				yield return new WaitForSeconds(0.0133f);
			}
			player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
			player.transform.rotation = Quaternion.Euler(0, RequiredAngle, 0);
		}
		yield return new WaitForSeconds(1);
		for (int i = 0; i < PlayerFuncs.EnemiesKill.Length; i++)
		{
			if (PlayerFuncs.EnemiesKill[i] == null)
			{
				PlayerFuncs.EnemiesKill[i] = gameObject;
				break;
			}
		}
		gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("IsDead", true);
		yield return new WaitForSeconds(0.5f);
		Time.timeScale = 0.6f;
		PlayerHandler.GetComponent<Animator>().SetBool("IsTaunting", false);
		yield return new WaitForSeconds(0.5f);
		Time.timeScale = 1;
		yield return null;
	}

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
					StartCoroutine("StopBreaking");
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
		PlayerFuncs = PlayerHandler.GetComponent<Player>();
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
