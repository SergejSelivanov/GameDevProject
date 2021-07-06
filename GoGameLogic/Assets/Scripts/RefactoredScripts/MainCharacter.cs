using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCharacter : WalkingHumanoid
{
	private bool IsMovable = true;
	private bool IsWaiting = false;
	private Animator playerAnimator;
	private CameraEnemy[] CameraEnemies;
	//private static GameObject[] EnemiesTokill;
	//[HideInInspector]
	public static GameObject[] EnemiesToKill { get; set; }
	public int InvisibleSteps { get; set; }
	public int LightsOffTurns { get; set; }

	public GameObject[] EnemiesKill
    {
        get
        {
			return EnemiesToKill;
        }
		set
        {
			EnemiesToKill = value;
        }
	}

	/* enum Sides 
	 {
		 Up = 0,
		 Right = 90,
		 Down = 180,
		 Left = 270
	 }*/

	/*IEnumerator KillEnemy()
	{
		GameObject[] KilledEnemies = FindEnemies();
		CheckIfThereIsStairway(gameObject);
		GameObject[] ListOfEnemies;
		ListOfEnemies = CheckEnemies();
		gameObject.GetComponent<Animator>().SetInteger("KillingWalk", 1);
		for (int i = 0; i < 42; i++)
		{
			gameObject.transform.position += gameObject.transform.forward / 100;
			yield return new WaitForSeconds(0.01f);
		}
		gameObject.GetComponent<Animator>().SetInteger("KillingWalk", 0);
		yield return null;
		gameObject.GetComponent<Animator>().SetBool("IsKilling", true);
		yield return new WaitForSeconds(0.7f);
		for (int i = 0; i < KilledEnemies.Length; i++)
		{
			if (KilledEnemies[i] != null)
			{
				for (int j = 0; j < EnemiesKill.Length; j++)
				{

					if (EnemiesTokill[j] == null)
					{
						EnemiesTokill[j] = KilledEnemies[i];
						break;
					}
				}
				try
				{
					KilledEnemies[i].GetComponent<Animator>().SetBool("IsDead", true);
				}
				catch
				{
					KilledEnemies[i].transform.GetChild(0).GetComponent<Animator>().SetBool("IsDead", true);
				}
			}
		}
		yield return new WaitForSeconds(1);
		gameObject.GetComponent<Animator>().SetBool("IsKilling", false);
		yield return null;
		gameObject.GetComponent<Animator>().SetInteger("KillingWalk", 2);
		for (int i = 0; i < 58; i++)
		{
			gameObject.transform.position += gameObject.transform.forward / 100;
			yield return new WaitForSeconds(0.01f);

		}
		gameObject.GetComponent<Animator>().SetInteger("KillingWalk", 0);
		gameObject.transform.position = new Vector3(Mathf.Round(gameObject.transform.position.x), gameObject.transform.position.y, Mathf.Round(gameObject.transform.position.z));

		transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
		for (int i = 0; i < CameraEnemies.Length; i++)
		{
			CameraEnemies[i].MoveCamera();
		}
		IsWaiting = true;
		if (LightOffTurns <= 0)
			yield return LineMovingEnemyFuncs.StartCoroutine("LineMovingEnemyWalk2", ListOfEnemies);
		else
			IsWaiting = false;

		yield return null;
	}*/

	public static bool CheckIfThereIsMovingEnemy(GameObject Obj)
	{
		GameObject[] ListOfMotEnemies = GameObject.FindGameObjectsWithTag("LineMovingEnemy");
		for (int i = 0; i < ListOfMotEnemies.Length; i++)
		{
			if (MovingEnemy.CheckifPlayerInfrontofEnemy(ListOfMotEnemies[i], Obj))
				return true;
		}
		return false;
	}

	public static bool CheckIfThereIsMotEnemy(GameObject Obj)
	{
		GameObject[] ListOfMotEnemies = GameObject.FindGameObjectsWithTag("MotionlessEnemy");
		for (int i = 0; i < ListOfMotEnemies.Length; i++)
		{
			if (MovingEnemy.CheckifPlayerInfrontofEnemy(ListOfMotEnemies[i], Obj))
				return true;
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

	IEnumerator WalkUpright(int sign, GameObject Obj, GameObject Node)
	{
		float diff = Mathf.Abs(Obj.transform.position.y - Node.transform.position.y);
		for (float i = 0; i < 1; i += 0.01f)
		{
			Obj.transform.position += new Vector3(0, diff / 100 * sign, 0);
			yield return new WaitForSeconds(0.004f);
		}
	}

	private GameObject FindNode(Node[] Nodes, int X, int Z)
	{
		for (int i = 0; i < Nodes.Length; i++)
		{
			if (Nodes[i].transform.position.x == X && Nodes[i].transform.position.z == Z)
				return (Nodes[i].gameObject);
		}
		return (null);
	}

	public GameObject GetStairwayNodePositionY(GameObject Obj)
	{
		Node[] Nodes = GameObject.FindObjectsOfType<Node>();
		GameObject DefiniteNode;
		if (Obj.transform.rotation.eulerAngles.y == 0)
			DefiniteNode = FindNode(Nodes, (int)Obj.transform.position.x, (int)Obj.transform.position.z + 1);
		else if (Obj.transform.rotation.eulerAngles.y == 90)
			DefiniteNode = FindNode(Nodes, (int)Obj.transform.position.x + 1, (int)Obj.transform.position.z);
		else if (Obj.transform.rotation.eulerAngles.y == 180)
			DefiniteNode = FindNode(Nodes, (int)Obj.transform.position.x, (int)Obj.transform.position.z - 1);
		else
			DefiniteNode = FindNode(Nodes, (int)Obj.transform.position.x - 1, (int)Obj.transform.position.z);
		return DefiniteNode;
	}

	public void CheckIfThereIsStairway(GameObject Obj)
	{
		GameObject DefiniteNode = GetStairwayNodePositionY(Obj);
		if (DefiniteNode == null)
			return;
		float NodePositionY = DefiniteNode.transform.position.y;
		float ObjPositionY = Obj.transform.position.y;
		if (Mathf.Abs(NodePositionY - ObjPositionY) > 0.1f)
		{
			if (NodePositionY > ObjPositionY)
				StartCoroutine(WalkUpright(1, Obj, DefiniteNode));
			else
				StartCoroutine(WalkUpright(-1, Obj, DefiniteNode));
		}
	}

	IEnumerator KillingAnimation(GameObject Enemy)
	{
		yield return new WaitForSeconds(0.6f);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsKilled", true);
		Enemy.transform.GetChild(1).gameObject.SetActive(true);
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene(0);
		yield return null;
	}

	private GameObject[] CheckEnemies()
	{
		GameObject[] ListOfMovingEnemies = GameObject.FindGameObjectsWithTag("LineMovingEnemy");
		GameObject[] RetArray = new GameObject[ListOfMovingEnemies.Length];
		if (EnemiesToKill == null)
			EnemiesToKill = new GameObject[ListOfMovingEnemies.Length];
		for (int i = 0; i < ListOfMovingEnemies.Length; i++)
		{
			if (MovingEnemy.CheckifPlayerInfrontofEnemy(gameObject, ListOfMovingEnemies[i]) && InvisibleSteps <= 0 && !IsThereGate(ListOfMovingEnemies[i].transform) && !IsThereCamera(ListOfMovingEnemies[i].transform) && LightsOffTurns <= 0)
			{
				ListOfMovingEnemies[i].transform.GetChild(0).GetComponent<Animator>().SetBool("IsKilling", true);
				StartCoroutine("KillingAnimation", ListOfMovingEnemies[i]);
				return null;
			}
			if (transform.position.x == ListOfMovingEnemies[i].transform.position.x
			&& transform.position.z == ListOfMovingEnemies[i].transform.position.z
			&& (!ImmovableEnemy.CheckIfFacing(gameObject, ListOfMovingEnemies[i]) || InvisibleSteps >= 0 || LightsOffTurns >= 0))
			{
				ListOfMovingEnemies[i].transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("IsDead", true);
				for (int j = 0; j < EnemiesToKill.Length; j++)
				{
					if (EnemiesToKill[j] == null)
					{
						EnemiesToKill[j] = ListOfMovingEnemies[i];
						break;
					}
				}
				RetArray[i] = null;
				continue;
			}
			if (ListOfMovingEnemies[i] != null)
				RetArray[i] = ListOfMovingEnemies[i];
		}
		InvisibleSteps--;
		LightsOffTurns--;
		return RetArray;
	}

	public int ReturnPositionsToTurn(GameObject[] ListOfEnemies, int[] Indexes)
	{
		int ret = 0;
		bool flag = false;
		for (int i = 0; i < ListOfEnemies.Length; i++)
		{
			flag = false;
			for (int j = 0; j < Indexes.Length; j++)
			{
				if (i == Indexes[j])
				{
					flag = true;
					break;
				}
			}
			if (flag == false && ListOfEnemies[i] != null)
			{
				if (ListOfEnemies[i].transform.GetChild(0).position.x - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.x) < 0)
				{
					ret++;
					ListOfEnemies[i].transform.GetChild(0).GetComponent<Animator>().SetBool("IsRunning", true);
					StartCoroutine("ReturnToX", ListOfEnemies[i]);
				}
				if (ListOfEnemies[i].transform.GetChild(0).position.x - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.x) > 0)
				{
					ret++;
					ListOfEnemies[i].transform.GetChild(0).GetComponent<Animator>().SetBool("IsRunning", true);
					StartCoroutine("ReturnToMinusX", ListOfEnemies[i]);
				}
				if (ListOfEnemies[i].transform.GetChild(0).position.z - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.z) < 0)
				{
					ret++;
					ListOfEnemies[i].transform.GetChild(0).GetComponent<Animator>().SetBool("IsRunning", true);
					StartCoroutine("ReturnToY", ListOfEnemies[i]);
				}
				if (ListOfEnemies[i].transform.GetChild(0).position.z - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.z) > 0)
				{
					ret++;
					ListOfEnemies[i].transform.GetChild(0).GetComponent<Animator>().SetBool("IsRunning", true);
					StartCoroutine("ReturnToMinusY", ListOfEnemies[i]);
				}
				if (ret != 0)
				{
					if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 0)
						ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, -45, 0);
					if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 90)
						ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 45, 0);
					if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 180)
						ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 135, 0);
					if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 270)
						ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 235, 0);
				}
			}
		}
		return ret;
	}

	public void ReturnPositions(GameObject[] ListOfEnemies, int[] Indexes)
	{
		bool flag = false;
		bool NeedToRotate = false;
		for (int i = 0; i < ListOfEnemies.Length; i++)
		{
			flag = false;
			for (int j = 0; j < Indexes.Length; j++)
			{
				if (i == Indexes[j])
				{
					flag = true;
					break;
				}
			}
			if (flag == false && ListOfEnemies[i] != null)
			{
				if (ListOfEnemies[i].transform.GetChild(0).position.x - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.x) < 0)
				{
					NeedToRotate = true;
					StartCoroutine("ReturnToX", ListOfEnemies[i]);
				}
				if (ListOfEnemies[i].transform.GetChild(0).position.x - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.x) > 0)
				{
					NeedToRotate = true;
					StartCoroutine("ReturnToMinusX", ListOfEnemies[i]);
				}
				if (ListOfEnemies[i].transform.GetChild(0).position.z - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.z) < 0)
				{
					NeedToRotate = true;
					StartCoroutine("ReturnToY", ListOfEnemies[i]);
				}
				if (ListOfEnemies[i].transform.GetChild(0).position.z - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.z) > 0)
				{
					NeedToRotate = true;
					StartCoroutine("ReturnToMinusY", ListOfEnemies[i]);
				}
				if (NeedToRotate == true)
				{
					if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 0)
						ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, -12.6f, 0);
					if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 90)
						ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 77.4f, 0);
					if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 180)
						ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 167.4f, 0);
					if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 270)
						ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 257.4f, 0);
				}
			}
		}
	}

	public void DestroyIfClose(GameObject Obj)
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if ((MovingEnemy.CheckifPlayerInfrontofEnemy(player, Obj) || (player.transform.position.x == Obj.transform.position.x && player.transform.position.z == Obj.transform.position.z)) && InvisibleSteps <= 0 && !IsThereGate(Obj.transform))
		{
			Obj.transform.GetChild(0).GetComponent<Animator>().SetBool("IsKilling", true);
			StartCoroutine("KillingAnimation", Obj);
			return;
		}
		if (player.transform.position.x == Obj.transform.position.x
			&& player.transform.position.z == Obj.transform.position.z && InvisibleSteps >= 0)
			Destroy(Obj);
	}

	public IEnumerator LineMovingEnemyWalk2(GameObject[] ListOfEnemies)
	{
		Transform[] ListOfTransforms = new Transform[ListOfEnemies.Length];
		GameObject[] ListBuf = new GameObject[ListOfEnemies.Length];
		ListOfEnemies.CopyTo(ListBuf, 0);
		for (int i = 0; i < ListOfEnemies.Length; i++)
		{
			if (ListOfEnemies[i] != null && (!MovingEnemy.CheckIfThereIsNodeToMove(ListOfEnemies[i]) || IsThereGate(ListOfEnemies[i].transform) || IsThereCamera(ListOfEnemies[i].transform) || CheckIfThereIsMotEnemy(ListOfEnemies[i])))
			{
				//ListBuf[i] = null;///gavno
			}
			else
			{
				ListBuf[i] = null;
			}
		}
		int[] Indexes = MovingEnemy.IsCrossing(ListBuf);
		//movingenemy
		if (ReturnPositionsToTurn(ListBuf, Indexes) != 0)
			yield return new WaitForSeconds(0.5f);
		for (int i = 0; i < ListBuf.Length; i++)
		{
			if (ListBuf[i] != null)
			{
				if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 0)
					ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);
				if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 90)
					ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 90, 0);
				if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 180)
					ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 180, 0);
				if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 270)
					ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 270, 0);
				ListBuf[i].transform.GetChild(0).GetComponent<Animator>().SetBool("IsRunning", false);
			}
		}
		for (int i = 0; i < ListOfEnemies.Length; i++)
		{
			if (ListOfEnemies[i] != null && (!MovingEnemy.CheckIfThereIsNodeToMove(ListOfEnemies[i]) || IsThereGate(ListOfEnemies[i].transform) || IsThereCamera(ListOfEnemies[i].transform) || CheckIfThereIsMotEnemy(ListOfEnemies[i])))
				StartCoroutine("RotateEnemies", ListOfEnemies[i]);
		}
		yield return new WaitForSeconds(0.5f);
		for (int i = 0; i < ListOfEnemies.Length; i++)
		{
			if (ListOfEnemies[i] == null || !MovingEnemy.CheckIfThereIsNodeToMove(ListOfEnemies[i])
			|| IsThereGate(ListOfEnemies[i].transform)
			|| IsThereCamera(ListOfEnemies[i].transform)
			|| CheckIfThereIsMotEnemy(ListOfEnemies[i])
			|| !(HorLine.CheckIfThereIsLine(ListOfEnemies[i].transform.position, -1, ListOfEnemies[i].transform.position + new Vector3(-1, 0, 0))
			|| HorLine.CheckIfThereIsLine(ListOfEnemies[i].transform.position, 1, ListOfEnemies[i].transform.position + new Vector3(1, 0, 0))
			|| VerLine.CheckIfThereIsLine(ListOfEnemies[i].transform.position, 1, ListOfEnemies[i].transform.position + new Vector3(0, 0, 1))
			|| VerLine.CheckIfThereIsLine(ListOfEnemies[i].transform.position, -1, ListOfEnemies[i].transform.position + new Vector3(0, 0, -1))))
				ListOfEnemies[i] = null;
		}
		for (int i = 0; i < ListOfEnemies.Length; i++)
		{
			if (ListOfEnemies[i] != null)
				ListOfTransforms[i] = ListOfEnemies[i].transform;
			else
				ListOfTransforms[i] = null;
		}
		Indexes = MovingEnemy.IsCrossing(ListOfEnemies);
		///movingenemy
		ReturnPositions(ListOfEnemies, Indexes);
		for (int i = 0; i < Indexes.Length; i++)
		{
			StartCoroutine("GettingSlower", ListOfEnemies[Indexes[i]]);
		}
		for (int i = 0; i < ListOfEnemies.Length; i++)
		{
			if (ListOfEnemies[i] != null)
				CheckIfThereIsStairway(ListOfEnemies[i]);
		}
		for (float i = 0; i < 1; i += 0.01f)
		{
			for (int j = 0; j < ListOfEnemies.Length; j++)
			{
				if (ListOfEnemies[j] != null)
				{
					ListOfEnemies[j].GetComponentInChildren<Animator>().SetBool("IsRunning", true);
					if (ListOfTransforms[j].rotation.eulerAngles.y == 0)
						ListOfEnemies[j].transform.position += new Vector3(0, 0, 0.01f);
					if (ListOfEnemies[j].transform.rotation.eulerAngles.y == 90)
						ListOfEnemies[j].transform.position += new Vector3(0.01f, 0, 0);
					if (ListOfEnemies[j].transform.rotation.eulerAngles.y == 180)
						ListOfEnemies[j].transform.position += new Vector3(0, 0, -0.01f);
					if (ListOfEnemies[j].transform.rotation.eulerAngles.y == 270)
						ListOfEnemies[j].transform.position += new Vector3(-0.01f, 0, 0);
				}
			}
			yield return new WaitForSeconds(0.004f);
		}
		for (int i = 0; i < ListOfEnemies.Length; i++)
		{
			if (ListOfEnemies[i] != null)
			{
				if (Mathf.Abs(ListOfEnemies[i].transform.GetChild(0).rotation.eulerAngles.y - 347.4f) < 0.1f)
					ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);
				if (Mathf.Abs(ListOfEnemies[i].transform.GetChild(0).rotation.eulerAngles.y - 77.4f) < 0.1f)
					ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 90, 0);
				if (Mathf.Abs(ListOfEnemies[i].transform.GetChild(0).rotation.eulerAngles.y - 167.4f) < 0.1f)
					ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 180, 0);
				if (Mathf.Abs(ListOfEnemies[i].transform.GetChild(0).rotation.eulerAngles.y - 257.4f) < 0.1f)
					ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 270, 0);
			}
		}
		for (int j = 0; j < ListOfEnemies.Length; j++)
		{
			if (ListOfEnemies[j] != null)
			{
				ListOfEnemies[j].GetComponentInChildren<Animator>().SetBool("IsRunning", false);
				if (ListOfEnemies[j].transform.rotation.eulerAngles.y == 270 || ListOfEnemies[j].transform.rotation.eulerAngles.y == 90)
					ListOfEnemies[j].transform.position = new Vector3(Mathf.Round(ListOfEnemies[j].transform.position.x), ListOfEnemies[j].transform.position.y, ListOfEnemies[j].transform.position.z);
				if (ListOfEnemies[j].transform.rotation.eulerAngles.y == 0 || ListOfEnemies[j].transform.rotation.eulerAngles.y == 180)
					ListOfEnemies[j].transform.position = new Vector3(ListOfEnemies[j].transform.position.x, ListOfEnemies[j].transform.position.y, Mathf.Round(ListOfEnemies[j].transform.position.z));
				DestroyIfClose(ListOfEnemies[j]);
			}
		}
		IsWaiting = false;
		yield return null;
	}

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
		CheckIfThereIsStairway(gameObject);
		GameObject[] ListOfEnemies;
		playerAnimator.SetBool("IsRunning", true);
		for (float i = 0; i < 1; i += 0.01f)
		{
			transform.position += new Vector3(0.01f * sign * X, 0, 0.01f * sign * Z);
			yield return new WaitForSeconds(0.004f);
		}
		playerAnimator.SetBool("IsRunning", false);
		transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
		//MoveEnemies();
		ListOfEnemies = CheckEnemies();
		for (int i = 0; i < CameraEnemies.Length; i++)
			CameraEnemies[i].MoveCamera();
		IsWaiting = true;
		if (LightsOffTurns <= 0 && ListOfEnemies != null)
			yield return StartCoroutine("LineMovingEnemyWalk2", ListOfEnemies);
		else
			IsWaiting = false;
		/*IsWaiting = true;
		if (LightOffTurns <= 0 && ListOfEnemies != null)
			yield return LineMovingEnemyFuncs.StartCoroutine("LineMovingEnemyWalk2", ListOfEnemies);
		else*/
		//IsWaiting = false;
		//yield return null;
	}

	IEnumerator RotateEnemies(GameObject ObjectToRotate)
	{
		if (ObjectToRotate != null)
		{
			int playerAngle = (int)gameObject.transform.rotation.eulerAngles.y;
			int requiredAngle = MovingEnemy.Opposite(ObjectToRotate);
			int Diff = requiredAngle - playerAngle;
			if (Diff == 270 || Diff == -270)
				Diff = -Diff % 180;
			if (Diff != 0)
			{
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 1);
				for (int i = 0; i < 30; i++)
				{
					ObjectToRotate.transform.rotation = Quaternion.Euler(0, (int)ObjectToRotate.transform.rotation.eulerAngles.y + Diff / 30, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				ObjectToRotate.transform.rotation = Quaternion.Euler(0, requiredAngle, 0);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
			}
		}
		yield return null;
	}

	private GameObject[] FindEnemies()
	{
		GameObject[] MotEnemies = GameObject.FindGameObjectsWithTag("MotionlessEnemy");
		GameObject[] MovingEnemies = GameObject.FindGameObjectsWithTag("LineMovingEnemy");
		GameObject[] Enemies = new GameObject[MotEnemies.Length + MovingEnemies.Length];
		Vector3 NewPos = gameObject.transform.position + gameObject.transform.forward;
		for (int i = 0; i < MotEnemies.Length; i++)
		{
			if (Mathf.Round(NewPos.x) == MotEnemies[i].transform.position.x && Mathf.Round(NewPos.z) == MotEnemies[i].transform.position.z)
			{
				for (int j = 0; j < Enemies.Length; j++)
				{
					if (Enemies[j] == null)
					{
						Enemies[j] = MotEnemies[i];
						break;
					}
				}
			}
		}
		for (int i = 0; i < MovingEnemies.Length; i++)
		{
			if (Mathf.Round(NewPos.x) == MovingEnemies[i].transform.position.x && Mathf.Round(NewPos.z) == MovingEnemies[i].transform.position.z)
			{
				for (int j = 0; j < Enemies.Length; j++)
				{
					if (Enemies[j] == null)
					{
						Enemies[j] = MovingEnemies[i];
						break;
					}
				}
			}
		}
		return Enemies;
	}

	IEnumerator KillEnemy()
	{
		GameObject[] KilledEnemies = FindEnemies();
		CheckIfThereIsStairway(gameObject);
		GameObject[] ListOfEnemies;
		ListOfEnemies = CheckEnemies();
		gameObject.GetComponent<Animator>().SetInteger("KillingWalk", 1);
		for (int i = 0; i < 42; i++)
		{
			gameObject.transform.position += gameObject.transform.forward / 100;
			yield return new WaitForSeconds(0.01f);
		}
		gameObject.GetComponent<Animator>().SetInteger("KillingWalk", 0);
		yield return null;
		gameObject.GetComponent<Animator>().SetBool("IsKilling", true);
		yield return new WaitForSeconds(0.7f);
		for (int i = 0; i < KilledEnemies.Length; i++)
		{
			if (KilledEnemies[i] != null)
			{
				for (int j = 0; j < EnemiesToKill.Length; j++)
				{
					if (EnemiesToKill[j] == null)
					{
						EnemiesToKill[j] = KilledEnemies[i];
						break;
					}
				}
				try
				{
					KilledEnemies[i].GetComponent<Animator>().SetBool("IsDead", true);
				}
				catch
				{
					KilledEnemies[i].transform.GetChild(0).GetComponent<Animator>().SetBool("IsDead", true);
				}
			}
		}
		yield return new WaitForSeconds(1);
		gameObject.GetComponent<Animator>().SetBool("IsKilling", false);
		yield return null;
		gameObject.GetComponent<Animator>().SetInteger("KillingWalk", 2);
		for (int i = 0; i < 58; i++)
		{
			gameObject.transform.position += gameObject.transform.forward / 100;
			yield return new WaitForSeconds(0.01f);
		}
		gameObject.GetComponent<Animator>().SetInteger("KillingWalk", 0);
		gameObject.transform.position = new Vector3(Mathf.Round(gameObject.transform.position.x), gameObject.transform.position.y, Mathf.Round(gameObject.transform.position.z));
		transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
		for (int i = 0; i < CameraEnemies.Length; i++)
			CameraEnemies[i].MoveCamera();
		IsWaiting = true;
		if (LightsOffTurns <= 0)
			yield return StartCoroutine("LineMovingEnemyWalk2", ListOfEnemies);
		else
			IsWaiting = false;
		yield return null;
	}

	IEnumerator Rotate(int RequiredAngle, bool NeedToKill)
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
		if (NeedToKill == false)
			StartCoroutine(Move(RequiredAngle));
		else
			StartCoroutine(KillEnemy());
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
				if (!IsThereGate(transform) && !IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
				{
					transform.rotation = OldRotation;
					IsWaiting = true;
					StartCoroutine(Rotate(0, true));
				}
				else if (!IsThereGate(transform) && !IsThereCamera(transform))
				{
					transform.rotation = OldRotation;
					IsWaiting = true;
					StartCoroutine(Rotate(0, false));
				}
				else
					transform.rotation = OldRotation;
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
				if (!IsThereGate(transform) && !IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
				{
					transform.rotation = OldRotation;
					IsWaiting = true;
					StartCoroutine(Rotate(180, true));
				}
				else if (!IsThereGate(transform) && !IsThereCamera(transform))
				{
					transform.rotation = OldRotation;
					IsWaiting = true;
					StartCoroutine(Rotate(180, false));
				}
				else
					transform.rotation = OldRotation;
			}
		}
	}

	public override void WalkLeft()
    {
		Quaternion OldRotation;
		if (IsMovable == true && IsWaiting == false && Time.timeScale == 1)
		{
			if (NodeNew.CheckIfNodeExist(transform.position, 'x', -1)
			&& HorLine.CheckIfThereIsLine(transform.position, -1, transform.position + new Vector3(-1, 0, 0)))
			{
				//Debug.Log("aaa");
				OldRotation = transform.rotation;
				transform.rotation = Quaternion.Euler(0, 270, 0);
				if (!IsThereGate(transform) && !IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
				{
					transform.rotation = OldRotation;
					IsWaiting = true;
					StartCoroutine(Rotate(270, true));
				}
				else if (!IsThereGate(transform) && !IsThereCamera(transform))
				{
					transform.rotation = OldRotation;
					IsWaiting = true;
					StartCoroutine(Rotate(270, false));
				}
				else
					transform.rotation = OldRotation;
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
				if (!IsThereGate(transform) && !IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
				{
					transform.rotation = OldRotation;
					IsWaiting = true;
					StartCoroutine(Rotate(90, true));
				}
				else if (!IsThereGate(transform) && !IsThereCamera(transform))
                {
					transform.rotation = OldRotation;
					IsWaiting = true;
					StartCoroutine(Rotate(90, false));
				}
				else
					transform.rotation = OldRotation;
			}
		}
	}
    private void Start()
    {
		playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
		CameraEnemies = GameObject.FindObjectsOfType<CameraEnemy>();
	}

    /*private void Update()
    {
		Debug.Log(Sides.Down);
		Debug.Log(Sides.Down.GetType());
    }*/
}
