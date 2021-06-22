using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public GameObject NodeHandler;
	public GameObject VerLineHandler;
	public GameObject HorLineHandler;
	public GameObject LineMovingEnemyHandler;
	public GameObject MotionlessEnemyHandler;
	//public GameObject Light;
	//public bool LightsNeeded = false;
	//public GameObject[] Lights;
	//public GameObject[] LightsToChange;
	public Texture2D[] SomeLightmaps;
	//public static LightmapData[] lightmaps;

	//public GameObject animator;
	private Node NodeFuncs;
	private VerticalLine VerLineFuncs;
	private HorizontalLine HorLineFuncs;
	private LineMovingEnemy LineMovingEnemyFuncs;
	private MotionlessEnemy MotionlessEnemyFuncs;
	private float SkillReady = 0;
	private int InvisibleSteps;
	private bool KnifeIsReady = false;
	private bool IsMovable = true;
	private bool IsWaiting = false;
	private int LightsOffTurns = 0;
	private bool ProjectionIsActive = false;
	private bool FlagGranade = false;
	private LightmapData[] LightMapBuf;
	private bool LightsOff = false;
	private CameraEnemy[] CameraEnemies;

	private GameObject FinalNode;
	private static GameObject[] EnemiesTokill;
	//private bool StopTheCoroutines = false;
//private Animator animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
//LineMovingEnemy[] ListOfMovingEnemies = GameObject.FindObjectsOfType<LineMovingEnemy>();
//public bool IsMoving = false;

/*public void CheckForDestroyAgain()
{
	GameObject[] ListOfMovingEnemies = GameObject.FindGameObjectsWithTag("LineMovingEnemy");
	for (int i = 0; i < ListOfMovingEnemies.Length; i++)
	{
	//	Debug.Log(ListOfMovingEnemies[i]);
		//Debug.Log(ListOfMovingEnemies[i].isActiveAndEnabled);
		//Debug.Log(ListOfMovingEnemies[i].transform.position);
		if (transform.position.x == ListOfMovingEnemies[i].transform.position.x
		&& transform.position.z == ListOfMovingEnemies[i].transform.position.z
		&& !MotionlessEnemyFuncs.CheckIfFacing(gameObject, ListOfMovingEnemies[i]))
			Destroy(ListOfMovingEnemies[i]);
		if (MotionlessEnemyFuncs.CheckifPlayerInfrontofEnemy(gameObject, ListOfMovingEnemies[i]))
			Application.LoadLevel(0);
		if (!LineMovingEnemyFuncs.CheckIfThereIsNodeToMove(ListOfMovingEnemies[i]))
			LineMovingEnemyFuncs.TurnOtherWay(ListOfMovingEnemies[i]);
		if (transform.position.x == ListOfMovingEnemies[i].transform.position.x
		&& transform.position.z == ListOfMovingEnemies[i].transform.position.z
		&& !MotionlessEnemyFuncs.CheckIfFacing(gameObject, ListOfMovingEnemies[i]))
			Destroy(ListOfMovingEnemies[i]);
		if (MotionlessEnemyFuncs.CheckifPlayerInfrontofEnemy(gameObject, ListOfMovingEnemies[i]))
			Application.LoadLevel(0);
	}
}*/

	//private GameObject FindNode(GameObject[] Nodes, int X, int Z)
	private GameObject FindNode(Node [] Nodes, int X, int Z)
    {
        for (int i = 0; i < Nodes.Length; i++)
        {
			if (Nodes[i].transform.position.x == X && Nodes[i].transform.position.z == Z)
				return (Nodes[i].gameObject);
        }
		return (null);
    }

	public GameObject GetStairwayNodePositionY(GameObject Obj)
	//public float GetStairwayNodePositionY(GameObject Obj)
	{
		//GameObject[] Nodes = GameObject.FindGameObjectsWithTag("Node");
		//GameObject[] Nodes = NodeFuncs.FindObjectsOfType<Node>;
		Node[] Nodes = GameObject.FindObjectsOfType<Node>();
        /*for (int i = 0; i < Nodes.Length; i++)
        {
			Debug.Log(Nodes[i]);
        }*/
		GameObject DefiniteNode;
		if (Obj.transform.rotation.eulerAngles.y == 0)
        {
			DefiniteNode = FindNode(Nodes, (int)Obj.transform.position.x, (int)Obj.transform.position.z + 1);
        }
		else if(Obj.transform.rotation.eulerAngles.y == 90)

		{
			DefiniteNode = FindNode(Nodes, (int)Obj.transform.position.x + 1, (int)Obj.transform.position.z);
		}
		else if(Obj.transform.rotation.eulerAngles.y == 180)

		{
			DefiniteNode = FindNode(Nodes, (int)Obj.transform.position.x, (int)Obj.transform.position.z - 1);
		}
		else

		{
			DefiniteNode = FindNode(Nodes, (int)Obj.transform.position.x - 1, (int)Obj.transform.position.z);
		}
		return DefiniteNode;
		//if (DefiniteNode == null)
			//return -500;
		//return DefiniteNode.transform.position.y;
	}

	IEnumerator WalkUpright(int sign, GameObject Obj, GameObject Node)
    {
		float diff = Mathf.Abs(Obj.transform.position.y - Node.transform.position.y);
        for (float i = 0; i < 1; i += 0.01f)
        {
			//Obj.transform.position += new Vector3(0, 0.007f * sign, 0);
			//Obj.transform.position += new Vector3(0, Mathf.Abs(Obj.transform.position.y - Node.transform.position.y) / 100 * sign, 0);
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
		//float NodePositionY = GetStairwayNodePositionY(Obj);
		//if (NodePositionY == -500)
			//return;
		float ObjPositionY = Obj.transform.position.y;
		//Debug.Log(Mathf.Abs(NodePositionY - ObjPositionY));
		if (Mathf.Abs(NodePositionY - ObjPositionY) > 0.1f)
        {
			//Debug.Log("YES");
			if (NodePositionY > ObjPositionY)
            {
				StartCoroutine(WalkUpright(1, Obj, DefiniteNode));
            }
            else
            {
				StartCoroutine(WalkUpright(-1, Obj, DefiniteNode));
			}
        }

    }

	public bool IsThereCamera(Transform ObjCoord)
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

public bool IsThereGate(Transform ObjCoord)
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

	/*public bool stopCoroutines
	{
		get
		{
			return StopTheCoroutines;
		}
		set
		{
			StopTheCoroutines = value;
		}
	}*/

	public bool ProjectionActive
	{
		get
		{
			return ProjectionIsActive;
		}
		set
		{
			ProjectionIsActive = value;
		}
	}

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
	public bool IsflagGranade
    {
		get
        {
			return FlagGranade;
        }
        set
        {
			FlagGranade = value;
        }
    }

	public int LightOffTurns
	{
		get
		{
			return LightsOffTurns;
		}
		set
		{
			LightsOffTurns = value;
		}
	}

	public bool Waiting
    {
		get
        {
			return IsWaiting;
        }
		set
        {
			IsWaiting = value;
        }
    }


	public bool IsPlayerMovable
    { 
		get
        {
			return IsMovable;
        }
		set
        {
			IsMovable = value;
        }
	}


	public bool KnifeReady
    {
        get
        {
			return KnifeIsReady;
        }
        set
        {
			KnifeIsReady = value;
        }
    }

	public float SkillSetter
    {
		get
        {
			return SkillReady;
        }
		set
        {
			//if (SkillReady < 1 && value != 0)
			//if (value <= 1)
				SkillReady = value;
			if (SkillReady > 1)
				SkillReady = 1;
        }
	}

	public int Invisible
    {
        get
        {
			return InvisibleSteps;
        }
		set
        {
			InvisibleSteps = value;
			//if (InvisibleSteps < 0)
				//InvisibleSteps = 0;

		}
	}


	private void MoveEnemies()
    {
		//LineMovingEnemy[] ListOfMovingEnemies = GameObject.FindObjectsOfType<LineMovingEnemy>();
		GameObject[] ListOfMovingEnemies = GameObject.FindGameObjectsWithTag("LineMovingEnemy");
		for (int i = 0; i < ListOfMovingEnemies.Length; i++)
		{
			//Debug.Log(ListOfMovingEnemies[i]);
			//Debug.Log(ListOfMovingEnemies[i].isActiveAndEnabled);

			if (!LineMovingEnemyFuncs.CheckIfThereIsNodeToMove(ListOfMovingEnemies[i]))
				LineMovingEnemyFuncs.TurnOtherWay(ListOfMovingEnemies[i]);
			/*if (transform.position.x == ListOfMovingEnemies[i].transform.position.x
			&& transform.position.z == ListOfMovingEnemies[i].transform.position.z
			&& !MotionlessEnemyFuncs.CheckIfFacing(gameObject, ListOfMovingEnemies[i]))
			{
				Destroy(ListOfMovingEnemies[i]);
				//if (SkillReady < 1)
					SkillReady += 0.5f;
				if (SkillReady > 1)
					SkillReady = 1;
			}*/
			//Debug.Log(ListOfMovingEnemies[i]);
			//Debug.Log(InvisibleSteps);
			if (MotionlessEnemyFuncs.CheckifPlayerInfrontofEnemy(gameObject, ListOfMovingEnemies[i]) && InvisibleSteps <= 0)
			{
				ListOfMovingEnemies[i].transform.GetChild(0).GetComponent<Animator>().SetBool("IsKilling", true);
				//StopAllCoroutines();
				StartCoroutine("KillingAnimation", ListOfMovingEnemies[i]);
				//ListOfMovingEnemies[i].transform.GetChild(1).gameObject.SetActive(true);
				//Debug.Log(ListOfMovingEnemies[i].transform.GetChild(1));
				//ListOfMovingEnemies[i].transform.GetChild(1).
				return;
				//Debug.Log("SRAN'");
				//Application.LoadLevel(0);
			}
			if (transform.position.x == ListOfMovingEnemies[i].transform.position.x
			&& transform.position.z == ListOfMovingEnemies[i].transform.position.z
			&& (!MotionlessEnemyFuncs.CheckIfFacing(gameObject, ListOfMovingEnemies[i]) || InvisibleSteps >= 0 || LightOffTurns >= 0))
			{
				Destroy(ListOfMovingEnemies[i]);
				//return;
				//if (SkillReady < 1)
				/*SkillReady += 0.5f;
				if (SkillReady > 1)
					SkillReady = 1;*/
				//if (GameObject.FindObjectOfType<FillKnife>() != null)
					//GameObject.FindObjectOfType<FillKnife>().StartCoroutine("FillButton");
				continue;
				//Start();
				//ListOfMovingEnemies = GameObject.FindGameObjectsWithTag("LineMovingEnemy");
			}
			//Debug.Log(LineMovingEnemyFuncs);
			if (ListOfMovingEnemies[i] != null)
				LineMovingEnemyFuncs.LineMovingEnemyMove(ListOfMovingEnemies[i]);
			/*if (!LineMovingEnemyFuncs.CheckIfThereIsNodeToMove(ListOfMovingEnemies[i]))
				LineMovingEnemyFuncs.TurnOtherWay(ListOfMovingEnemies[i]);*/
			/*if (transform.position.x == ListOfMovingEnemies[i].transform.position.x
			&& transform.position.z == ListOfMovingEnemies[i].transform.position.z
			&& !MotionlessEnemyFuncs.CheckIfFacing(gameObject, ListOfMovingEnemies[i]))
				Destroy(ListOfMovingEnemies[i]);
			if (MotionlessEnemyFuncs.CheckifPlayerInfrontofEnemy(gameObject, ListOfMovingEnemies[i]))
				Application.LoadLevel(0);*/
		}
		InvisibleSteps--;
		//LightsOffTurns--;

	}

	public bool CheckIfThereIsMovingEnemy(GameObject Obj)
	{
		GameObject[] ListOfMotEnemies = GameObject.FindGameObjectsWithTag("LineMovingEnemy");
		for (int i = 0; i < ListOfMotEnemies.Length; i++)
		{
			if (LineMovingEnemyFuncs.CheckifPlayerInfrontofEnemy(ListOfMotEnemies[i], Obj))
				return true;
		}
		return false;
	}

	public bool CheckIfThereIsMotEnemy(GameObject Obj)
    {
		GameObject[] ListOfMotEnemies = GameObject.FindGameObjectsWithTag("MotionlessEnemy");
        for (int i = 0; i < ListOfMotEnemies.Length; i++)
        {
			if (LineMovingEnemyFuncs.CheckifPlayerInfrontofEnemy(ListOfMotEnemies[i], Obj))
				return true;
        }
		return false;
    }

	IEnumerator KillingAnimation(GameObject Enemy)
    {
		yield return new WaitForSeconds(0.6f);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsKilled", true);
		//gameObject.GetComponent<Animator>().SetBool("IsKilled", true);
		Enemy.transform.GetChild(1).gameObject.SetActive(true);
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene(0);
		yield return null;
	}

	private GameObject[] CheckEnemies()
	{
		GameObject[] ListOfMovingEnemies = GameObject.FindGameObjectsWithTag("LineMovingEnemy");
		GameObject[] RetArray = new GameObject[ListOfMovingEnemies.Length];
		if (EnemiesTokill == null)
			EnemiesTokill = new GameObject[ListOfMovingEnemies.Length];
		for (int i = 0; i < ListOfMovingEnemies.Length; i++)
		{
			//Debug.Log(ListOfMovingEnemies[i].transform);
			//	if (!LineMovingEnemyFuncs.CheckIfThereIsNodeToMove(ListOfMovingEnemies[i]) || IsThereGate(ListOfMovingEnemies[i].transform) || CheckIfThereIsMotEnemy(ListOfMovingEnemies[i]))
			//	LineMovingEnemyFuncs.TurnOtherWay(ListOfMovingEnemies[i]);
			if (MotionlessEnemyFuncs.CheckifPlayerInfrontofEnemy(gameObject, ListOfMovingEnemies[i]) && InvisibleSteps <= 0 && !IsThereGate(ListOfMovingEnemies[i].transform) && !IsThereCamera(ListOfMovingEnemies[i].transform) && LightOffTurns <= 0)
			{
				//Debug.Log("LOLIKS");
				ListOfMovingEnemies[i].transform.GetChild(0).GetComponent<Animator>().SetBool("IsKilling", true);
				//StopAllCoroutines();
				StartCoroutine("KillingAnimation", ListOfMovingEnemies[i]);
				//ListOfMovingEnemies[i].transform.GetChild(1).gameObject.SetActive(true);
				//Debug.Log(ListOfMovingEnemies[i].transform.GetChild(1));
				//ListOfMovingEnemies[i].transform.GetChild(1).
				return null;
				//this.enabled = false;
				//Application.LoadLevel(0);
			}
			if (transform.position.x == ListOfMovingEnemies[i].transform.position.x
			&& transform.position.z == ListOfMovingEnemies[i].transform.position.z
			&& (!MotionlessEnemyFuncs.CheckIfFacing(gameObject, ListOfMovingEnemies[i]) || InvisibleSteps >= 0 || LightOffTurns >= 0))
			{
				//animator
				//Debug.Log("AUE");
				//Debug.Log(ListOfMovingEnemies[i].transform.GetChild(0).gameObject);
				ListOfMovingEnemies[i].transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("IsDead", true);
                for (int j = 0; j < EnemiesTokill.Length; j++)
                {
					if (EnemiesTokill[j] == null)
                    {
						//Debug.Log("AA");
						EnemiesTokill[j] = ListOfMovingEnemies[i];
						break;
                    }
                }
				//EnemyTokill = ListOfMovingEnemies[i];
				//Destroy(ListOfMovingEnemies[i]);
				/*SkillReady += 0.5f;
				if (SkillReady > 1)
					SkillReady = 1;*/
				//Debug.Log("KILL");
				//if (GameObject.FindObjectOfType<FillKnife>() != null)
					//GameObject.FindObjectOfType<FillKnife>().StartCoroutine("FillButton");
				RetArray[i] = null;
				continue;
			}
			if (ListOfMovingEnemies[i] != null)
				RetArray[i] = ListOfMovingEnemies[i];
				//LineMovingEnemyFuncs.LineMovingEnemyMove(ListOfMovingEnemies[i]);
		}
		InvisibleSteps--;
		LightsOffTurns--;
		//IsWaiting = true;
		return RetArray;
	}

	IEnumerator RotateEnemies(GameObject ObjectToRotate)
	{
		int RequiredAngle = LineMovingEnemyFuncs.Opposite(ObjectToRotate);
		int playerangle = (int)ObjectToRotate.transform.rotation.eulerAngles.y;
		string EnemyTag = "LineMovingEnemy";
		//int Diff = playerangle - RequiredAngle;
		if (playerangle == 0)
		{
			if (RequiredAngle == 0)
			{
				//StartCoroutine("WalkUp");
				yield return null;
			}
			if (RequiredAngle == 90)
			{
				//GameObject.FindGameObjectWithTag(EnemyTag).GetComponent<Animator>().SetInteger("IsRotating", 1);
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 1);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 1);
				for (int i = 0; i < 30; i++)
				{
					ObjectToRotate.transform.rotation = Quaternion.Euler(0, (int)ObjectToRotate.transform.rotation.eulerAngles.y + 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 0);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				ObjectToRotate.transform.rotation = Quaternion.Euler(0, 90, 0);
				//StartCoroutine("WalkRight");
				yield return null;
			}
			if (RequiredAngle == 180)
			{
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 3);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 3);
				for (int i = 0; i < 30; i++)
				{
					ObjectToRotate.transform.rotation = Quaternion.Euler(0, (int)ObjectToRotate.transform.rotation.eulerAngles.y + 6, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 0);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				ObjectToRotate.transform.rotation = Quaternion.Euler(0, 180, 0);
				//StartCoroutine("WalkDown");
				yield return null;
			}
			if (RequiredAngle == 270)
			{
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 2);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 2);
				for (int i = 0; i < 30; i++)
				{
					ObjectToRotate.transform.rotation = Quaternion.Euler(0, (int)ObjectToRotate.transform.rotation.eulerAngles.y - 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 0);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 2);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				ObjectToRotate.transform.rotation = Quaternion.Euler(0, 270, 0);
				//StartCoroutine("WalkLeft");
				yield return null;
			}
		}
		if (playerangle == 90)
		{
			if (RequiredAngle == 90)
			{
				//StartCoroutine("WalkRight");
				yield return null;
			}
			if (RequiredAngle == 0)
			{
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 2);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 2);
				for (int i = 0; i < 30; i++)
				{
					ObjectToRotate.transform.rotation = Quaternion.Euler(0, (int)ObjectToRotate.transform.rotation.eulerAngles.y - 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 0);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				ObjectToRotate.transform.rotation = Quaternion.Euler(0, 0, 0);
				//StartCoroutine("WalkUp");
				yield return null;
			}
			if (RequiredAngle == 180)
			{
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 1);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 1);
				for (int i = 0; i < 30; i++)
				{
					ObjectToRotate.transform.rotation = Quaternion.Euler(0, (int)ObjectToRotate.transform.rotation.eulerAngles.y + 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 0);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				ObjectToRotate.transform.rotation = Quaternion.Euler(0, 180, 0);
				//StartCoroutine("WalkDown");
				yield return null;
			}
			if (RequiredAngle == 270)
			{
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 3);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 3);
				for (int i = 0; i < 30; i++)
				{
					ObjectToRotate.transform.rotation = Quaternion.Euler(0, (int)ObjectToRotate.transform.rotation.eulerAngles.y - 6, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 0);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				ObjectToRotate.transform.rotation = Quaternion.Euler(0, 270, 0);
				//StartCoroutine("WalkLeft");
				yield return null;
			}
		}
		if (playerangle == 180)
		{
			//Debug.Log("aaa");
			if (RequiredAngle == 180)
			{
				//StartCoroutine("WalkDown");
				yield return null;
			}
			if (RequiredAngle == 0)
			{
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 3);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 3);
				for (int i = 0; i < 30; i++)
				{
					ObjectToRotate.transform.rotation = Quaternion.Euler(0, (int)ObjectToRotate.transform.rotation.eulerAngles.y + 6, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 0);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				ObjectToRotate.transform.rotation = Quaternion.Euler(0, 0, 0);
				//StartCoroutine("WalkUp");
				yield return null;
			}
			if (RequiredAngle == 90)
			{
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 2);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 2);
				for (int i = 0; i < 30; i++)
				{
					ObjectToRotate.transform.rotation = Quaternion.Euler(0, (int)ObjectToRotate.transform.rotation.eulerAngles.y - 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 0);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				ObjectToRotate.transform.rotation = Quaternion.Euler(0, 90, 0);
				//StartCoroutine("WalkRight");
				yield return null;
			}
			if (RequiredAngle == 270)
			{
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 1);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 1);
				for (int i = 0; i < 30; i++)
				{
					ObjectToRotate.transform.rotation = Quaternion.Euler(0, (int)ObjectToRotate.transform.rotation.eulerAngles.y + 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 0);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				ObjectToRotate.transform.rotation = Quaternion.Euler(0, 270, 0);
				//StartCoroutine("WalkLeft");
				yield return null;
			}
		}
		if (playerangle == 270)
		{
			//Debug.Log("aaa");
			if (RequiredAngle == 270)
			{
				//StartCoroutine("WalkLeft");
				yield return null;
			}
			if (RequiredAngle == 0)
			{
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 1);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 1);
				for (int i = 0; i < 30; i++)
				{
					ObjectToRotate.transform.rotation = Quaternion.Euler(0, (int)ObjectToRotate.transform.rotation.eulerAngles.y + 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 0);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				ObjectToRotate.transform.rotation = Quaternion.Euler(0, 0, 0);
				//StartCoroutine("WalkUp");
				yield return null;
			}
			if (RequiredAngle == 90)
			{
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 3);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 3);
				for (int i = 0; i < 30; i++)
				{
					ObjectToRotate.transform.rotation = Quaternion.Euler(0, (int)ObjectToRotate.transform.rotation.eulerAngles.y - 6, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 0);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				ObjectToRotate.transform.rotation = Quaternion.Euler(0, 90, 0);
				//StartCoroutine("WalkRight");
				yield return null;
			}
			if (RequiredAngle == 180)
			{
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 2);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 2);
				for (int i = 0; i < 30; i++)
				{
					ObjectToRotate.transform.rotation = Quaternion.Euler(0, (int)ObjectToRotate.transform.rotation.eulerAngles.y - 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				//ObjectToRotate.GetComponent<Animator>().SetInteger("IsRotating", 0);
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				ObjectToRotate.transform.rotation = Quaternion.Euler(0, 180, 0);
				//StartCoroutine("WalkDown");
				yield return null;
			}
		}
		//Debug.Log(Diff);
		yield return null;
		/* for (int i = 0; i < length; i++)
		 {

		 }*/
	}

	IEnumerator Rotate(int RequiredAngle)
    {
		int playerangle = (int)gameObject.transform.rotation.eulerAngles.y;
		int Diff = playerangle - RequiredAngle;
		if (playerangle == 0)
        {
			if (RequiredAngle == 0)
			{
				StartCoroutine("WalkUp");
				yield return null;
			}
			if (RequiredAngle == 90)
            {
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 1);
                for (int i = 0; i < 30; i++)
                {
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y + 3, 0);
					yield return new WaitForSeconds(0.0133f);
                }
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
				StartCoroutine("WalkRight");
				yield return null;
			}
			if (RequiredAngle == 180)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 3);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y + 6, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
				StartCoroutine("WalkDown");
				yield return null;
			}
			if (RequiredAngle == 270)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 2);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y - 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
				StartCoroutine("WalkLeft");
				yield return null;
			}
		}
		if (playerangle == 90)
		{
			if (RequiredAngle == 90)
			{
				StartCoroutine("WalkRight");
				yield return null;
			}
			if (RequiredAngle == 0)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 2);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y - 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
				StartCoroutine("WalkUp");
				yield return null;
			}
			if (RequiredAngle == 180)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 1);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y + 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
				StartCoroutine("WalkDown");
				yield return null;
			}
			if (RequiredAngle == 270)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 3);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y - 6, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
				StartCoroutine("WalkLeft");
				yield return null;
			}
		}
		if (playerangle == 180)
		{
			//Debug.Log("aaa");
			if (RequiredAngle == 180)
			{
				StartCoroutine("WalkDown");
				yield return null;
			}
			if (RequiredAngle == 0)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 3);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y + 6, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
				StartCoroutine("WalkUp");
				yield return null;
			}
			if (RequiredAngle == 90)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 2);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y - 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
				StartCoroutine("WalkRight");
				yield return null;
			}
			if (RequiredAngle == 270)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 1);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y +3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
				StartCoroutine("WalkLeft");
				yield return null;
			}
		}
		if (playerangle == 270)
		{
			//Debug.Log("aaa");
			if (RequiredAngle == 270)
			{
				StartCoroutine("WalkLeft");
				yield return null;
			}
			if (RequiredAngle == 0)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 1);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y + 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
				StartCoroutine("WalkUp");
				yield return null;
			}
			if (RequiredAngle == 90)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 3);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y - 6, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
				StartCoroutine("WalkRight");
				yield return null;
			}
			if (RequiredAngle == 180)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 2);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y - 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
				StartCoroutine("WalkDown");
				yield return null;
			}
		}
		//Debug.Log(Diff);
		yield return null;
       /* for (int i = 0; i < length; i++)
        {

        }*/
    }

	IEnumerator WalkLeft()
	{
		CheckIfThereIsStairway(gameObject);
		GameObject[] ListOfEnemies;
		//animator.SetBool("IsRunning", true);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);

		for (float i = 0; i < 1; i += 0.01f)
		{
			transform.position += new Vector3(-0.01f, 0, 0);
			yield return new WaitForSeconds(0.004f);
		}
		//animator.SetBool("IsRunning", false);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", false);
		transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
		//MoveEnemies();
		ListOfEnemies = CheckEnemies();
		for (int i = 0; i < CameraEnemies.Length; i++)
		{
			CameraEnemies[i].MoveCamera();
		}
		IsWaiting = true;
		if (LightOffTurns <= 0 && ListOfEnemies != null)
			yield return LineMovingEnemyFuncs.StartCoroutine("LineMovingEnemyWalk2", ListOfEnemies);
			//yield return StartCoroutine("RotateEnemies", 
		else
			IsWaiting = false;
		//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
		//yield return null;
		//yield return new WaitForSeconds(2);
	}

	IEnumerator WalkRight()
    {
		CheckIfThereIsStairway(gameObject);
		GameObject[] ListOfEnemies;
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
		for (float i = 0;  i < 1; i += 0.01f)
        {
			transform.position += new Vector3(0.01f, 0, 0);
			yield return new WaitForSeconds(0.004f);
        }
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", false);
		transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
		//MoveEnemies();
		ListOfEnemies = CheckEnemies();
		for (int i = 0; i < CameraEnemies.Length; i++)
		{
			CameraEnemies[i].MoveCamera();
		}
		IsWaiting = true;
		if (LightOffTurns <= 0 && ListOfEnemies != null)
			yield return LineMovingEnemyFuncs.StartCoroutine("LineMovingEnemyWalk2", ListOfEnemies);
		else
			IsWaiting = false;
		//yield return null;
	}

	IEnumerator WalkUp()
	{
		CheckIfThereIsStairway(gameObject);
		GameObject[] ListOfEnemies;
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
		for (float i = 0; i < 1; i += 0.01f)
		{
			transform.position += new Vector3(0, 0, 0.01f);
			yield return new WaitForSeconds(0.004f);
		}
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", false);
		transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
		//MoveEnemies();
		//IsMoving = false;
		ListOfEnemies = CheckEnemies();
		for (int i = 0; i < CameraEnemies.Length; i++)
		{
			CameraEnemies[i].MoveCamera();
		}
		IsWaiting = true;
		if (LightOffTurns <= 0 && ListOfEnemies != null)
			yield return LineMovingEnemyFuncs.StartCoroutine("LineMovingEnemyWalk2", ListOfEnemies);
		else
			IsWaiting = false;
		//yield return new WaitForSeconds(2);
		//yield return LineMovingEnemyFuncs.LineMovingEnemyWalk()
	}



	public void ChangeLights()
	{
		/*if (Lights[0].activeSelf == true)
		{
			for (int i = 0; i < Lights.Length; i++)
			{
				Lights[i].SetActive(false);
			}
			for (int i = 0; i < LightsToChange.Length; i++)
			{
				LightsToChange[i].SetActive(true);
			}
		}
		else
		{
			for (int i = 0; i < Lights.Length; i++)
			{
				Lights[i].SetActive(true);
			}
			for (int i = 0; i < LightsToChange.Length; i++)
			{
				LightsToChange[i].SetActive(false);
			}
		}*/
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
			//Debug.Log("ueeeee");
			/*LmData[0] = LightmapSettings.lightmaps[0];
			LmData[0].lightmapDir = SomeLightmaps[2];
			LmData[0].lightmapColor = SomeLightmaps[3];
			LightmapSettings.lightmaps = LmData;
			LightsOff = false;*/
			LightmapSettings.lightmaps = LightMapBuf;
			LightsOff = false;
		}
	}

	IEnumerator WalkDown()
	{
		CheckIfThereIsStairway(gameObject);
		GameObject[] ListOfEnemies;
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
		for (float i = 0; i < 1; i += 0.01f)
		{
			transform.position += new Vector3(0, 0, -0.01f);
			yield return new WaitForSeconds(0.004f);
		}
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", false);
		transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
		//MoveEnemies();
		//yield return null;
		ListOfEnemies = CheckEnemies();
		for (int i = 0; i < CameraEnemies.Length; i++)
		{
			CameraEnemies[i].MoveCamera();
		}
		IsWaiting = true;
		if (LightOffTurns <= 0 && ListOfEnemies != null)
			yield return LineMovingEnemyFuncs.StartCoroutine("LineMovingEnemyWalk2", ListOfEnemies);
		else
			IsWaiting = false;

	}

	IEnumerator RotateAndKill(int RequiredAngle)
	{
		int playerangle = (int)gameObject.transform.rotation.eulerAngles.y;
		int Diff = playerangle - RequiredAngle;
		if (playerangle == 0)
		{
			if (RequiredAngle == 0)
			{
				StartCoroutine("KillEnemy");
				yield return null;
			}
			if (RequiredAngle == 90)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 1);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y + 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
				StartCoroutine("KillEnemy");
				yield return null;
			}
			if (RequiredAngle == 180)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 3);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y + 6, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
				StartCoroutine("KillEnemy");
				yield return null;
			}
			if (RequiredAngle == 270)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 2);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y - 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
				StartCoroutine("KillEnemy");
				yield return null;
			}
		}
		if (playerangle == 90)
		{
			if (RequiredAngle == 90)
			{
				StartCoroutine("KillEnemy");
				yield return null;
			}
			if (RequiredAngle == 0)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 2);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y - 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
				StartCoroutine("KillEnemy");
				yield return null;
			}
			if (RequiredAngle == 180)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 1);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y + 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
				StartCoroutine("KillEnemy");
				yield return null;
			}
			if (RequiredAngle == 270)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 3);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y - 6, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
				StartCoroutine("KillEnemy");
				yield return null;
			}
		}
		if (playerangle == 180)
		{
			//Debug.Log("aaa");
			if (RequiredAngle == 180)
			{
				StartCoroutine("KillEnemy");
				yield return null;
			}
			if (RequiredAngle == 0)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 3);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y + 6, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
				StartCoroutine("KillEnemy");
				yield return null;
			}
			if (RequiredAngle == 90)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 2);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y - 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
				StartCoroutine("KillEnemy");
				yield return null;
			}
			if (RequiredAngle == 270)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 1);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y + 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
				StartCoroutine("KillEnemy");
				yield return null;
			}
		}
		if (playerangle == 270)
		{
			//Debug.Log("aaa");
			if (RequiredAngle == 270)
			{
				StartCoroutine("KillEnemy");
				yield return null;
			}
			if (RequiredAngle == 0)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 1);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y + 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
				StartCoroutine("KillEnemy");
				yield return null;
			}
			if (RequiredAngle == 90)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 3);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y - 6, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
				StartCoroutine("KillEnemy");
				yield return null;
			}
			if (RequiredAngle == 180)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 2);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y - 3, 0);
					yield return new WaitForSeconds(0.0133f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
				StartCoroutine("KillEnemy");
				yield return null;
			}
		}
		//Debug.Log(Diff);
		yield return null;
		/* for (int i = 0; i < length; i++)
		 {

		 }*/
	}


	private GameObject[] FindEnemies()
    {
		GameObject[] MotEnemies =  GameObject.FindGameObjectsWithTag("MotionlessEnemy");
		GameObject[] MovingEnemies = GameObject.FindGameObjectsWithTag("LineMovingEnemy");
		GameObject[] Enemies = new GameObject[MotEnemies.Length + MovingEnemies.Length];
		Vector3 NewPos = gameObject.transform.position + gameObject.transform.forward;
		//Debug.Log(NewPos);
		for (int i = 0; i < MotEnemies.Length; i++)
        {
			if (Mathf.Round(NewPos.x) == MotEnemies[i].transform.position.x && Mathf.Round(NewPos.z) == MotEnemies[i].transform.position.z)
            {
				//Debug.Log("aa");
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
        /*for (int i = 0; i < KilledEnemies.Length; i++)
        {
			Debug.Log(KilledEnemies[i]);
        }*/
		CheckIfThereIsStairway(gameObject);
		GameObject[] ListOfEnemies;
		ListOfEnemies = CheckEnemies();
		//////////
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
				//KilledEnemies[i].GetComponent<Animator>().SetBool("IsDead", true);
				//KilledEnemies[i].GetComponent<Animator>().
				for (int j = 0; j < EnemiesKill.Length; j++)
				{

					if (EnemiesTokill[j] == null)
					{
						EnemiesTokill[j] = KilledEnemies[i];
						//FindObjectOfType<AudioManager>().Play("Kill");
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
		//MoveEnemies();
		//yield return null;
		//ListOfEnemies = CheckEnemies();
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
	}

    public void Up()
    {
		Quaternion OldRotation;
		if (IsMovable == true && IsWaiting == false && Time.timeScale == 1 && ProjectionIsActive == false)
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

    public void Down()
    {
		Quaternion OldRotation;
		if (IsMovable == true && IsWaiting == false && Time.timeScale == 1 && ProjectionIsActive == false)
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

	public void Right()
    {
		Quaternion OldRotation;
		if (IsMovable == true && IsWaiting == false && Time.timeScale == 1 && ProjectionIsActive == false)
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

	public void Left()
    {
		Quaternion OldRotation;
		if (IsMovable == true && IsWaiting == false && Time.timeScale == 1 && ProjectionIsActive == false)
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

	// Start is called before the first frame update
	void Start()
	{
		NodeFuncs = NodeHandler.GetComponent<Node>();
		VerLineFuncs = VerLineHandler.GetComponent<VerticalLine>();
		HorLineFuncs = HorLineHandler.GetComponent<HorizontalLine>();
		LineMovingEnemyFuncs = LineMovingEnemyHandler.GetComponent<LineMovingEnemy>();
		MotionlessEnemyFuncs = MotionlessEnemyHandler.GetComponent<MotionlessEnemy>();
		FinalNode = GameObject.FindGameObjectWithTag("FinalNode");
		LightMapBuf = LightmapSettings.lightmaps;
		//CameraEnemies = GameObject.FindGameObjectsWithTag("CameraEnemy");
		CameraEnemies = GameObject.FindObjectsOfType<CameraEnemy>();
		//Debug.Log(LineMovingEnemyFuncs);
		//LineMovingEnemyFuncs = LineMovingEnemyHandler.GetComponentInParent<LineMovingEnemy>();
		//FindObjectOfType<AudioManager>().Play("Kill");
	}

	// Update is called once per frame
	void Update()
	{
		/*if (StopTheCoroutines == true)
		{
			StopTheCoroutines = false;
			StopAllCoroutines();
		}*/

		//FindObjectOfType<AudioManager>().Play("Theme");
		//Time.timeScale = 0.1f;
		//Debug.Log(gameObject);
		Quaternion OldRotation;
		/*if (LightOffTurns <= 0 && Lights.Length != 0 && Lights[0].activeSelf == false)
		{
			ChangeLights();
		}*/
		if (LightOffTurns <= 0 && SomeLightmaps.Length != 0 && LightsOff == true)
		{
			ChangeLights();
		}
		//Light.SetActive(true);
		//Debug.Log(IsMovable);
		//Debug.Log(IsWaiting);
		//if (IsMovable == true && IsWaiting == false && ProjectionIsActive == false)
		if (IsMovable == true && IsWaiting == false && Time.timeScale == 1 && ProjectionIsActive == false)
		{
			//Debug.Log("aa");
			if (Input.GetKeyDown("a"))
			{
				//transform.position = FinalPos;
				//Debug.Log(transform.position);

				if (NodeFuncs.CheckIfNodeExist(transform.position, 'x', -1)
				&& HorLineFuncs.CheckIfThereIsLine(transform.position, -1, transform.position + new Vector3(-1, 0, 0)))
				{
					//IsMoving = true;
					OldRotation = transform.rotation;
					
					transform.rotation = Quaternion.Euler(0, 270, 0);
					if (!IsThereGate(transform) && !IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
					{
						//Debug.Log("HERE");
						//gameObject.GetComponent<Animator>().SetBool("IsKilling", true);
						transform.rotation = OldRotation;
						//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 2);
						IsWaiting = true;
						StartCoroutine("RotateAndKill", 270);
						//StartCoroutine("KillEnemy");
					}
					else if (!IsThereGate(transform) && !IsThereCamera(transform))
					{
						transform.rotation = OldRotation;
						//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 2);
						IsWaiting = true;
						StartCoroutine("Rotate", 270);
						/*for (int i = 0; i < CameraEnemies.Length; i++)
						{
							CameraEnemies[i].MoveCamera();
						}*/
						//CameraEnemy.MoveCamera();
						//StartCoroutine("WalkLeft");
					}
					else
						transform.rotation = OldRotation;
					//IsWaiting = true;
					//StartCoroutine("WalkLeft");
					//return;
					//CheckForDestroyAgain();
					//MoveEnemies();
					//LineMovingEnemyFuncs.LineMovingEnemyMove();
					//transform.position += new Vector3(-1, 0, 0);
				}
				//IsMoving = false;
			}
			if (Input.GetKeyDown("d"))
			{

				if (NodeFuncs.CheckIfNodeExist(transform.position, 'x', 1)
				&& HorLineFuncs.CheckIfThereIsLine(transform.position, 1, transform.position + new Vector3(1, 0, 0)))
				{
					//IsMoving = true;
					OldRotation = transform.rotation;
					transform.rotation = Quaternion.Euler(0, 90, 0);
					//IsWaiting = true;
					//FinalPos = transform.position + new Vector3(1, 0, 0);
					//StartCoroutine("WalkRight");
					if (!IsThereGate(transform) && !IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
                    {
						//Debug.Log("HERE");
						//gameObject.GetComponent<Animator>().SetBool("IsKilling", true);
						//StartCoroutine("KillEnemy");
						transform.rotation = OldRotation;
						//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 2);
						IsWaiting = true;
						StartCoroutine("RotateAndKill", 90);
					}
					else if (!IsThereGate(transform) && !IsThereCamera(transform))
					{
						transform.rotation = OldRotation;
						IsWaiting = true;
						StartCoroutine("Rotate", 90);
						//StartCoroutine("WalkRight");
					}
					else
						transform.rotation = OldRotation;
					//return;
					//CheckForDestroyAgain();
					//MoveEnemies();
					//LineMovingEnemyFuncs.LineMovingEnemyMove();
					//StopCoroutine("Walk");
					//Debug.Log(transform.position);
					//Walk();
					//transform.position += new Vector3(1, 0, 0);
				}
				//IsMoving = false;
			}
			if (Input.GetKeyDown("w"))
			{
				//IsThereGate(transform);
				if (NodeFuncs.CheckIfNodeExist(transform.position, 'y', 1)
				&& VerLineFuncs.CheckIfThereIsLine(transform.position, 1, transform.position + new Vector3(0, 0, 1)))
				{
					//IsMoving = true;
					OldRotation = transform.rotation;
					transform.rotation = Quaternion.Euler(0, 0, 0);
					if (!IsThereGate(transform) && !IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
					{
						//Debug.Log("HERE");
						//gameObject.GetComponent<Animator>().SetBool("IsKilling", true);
						//StartCoroutine("KillEnemy");
						transform.rotation = OldRotation;
						//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 2);
						IsWaiting = true;
						StartCoroutine("RotateAndKill", 0);
					}
					else if (!IsThereGate(transform) && !IsThereCamera(transform))
					{
						transform.rotation = OldRotation;
						IsWaiting = true;
						StartCoroutine("Rotate", 0);

						//StartCoroutine("WalkUp");
					}
					else
						transform.rotation = OldRotation;
					//IsWaiting = true;
					//StartCoroutine("WalkUp");
					//return;
					//CheckForDestroyAgain();
					//MoveEnemies();
					//LineMovingEnemyFuncs.LineMovingEnemyMove();
					//transform.position += new Vector3(0, 0, 1);
				}
				//IsMoving = false;
			}
			if (Input.GetKeyDown("s"))
			{

				if (NodeFuncs.CheckIfNodeExist(transform.position, 'y', -1)
				&& VerLineFuncs.CheckIfThereIsLine(transform.position, -1, transform.position + new Vector3(0, 0, -1)))
				{
					//IsMoving = true;
					OldRotation = transform.rotation;
					transform.rotation = Quaternion.Euler(0, 180, 0);
					if (!IsThereGate(transform) && !IsThereCamera(transform) && (CheckIfThereIsMotEnemy(gameObject) || CheckIfThereIsMovingEnemy(gameObject)))
					{
						//Debug.Log("HERE");
						//gameObject.GetComponent<Animator>().SetBool("IsKilling", true);
						//StartCoroutine("KillEnemy");
						transform.rotation = OldRotation;
						//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 2);
						IsWaiting = true;
						StartCoroutine("RotateAndKill", 180);
					}
					else if (!IsThereGate(transform) && !IsThereCamera(transform))
					{
						transform.rotation = OldRotation;
						IsWaiting = true;
						StartCoroutine("Rotate", 180);
						
						//StartCoroutine("WalkDown");
					}
					else
						transform.rotation = OldRotation;
					//return;
					//CheckForDestroyAgain();
					//MoveEnemies();
					//LineMovingEnemyFuncs.LineMovingEnemyMove();
					//transform.position += new Vector3(0, 0, -1);
				}
				//IsMoving = false;
			}
			if (transform.position.x == FinalNode.transform.position.x && transform.position.z == FinalNode.transform.position.z)
				Application.LoadLevel(1);
		}
	}
}
