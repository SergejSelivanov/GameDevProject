﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	

    private void OnMouseDown()
    {
		if (Time.timeScale == 1)
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			//Debug.Log("REGISTERED");
			if (PlayerFuncs.KnifeReady == true)
			{
				if (KnifeFuncs.CheckIfInRange(gameObject, player))
				{
					Destroy(gameObject);
					PlayerFuncs.SkillSetter = 0;
					PlayerFuncs.IsPlayerMovable = true;
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
		//Debug.Log(gameObject);
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
		if (transform.position.x == player.transform.position.x
		&& transform.position.z == player.transform.position.z
		&& (!CheckIfFacing(player) || PlayerFuncs.Invisible >= 0 || PlayerFuncs.LightOffTurns >= 0))
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
			PlayerFuncs.SkillSetter += 0.5f;
			//Debug.Log(PlayerFuncs.SkillSetter);
		}
		if (CheckifPlayerInfrontofEnemy(player) && PlayerFuncs.Invisible <= 0 && !PlayerFuncs.IsThereGate(gameObject.transform) && PlayerFuncs.LightOffTurns <= 0)
			Application.LoadLevel(0);
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
				PlayerFuncs.SkillSetter += 0.5f;
				//Debug.Log(PlayerFuncs.SkillSetter);
			}
			if (CheckifPlayerInfrontofEnemy(projection) && !PlayerFuncs.IsThereGate(gameObject.transform) && PlayerFuncs.LightOffTurns <= 0)
			{
				PlayerFuncs.ProjectionActive = false;
				projection.SetActive(false);
			}
		}
	}
}
