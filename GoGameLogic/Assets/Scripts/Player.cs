using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject NodeHandler;
	public GameObject VerLineHandler;
	public GameObject HorLineHandler;
	private Node NodeFuncs;
	private VerticalLine VerLineFuncs;
	private HorizontalLine HorLineFuncs;

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
		Debug.Log(transform.position);
		yield return null;
	}

	// Start is called before the first frame update
	void Start()
	{
		NodeFuncs = NodeHandler.GetComponent<Node>();
		VerLineFuncs = VerLineHandler.GetComponent<VerticalLine>();
		HorLineFuncs = HorLineHandler.GetComponent<HorizontalLine>();
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
				transform.rotation = Quaternion.Euler(0, 270, 0);
				StartCoroutine("WalkLeft");
				//transform.position += new Vector3(-1, 0, 0);
			}
		}
		if (Input.GetKeyDown("d"))
		{
			if (NodeFuncs.CheckIfNodeExist(transform.position, 'x', 1)
			&& HorLineFuncs.CheckIfThereIsLine(transform.position, 1, transform.position + new Vector3(1,0,0)))
			{
				transform.rotation = Quaternion.Euler(0, 90, 0);
				//FinalPos = transform.position + new Vector3(1, 0, 0);
				StartCoroutine("WalkRight");
				//StopCoroutine("Walk");
				//Debug.Log(transform.position);
				//Walk();
				//transform.position += new Vector3(1, 0, 0);
			}
		}
		if (Input.GetKeyDown("w"))
		{
			if (NodeFuncs.CheckIfNodeExist(transform.position, 'y', 1) 
			&& VerLineFuncs.CheckIfThereIsLine(transform.position, 1, transform.position + new Vector3(0, 0, 1)))
			{
				transform.rotation = Quaternion.Euler(0, 0, 0);
				StartCoroutine("WalkUp");
				//transform.position += new Vector3(0, 0, 1);
			}
		}
		if (Input.GetKeyDown("s"))
		{
			if (NodeFuncs.CheckIfNodeExist(transform.position, 'y', -1) && VerLineFuncs.CheckIfThereIsLine(transform.position, -1, transform.position + new Vector3(0, 0, -1)))
			{
				transform.rotation = Quaternion.Euler(0, 180, 0);
				StartCoroutine("WalkDown");
				//transform.position += new Vector3(0, 0, -1);
			}
		}

	}
}
