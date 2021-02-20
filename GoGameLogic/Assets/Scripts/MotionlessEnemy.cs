using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionlessEnemy : MonoBehaviour
{
	public GameObject VerLineHandler;
	public GameObject HorLineHandler;
	private VerticalLine VerLineFuncs;
	private HorizontalLine HorLineFuncs;


	// Start is called before the first frame update
	private bool CheckIfFacing(GameObject player)
	{
		if (gameObject.transform.rotation.eulerAngles.y == 0 
		&& player.transform.rotation.eulerAngles.y == gameObject.transform.rotation.y + 180)
			return true;
		if (gameObject.transform.rotation.eulerAngles.y == 90 
		&& player.transform.rotation.eulerAngles.y - 180 == gameObject.transform.rotation.eulerAngles.y)
			return true;
		if (gameObject.transform.rotation.eulerAngles.y == 180 
		&& player.transform.rotation.eulerAngles.y + 180 == gameObject.transform.rotation.eulerAngles.y)
			return true;
		if (gameObject.transform.rotation.eulerAngles.y == 270 
		&& player.transform.rotation.eulerAngles.y == gameObject.transform.rotation.eulerAngles.y - 180)
			return true;
		return false;
	}

	private bool CheckifPlayerInfrontofEnemy(GameObject player)
	{
		if (gameObject.transform.rotation.eulerAngles.y == 0 
		&& gameObject.transform.position.x == player.transform.position.x 
		&& gameObject.transform.position.z + 1 == player.transform.position.z
		&& VerLineFuncs.CheckIfThereIsLine(gameObject.transform.position, 1, gameObject.transform.position + new Vector3(0,0,1)))
			return true;
		if (gameObject.transform.rotation.eulerAngles.y == 90 
		&& gameObject.transform.position.z == player.transform.position.z
		&& gameObject.transform.position.x + 1 == player.transform.position.x
		&& HorLineFuncs.CheckIfThereIsLine(gameObject.transform.position, 1, gameObject.transform.position + new Vector3(1, 0, 0)))
			return true;
		if (gameObject.transform.rotation.eulerAngles.y == 180 
		&& gameObject.transform.position.x == player.transform.position.x 
		&& gameObject.transform.position.z - 1 == player.transform.position.z
		&& VerLineFuncs.CheckIfThereIsLine(gameObject.transform.position, -1, gameObject.transform.position + new Vector3(0, 0, -1)))
			return true;
		if (gameObject.transform.rotation.eulerAngles.y == 270
		&& gameObject.transform.position.z == player.transform.position.z 
		&& gameObject.transform.position.x - 1 == player.transform.position.x
		&& HorLineFuncs.CheckIfThereIsLine(gameObject.transform.position, -1, gameObject.transform.position + new Vector3(-1, 0, 0)))
			return true;
		return false;
	}

	void Start()
	{
		VerLineFuncs = VerLineHandler.GetComponent<VerticalLine>();
		HorLineFuncs = HorLineHandler.GetComponent<HorizontalLine>();
	}

	// Update is called once per frame
	void Update()
	{
		//Transform PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (transform.position.x == player.transform.position.x
		&& transform.position.z == player.transform.position.z
		&& !CheckIfFacing(player))
			Destroy(gameObject);
		if (CheckifPlayerInfrontofEnemy(player))
			Application.LoadLevel(0);
	}
}
