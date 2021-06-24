using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmovableEnemy : MonoBehaviour
{
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
}
