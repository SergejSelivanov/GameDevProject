using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public Texture2D[] SomeLightmaps; // Array to switch lightmaps
	public bool KnifeIsReady { get; set; } //is killing button is pressed
	public bool IsMovable { get; set; } //is player ready to move
	public bool IsWaiting { get; set; } //is player waiting something to happen
	public int LightsOffTurns { get; set; } //number of turns lights will be off
	private LightmapData[] LightMapBuf; //buffer for lightmaps to switch
	private bool LightsOff = false; //is light switched off
	private CameraEnemy[] CameraEnemies; //array of cameras
	private GameObject FinalNode; 
	private static GameObject[] EnemiesTokill; //array of dead enemies
	public GameObject turnManagerHandler;
	private TurnManager turnManager;
	private AudioManager audioManager;

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

	IEnumerator StopBreaking(GameObject Enemy) //Coroutine to turn player and break-in enemies
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		int RequiredAngle = 0;
		if (player.transform.position.x > Enemy.transform.position.x) //getting needed angle to rotate player
			RequiredAngle = 270;
		else if (player.transform.position.x < Enemy.transform.position.x)
			RequiredAngle = 90;
		else if (player.transform.position.z > Enemy.transform.position.z)
			RequiredAngle = 180;
		else
			RequiredAngle = 0;
		int playerangle = (int)player.transform.rotation.eulerAngles.y;
		int Diff = RequiredAngle - playerangle; // getting difference between required and present angles
		if (Diff == 270 || Diff == -270)
			Diff = -Diff % 180;
		if (Diff != 0)
		{
			player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 1); //activate rotation animation
			for (int i = 0; i < 30; i++)
			{
				player.transform.rotation = Quaternion.Euler(0, (int)player.transform.rotation.eulerAngles.y + Diff / 30, 0); //rotating player in needed angle
				yield return new WaitForSeconds(0.0133f);
			}
			player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0); //stop rotation animation
			player.transform.rotation = Quaternion.Euler(0, RequiredAngle, 0); //avoiding extra fraction
		}
		yield return new WaitForSeconds(0.5f);
		FindObjectOfType<AudioManager>().Play("Wrist");
		yield return new WaitForSeconds(0.5f);
		//yield return new WaitForSeconds(1);
		for (int i = 0; i < EnemiesKill.Length; i++)
		{
			if (EnemiesKill[i] == null)
			{
				EnemiesKill[i] = Enemy; //add enemy to list of enemies to kill
				break;
			}
		}
		try
		{
			Enemy.transform.GetChild(0).GetComponent<Animator>().SetBool("IsDead", true); //activate LineMovingEnemy death animation
		}
		catch
        {
			Enemy.GetComponent<Animator>().SetBool("IsDead", true); //activate MotionlessEnemy death animation
		}
		yield return new WaitForSeconds(0.5f);
		Time.timeScale = 0.6f; //to animate death correctly
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsTaunting", false); //stop break-in player animation
		yield return new WaitForSeconds(0.5f);
		Time.timeScale = 1;
		yield return null;
	}

	private GameObject GetStairwayNodePositionY(GameObject Obj) //Find node to move 
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

	IEnumerator WalkUpright(int sign, GameObject Obj, GameObject Node) //Coroutine to walk down or up
    {
		float diff = Mathf.Abs(Obj.transform.position.y - Node.transform.position.y); //getting difference between heights
        for (float i = 0; i < 1; i += 0.01f)
        {
			Obj.transform.position += new Vector3(0, diff / 100 * sign, 0); //walking down or up
			yield return new WaitForSeconds(0.004f);
        }
    }

	public void CheckIfThereIsStairway(GameObject Obj) //Move up or down if there is stairway
    {
		GameObject DefiniteNode = GetStairwayNodePositionY(Obj); //find node to move
		if (DefiniteNode == null)
			return;
		float NodePositionY = DefiniteNode.transform.position.y;
		float ObjPositionY = Obj.transform.position.y;
		if (Mathf.Abs(NodePositionY - ObjPositionY) > 0.1f) // if there is difference between heights of two nodes
        {
			if (NodePositionY > ObjPositionY)
				StartCoroutine(WalkUpright(1, Obj, DefiniteNode));	//move up
            else
				StartCoroutine(WalkUpright(-1, Obj, DefiniteNode));	//move down
        }
    }

	IEnumerator KillingAnimation(GameObject Enemy) //Player death animation
    {
		Time.timeScale = 0.99f;
		yield return new WaitForSeconds(0.6f);
		audioManager.Play("Lightning");
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsKilled", true); //start player death animation
		audioManager.Play("PlayerDeath");
		Enemy.transform.GetChild(1).gameObject.SetActive(true); //activate lightning 
		yield return new WaitForSeconds(1.5f);
		Time.timeScale = 1;
		FindObjectOfType<LevelLoader>().LoadSameLevel();
		//SceneManager.LoadScene(0); //end level
		yield return null;
	}

	private void InitDeathArr() //Init EnemiesToKill array
	{
		GameObject[] ListOfMovingEnemies = GameObject.FindGameObjectsWithTag("LineMovingEnemy");
		GameObject[] ListOfMotEnemies = GameObject.FindGameObjectsWithTag("MotionlessEnemy");
		if (EnemiesTokill == null)
			EnemiesTokill = new GameObject[ListOfMovingEnemies.Length + ListOfMotEnemies.Length]; // it must be lenght of number of all enemies
		LightsOffTurns--;
	}

	IEnumerator RotateEnemies(GameObject ObjectToRotate) //Coroutine to rotate enemies
	{
		if (ObjectToRotate != null)
		{
			int requiredAngle = Utilities.Opposite(ObjectToRotate); //get required angle which is opposite of present
			ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 1); //activate rotation animation of enemy
			for (int i = 0; i < 30; i++)
			{
				if (ObjectToRotate != null)
				{
					ObjectToRotate.transform.rotation = Quaternion.Euler(0, (int)ObjectToRotate.transform.rotation.eulerAngles.y + 6, 0); //rotate enemy
					yield return new WaitForSeconds(0.0133f);
				}
			}
			if (ObjectToRotate != null)
			{
				ObjectToRotate.transform.rotation = Quaternion.Euler(0, requiredAngle, 0); //to avoid extra fraction
				ObjectToRotate.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0); //end rotating animation
			}
		}
		yield return null;
	}

	IEnumerator Rotate(int RequiredAngle, bool NeedToKill) //Coroutine to rotate player
    {
		int playerangle = (int)gameObject.transform.rotation.eulerAngles.y;
		int Diff = RequiredAngle - playerangle; //getting difference between present angle and angle to rotate
		if (Diff == 270 || Diff == -270)
			Diff = -Diff % 180;
		if (Diff != 0)
		{
			GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 1); //activate player rotating animation
			for (int i = 0; i < 30; i++)
			{
				gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y + Diff / 30, 0); //rotate
				yield return new WaitForSeconds(0.0133f);
			}
			GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0); //stop player rotating animation
			gameObject.transform.rotation = Quaternion.Euler(0, RequiredAngle, 0); //avoiding extra fraction
		}
		if (NeedToKill == false) //if there is enemy to kill
			StartCoroutine("Walk", RequiredAngle);
		else
			StartCoroutine("KillEnemy");
		yield return null;
	}

	IEnumerator Walk(int requiredAngle) //Coroutine to move player
    {
		int X = 0;
		int sign = 1;
		int Z = 0;
		if (requiredAngle == 0 || requiredAngle == 180) //if player need to move up or down
			Z = 1;
		else											//if player need to move left or right
			X = 1;
		if (requiredAngle == 180 || requiredAngle == 270) //if player needs to move to negative side 
			sign = -1;
		CheckIfThereIsStairway(gameObject); //check if there is stairway and move up or down on it(on Y axis)
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true); // start moving animation
		audioManager.Play("PlayerWalk");
		for (float i = 0; i < 1; i += 0.01f)
		{
			transform.position += new Vector3(0.01f * sign * X, 0, 0.01f * sign * Z); //move player
			yield return new WaitForSeconds(0.004f);
		}
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", false); //stop moving animation
		transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z)); //to avoid extra fraction
		InitDeathArr(); //init array of dead enemies if needed
		for (int i = 0; i < CameraEnemies.Length; i++)
		{
			audioManager.Play("RotateCamera");
			CameraEnemies[i].MoveCamera(); //rotate all camera enemies
		}
		IsWaiting = true; //player is waiting for enemies to move if there are any
		turnManager.EndPlayersTurn(); //end players turn
	}

	public void ChangeLights() //Switch lightmaps
	{
		LightmapData[] LmData = new LightmapData[1];
		LmData[0] = new LightmapData();
		if (LightsOff == false)
		{
			audioManager.Play("LightsOff");
			LmData[0] = LightmapSettings.lightmaps[0];
			LmData[0].lightmapDir = SomeLightmaps[1];
			LmData[0].lightmapColor = SomeLightmaps[0];
			LightmapSettings.lightmaps = LmData;
			LightsOff = true;
		}
		else
		{
			audioManager.Play("LightsOn");
			LightmapSettings.lightmaps = LightMapBuf;
			LightsOff = false;
		}
	}

	private GameObject[] FindEnemies() //Return array of enemies that had to be killed
    {
		GameObject[] MotEnemies =  GameObject.FindGameObjectsWithTag("MotionlessEnemy");
		GameObject[] MovingEnemies = GameObject.FindGameObjectsWithTag("LineMovingEnemy");
		GameObject[] Enemies = new GameObject[MotEnemies.Length + MovingEnemies.Length];
		Vector3 NewPos = gameObject.transform.position + gameObject.transform.forward;
		for (int i = 0; i < MotEnemies.Length; i++)
        {
			if (Mathf.Round(NewPos.x) == MotEnemies[i].transform.position.x && Mathf.Round(NewPos.z) == MotEnemies[i].transform.position.z) //if on next step there will be motionless enemy
            {
                for (int j = 0; j < Enemies.Length; j++)
                {
					if (Enemies[j] == null)
                    {
						Enemies[j] = MotEnemies[i]; //add enemy to return array
						break;
                    }
                }
            }
        }
		for (int i = 0; i < MovingEnemies.Length; i++)
		{
			if (Mathf.Round(NewPos.x) == MovingEnemies[i].transform.position.x && Mathf.Round(NewPos.z) == MovingEnemies[i].transform.position.z) //if on next step there will be linemoving enemy
			{
				for (int j = 0; j < Enemies.Length; j++)
				{
					if (Enemies[j] == null)
					{
						Enemies[j] = MovingEnemies[i];  //add enemy to return array
						break;
					}
				}
			}
		}
		return Enemies;
    }

	IEnumerator KillEnemy()
    {
		GameObject[] KilledEnemies = FindEnemies(); //array of enemies that had to be killed
		CheckIfThereIsStairway(gameObject); //check if there is stairway and move on it
		InitDeathArr(); //init array of dead enemies
		gameObject.GetComponent<Animator>().SetInteger("KillingWalk",1); // start animation of moving to enemy
		for (int i = 0; i < 42; i++)
        {
			gameObject.transform.position += gameObject.transform.forward / 100; //move player forward
			yield return new WaitForSeconds(0.01f);
        }
		gameObject.GetComponent<Animator>().SetInteger("KillingWalk", 0); //stop  animation of moving to enemy
		yield return null;
		gameObject.GetComponent<Animator>().SetBool("IsKilling", true); //start animation of punching 
		audioManager.Play("Punch");
		//yield return new WaitForSeconds(0.7f);
		yield return new WaitForSeconds(0.4f);
		audioManager.Play("Punch2");
		yield return new WaitForSeconds(0.3f);
		audioManager.Play("RobotDeath");
		for (int i = 0; i < KilledEnemies.Length; i++)
		{
			if (KilledEnemies[i] != null)
			{
				for (int j = 0; j < EnemiesKill.Length; j++)
				{
					if (EnemiesTokill[j] == null)
					{
						EnemiesTokill[j] = KilledEnemies[i]; //add dead enemy to array of dead enemies
						break;
					}
				}
				//KilledEnemies[i].transform.GetChild(0).GetComponent<Animator>().applyRootMotion = true;
				try
				{
					KilledEnemies[i].GetComponent<Animator>().applyRootMotion = true;
					KilledEnemies[i].GetComponent<Animator>().SetBool("IsDead", true); //start animation of death of motionless enemy
				}
				catch
                {
					KilledEnemies[i].transform.GetChild(0).GetComponent<Animator>().applyRootMotion = true;
					KilledEnemies[i].transform.GetChild(0).GetComponent<Animator>().SetBool("IsDead", true); //start animation of death of line moving enemy
				}
				//audioManager.Play("RobotDeath");
			}
		}
		yield return new WaitForSeconds(1);
		gameObject.GetComponent<Animator>().SetBool("IsKilling", false); //stop animation of punching 
		yield return null;
		gameObject.GetComponent<Animator>().SetInteger("KillingWalk", 2); // start animation of remaining walk
		for (int i = 0; i < 58; i++)
		{
			gameObject.transform.position += gameObject.transform.forward / 100; //move player 
			yield return new WaitForSeconds(0.01f);
		}
		gameObject.GetComponent<Animator>().SetInteger("KillingWalk", 0); // stop animation of remaining walk
		gameObject.transform.position = new Vector3(Mathf.Round(gameObject.transform.position.x), gameObject.transform.position.y, Mathf.Round(gameObject.transform.position.z)); //to avoid extra fraction
																																												  //transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
		for (int i = 0; i < CameraEnemies.Length; i++)
		{
			audioManager.Play("RotateCamera");
			CameraEnemies[i].MoveCamera(); //rotate camera enemies
		}
		IsWaiting = true; //player starts waiting for enemies turns
		turnManager.EndPlayersTurn(); // end players turn
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
				if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform) && (Utilities.CheckIfThereIsMotEnemy(gameObject) || Utilities.CheckIfThereIsMovingEnemy(gameObject)))
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
				if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform) && (Utilities.CheckIfThereIsMotEnemy(gameObject) || Utilities.CheckIfThereIsMovingEnemy(gameObject)))
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
				if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform) && (Utilities.CheckIfThereIsMotEnemy(gameObject) || Utilities.CheckIfThereIsMovingEnemy(gameObject)))
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
				if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform) && (Utilities.CheckIfThereIsMotEnemy(gameObject) || Utilities.CheckIfThereIsMovingEnemy(gameObject)))
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
		FinalNode = GameObject.FindGameObjectWithTag("FinalNode"); //find final node 
		LightMapBuf = LightmapSettings.lightmaps;
		CameraEnemies = GameObject.FindObjectsOfType<CameraEnemy>();
		turnManager = turnManagerHandler.GetComponent<TurnManager>();
		audioManager = FindObjectOfType<AudioManager>();
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
					if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform) && (Utilities.CheckIfThereIsMotEnemy(gameObject) || Utilities.CheckIfThereIsMovingEnemy(gameObject)))
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
					if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform) && (Utilities.CheckIfThereIsMotEnemy(gameObject) || Utilities.CheckIfThereIsMovingEnemy(gameObject)))
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
					if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform) && (Utilities.CheckIfThereIsMotEnemy(gameObject) || Utilities.CheckIfThereIsMovingEnemy(gameObject)))
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
					if (!Utilities.IsThereGate(transform) && !Utilities.IsThereCamera(transform) && (Utilities.CheckIfThereIsMotEnemy(gameObject) || Utilities.CheckIfThereIsMovingEnemy(gameObject)))
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
				FindObjectOfType<LevelLoader>().LoadNextLevel();
		}
	}
}
