using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : WalkingHumanoid
{
	private bool IsMovable = true;
	private bool IsWaiting = false;
	private Animator playerAnimator;

   /* enum Sides 
	{
		Up = 0,
		Right = 90,
		Down = 180,
		Left = 270
	}*/


	IEnumerator Move(int requiredAngle)
	{
		int X = 0;
		int sign = 1;
		int Z = 0;
		if (requiredAngle == 0 || requiredAngle == 180)
			Z = 1;
		else
			X = 1;
		if (requiredAngle == 180 || requiredAngle == 270)
			sign = -1;
		//CheckIfThereIsStairway(gameObject);
		//GameObject[] ListOfEnemies;
		playerAnimator.SetBool("IsRunning", true);
		for (float i = 0; i < 1; i += 0.01f)
		{
			//transform.position += new Vector3(0.01f, 0, 0);
			transform.position += new Vector3(0.01f * sign * X, 0, 0.01f * sign * Z);
			yield return new WaitForSeconds(0.004f);
		}
		playerAnimator.SetBool("IsRunning", false);
		transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
		//MoveEnemies();
		/*ListOfEnemies = CheckEnemies();
		for (int i = 0; i < CameraEnemies.Length; i++)
		{
			CameraEnemies[i].MoveCamera();
		}
		IsWaiting = true;
		if (LightOffTurns <= 0 && ListOfEnemies != null)
			yield return LineMovingEnemyFuncs.StartCoroutine("LineMovingEnemyWalk2", ListOfEnemies);
		else*/
			IsWaiting = false;
		//yield return null;
	}

	IEnumerator Rotate(int RequiredAngle)
	{
		int playerangle = (int)gameObject.transform.rotation.eulerAngles.y;
		int Diff = RequiredAngle - playerangle;
		if (Diff == 270 || Diff == -270)
			Diff = -Diff % 180;
		if (Diff != 0)
		{
			playerAnimator.SetInteger("IsRotating", 1);
			for (int i = 0; i < 30; i++)
			{
				gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y + Diff / 30, 0);
				yield return new WaitForSeconds(0.0133f);
			}
			gameObject.transform.rotation = Quaternion.Euler(0, RequiredAngle, 0);
			playerAnimator.SetInteger("IsRotating", 0);
		}
		StartCoroutine(Move(RequiredAngle));
		yield return null;
	}

	public override void WalkUp()
	{
		Quaternion OldRotation;
		if (IsMovable == true && IsWaiting == false && Time.timeScale == 1)
		{
			if (NodeNew.CheckIfNodeExist(transform.position, 'y', 1)
		&& VerLine.CheckIfThereIsLine(transform.position, 1, transform.position + new Vector3(0, 0, 1)))
			{
				OldRotation = transform.rotation;
				transform.rotation = Quaternion.Euler(0, 0, 0);
				/*if (!IsThereGate(transform) && !IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
				{
					transform.rotation = OldRotation;
					IsWaiting = true;
					StartCoroutine("RotateAndKill", 0);
				}
				else if (!IsThereGate(transform) && !IsThereCamera(transform))
				{
					transform.rotation = OldRotation;
					IsWaiting = true;
					StartCoroutine("Rotate", 0);
				}
				else
					transform.rotation = OldRotation;*/
				transform.rotation = OldRotation;
				IsWaiting = true;
				StartCoroutine("Rotate", 0);
			}
		}
	}

	public override void WalkDown()
    {
		Quaternion OldRotation;
		if (IsMovable == true && IsWaiting == false && Time.timeScale == 1)
		{
			if (NodeNew.CheckIfNodeExist(transform.position, 'y', -1)
			&& VerLine.CheckIfThereIsLine(transform.position, -1, transform.position + new Vector3(0, 0, -1)))
			{
				OldRotation = transform.rotation;
				transform.rotation = Quaternion.Euler(0, 180, 0);
				/*if (!IsThereGate(transform) && !IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
				{
					transform.rotation = OldRotation;
					IsWaiting = true;
					StartCoroutine("RotateAndKill", 180);
				}
				else if (!IsThereGate(transform) && !IsThereCamera(transform))
				{
					transform.rotation = OldRotation;
					IsWaiting = true;
					StartCoroutine("Rotate", 180);

				}
				else
					transform.rotation = OldRotation;
			}*/
				transform.rotation = OldRotation;
				IsWaiting = true;
				StartCoroutine("Rotate", 180);
			}
		}
	}

	public override void WalkLeft()
    {
		Quaternion OldRotation;
		if (IsMovable == true && IsWaiting == false && Time.timeScale == 1)
		{
			//if (node.CheckIfNodeExist(transform.position, 'x', -1)
			//Debug.Log(HorLine.CheckIfThereIsLine(transform.position, -1, transform.position + new Vector3(-1, 0, 0)));
			if (NodeNew.CheckIfNodeExist(transform.position, 'x', -1)
			&& HorLine.CheckIfThereIsLine(transform.position, -1, transform.position + new Vector3(-1, 0, 0)))
			{
				//Debug.Log("aaa");
				OldRotation = transform.rotation;
				transform.rotation = Quaternion.Euler(0, 270, 0);
				/*if (!IsThereGate(transform) && !IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
				{
					transform.rotation = OldRotation;
					IsWaiting = true;
					StartCoroutine("RotateAndKill", 270);
				}
				else if (!IsThereGate(transform) && !IsThereCamera(transform))
				{
					transform.rotation = OldRotation;
					IsWaiting = true;
					StartCoroutine("Rotate", 270);
				}
				else
					transform.rotation = OldRotation;
			}*/
				transform.rotation = OldRotation;
				IsWaiting = true;
				StartCoroutine("Rotate", 270);
			}
		}
	}

	public override void WalkRight()
    {
		Quaternion OldRotation;
		if (IsMovable == true && IsWaiting == false && Time.timeScale == 1)
		{
			if (NodeNew.CheckIfNodeExist(transform.position, 'x', 1)
		&& HorLine.CheckIfThereIsLine(transform.position, 1, transform.position + new Vector3(1, 0, 0)))
			{
				OldRotation = transform.rotation;
				transform.rotation = Quaternion.Euler(0, 90, 0);
				/*if (!IsThereGate(transform) && !IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
				{
					transform.rotation = OldRotation;
					IsWaiting = true;
					StartCoroutine("RotateAndKill", 90);
				}
				else if (!IsThereGate(transform) && !IsThereCamera(transform))
				{
					transform.rotation = OldRotation;
					IsWaiting = true;
					StartCoroutine("Rotate", 90);
				}
				else
					transform.rotation = OldRotation;*/
				transform.rotation = OldRotation;
				IsWaiting = true;
				StartCoroutine("Rotate", 90);
			}
		}
	}
    private void Start()
    {
		playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

	}
}
