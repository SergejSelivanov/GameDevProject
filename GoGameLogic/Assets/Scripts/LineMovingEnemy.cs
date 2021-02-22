using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMovingEnemy : MonoBehaviour
{
    public GameObject NodeHandler;
    public GameObject VerLineHandler;
    public GameObject HorLineHandler;
   // public GameObject PlayerHandler;
    private Node NodeFuncs;
    private VerticalLine VerLineFuncs;
    private HorizontalLine HorLineFuncs;
    //private Player PlayerFuncs;

    private char XorY;
    private int increment;

    /*private void TurnOtherWay()
    {
        // Debug.Log(transform.rotation);
        if (transform.rotation.eulerAngles.y == 0)
        {
            increment = -1;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.rotation.eulerAngles.y == 90)
        {
            increment = -1;
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        else if (transform.rotation.eulerAngles.y == 180)
        {
            increment = 1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (transform.rotation.eulerAngles.y == 270)
        {
            increment = 1;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        //Debug.Log(transform.rotation);
    }

    IEnumerator LineMovingEnemyWalk()
    {
        for (float i = 0; i < 1; i += 0.2f)
        {
            if (XorY == 'x')
            {
                transform.position += new Vector3(0.2f * increment, 0, 0);
            }
            if (XorY == 'y')
            {
                transform.position += new Vector3(0, 0, 0.2f * increment);
            }
            yield return new WaitForSeconds(0.1f);
        }
        if (XorY == 'x')
            transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
        if (XorY == 'y')
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
        yield return null;
    }


    public bool CheckIfThereIsNodeToMove()
    {
        if (transform.rotation.eulerAngles.y == 0)
        {
            XorY = 'y';
            increment = 1;
            if (NodeFuncs.CheckIfNodeExist(transform.position, 'y', 1)
            && VerLineFuncs.CheckIfThereIsLine(transform.position, 1, transform.position + new Vector3(0, 0, 1)))
                return true;
        }
        if (transform.rotation.eulerAngles.y == 90)
        {
            XorY = 'x';
            increment = 1;
            if (NodeFuncs.CheckIfNodeExist(transform.position, 'x', 1)
            && HorLineFuncs.CheckIfThereIsLine(transform.position, 1, transform.position + new Vector3(1, 0, 0)))
                return true;
        }
        if (transform.rotation.eulerAngles.y == 180)
        {
            XorY = 'y';
            increment = -1;
            if (NodeFuncs.CheckIfNodeExist(transform.position, 'y', -1)
            && VerLineFuncs.CheckIfThereIsLine(transform.position, -1, transform.position + new Vector3(0, 0, -1)))
                return true;
        }
        if (transform.rotation.eulerAngles.y == 270)
        {
            XorY = 'x';
            increment = -1;
            if (NodeFuncs.CheckIfNodeExist(transform.position, 'x', -1)
            && HorLineFuncs.CheckIfThereIsLine(transform.position, -1, transform.position + new Vector3(-1, 0, 0)))
                return true;
        }
        return false;
    }

    public void LineMovingEnemyMove()
    {
        if (CheckIfThereIsNodeToMove())
            StartCoroutine("LineMovingEnemyWalk");
        else
        {
            TurnOtherWay();
            StartCoroutine("LineMovingEnemyWalk");
        }
    }*/
    private void TurnOtherWay(LineMovingEnemy Obj)
    {
        // Debug.Log(transform.rotation);
        if (Obj.transform.rotation.eulerAngles.y == 0)
        {
            increment = -1;
            Obj.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Obj.transform.rotation.eulerAngles.y == 90)
        {
            increment = -1;
            Obj.transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        else if (Obj.transform.rotation.eulerAngles.y == 180)
        {
            increment = 1;
            Obj.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Obj.transform.rotation.eulerAngles.y == 270)
        {
            increment = 1;
            Obj.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        //Debug.Log(transform.rotation);
    }

    IEnumerator LineMovingEnemyWalk(LineMovingEnemy Obj)
    {
        // Debug.Log(Obj);
        for (float i = 0; i < 1; i += 0.2f)
        {
            /*if (XorY == 'x')
            {
                Obj.transform.position += new Vector3(0.2f * increment, 0, 0);
            }
            if (XorY == 'y')
            {
                //Debug.Log(Obj);
                Obj.transform.position += new Vector3(0, 0, 0.2f * increment);
            }*/
            if (Obj.transform.rotation.eulerAngles.y == 0)
                Obj.transform.position += new Vector3(0, 0, 0.2f);
            if (Obj.transform.rotation.eulerAngles.y == 90)
                Obj.transform.position += new Vector3(0.2f, 0, 0);
            if (Obj.transform.rotation.eulerAngles.y == 180)
                Obj.transform.position += new Vector3(0, 0, -0.2f);
            if (Obj.transform.rotation.eulerAngles.y == 270)
                Obj.transform.position += new Vector3(-0.2f , 0,0);
            yield return new WaitForSeconds(0.1f);
        }
        // Debug.Log(XorY);
        if (Obj.transform.rotation.eulerAngles.y == 270 || Obj.transform.rotation.eulerAngles.y == 90)
            Obj.transform.position = new Vector3(Mathf.Round(Obj.transform.position.x), Obj.transform.position.y, Obj.transform.position.z);
        if (Obj.transform.rotation.eulerAngles.y == 0 || Obj.transform.rotation.eulerAngles.y == 180)
            Obj.transform.position = new Vector3(Obj.transform.position.x, Obj.transform.position.y, Mathf.Round(Obj.transform.position.z));
        /* if (XorY == 'x')
             Obj.transform.position = new Vector3(Mathf.Round(Obj.transform.position.x), Obj.transform.position.y, Obj.transform.position.z);
         if (XorY == 'y')
             Obj.transform.position = new Vector3(Obj.transform.position.x, Obj.transform.position.y, Mathf.Round(Obj.transform.position.z));*/
        yield return null;
    }


    private bool CheckIfThereIsNodeToMove(LineMovingEnemy Obj)
    {
        if (Obj.transform.rotation.eulerAngles.y == 0)
        {
            XorY = 'y';
            increment = 1;
            if (NodeFuncs.CheckIfNodeExist(Obj.transform.position, 'y', 1)
            && VerLineFuncs.CheckIfThereIsLine(Obj.transform.position, 1, Obj.transform.position + new Vector3(0, 0, 1)))
                return true;
        }
        if (Obj.transform.rotation.eulerAngles.y == 90)
        {
            XorY = 'x';
            increment = 1;
            if (NodeFuncs.CheckIfNodeExist(Obj.transform.position, 'x', 1)
            && HorLineFuncs.CheckIfThereIsLine(Obj.transform.position, 1, Obj.transform.position + new Vector3(1, 0, 0)))
                return true;
        }
        if (Obj.transform.rotation.eulerAngles.y == 180)
        {
            XorY = 'y';
            increment = -1;
            if (NodeFuncs.CheckIfNodeExist(Obj.transform.position, 'y', -1)
            && VerLineFuncs.CheckIfThereIsLine(Obj.transform.position, -1, Obj.transform.position + new Vector3(0, 0, -1)))
                return true;
        }
        if (Obj.transform.rotation.eulerAngles.y == 270)
        {
            XorY = 'x';
            increment = -1;
            if (NodeFuncs.CheckIfNodeExist(Obj.transform.position, 'x', -1)
            && HorLineFuncs.CheckIfThereIsLine(Obj.transform.position, -1, Obj.transform.position + new Vector3(-1, 0, 0)))
                return true;
        }
        return false;
    }

    public void LineMovingEnemyMove(LineMovingEnemy Obj)
    {
        if (CheckIfThereIsNodeToMove(Obj))
            StartCoroutine("LineMovingEnemyWalk", Obj);
        else
        {
            TurnOtherWay(Obj);
            StartCoroutine("LineMovingEnemyWalk", Obj);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        NodeFuncs = NodeHandler.GetComponent<Node>();
        VerLineFuncs = VerLineHandler.GetComponent<VerticalLine>();
        HorLineFuncs = HorLineHandler.GetComponent<HorizontalLine>();
        //PlayerFuncs = PlayerHandler.GetComponent<Player>();
    }

    // Update is called once per frame
   /* void Update()
    {
         if (CheckIfThereIsNodeToMove())
             StartCoroutine("LineMovingEnemyWalk");
         else
         {
             TurnOtherWay();
             StartCoroutine("LineMovingEnemyWalk");
         }
        LocalIsMoving = PlayerFuncs.IsMoving;
        if (LocalIsMoving)
        {
            LineMovingEnemyMove();
            LocalIsMoving = false;
           // PlayerFuncs.IsMoving = false;
        }
    }*/
}
