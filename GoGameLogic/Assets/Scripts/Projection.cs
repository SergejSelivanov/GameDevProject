using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projection : MonoBehaviour
{
    public GameObject projection;
    public GameObject ButtonUI;
    public GameObject PlayerHandler;
    public GameObject LineMovingEnemyHandler;
    public GameObject VerLineHandler;
    public GameObject HorLineHandler;
    private Player PlayerFuncs;
    private LineMovingEnemy LineMovingEnemyFuncs;
    private VerticalLine VerLineFuncs;
    private HorizontalLine HorLineFuncs;

    public void CreateProjection()
    {
       // ButtonUI.SetActive(false);
        //Instantiate(ProjectionPrefab, new Vector3(1, 1, 0), Quaternion.Euler(0,0,0));
        //projection.transform.position = new Vector3(1, 1, 1);
        if (PlayerHandler.transform.rotation.eulerAngles.y == 0
        && LineMovingEnemyFuncs.CheckIfThereIsNodeToMove(PlayerHandler)
        && VerLineFuncs.CheckIfThereIsLine(PlayerHandler.transform.position, 1, PlayerHandler.transform.position + new Vector3(0, 0, 1))
        && !PlayerFuncs.IsThereGate(PlayerHandler.transform))
        {
            ButtonUI.SetActive(false);
            projection.transform.position = PlayerHandler.transform.position + new Vector3(0, 0, 1);
            projection.SetActive(true);
            PlayerFuncs.ProjectionActive = true;
        }
        if (PlayerHandler.transform.rotation.eulerAngles.y == 90
        && LineMovingEnemyFuncs.CheckIfThereIsNodeToMove(PlayerHandler)
        && HorLineFuncs.CheckIfThereIsLine(PlayerHandler.transform.position, 1, PlayerHandler.transform.position + new Vector3(1, 0, 0))
        && !PlayerFuncs.IsThereGate(PlayerHandler.transform))
        {
            ButtonUI.SetActive(false);
            projection.transform.position = PlayerFuncs.transform.position + new Vector3(1, 0, 0);
            projection.SetActive(true);
            PlayerFuncs.ProjectionActive = true;
        }
        if (PlayerHandler.transform.rotation.eulerAngles.y == 180
        && LineMovingEnemyFuncs.CheckIfThereIsNodeToMove(PlayerHandler)
        && VerLineFuncs.CheckIfThereIsLine(PlayerHandler.transform.position, -1, PlayerHandler.transform.position + new Vector3(0, 0, -1))
        && !PlayerFuncs.IsThereGate(PlayerHandler.transform))
        {
            ButtonUI.SetActive(false);
            projection.transform.position = PlayerFuncs.transform.position + new Vector3(0, 0, -1);
            projection.SetActive(true);
            PlayerFuncs.ProjectionActive = true;
        }
        if (PlayerHandler.transform.rotation.eulerAngles.y == 270
        && LineMovingEnemyFuncs.CheckIfThereIsNodeToMove(PlayerHandler)
        && HorLineFuncs.CheckIfThereIsLine(PlayerHandler.transform.position, -1, PlayerHandler.transform.position + new Vector3(-1, 0, 0))
        && !PlayerFuncs.IsThereGate(PlayerHandler.transform))
        {
            ButtonUI.SetActive(false);
            projection.transform.position = PlayerFuncs.transform.position + new Vector3(-1, 0, 0);
            projection.SetActive(true);
            PlayerFuncs.ProjectionActive = true;
        }
        /*Debug.Log(PlayerHandler.transform.rotation.eulerAngles.y == 180);
        Debug.Log(LineMovingEnemyFuncs.CheckIfThereIsNodeToMove(PlayerHandler));
        Debug.Log(VerLineFuncs.CheckIfThereIsLine(PlayerHandler.transform.position, -1, PlayerHandler.transform.position + new Vector3(0, 0, -1)));
        Debug.Log(PlayerFuncs.IsThereGate(PlayerHandler.transform));*/
        //projection.SetActive(true);
        //PlayerFuncs.ProjectionActive = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        PlayerFuncs = PlayerHandler.GetComponent<Player>();
        LineMovingEnemyFuncs = LineMovingEnemyHandler.GetComponent<LineMovingEnemy>();
        VerLineFuncs = VerLineHandler.GetComponent<VerticalLine>();
        HorLineFuncs = HorLineHandler.GetComponent<HorizontalLine>();
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}
