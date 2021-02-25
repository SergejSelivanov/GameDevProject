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
	private Node NodeFuncs;
	private VerticalLine VerLineFuncs;
	private HorizontalLine HorLineFuncs;
	private LineMovingEnemy LineMovingEnemyFuncs;
	private MotionlessEnemy MotionlessEnemyFuncs;
	private float SkillReady = 0;

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

	public float SkillSetter
    {
		get
        {
			return SkillReady;
        }
		set
        {
			//if (SkillReady < 1 && value != 0)
			if (value <= 1)
				SkillReady = value;
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
			if (transform.position.x == ListOfMovingEnemies[i].transform.position.x
			&& transform.position.z == ListOfMovingEnemies[i].transform.position.z
			&& !MotionlessEnemyFuncs.CheckIfFacing(gameObject, ListOfMovingEnemies[i]))
			{
				Destroy(ListOfMovingEnemies[i]);
				if (SkillReady < 1)
					SkillReady += 0.5f;
			}
			if (MotionlessEnemyFuncs.CheckifPlayerInfrontofEnemy(gameObject, ListOfMovingEnemies[i]))
				Application.LoadLevel(0);
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
    }

	IEnumerator WalkLeft()
	{
		for (float i = 0; i < 1; i += 0.2f)
		{
			transform.position += new Vector3(-0.2f, 0, 0);
			yield return new WaitForSeconds(0.1f);
		}
		transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
		MoveEnemies();
		//yield return null;
		yield return new WaitForSeconds(2);
	}

	IEnumerator WalkRight()
    {
        for (float i = 0;  i < 1; i += 0.2f)
        {
			transform.position += new Vector3(0.2f, 0, 0);
			yield return new WaitForSeconds(0.1f);
        }
		transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
		MoveEnemies();
		yield return new WaitForSeconds(2);
		//yield return null;
	}

	IEnumerator WalkUp()
	{
		
		for (float i = 0; i < 1; i += 0.2f)
		{
			transform.position += new Vector3(0, 0, 0.2f);
			yield return new WaitForSeconds(0.1f);
		}
		transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
		MoveEnemies();
		//IsMoving = false;
		//yield return null;
		yield return new WaitForSeconds(2);
	}

	IEnumerator WalkDown()
	{
		for (float i = 0; i < 1; i += 0.2f)
		{
			transform.position += new Vector3(0, 0, -0.2f);
			yield return new WaitForSeconds(0.1f);
		}
		transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
		MoveEnemies();
		//yield return null;
		yield return new WaitForSeconds(2);
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
		if (Input.GetKeyDown("a"))
		{
			//transform.position = FinalPos;
			//Debug.Log(transform.position);
			
			if (NodeFuncs.CheckIfNodeExist(transform.position, 'x', -1)
			&& HorLineFuncs.CheckIfThereIsLine(transform.position, -1, transform.position + new Vector3(-1, 0, 0)))
			{
				//IsMoving = true;
				transform.rotation = Quaternion.Euler(0, 270, 0);
				StartCoroutine("WalkLeft");
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
			&& HorLineFuncs.CheckIfThereIsLine(transform.position, 1, transform.position + new Vector3(1,0,0)))
			{
				//IsMoving = true;
				transform.rotation = Quaternion.Euler(0, 90, 0);
				//FinalPos = transform.position + new Vector3(1, 0, 0);
				StartCoroutine("WalkRight");
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
			if (NodeFuncs.CheckIfNodeExist(transform.position, 'y', 1) 
			&& VerLineFuncs.CheckIfThereIsLine(transform.position, 1, transform.position + new Vector3(0, 0, 1)))
			{
				//IsMoving = true;
				transform.rotation = Quaternion.Euler(0, 0, 0);
				StartCoroutine("WalkUp");
				//CheckForDestroyAgain();
				//MoveEnemies();
				//LineMovingEnemyFuncs.LineMovingEnemyMove();
				//transform.position += new Vector3(0, 0, 1);
			}
			//IsMoving = false;
		}
		if (Input.GetKeyDown("s"))
		{
			
			if (NodeFuncs.CheckIfNodeExist(transform.position, 'y', -1) && VerLineFuncs.CheckIfThereIsLine(transform.position, -1, transform.position + new Vector3(0, 0, -1)))
			{
				//IsMoving = true;
				transform.rotation = Quaternion.Euler(0, 180, 0);
				StartCoroutine("WalkDown");
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
