using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject NodeHandler;
	public GameObject VerLineHandler;
	public GameObject HorLineHandler;
	public GameObject LineMovingEnemyHandler;
	public GameObject MotionlessEnemyHandler;
	public GameObject Light;
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

	private GameObject FinalNode;

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

	public bool IsThereGate(Transform ObjCoord)
    {
		//Debug.Log(ObjCoord);
		Object[] Gates = GameObject.FindGameObjectsWithTag("Gate");
		RaycastHit Hit;
		//Ray ray = new Ray(ObjCoord, ObjCoord);
		//Ray ray;
		/*if (ObjCoord.rotation.eulerAngles.y == 0)
			ray = new Ray(ObjCoord.position, ObjCoord.transform.forward);
		if (ObjCoord.rotation.eulerAngles.y == 180)
			ray = new Ray(ObjCoord.position, ObjCoord.transform.forward);
		if (ObjCoord.rotation.eulerAngles.y == 270)
			ray = new Ray(ObjCoord.position, ObjCoord.transform.right);
		else
			ray = new Ray(ObjCoord.position, ObjCoord.transform.right);*/
		//Debug.Log(ObjCoord.position);
		//Ray ray = new Ray(ObjCoord.position, ObjCoord.transform.forward);
		Ray ray = new Ray(ObjCoord.position, ObjCoord.forward);
		//Debug.DrawRay(ObjCoord.position, ObjCoord.forward * 100, Color.yellow);
		//Ray ray = new Ray(ObjCoord.position, ObjCoord.TransformDirection(Vector3.forward));
		//Ray ray = new Ray(ObjCoord.position, Vector3.up, out Hit, 10);
        //Ray ray = new Ray(ObjCoord.position, ObjCoord.right);
        //Ray ray = new Ray(ObjCoord.position, ObjCoord.transform.right);
        //if (ObjCoord.rotation.eulerAngles.y == 0)
        //Ray ray = new Ray(ObjCoord.position, ObjCoord.transform.forward);
        //else
        //ray = 
        //Debug.DrawRay(ObjCoord.position, ObjCoord.TransformDirection(Vector3.forward) * 1000, Color.red);
		
		Physics.Raycast(ray, out Hit, 1);
		//Debug.DrawLine(ray.origin, Hit.point);
		//Debug.DrawRay(ObjCoord.position, ObjCoord.transform.forward * 20, Color.red, 10);
		//Physics.Raycast(ObjCoord.position, Vector3.up, out Hit, 10);
		//Debug.DrawRay(ObjCoord.position, ObjCoord.transform.forward * 3, Color.red, 10);
		//Debug.Log(Hit.collider.gameObject);
		//Debug.Log(Hit.collider);
		if (Hit.collider != null)
        {
			//string a;
			//a =  Hit.collider.ToString;
			//Debug.Log(Hit.collider);
            for (int i = 0; i < Gates.Length; i++)
            {
				//Debug.Log(Hit.collider.gameObject);
				//Debug.Log(Hit.collider);
				//Debug.Log(Gates[i]);
				//if (Hit.collider == Gates[i])
				if (Hit.collider.gameObject == Gates[i])
				{
					//Debug.Log("yess");
					return true;
				}
            }
        }
		//if (Hit.collider.ToString == "fence_gate";
		return false;
    }

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
				Application.LoadLevel(0);
			if (transform.position.x == ListOfMovingEnemies[i].transform.position.x
			&& transform.position.z == ListOfMovingEnemies[i].transform.position.z
			&& (!MotionlessEnemyFuncs.CheckIfFacing(gameObject, ListOfMovingEnemies[i]) || InvisibleSteps >= 0 || LightOffTurns >= 0))
			{
				Destroy(ListOfMovingEnemies[i]);
				//return;
				//if (SkillReady < 1)
				SkillReady += 0.5f;
				if (SkillReady > 1)
					SkillReady = 1;
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

	private GameObject[] CheckEnemies()
	{
		GameObject[] ListOfMovingEnemies = GameObject.FindGameObjectsWithTag("LineMovingEnemy");
		GameObject[] RetArray = new GameObject[ListOfMovingEnemies.Length];
		for (int i = 0; i < ListOfMovingEnemies.Length; i++)
		{
			//Debug.Log(ListOfMovingEnemies[i].transform);
			if (!LineMovingEnemyFuncs.CheckIfThereIsNodeToMove(ListOfMovingEnemies[i]) || IsThereGate(ListOfMovingEnemies[i].transform) || CheckIfThereIsMotEnemy(ListOfMovingEnemies[i]))
				LineMovingEnemyFuncs.TurnOtherWay(ListOfMovingEnemies[i]);
			if (MotionlessEnemyFuncs.CheckifPlayerInfrontofEnemy(gameObject, ListOfMovingEnemies[i]) && InvisibleSteps <= 0 && !IsThereGate(ListOfMovingEnemies[i].transform) && LightOffTurns <= 0)
				Application.LoadLevel(0);
			if (transform.position.x == ListOfMovingEnemies[i].transform.position.x
			&& transform.position.z == ListOfMovingEnemies[i].transform.position.z
			&& (!MotionlessEnemyFuncs.CheckIfFacing(gameObject, ListOfMovingEnemies[i]) || InvisibleSteps >= 0 || LightOffTurns >= 0))
			{
				Destroy(ListOfMovingEnemies[i]);
				SkillReady += 0.5f;
				if (SkillReady > 1)
					SkillReady = 1;
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

	IEnumerator WalkLeft()
	{
		GameObject[] ListOfEnemies;
		for (float i = 0; i < 1; i += 0.2f)
		{
			transform.position += new Vector3(-0.2f, 0, 0);
			yield return new WaitForSeconds(0.1f);
		}
		transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
		//MoveEnemies();
		ListOfEnemies = CheckEnemies();
		IsWaiting = true;
		if (LightOffTurns <= 0)
			yield return LineMovingEnemyFuncs.StartCoroutine("LineMovingEnemyWalk2", ListOfEnemies);
		else
			IsWaiting = false;
		//yield return null;
		//yield return new WaitForSeconds(2);
	}

	IEnumerator WalkRight()
    {
		GameObject[] ListOfEnemies;
		for (float i = 0;  i < 1; i += 0.2f)
        {
			transform.position += new Vector3(0.2f, 0, 0);
			yield return new WaitForSeconds(0.1f);
        }
		transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
		//MoveEnemies();
		ListOfEnemies = CheckEnemies();
		IsWaiting = true;
		if (LightOffTurns <= 0)
			yield return LineMovingEnemyFuncs.StartCoroutine("LineMovingEnemyWalk2", ListOfEnemies);
		else
			IsWaiting = false;
		//yield return null;
	}

	IEnumerator WalkUp()
	{
		GameObject[] ListOfEnemies;
		for (float i = 0; i < 1; i += 0.2f)
		{
			transform.position += new Vector3(0, 0, 0.2f);
			yield return new WaitForSeconds(0.1f);
		}
		transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
		//MoveEnemies();
		//IsMoving = false;
		ListOfEnemies = CheckEnemies();
		IsWaiting = true;
		if (LightOffTurns <= 0)
			yield return LineMovingEnemyFuncs.StartCoroutine("LineMovingEnemyWalk2", ListOfEnemies);
		else
			IsWaiting = false;
		//yield return new WaitForSeconds(2);
		//yield return LineMovingEnemyFuncs.LineMovingEnemyWalk()
	}

	IEnumerator WalkDown()
	{
		GameObject[] ListOfEnemies;
		for (float i = 0; i < 1; i += 0.2f)
		{
			transform.position += new Vector3(0, 0, -0.2f);
			yield return new WaitForSeconds(0.1f);
		}
		transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
		//MoveEnemies();
		//yield return null;
		ListOfEnemies = CheckEnemies();
		IsWaiting = true;
		if (LightOffTurns <= 0)
			yield return LineMovingEnemyFuncs.StartCoroutine("LineMovingEnemyWalk2", ListOfEnemies);
		else
			IsWaiting = false;
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
		//Debug.Log(LineMovingEnemyFuncs);
		//LineMovingEnemyFuncs = LineMovingEnemyHandler.GetComponentInParent<LineMovingEnemy>();
		
	}

	// Update is called once per frame
	void Update()
	{
		Quaternion OldRotation;
		if (LightOffTurns <= 0 && Light.activeSelf == false)
			Light.SetActive(true);
		if (IsMovable == true && IsWaiting == false && Time.timeScale == 1 && ProjectionIsActive == false)
		{
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
					if (!IsThereGate(transform))
					{
						IsWaiting = true;
						StartCoroutine("WalkLeft");
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
					if (!IsThereGate(transform))
					{
						IsWaiting = true;
						StartCoroutine("WalkRight");
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
					if (!IsThereGate(transform))
					{
						IsWaiting = true;
						StartCoroutine("WalkUp");
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
					if (!IsThereGate(transform))
					{
						IsWaiting = true;
						StartCoroutine("WalkDown");
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
				Application.LoadLevel(0);
		}
	}
}
