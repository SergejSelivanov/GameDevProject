using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionBehaviour : MonoBehaviour
{
	public GameObject PlayerHandler;
	public GameObject NodeHandler;
	public GameObject VerLineHandler;
	public GameObject HorLineHandler;
	public GameObject LineMovingEnemyHandler;
	public GameObject MotionlessEnemyHandler;

	private Player PlayerFuncs;
	private Node NodeFuncs;
	private VerticalLine VerLineFuncs;
	private HorizontalLine HorLineFuncs;
	private LineMovingEnemy LineMovingEnemyFuncs;
	private MotionlessEnemy MotionlessEnemyFuncs;
	private bool IsWaiting = false;

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
			if (!LineMovingEnemyFuncs.CheckIfThereIsNodeToMove(ListOfMovingEnemies[i]) || PlayerFuncs.IsThereGate(ListOfMovingEnemies[i].transform) || CheckIfThereIsMotEnemy(ListOfMovingEnemies[i]))
				LineMovingEnemyFuncs.TurnOtherWay(ListOfMovingEnemies[i]);
			//Debug.Log(MotionlessEnemyFuncs.CheckifPlayerInfrontofEnemy(gameObject, ListOfMovingEnemies[i]));
			//Debug.Log(MotionlessEnemyFuncs.CheckifPlayerInfrontofEnemy(ListOfMovingEnemies[i], gameObject));
			//Debug.Log(MotionlessEnemyFuncs.CheckifPlayerInfrontofEnemy(gameObject, ListOfMovingEnemies[i]));
			if (MotionlessEnemyFuncs.CheckifPlayerInfrontofEnemy(gameObject, ListOfMovingEnemies[i]) && !PlayerFuncs.IsThereGate(ListOfMovingEnemies[i].transform) && PlayerFuncs.LightOffTurns <= 0)
            //Application.LoadLevel(0);
            {
				//Destroy(gameObject);
				gameObject.SetActive(false);
				PlayerFuncs.ProjectionActive = false;
            }
			if (transform.position.x == ListOfMovingEnemies[i].transform.position.x
			&& transform.position.z == ListOfMovingEnemies[i].transform.position.z
			&& (!MotionlessEnemyFuncs.CheckIfFacing(gameObject, ListOfMovingEnemies[i]) || PlayerFuncs.LightOffTurns >= 0))
			{
				Destroy(ListOfMovingEnemies[i]);
				PlayerFuncs.SkillSetter += 0.5f;
				if (PlayerFuncs.SkillSetter > 1)
					PlayerFuncs.SkillSetter = 1;
				RetArray[i] = null;
				continue;
			}
			if (ListOfMovingEnemies[i] != null)
				RetArray[i] = ListOfMovingEnemies[i];
			//LineMovingEnemyFuncs.LineMovingEnemyMove(ListOfMovingEnemies[i]);
		}
		PlayerFuncs.Invisible--;
		PlayerFuncs.LightOffTurns--;
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
		if (PlayerFuncs.LightOffTurns <= 0)
			yield return LineMovingEnemyFuncs.StartCoroutine("LineMovingEnemyWalk2", ListOfEnemies);
		else
			IsWaiting = false;
		//yield return null;
		//yield return new WaitForSeconds(2);
	}

	IEnumerator WalkRight()
	{
		GameObject[] ListOfEnemies;
		for (float i = 0; i < 1; i += 0.2f)
		{
			transform.position += new Vector3(0.2f, 0, 0);
			yield return new WaitForSeconds(0.1f);
		}
		transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
		//MoveEnemies();
		ListOfEnemies = CheckEnemies();
		IsWaiting = true;
		if (PlayerFuncs.LightOffTurns <= 0)
			yield return LineMovingEnemyFuncs.StartCoroutine("LineMovingEnemyWalk2", ListOfEnemies);
		else
			IsWaiting = false;
		//yield return null;
	}

	IEnumerator WalkUp()
	{
		//Debug.Log("HEEYYY");
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
		if (PlayerFuncs.LightOffTurns <= 0)
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
		if (PlayerFuncs.LightOffTurns <= 0)
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
		PlayerFuncs = PlayerHandler.GetComponent<Player>();
		LineMovingEnemyFuncs = LineMovingEnemyHandler.GetComponent<LineMovingEnemy>();
		MotionlessEnemyFuncs = MotionlessEnemyHandler.GetComponent<MotionlessEnemy>();
	}

    // Update is called once per frame
    void Update()
    {
		Quaternion OldRotation;
		//IsWaiting = PlayerFuncs.Waiting;
		//Debug.Log(IsWaiting);
		if (IsWaiting == false && Time.timeScale == 1)
		{
			//Debug.Log("lol");
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
					if (!PlayerFuncs.IsThereGate(transform))
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
					if (!PlayerFuncs.IsThereGate(transform))
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
				//Debug.Log("LOL");
				if (NodeFuncs.CheckIfNodeExist(transform.position, 'y', 1)
				&& VerLineFuncs.CheckIfThereIsLine(transform.position, 1, transform.position + new Vector3(0, 0, 1)))
				{
					//IsMoving = true;
					//Debug.Log("LOL");
					OldRotation = transform.rotation;
					transform.rotation = Quaternion.Euler(0, 0, 0);
					if (!PlayerFuncs.IsThereGate(transform))
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
					if (!PlayerFuncs.IsThereGate(transform))
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
			//if (transform.position.x == FinalNode.transform.position.x && transform.position.z == FinalNode.transform.position.z)
				//Application.LoadLevel(0);
		}
	}
}
    
