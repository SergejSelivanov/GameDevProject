using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public Texture2D[] SomeLightmaps;
	public bool KnifeIsReady { get; set; }
	public bool IsMovable { get; set; }
	public bool IsWaiting { get; set; }
	public int LightsOffTurns { get; set; }
	private LightmapData[] LightMapBuf;
	private bool LightsOff = false;
	private CameraEnemy[] CameraEnemies;
	private GameObject FinalNode;
	private static GameObject[] EnemiesTokill;
	public GameObject turnManagerHandler;
	private TurnManager turnManager;

	public GameObject[] EnemiesKill
	{
		get
		{
			return EnemiesTokill;
		}
		set
		{
			EnemiesTokill = value;
		}
	}

	public GameObject GetStairwayNodePositionY(GameObject Obj)
	{
		Node[] Nodes = GameObject.FindObjectsOfType<Node>();
		GameObject DefiniteNode;
		if (Obj.transform.rotation.eulerAngles.y == 0)
			DefiniteNode = Node.FindNode(Nodes, (int)Obj.transform.position.x, (int)Obj.transform.position.z + 1);
		else if(Obj.transform.rotation.eulerAngles.y == 90)
			DefiniteNode = Node.FindNode(Nodes, (int)Obj.transform.position.x + 1, (int)Obj.transform.position.z);
		else if(Obj.transform.rotation.eulerAngles.y == 180)
			DefiniteNode = Node.FindNode(Nodes, (int)Obj.transform.position.x, (int)Obj.transform.position.z - 1);
		else
			DefiniteNode = Node.FindNode(Nodes, (int)Obj.transform.position.x - 1, (int)Obj.transform.position.z);
		return DefiniteNode;
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

	public bool CheckIfThereIsMovingEnemy(GameObject Obj)
	{
		GameObject[] ListOfMotEnemies = GameObject.FindGameObjectsWithTag("LineMovingEnemy");
		for (int i = 0; i < ListOfMotEnemies.Length; i++)
		{
			if (Utilities.CheckifPlayerInfrontofEnemy(ListOfMotEnemies[i], Obj))
				return true;
		}
		return false;
	}

	public bool CheckIfThereIsMotEnemy(GameObject Obj)
    {
		GameObject[] ListOfMotEnemies = GameObject.FindGameObjectsWithTag("MotionlessEnemy");
        for (int i = 0; i < ListOfMotEnemies.Length; i++)
        {
			if (Utilities.CheckifPlayerInfrontofEnemy(ListOfMotEnemies[i], Obj))
				return true;
        }
		return false;
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

	private void InitDeathArr()
	{
		GameObject[] ListOfMovingEnemies = GameObject.FindGameObjectsWithTag("LineMovingEnemy");
		GameObject[] ListOfMotEnemies = GameObject.FindGameObjectsWithTag("MotionlessEnemy");
		if (EnemiesTokill == null)
			EnemiesTokill = new GameObject[ListOfMovingEnemies.Length + ListOfMotEnemies.Length];
		LightsOffTurns--;
	}

	IEnumerator RotateEnemies(GameObject ObjectToRotate)
	{
		if (ObjectToRotate != null)
		{
			int requiredAngle = Utilities.Opposite(ObjectToRotate);
			ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 1);
			for (int i = 0; i < 30; i++)
			{
				ObjectToRotate.transform.rotation = Quaternion.Euler(0, (int)ObjectToRotate.transform.rotation.eulerAngles.y + 6, 0);
				yield return new WaitForSeconds(0.0133f);
			}
			ObjectToRotate.transform.rotation = Quaternion.Euler(0, requiredAngle, 0);
			ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
		}
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
			GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 1);
			for (int i = 0; i < 30; i++)
			{
				gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y + Diff / 30, 0);
				yield return new WaitForSeconds(0.0133f);
			}
			GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
			gameObject.transform.rotation = Quaternion.Euler(0, RequiredAngle, 0);
		}
		if (NeedToKill == false)
			StartCoroutine("Walk", RequiredAngle);
		else
			StartCoroutine("KillEnemy");
		yield return null;
	}

	IEnumerator Walk(int requiredAngle)
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
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
		for (float i = 0; i < 1; i += 0.01f)
		{
			transform.position += new Vector3(0.01f * sign * X, 0, 0.01f * sign * Z);
			yield return new WaitForSeconds(0.004f);
		}
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", false);
		transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
		InitDeathArr();
		for (int i = 0; i < CameraEnemies.Length; i++)
			CameraEnemies[i].MoveCamera();
		IsWaiting = true;
		turnManager.EndPlayersTurn();
	}

	public void ChangeLights()
	{
		LightmapData[] LmData = new LightmapData[1];
		LmData[0] = new LightmapData();
		if (LightsOff == false)
		{
			LmData[0] = LightmapSettings.lightmaps[0];
			LmData[0].lightmapDir = SomeLightmaps[1];
			LmData[0].lightmapColor = SomeLightmaps[0];
			LightmapSettings.lightmaps = LmData;
			LightsOff = true;
		}
		else
		{
			LightmapSettings.lightmaps = LightMapBuf;
			LightsOff = false;
		}
	}

	private GameObject[] FindEnemies()
    {
		GameObject[] MotEnemies =  GameObject.FindGameObjectsWithTag("MotionlessEnemy");
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
		InitDeathArr();
		gameObject.GetComponent<Animator>().SetInteger("KillingWalk",1);
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
			CameraEnemies[i].MoveCamera();
		IsWaiting = true;
		turnManager.EndPlayersTurn();
		yield return null;
	}

    public void Up()
    {
		Quaternion OldRotation;
		if (IsMovable == true && IsWaiting == false && Time.timeScale == 1)
		{
			if (Node.CheckIfNodeExist(transform.position, 'y', 1)
			&& VerticalLine.CheckIfThereIsLine(transform.position, 1, transform.position + new Vector3(0, 0, 1)))
			{
				OldRotation = transform.rotation;
				transform.rotation = Quaternion.Euler(0, 0, 0);
				if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
				{
					transform.rotation = OldRotation;
					IsWaiting = true;
					StartCoroutine("RotateAndKill", 0);
				}
				else if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform))
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

    public void Down()
    {
		Quaternion OldRotation;
		if (IsMovable == true && IsWaiting == false && Time.timeScale == 1)
		{
			if (Node.CheckIfNodeExist(transform.position, 'y', -1)
				 && VerticalLine.CheckIfThereIsLine(transform.position, -1, transform.position + new Vector3(0, 0, -1)))
			{
				OldRotation = transform.rotation;
				transform.rotation = Quaternion.Euler(0, 180, 0);
				if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
				{
					transform.rotation = OldRotation;
					IsWaiting = true;
					StartCoroutine("RotateAndKill", 180);
				}
				else if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform))
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

	public void Right()
    {
		Quaternion OldRotation;
		if (IsMovable == true && IsWaiting == false && Time.timeScale == 1)
		{
			if (Node.CheckIfNodeExist(transform.position, 'x', 1)
			&& HorizontalLine.CheckIfThereIsLine(transform.position, 1, transform.position + new Vector3(1, 0, 0)))
			{
				OldRotation = transform.rotation;
				transform.rotation = Quaternion.Euler(0, 90, 0);
				if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
				{
					transform.rotation = OldRotation;
					IsWaiting = true;
					StartCoroutine("RotateAndKill", 90);
				}
				else if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform))
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

	public void Left()
    {
		Quaternion OldRotation;
		if (IsMovable == true && IsWaiting == false && Time.timeScale == 1)
		{
			if (Node.CheckIfNodeExist(transform.position, 'x', -1)
			&& HorizontalLine.CheckIfThereIsLine(transform.position, -1, transform.position + new Vector3(-1, 0, 0)))
			{
				OldRotation = transform.rotation;
				transform.rotation = Quaternion.Euler(0, 270, 0);
				if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
				{
					transform.rotation = OldRotation;
					IsWaiting = true;
					StartCoroutine("RotateAndKill", 270);
				}
				else if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform))
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

    private void Awake()
    {
		IsMovable = true;
		FinalNode = GameObject.FindGameObjectWithTag("FinalNode");
		LightMapBuf = LightmapSettings.lightmaps;
		CameraEnemies = GameObject.FindObjectsOfType<CameraEnemy>();
		turnManager = turnManagerHandler.GetComponent<TurnManager>();
	}

	void Update()
	{
		Quaternion OldRotation;
		if (LightsOffTurns <= 0 && SomeLightmaps.Length != 0 && LightsOff == true)
		{
			ChangeLights();
		}
		if (IsMovable == true && IsWaiting == false && Time.timeScale == 1)
		{
			if (Input.GetKeyDown("a"))
			{
				if (Node.CheckIfNodeExist(transform.position, 'x', -1)
				&& HorizontalLine.CheckIfThereIsLine(transform.position, -1, transform.position + new Vector3(-1, 0, 0)))
				{
					OldRotation = transform.rotation;
					transform.rotation = Quaternion.Euler(0, 270, 0);
					if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
					{
						transform.rotation = OldRotation;
						IsWaiting = true;
						StartCoroutine(Rotate(270, true));
					}
					else if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform))
					{
						transform.rotation = OldRotation;
						IsWaiting = true;
						StartCoroutine(Rotate(270, false));
					}
					else
						transform.rotation = OldRotation;
				}
			}
			if (Input.GetKeyDown("d"))
			{

				if (Node.CheckIfNodeExist(transform.position, 'x', 1)
				&& HorizontalLine.CheckIfThereIsLine(transform.position, 1, transform.position + new Vector3(1, 0, 0)))
				{
					OldRotation = transform.rotation;
					transform.rotation = Quaternion.Euler(0, 90, 0);
					if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
                    {
						transform.rotation = OldRotation;
						IsWaiting = true;
						StartCoroutine(Rotate(90, true));
					}
					else if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform))
					{
						transform.rotation = OldRotation;
						IsWaiting = true;
						StartCoroutine(Rotate(90, false));
					}
					else
						transform.rotation = OldRotation;
				}
			}
			if (Input.GetKeyDown("w"))
			{
				if (Node.CheckIfNodeExist(transform.position, 'y', 1)
				&& VerticalLine.CheckIfThereIsLine(transform.position, 1, transform.position + new Vector3(0, 0, 1)))
				{
					OldRotation = transform.rotation;
					transform.rotation = Quaternion.Euler(0, 0, 0);
					if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
					{
						transform.rotation = OldRotation;
						IsWaiting = true;
						StartCoroutine(Rotate(0, true));
					}
					else if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform))
					{
						transform.rotation = OldRotation;
						IsWaiting = true;
						StartCoroutine(Rotate(0, false));
					}
					else
						transform.rotation = OldRotation;
				}
			}
			if (Input.GetKeyDown("s"))
			{
				if (Node.CheckIfNodeExist(transform.position, 'y', -1)
				&& VerticalLine.CheckIfThereIsLine(transform.position, -1, transform.position + new Vector3(0, 0, -1)))
				{
					OldRotation = transform.rotation;
					transform.rotation = Quaternion.Euler(0, 180, 0);
					if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
					{
						transform.rotation = OldRotation;
						IsWaiting = true;
						StartCoroutine(Rotate(180, true));
					}
					else if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform))
					{
						transform.rotation = OldRotation;
						IsWaiting = true;
						StartCoroutine(Rotate(180, false));
					}
					else
						transform.rotation = OldRotation;
				}
			}
			if (transform.position.x == FinalNode.transform.position.x && transform.position.z == FinalNode.transform.position.z)
				Application.LoadLevel(1);
		}
	}
}
