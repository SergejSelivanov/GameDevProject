using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject NodeHandler;
	public GameObject VerLineHandler;
	public GameObject HorLineHandler;
	public GameObject LineMovingEnemyHandler;
	private Node NodeFuncs;
	private VerticalLine VerLineFuncs;
	private HorizontalLine HorLineFuncs;
	private LineMovingEnemy LineMovingEnemyFuncs;

	//LineMovingEnemy[] ListOfMovingEnemies = GameObject.FindObjectsOfType<LineMovingEnemy>();
	//public bool IsMoving = false;

	private void MoveEnemies()
    {
		LineMovingEnemy[] ListOfMovingEnemies = GameObject.FindObjectsOfType<LineMovingEnemy>();
		for (int i = 0; i < ListOfMovingEnemies.Length; i++)
        {
			LineMovingEnemyFuncs.LineMovingEnemyMove(ListOfMovingEnemies[i]);
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
		yield return null;
	}

	IEnumerator WalkRight()
    {
        for (float i = 0;  i < 1; i += 0.2f)
        {
			transform.position += new Vector3(0.2f, 0, 0);
			yield return new WaitForSeconds(0.1f);
        }
		transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
		yield return null;
    }

	IEnumerator WalkUp()
	{
		for (float i = 0; i < 1; i += 0.2f)
		{
			transform.position += new Vector3(0, 0, 0.2f);
			yield return new WaitForSeconds(0.1f);
		}
		transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
		//IsMoving = false;
		yield return null;
	}

	IEnumerator WalkDown()
	{
		for (float i = 0; i < 1; i += 0.2f)
		{
			transform.position += new Vector3(0, 0, -0.2f);
			yield return new WaitForSeconds(0.1f);
		}
		transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
		yield return null;
	}

	// Start is called before the first frame update
	void Start()
	{
		NodeFuncs = NodeHandler.GetComponent<Node>();
		VerLineFuncs = VerLineHandler.GetComponent<VerticalLine>();
		HorLineFuncs = HorLineHandler.GetComponent<HorizontalLine>();
		LineMovingEnemyFuncs = LineMovingEnemyHandler.GetComponent<LineMovingEnemy>();
		
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
				MoveEnemies();
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
				MoveEnemies();
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
				MoveEnemies();
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
				MoveEnemies();
				//LineMovingEnemyFuncs.LineMovingEnemyMove();
				//transform.position += new Vector3(0, 0, -1);
			}
			//IsMoving = false;
		}

	}
}
