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
			if (NodeFuncs.CheckIfNodeExist(transform.position, 'x', -1)
			&& HorLineFuncs.CheckIfThereIsLine(transform.position, -1, transform.position + new Vector3(-1, 0, 0)))
			{
				transform.position += new Vector3(-1, 0, 0);
			}
		}
		if (Input.GetKeyDown("d"))
		{
			if (NodeFuncs.CheckIfNodeExist(transform.position, 'x', 1)
			&& HorLineFuncs.CheckIfThereIsLine(transform.position, 1, transform.position + new Vector3(1,0,0)))
			{
				transform.position += new Vector3(1, 0, 0);
			}
		}
		if (Input.GetKeyDown("w"))
		{
			if (NodeFuncs.CheckIfNodeExist(transform.position, 'y', 1) 
			&& VerLineFuncs.CheckIfThereIsLine(transform.position, 1, transform.position + new Vector3(0, 0, 1)))
			{
				transform.position += new Vector3(0, 0, 1);
			}
		}
		if (Input.GetKeyDown("s"))
		{
			if (NodeFuncs.CheckIfNodeExist(transform.position, 'y', -1) && VerLineFuncs.CheckIfThereIsLine(transform.position, -1, transform.position + new Vector3(0, 0, -1)))
			{
				transform.position += new Vector3(0, 0, -1);
			}
		}

	}
}
