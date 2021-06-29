using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
	public static bool IsThereGate(Transform ObjCoord)
	{
		Object[] Gates = GameObject.FindGameObjectsWithTag("Gate");
		RaycastHit Hit;
		Ray ray = new Ray(ObjCoord.position, ObjCoord.forward);
		Physics.Raycast(ray, out Hit, 1);
		if (Hit.collider != null)
		{
			for (int i = 0; i < Gates.Length; i++)
			{
				if (Hit.collider.gameObject == Gates[i])
					return true;
			}
		}
		return false;
	}

	public static bool IsThereCamera(Transform ObjCoord)
	{
		Object[] Cameras = GameObject.FindGameObjectsWithTag("CameraEnemy");
		RaycastHit Hit;
		Ray ray = new Ray(ObjCoord.position, ObjCoord.forward);
		Physics.Raycast(ray, out Hit, 1);
		if (Hit.collider != null)
		{
			for (int i = 0; i < Cameras.Length; i++)
			{
				if (Hit.collider.gameObject == Cameras[i])
					return true;
			}
		}
		return false;
	}

	public static int Opposite(GameObject Obj)
	{
		if (Obj.transform.rotation.eulerAngles.y == 0)
			return (180);
		else if (Obj.transform.rotation.eulerAngles.y == 90)
			return (270);
		else if (Obj.transform.rotation.eulerAngles.y == 180)
			return (0);
		else if (Obj.transform.rotation.eulerAngles.y == 270)
			return (90);
		return (0);
	}

	public static bool CheckIfFacing(GameObject player, GameObject Enemy)
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

	public static bool CheckifPlayerInfrontofEnemy(GameObject player, GameObject Enemy)
	{
		if (Enemy.transform.rotation.eulerAngles.y == 0
		&& Enemy.transform.position.x == player.transform.position.x
		&& Enemy.transform.position.z + 1 == player.transform.position.z
		&& VerticalLine.CheckIfThereIsLine(Enemy.transform.position, 1, Enemy.transform.position + new Vector3(0, 0, 1)))
			return true;
		if (Enemy.transform.rotation.eulerAngles.y == 90
		&& Enemy.transform.position.z == player.transform.position.z
		&& Enemy.transform.position.x + 1 == player.transform.position.x
		&& HorizontalLine.CheckIfThereIsLine(Enemy.transform.position, 1, Enemy.transform.position + new Vector3(1, 0, 0)))
			return true;
		if (Enemy.transform.rotation.eulerAngles.y == 180
		&& Enemy.transform.position.x == player.transform.position.x
		&& Enemy.transform.position.z - 1 == player.transform.position.z
		&& VerticalLine.CheckIfThereIsLine(Enemy.transform.position, -1, Enemy.transform.position + new Vector3(0, 0, -1)))
			return true;
		if (Enemy.transform.rotation.eulerAngles.y == 270
		&& Enemy.transform.position.z == player.transform.position.z
		&& Enemy.transform.position.x - 1 == player.transform.position.x
		&& HorizontalLine.CheckIfThereIsLine(Enemy.transform.position, -1, Enemy.transform.position + new Vector3(-1, 0, 0)))
			return true;
		return false;
	}
}
