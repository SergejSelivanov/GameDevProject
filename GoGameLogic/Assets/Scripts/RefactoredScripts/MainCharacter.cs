using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : WalkingHumanoid
{
	private bool IsMovable = true;
	private bool IsWaiting = false;

	protected override void WalkDown()
    {
		Quaternion OldRotation;
		if (IsMovable == true && IsWaiting == false && Time.timeScale == 1)
		{
			if (NodeFuncs.CheckIfNodeExist(transform.position, 'y', -1)
				 && VerLineFuncs.CheckIfThereIsLine(transform.position, -1, transform.position + new Vector3(0, 0, -1)))
			{
				OldRotation = transform.rotation;
				transform.rotation = Quaternion.Euler(0, 180, 0);
				if (!IsThereGate(transform) && !IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
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
			}
		}
	}

    protected override void WalkLeft()
    {
		Quaternion OldRotation;
		if (IsMovable == true && IsWaiting == false && Time.timeScale == 1)
		{
			if (NodeFuncs.CheckIfNodeExist(transform.position, 'x', -1)
		&& HorLineFuncs.CheckIfThereIsLine(transform.position, -1, transform.position + new Vector3(-1, 0, 0)))
			{
				OldRotation = transform.rotation;
				transform.rotation = Quaternion.Euler(0, 270, 0);
				if (!IsThereGate(transform) && !IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
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
			}
		}
	}

    protected override void WalkRight()
    {
		Quaternion OldRotation;
		if (IsMovable == true && IsWaiting == false && Time.timeScale == 1)
		{
			if (NodeFuncs.CheckIfNodeExist(transform.position, 'x', 1)
		&& HorLineFuncs.CheckIfThereIsLine(transform.position, 1, transform.position + new Vector3(1, 0, 0)))
			{
				OldRotation = transform.rotation;
				transform.rotation = Quaternion.Euler(0, 90, 0);
				if (!IsThereGate(transform) && !IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
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
					transform.rotation = OldRotation;
			}
		}
	}

    protected override void WalkUp()
    {
		Quaternion OldRotation;
		if (IsMovable == true && IsWaiting == false && Time.timeScale == 1)
		{
			if (NodeFuncs.CheckIfNodeExist(transform.position, 'y', 1)
		&& VerLineFuncs.CheckIfThereIsLine(transform.position, 1, transform.position + new Vector3(0, 0, 1)))
			{
				OldRotation = transform.rotation;
				transform.rotation = Quaternion.Euler(0, 0, 0);
				if (!IsThereGate(transform) && !IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
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
					transform.rotation = OldRotation;
			}
		}
	}
}
