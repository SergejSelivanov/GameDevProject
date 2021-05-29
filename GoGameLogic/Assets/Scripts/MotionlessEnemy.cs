﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotionlessEnemy : MonoBehaviour
{
	public GameObject VerLineHandler;
	public GameObject HorLineHandler;
	public GameObject PlayerHandler;
	public GameObject KnifeHandler;
	private VerticalLine VerLineFuncs;
	private HorizontalLine HorLineFuncs;
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


		//int RequiredAngle = LineMovingEnemyFuncs.Opposite(ObjectToRotate);
		int playerangle = (int)player.transform.rotation.eulerAngles.y;
		//string EnemyTag = "MotionlessEnemy";
		if (playerangle == 0)
		{
			if (RequiredAngle == 0)
			{
				yield return null;
			}
			if (RequiredAngle == 90)
			{
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 1);
				for (int i = 0; i < 30; i++)
				{
					player.transform.rotation = Quaternion.Euler(0, (int)player.transform.rotation.eulerAngles.y + 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
				player.transform.rotation = Quaternion.Euler(0, 90, 0);
				yield return null;
			}
			if (RequiredAngle == 180)
			{
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 3);
				for (int i = 0; i < 30; i++)
				{
					player.transform.rotation = Quaternion.Euler(0, (int)player.transform.rotation.eulerAngles.y + 6, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
				player.transform.rotation = Quaternion.Euler(0, 180, 0);
				yield return null;
			}
			if (RequiredAngle == 270)
			{
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 2);
				for (int i = 0; i < 30; i++)
				{
					player.transform.rotation = Quaternion.Euler(0, (int)player.transform.rotation.eulerAngles.y - 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 2);
				player.transform.rotation = Quaternion.Euler(0, 270, 0);
				yield return null;
			}
		}
		if (playerangle == 90)
		{
			if (RequiredAngle == 90)
			{
				yield return null;
			}
			if (RequiredAngle == 0)
			{
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 2);
				for (int i = 0; i < 30; i++)
				{
					player.transform.rotation = Quaternion.Euler(0, (int)player.transform.rotation.eulerAngles.y - 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
				player.transform.rotation = Quaternion.Euler(0, 0, 0);
				yield return null;
			}
			if (RequiredAngle == 180)
			{
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 1);
				for (int i = 0; i < 30; i++)
				{
					player.transform.rotation = Quaternion.Euler(0, (int)player.transform.rotation.eulerAngles.y + 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
				player.transform.rotation = Quaternion.Euler(0, 180, 0);
				yield return null;
			}
			if (RequiredAngle == 270)
			{
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 3);
				for (int i = 0; i < 30; i++)
				{
					player.transform.rotation = Quaternion.Euler(0, (int)player.transform.rotation.eulerAngles.y - 6, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
				player.transform.rotation = Quaternion.Euler(0, 270, 0);
				yield return null;
			}
		}
		if (playerangle == 180)
		{
			if (RequiredAngle == 180)
			{
				yield return null;
			}
			if (RequiredAngle == 0)
			{
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 3);
				for (int i = 0; i < 30; i++)
				{
					player.transform.rotation = Quaternion.Euler(0, (int)player.transform.rotation.eulerAngles.y + 6, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
				player.transform.rotation = Quaternion.Euler(0, 0, 0);
				yield return null;
			}
			if (RequiredAngle == 90)
			{
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 2);
				for (int i = 0; i < 30; i++)
				{
					player.transform.rotation = Quaternion.Euler(0, (int)player.transform.rotation.eulerAngles.y - 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
				player.transform.rotation = Quaternion.Euler(0, 90, 0);
				yield return null;
			}
			if (RequiredAngle == 270)
			{
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 1);
				for (int i = 0; i < 30; i++)
				{
					player.transform.rotation = Quaternion.Euler(0, (int)player.transform.rotation.eulerAngles.y + 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
				player.transform.rotation = Quaternion.Euler(0, 270, 0);
				yield return null;
			}
		}
		if (playerangle == 270)
		{
			if (RequiredAngle == 270)
			{
				yield return null;
			}
			if (RequiredAngle == 0)
			{
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 1);
				for (int i = 0; i < 30; i++)
				{
					player.transform.rotation = Quaternion.Euler(0, (int)player.transform.rotation.eulerAngles.y + 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
				player.transform.rotation = Quaternion.Euler(0, 0, 0);
				yield return null;
			}
			if (RequiredAngle == 90)
			{
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 3);
				for (int i = 0; i < 30; i++)
				{
					player.transform.rotation = Quaternion.Euler(0, (int)player.transform.rotation.eulerAngles.y - 6, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
				player.transform.rotation = Quaternion.Euler(0, 90, 0);
				yield return null;
			}
			if (RequiredAngle == 180)
			{
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 2);
				for (int i = 0; i < 30; i++)
				{
					player.transform.rotation = Quaternion.Euler(0, (int)player.transform.rotation.eulerAngles.y - 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
				player.transform.rotation = Quaternion.Euler(0, 180, 0);
				yield return null;
			}
		}
		//yield return null;





		yield return new WaitForSeconds(1);
		for (int i = 0; i < PlayerFuncs.EnemiesKill.Length; i++)
		{
			if (PlayerFuncs.EnemiesKill[i] == null)
			{
				PlayerFuncs.EnemiesKill[i] = gameObject;
				break;
			}
		}
		gameObject.GetComponent<Animator>().SetBool("IsDead", true);
		//yield return new WaitForSeconds(3);
		yield return new WaitForSeconds(0.5f);
		Time.timeScale = 0.6f;
		PlayerHandler.GetComponent<Animator>().SetBool("IsTaunting", false);
		yield return new WaitForSeconds(0.5f);
		Time.timeScale = 1;
		yield return null;
	}

    private void OnMouseDown()
    {
		//Debug.Log("REGISTERED");
		if (Time.timeScale == 1)
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			
			if (PlayerFuncs.KnifeReady == true)
			{
				
				if (KnifeFuncs.CheckIfInRange(gameObject, player))
				{
					Time.timeScale = 0.99f;
					PlayerFuncs.gameObject.GetComponent<Animator>().SetBool("IsTaunting", true);
                    /*for (int i = 0; i < PlayerFuncs.EnemiesKill.Length; i++)
                    {
						if (PlayerFuncs.EnemiesKill[i] == null)
                        {
							PlayerFuncs.EnemiesKill[i] = gameObject;
							break;
                        }
                    }
					gameObject.GetComponent<Animator>().SetBool("IsDead", true);*/
					StartCoroutine("StopBreaking");
					//Debug.Log(PlayerFuncs.EnemiesKill);
					//Destroy(gameObject);
					if (GameObject.FindObjectOfType<FillKnife>() != null)
						GameObject.FindObjectOfType<FillKnife>().GetComponent<Image>().fillAmount = 0; 
					//PlayerFuncs.SkillSetter = 0;
					PlayerFuncs.IsPlayerMovable = true;
					//StartCoroutine("StopBreaking");
				}
				PlayerFuncs.KnifeReady = false;
			}
		}
    }

    public bool CheckIfFacing(GameObject player, GameObject Enemy)
	{
		if (Enemy.transform.rotation.eulerAngles.y == 0
		&& player.transform.rotation.eulerAngles.y == Enemy.transform.rotation.y + 180)
			return true;
		if (Enemy.transform.rotation.eulerAngles.y == 90
		&& player.transform.rotation.eulerAngles.y - 180 == Enemy.transform.rotation.eulerAngles.y)
			return true;
		if (Enemy.transform.rotation.eulerAngles.y == 180
		&& player.transform.rotation.eulerAngles.y + 180 == Enemy.transform.rotation.eulerAngles.y)
			return true;
		if (Enemy.transform.rotation.eulerAngles.y == 270
		&& player.transform.rotation.eulerAngles.y == Enemy.transform.rotation.eulerAngles.y - 180)
			return true;
		return false;
	}

	public bool CheckIfFacing(GameObject player)
	{
		if (gameObject.transform.rotation.eulerAngles.y == 0
		&& player.transform.rotation.eulerAngles.y == gameObject.transform.rotation.y + 180)
			return true;
		if (gameObject.transform.rotation.eulerAngles.y == 90
		&& player.transform.rotation.eulerAngles.y - 180 == gameObject.transform.rotation.eulerAngles.y)
			return true;
		if (gameObject.transform.rotation.eulerAngles.y == 180
		&& player.transform.rotation.eulerAngles.y + 180 == gameObject.transform.rotation.eulerAngles.y)
			return true;
		if (gameObject.transform.rotation.eulerAngles.y == 270
		&& player.transform.rotation.eulerAngles.y == gameObject.transform.rotation.eulerAngles.y - 180)
			return true;
		return false;
	}


	public bool CheckifPlayerInfrontofEnemy(GameObject player, GameObject Enemy)
	{
		/*Debug.Log("ENemy:");
		Debug.Log(Enemy);
		Debug.Log(Enemy.transform.position.z);
		Debug.Log("player:");
		Debug.Log(player.transform.position.z);*/
		/*if (Enemy.transform.rotation.eulerAngles.y == 90)
		{
			//Debug.Log(Enemy.transform.rotation.eulerAngles.y == 90);
			Debug.Log(Enemy.transform.position.z == player.transform.position.z);
			Debug.Log(Enemy.transform.position.x + 1 == player.transform.position.x);
			Debug.Log(HorLineFuncs.CheckIfThereIsLine(Enemy.transform.position, 1, Enemy.transform.position + new Vector3(1, 0, 0)));
		}*/
		//Debug.Log(player.transform.position);
		if (Enemy.transform.rotation.eulerAngles.y == 0
		&& Enemy.transform.position.x == player.transform.position.x
		&& Enemy.transform.position.z + 1 == player.transform.position.z
		&& VerLineFuncs.CheckIfThereIsLine(Enemy.transform.position, 1, Enemy.transform.position + new Vector3(0, 0, 1)))
			return true;
		if (Enemy.transform.rotation.eulerAngles.y == 90
		&& Enemy.transform.position.z == player.transform.position.z
		&& Enemy.transform.position.x + 1 == player.transform.position.x
		&& HorLineFuncs.CheckIfThereIsLine(Enemy.transform.position, 1, Enemy.transform.position + new Vector3(1, 0, 0)))
			return true;
		if (Enemy.transform.rotation.eulerAngles.y == 180
		&& Enemy.transform.position.x == player.transform.position.x
		&& Enemy.transform.position.z - 1 == player.transform.position.z
		&& VerLineFuncs.CheckIfThereIsLine(Enemy.transform.position, -1, Enemy.transform.position + new Vector3(0, 0, -1)))
			return true;
		if (Enemy.transform.rotation.eulerAngles.y == 270
		&& Enemy.transform.position.z == player.transform.position.z
		&& Enemy.transform.position.x - 1 == player.transform.position.x
		&& HorLineFuncs.CheckIfThereIsLine(Enemy.transform.position, -1, Enemy.transform.position + new Vector3(-1, 0, 0)))
			return true;
		return false;
	}

	public bool CheckifPlayerInfrontofEnemy(GameObject player)
	{
		/*Debug.Log(gameObject);
		//Debug.Log("HERE");
		Debug.Log(gameObject.transform.rotation.eulerAngles.y == 0);
		Debug.Log(gameObject.transform.position.x == player.transform.position.x);
		Debug.Log(gameObject.transform.position.z + 1 == player.transform.position.z);
		Debug.Log(VerLineFuncs.CheckIfThereIsLine(gameObject.transform.position, 1, gameObject.transform.position + new Vector3(0, 0, 1)));*/
		if (gameObject.transform.rotation.eulerAngles.y == 0 
		&& gameObject.transform.position.x == player.transform.position.x 
		&& gameObject.transform.position.z + 1 == player.transform.position.z
		&& VerLineFuncs.CheckIfThereIsLine(gameObject.transform.position, 1, gameObject.transform.position + new Vector3(0,0,1)))
			return true;
		if (gameObject.transform.rotation.eulerAngles.y == 90 
		&& gameObject.transform.position.z == player.transform.position.z
		&& gameObject.transform.position.x + 1 == player.transform.position.x
		&& HorLineFuncs.CheckIfThereIsLine(gameObject.transform.position, 1, gameObject.transform.position + new Vector3(1, 0, 0)))
			return true;
		if (gameObject.transform.rotation.eulerAngles.y == 180 
		&& gameObject.transform.position.x == player.transform.position.x 
		&& gameObject.transform.position.z - 1 == player.transform.position.z
		&& VerLineFuncs.CheckIfThereIsLine(gameObject.transform.position, -1, gameObject.transform.position + new Vector3(0, 0, -1)))
			return true;
		if (gameObject.transform.rotation.eulerAngles.y == 270
		&& gameObject.transform.position.z == player.transform.position.z 
		&& gameObject.transform.position.x - 1 == player.transform.position.x
		&& HorLineFuncs.CheckIfThereIsLine(gameObject.transform.position, -1, gameObject.transform.position + new Vector3(-1, 0, 0)))
			return true;
		return false;
	}

    void Start()
	{
		VerLineFuncs = VerLineHandler.GetComponent<VerticalLine>();
		HorLineFuncs = HorLineHandler.GetComponent<HorizontalLine>();
		PlayerFuncs = PlayerHandler.GetComponent<Player>();
		KnifeFuncs = KnifeHandler.GetComponent<ThrowKnife>();
	}

	// Update is called once per frame
	void Update()
	{
		//Transform PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		//Debug.Log(PlayerFuncs.Invisible);
		GameObject projection = GameObject.FindGameObjectWithTag("Projection");
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		//Debug.Log(PlayerFuncs.LightOffTurns >= 0);
		//Debug.Log(PlayerFuncs.LightOffTurns);
		if (transform.position.x == player.transform.position.x
		&& transform.position.z == player.transform.position.z
		&& (!CheckIfFacing(player) || PlayerFuncs.Invisible >= 0 || PlayerFuncs.LightOffTurns >= 0))
		{
			//Destroy(gameObject);
			/*gameObject.GetComponent<Animator>().SetBool("IsDead", true);
			for (int j = 0; j < PlayerFuncs.EnemiesKill.Length; j++)
			{
				if (PlayerFuncs.EnemiesKill[j] == null)
				{
					PlayerFuncs.EnemiesKill[j] = gameObject;
					FindObjectOfType<AudioManager>().Play("Kill");
					break;
				}
			}*/
			//PlayerFuncs.EnemiesKill = gameObject;
			//PlayerFuncs.SkillSetter += 0.5f;
			//if (GameObject.FindObjectOfType<FillKnife>() != null)
				//GameObject.FindObjectOfType<FillKnife>().StartCoroutine("FillButton");
			//Debug.Log(PlayerFuncs.SkillSetter);
		}
		//Debug.Log(CheckifPlayerInfrontofEnemy(player));
		//Debug.Log(PlayerFuncs.Invisible <= 0);
		//Debug.Log(!PlayerFuncs.IsThereGate(gameObject.transform));
		//Debug.Log(PlayerFuncs.LightOffTurns <= 0);
		if (CheckifPlayerInfrontofEnemy(player) && PlayerFuncs.Invisible <= 0 && !PlayerFuncs.IsThereGate(gameObject.transform) && !PlayerFuncs.IsThereCamera(gameObject.transform) && PlayerFuncs.LightOffTurns <= 0)
		{
			//Application.LoadLevel(0);
				//Debug.Log("LOLIKS");
				gameObject.GetComponent<Animator>().SetBool("IsKilling", true);
				//StopAllCoroutines();
				PlayerFuncs.StartCoroutine("KillingAnimation", gameObject);
				//ListOfMovingEnemies[i].transform.GetChild(1).gameObject.SetActive(true);
				//Debug.Log(ListOfMovingEnemies[i].transform.GetChild(1));
				//ListOfMovingEnemies[i].transform.GetChild(1).
				//return null;
				//this.enabled = false;
				//Application.LoadLevel(0);
		}
		if (projection != null)
		{
			if (transform.position.x == projection.transform.position.x
			&& transform.position.z == projection.transform.position.z
			&& (!CheckIfFacing(projection) || PlayerFuncs.LightOffTurns >= 0))
			{
				//Destroy(gameObject);
				gameObject.GetComponent<Animator>().SetBool("IsDead", true);
				for (int j = 0; j < PlayerFuncs.EnemiesKill.Length; j++)
				{
					if (PlayerFuncs.EnemiesKill[j] == null)
					{
						PlayerFuncs.EnemiesKill[j] = gameObject;
						break;
					}
				}
				//PlayerFuncs.EnemiesKill = gameObject;
				//PlayerFuncs.SkillSetter += 0.5f;
			//	if (GameObject.FindObjectOfType<FillKnife>() != null)
				//	GameObject.FindObjectOfType<FillKnife>().StartCoroutine("FillButton");
				//Debug.Log(PlayerFuncs.SkillSetter);
			}
			if (CheckifPlayerInfrontofEnemy(projection) && !PlayerFuncs.IsThereGate(gameObject.transform) && !PlayerFuncs.IsThereCamera(gameObject.transform) && PlayerFuncs.LightOffTurns <= 0)
			{
				PlayerFuncs.ProjectionActive = false;
				projection.SetActive(false);
			}
		}
	}
}
