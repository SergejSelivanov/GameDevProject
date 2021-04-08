using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LineMovingEnemy : MonoBehaviour
{
    public GameObject NodeHandler;
    public GameObject VerLineHandler;
    public GameObject HorLineHandler;
    public GameObject MotionlessEnemyHandler;
    public GameObject PlayerHandler;
    public GameObject KnifeHandler;
    public GameObject ProjectionBehaviourHandler;
    // public GameObject PlayerHandler;
    private Node NodeFuncs;
    private VerticalLine VerLineFuncs;
    private HorizontalLine HorLineFuncs;
    private MotionlessEnemy MotEnemyFuncs;
    private Player PlayerFuncs;
    private ThrowKnife KnifeFuncs;
    private ProjectionBehaviour ProjectionBehaviourFuncs;

    private Animator animator;
    
    //private Player PlayerFuncs;

    //  private char XorY;
    //private int increment;

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

    private void OnMouseDown()
    {
        if (Time.timeScale == 1)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (PlayerFuncs.KnifeReady == true)
            {
                if (KnifeFuncs.CheckIfInRange(gameObject, player))
                {
                    Destroy(gameObject);
                    PlayerFuncs.SkillSetter = 0;
                    PlayerFuncs.IsPlayerMovable = true;
                }
                PlayerFuncs.KnifeReady = false;
            }
        }
    }

    public bool CheckifPlayerInfrontofEnemy(GameObject player, GameObject Enemy)
    {
        /*Debug.Log("ENemy:");
		Debug.Log(Enemy);
		Debug.Log(Enemy.transform.position.z);
		Debug.Log("player:");
		Debug.Log(player.transform.position.z);*/
        if (Enemy.transform.rotation.eulerAngles.y == 0
        && Enemy.transform.position.x == player.transform.position.x
        && Enemy.transform.position.z + 1 == player.transform.position.z
        && VerLineFuncs.CheckIfThereIsLine(Enemy.transform.position, 1, Enemy.transform.position + new Vector3(0, 0, 1)))
            return true;
        if (Enemy.transform.rotation.eulerAngles.y == 90
        && Enemy.transform.position.z == player.transform.position.z
        && Enemy.transform.position.x + 1 == player.transform.position.x
        && HorLineFuncs.CheckIfThereIsLine(Enemy.transform.position, 1, Enemy.transform.position + new Vector3(1, 0, 0)))
            return true;
        if (Enemy.transform.rotation.eulerAngles.y == 180
        && Enemy.transform.position.x == player.transform.position.x
        && Enemy.transform.position.z - 1 == player.transform.position.z
        && VerLineFuncs.CheckIfThereIsLine(Enemy.transform.position, -1, Enemy.transform.position + new Vector3(0, 0, -1)))
            return true;
        if (Enemy.transform.rotation.eulerAngles.y == 270
        && Enemy.transform.position.z == player.transform.position.z
        && Enemy.transform.position.x - 1 == player.transform.position.x
        && HorLineFuncs.CheckIfThereIsLine(Enemy.transform.position, -1, Enemy.transform.position + new Vector3(-1, 0, 0)))
            return true;
        return false;
    }


    private void DestroyIfClose(GameObject Obj)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject Projection = GameObject.FindGameObjectWithTag("Projection");
        //Debug.Log(Projection);
       /* Debug.Log(Obj.transform.rotation.eulerAngles.y == 0);
        Debug.Log(Obj.transform.position.x == player.transform.position.x);
        Debug.Log(Obj.transform.position.z + 1 == player.transform.position.z);
        Debug.Log(VerLineFuncs.CheckIfThereIsLine(Obj.transform.position, 1, Obj.transform.position + new Vector3(0, 0, 1)));*/
        //Debug.Log(Obj.);
        //Debug.Log(Obj.transform.position);
       // Debug.Log(MotEnemyFuncs.CheckifPlayerInfrontofEnemy(player, Obj));
        if (player.transform.position.x == Obj.transform.position.x
            && player.transform.position.z == Obj.transform.position.z
            && (!MotEnemyFuncs.CheckIfFacing(player, Obj) || PlayerFuncs.Invisible >= 0))
            Destroy(Obj);
        if (CheckifPlayerInfrontofEnemy(player, Obj) && PlayerFuncs.Invisible <= 0 && !PlayerFuncs.IsThereGate(Obj.transform))
            Application.LoadLevel(0);
        if (Projection != null)
        {
            if (Projection.transform.position.x == Obj.transform.position.x
                && Projection.transform.position.z == Obj.transform.position.z
                && (!MotEnemyFuncs.CheckIfFacing(Projection, Obj)))
                Destroy(Obj);
            if (CheckifPlayerInfrontofEnemy(Projection, Obj) && !PlayerFuncs.IsThereGate(Obj.transform))
            {
                
                PlayerFuncs.ProjectionActive = false;
                //Destroy(Projection);
                Projection.SetActive(false);
                //return;
            }
        }
    }

    public int Opposite(GameObject Obj)
    {
        if (Obj.transform.rotation.eulerAngles.y == 0)
        {
            // increment = -1;
           // Obj.transform.rotation = Quaternion.Euler(0, 180, 0);
            return (180);
        }
        else if (Obj.transform.rotation.eulerAngles.y == 90)
        {
            //    increment = -1;
            //Obj.transform.rotation = Quaternion.Euler(0, 270, 0);
            return (270);
        }
        else if (Obj.transform.rotation.eulerAngles.y == 180)
        {
            //   increment = 1;
           // Obj.transform.rotation = Quaternion.Euler(0, 0, 0);
            return (0);
        }
        else if (Obj.transform.rotation.eulerAngles.y == 270)
        {
            // increment = 1;
           // Obj.transform.rotation = Quaternion.Euler(0, 90, 0);
            return (90);
        }
        return (0);
    }

    public void TurnOtherWay(GameObject Obj)
    {
        // Debug.Log(transform.rotation);
        if (Obj.transform.rotation.eulerAngles.y == 0)
        {
           // increment = -1;
            Obj.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Obj.transform.rotation.eulerAngles.y == 90)
        {
        //    increment = -1;
            Obj.transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        else if (Obj.transform.rotation.eulerAngles.y == 180)
        {
         //   increment = 1;
            Obj.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Obj.transform.rotation.eulerAngles.y == 270)
        {
           // increment = 1;
            Obj.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        //Debug.Log(transform.rotation);
    }

    private int[] IsCrossing(GameObject[] ListOfEnemies)
    {
        int[] IndexArray = new int[ListOfEnemies.Length * 2];
        for (int i = 0; i < IndexArray.Length; i++)
        {
            IndexArray[i] = 50;
        }
        Vector3[] Positions = new Vector3[ListOfEnemies.Length];
        for (int i = 0; i < ListOfEnemies.Length; i++)
        {
            if (ListOfEnemies[i] != null)
            {
                if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 0)
                    Positions[i] = new Vector3(ListOfEnemies[i].transform.position.x, 1, ListOfEnemies[i].transform.position.z + 1);
                if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 90)
                    Positions[i] = new Vector3(ListOfEnemies[i].transform.position.x + 1, 1, ListOfEnemies[i].transform.position.z);
                if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 180)
                    Positions[i] = new Vector3(ListOfEnemies[i].transform.position.x, 1, ListOfEnemies[i].transform.position.z - 1);
                if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 270)
                    Positions[i] = new Vector3(ListOfEnemies[i].transform.position.x - 1, 1, ListOfEnemies[i].transform.position.z);
            }
            else
                Positions[i] = Vector3.zero;
        }

        for (int i = 0; i < Positions.Length; i++)
        {
            if (Positions[i] != Vector3.zero)
            {
                for (int j = i + 1; j < Positions.Length; j++)
                {
                    if (Positions[j] != Vector3.zero)
                    {
                        if (Positions[i] == Positions[j])
                        {
                            //Debug.Log("AAA");
                            for (int k = 0; k < IndexArray.Length; k++)
                            {
                                // Debug.Log("LOL");
                                //Debug.Log(i);
                                //Debug.Log(j);
                                //Debug.Log(IndexArray[k]);
                                if (IndexArray[k] == 50)
                                {
                                  //  Debug.Log("HUE");
                                    IndexArray[k] = i;
                                    IndexArray[k + 1] = j;
                                    break;
                                }
                                    
                            }
                        }
                    }
                }
            }
        }
        /*  for (int i = 0; i < IndexArray.Length; i++)
          {

          }*/
        //int[] RetArray = IndexArray.Distinct(int);
        int[] RetArray = new int[IndexArray.Length];
        for (int i = 0; i < RetArray.Length; i++)
        {
            RetArray[i] = 50;
        }
        for (int i = 0; i < IndexArray.Length; i++)
        {
            if (IndexArray[i] != 50)
                RetArray[i] = IndexArray[i];
        }
        int[] smth = new int[RetArray.Length];
        for (int i = 0; i < smth.Length; i++)
        {
            smth[i] = 50;
        }
        // bool flag = false;
        /*for (int i = 0; i < RetArray.Length; i++)
        {
            flag = false;
            for (int j = 0; j < smth.Length; j++)
            {
                if (RetArray[i] == smth[j])
                    break;
                flag = true;
            }
            if (flag == true)
                smth[i] = RetArray[i];
            else
                smth[i] = 50;
        }
        for (int i = 0; i < smth.Length; i++)
        {
            Debug.Log(smth[i]);
        }*/
        bool flag = false;
        for (int i = 0; i < RetArray.Length; i++)
        {
            flag = false ;
            for (int j = 0; j < smth.Length; j++)
            {
                if (RetArray[i] == smth[j])
                {
                    flag = true;
                    break;
                }
            }
            if (flag == false)
                smth[i] = RetArray[i];
        }
        int numb = 0;
        // int[] LastArray
        for (int i = 0; i < smth.Length; i++)
        {
            if (smth[i] != 50)
                numb++;
        }
        int[] LastArray = new int[numb];
        int m = 0;
        for (int i = 0; i < smth.Length; i++)
        {
            if (smth[i] != 50)
            {
                LastArray[m] = smth[i];
                m++;
            }
        }
       /* for (int i = 0; i < LastArray.Length; i++)
        {
            Debug.Log(LastArray[i]);
        }*/
        return LastArray;
    }

    public IEnumerator ReturnToMinusX(GameObject Enemy)
    {
        for (float i = 0; i < 1; i += 0.01f)
        {
            Enemy.transform.GetChild(0).position += new Vector3(-0.0028f, 0, 0);
            yield return new WaitForSeconds(0.004f);
        }
        yield return new WaitForSeconds(0.05f);
        Enemy.transform.GetChild(0).position = new Vector3(Mathf.Round(Enemy.transform.GetChild(0).position.x), Enemy.transform.GetChild(0).position.y, Enemy.transform.GetChild(0).position.z);
        yield return null;
    }

    public IEnumerator ReturnToX(GameObject Enemy)
    {
        for (float i = 0; i < 1; i += 0.01f)
        {
            Enemy.transform.GetChild(0).position += new Vector3(0.0028f, 0, 0);
            yield return new WaitForSeconds(0.004f);
        }
        yield return new WaitForSeconds(0.05f);
        Enemy.transform.GetChild(0).position = new Vector3(Mathf.Round(Enemy.transform.GetChild(0).position.x), Enemy.transform.GetChild(0).position.y, Enemy.transform.GetChild(0).position.z);
        yield return null;
    }

    public IEnumerator ReturnToY(GameObject Enemy)
    {
        for (float i = 0; i < 1; i += 0.01f)
        {
            Enemy.transform.GetChild(0).position += new Vector3(0, 0, 0.0028f);
            yield return new WaitForSeconds(0.004f);
        }
        yield return new WaitForSeconds(0.05f);
        Enemy.transform.GetChild(0).position = new Vector3(Enemy.transform.GetChild(0).position.x, Enemy.transform.GetChild(0).position.y, Mathf.Round(Enemy.transform.GetChild(0).position.z));
        yield return null;
    }

    public IEnumerator ReturnToMinusY(GameObject Enemy)
    {
        for (float i = 0; i < 1; i += 0.01f)
        {
            Enemy.transform.GetChild(0).position += new Vector3(0, 0, -0.0028f);
            yield return new WaitForSeconds(0.004f);
        }
        yield return new WaitForSeconds(0.05f);
        Enemy.transform.GetChild(0).position = new Vector3(Enemy.transform.GetChild(0).position.x, Enemy.transform.GetChild(0).position.y, Mathf.Round(Enemy.transform.GetChild(0).position.z));
        yield return null;
    }

    private void ReturnPositions(GameObject[] ListOfEnemies, int [] Indexes)
    {
        bool flag = false;
            for (int i = 0; i < ListOfEnemies.Length; i++)
            {
                flag = false;
                for (int j = 0; j < Indexes.Length; j++)
                {
                    if (i == Indexes[j])
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag == false && ListOfEnemies[i] != null)
                {
                //  Debug.Log("HERE");
                //Debug.Log(ListOfEnemies[i]);
                // if (Mathf.Abs(ListOfEnemies[i].transform.GetChild(0).position.x - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.x)) > 0 || Mathf.Abs(ListOfEnemies[i].transform.GetChild(0).position.z - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.z)) > 0)
                //if (Mathf.Abs(ListOfEnemies[i].transform.GetChild(0).position.x - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.x)) > 0)
                if (ListOfEnemies[i].transform.GetChild(0).position.x - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.x) < 0)
                {
                  //  Debug.Log(ListOfEnemies[i]);
                   // Debug.Log(1);
                    //ListOfEnemies[i].transform.GetChild(0).position = new Vector3(Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.x), ListOfEnemies[i].transform.GetChild(0).position.y, ListOfEnemies[i].transform.GetChild(0).position.z);
                    StartCoroutine("ReturnToX", ListOfEnemies[i]);
                }
                if (ListOfEnemies[i].transform.GetChild(0).position.x - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.x) > 0)
                {
                   // Debug.Log(ListOfEnemies[i]);
                    //Debug.Log(2);
                    StartCoroutine("ReturnToMinusX", ListOfEnemies[i]);
                }
                //ListOfEnemies[i].transform.GetChild(0).position = new Vector3(Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.x), ListOfEnemies[i].transform.GetChild(0).position.y, ListOfEnemies[i].transform.GetChild(0).position.z);
                //if (Mathf.Abs(ListOfEnemies[i].transform.GetChild(0).position.z - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.z)) > 0)
                if (ListOfEnemies[i].transform.GetChild(0).position.z - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.z) < 0)
                {
                  //  Debug.Log(ListOfEnemies[i]);
                    //Debug.Log(3);
                    StartCoroutine("ReturnToY", ListOfEnemies[i]);
                }
                // ListOfEnemies[i].transform.GetChild(0).position = new Vector3(ListOfEnemies[i].transform.GetChild(0).position.x, ListOfEnemies[i].transform.GetChild(0).position.y, Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.z));
                if (ListOfEnemies[i].transform.GetChild(0).position.z - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.z) > 0)
                {
                   // Debug.Log(ListOfEnemies[i]);
                    //Debug.Log(4);
                    //Debug.Log(ListOfEnemies[i].transform.GetChild(0).position.z);
                    //Debug.Log(Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.z));
                    StartCoroutine("ReturnToMinusY", ListOfEnemies[i]);
                }
                    // ListOfEnemies[i].transform.GetChild(0).position = new Vector3(ListOfEnemies[i].transform.GetChild(0).position.x, ListOfEnemies[i].transform.GetChild(0).position.y, Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.z));
                    //ListOfEnemies[i].transform.GetChild(0).position = new Vector3(Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.x), ListOfEnemies[i].transform.GetChild(0).position.y, Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.z));
                    // if (ListOfEnemies[i].transform.GetChild(0).position.z - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.z) >= 0.01f)
                    //   ListOfEnemies[i].transform.GetChild(0).position = new Vector3(ListOfEnemies[i].transform.GetChild(0).position.x, ListOfEnemies[i].transform.GetChild(0).position.y, Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.z));
                }
            }
    }

    public IEnumerator GettingSlower(GameObject Enemy)
    {
        //Time.timeScale = 0.05f;
        //for (float i = 0; i < 0.25f; i += 0.01f)
        for (float i = 0; i < 1; i += 0.01f)
        {
            if (Enemy.transform.GetChild(0).rotation.eulerAngles.y == 0)
            {
                //Enemy.transform.GetChild(0).position += new Vector3(0.01f, 0, -0.01f);
                //Enemy.transform.GetChild(0).rotation = Quaternion.Euler(0, 12.6f, 0);
                Enemy.transform.GetChild(0).position += new Vector3(0.0028f, 0, -0.0028f);
            }
            if (Enemy.transform.GetChild(0).rotation.eulerAngles.y == 90)
            {
                //Enemy.transform.GetChild(0).position += new Vector3(-0.01f, 0, -0.01f);
                Enemy.transform.GetChild(0).position += new Vector3(-0.0028f, 0, -0.0028f);
            }
            if (Enemy.transform.GetChild(0).rotation.eulerAngles.y == 180)
            {
                //Enemy.transform.GetChild(0).position += new Vector3(-0.01f, 0, 0.01f);
                Enemy.transform.GetChild(0).position += new Vector3(-0.0028f, 0, 0.0028f);
            }
            if (Enemy.transform.GetChild(0).rotation.eulerAngles.y == 270)
            {
                //Enemy.transform.GetChild(0).position += new Vector3(0.01f, 0, 0.01f);
                Enemy.transform.GetChild(0).position += new Vector3(0.0028f, 0, 0.0028f);
            }
            yield return new WaitForSeconds(0.004f);
        }
        yield return null;
    }

    public IEnumerator LineMovingEnemyWalk2(GameObject[] ListOfEnemies)
    {
        Transform[] ListOfTransforms = new Transform[ListOfEnemies.Length];
        //for (int j = 0; j < ListOfEnemies.Length; j++)
        // {
        //Debug.Log(PlayerFuncs.Waiting);
        //if (ListOfEnemies[j] != null)
        //{
        // Debug.Log(Obj);
        //animator.SetBool("IsRunning", true);
        for (int i = 0; i < ListOfEnemies.Length; i++)
        {
           // Debug.Log(ListOfEnemies[i]);
           // Debug.Log(CheckIfThereIsNodeToMove(ListOfEnemies[i]));
           // Debug.Log(PlayerFuncs.IsThereGate(ListOfEnemies[i].transform));
            //Debug.Log(PlayerFuncs.CheckIfThereIsMotEnemy(ListOfEnemies[i]));
            if (ListOfEnemies[i] != null && (!CheckIfThereIsNodeToMove(ListOfEnemies[i]) || PlayerFuncs.IsThereGate(ListOfEnemies[i].transform) || PlayerFuncs.CheckIfThereIsMotEnemy(ListOfEnemies[i])))
            {
                //Debug.Log("HERE");
                PlayerFuncs.StartCoroutine("RotateEnemies", ListOfEnemies[i]);
            }
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < ListOfEnemies.Length; i++)
        {
            
            //Debug.Log(ListOfEnemies[i]);
            if (ListOfEnemies[i] == null || !CheckIfThereIsNodeToMove(ListOfEnemies[i]) 
            || PlayerFuncs.IsThereGate(ListOfEnemies[i].transform)
            || PlayerFuncs.CheckIfThereIsMotEnemy(ListOfEnemies[i])
            || !(HorLineFuncs.CheckIfThereIsLine(ListOfEnemies[i].transform.position, -1, ListOfEnemies[i].transform.position + new Vector3(-1, 0, 0)) 
            || HorLineFuncs.CheckIfThereIsLine(ListOfEnemies[i].transform.position, 1, ListOfEnemies[i].transform.position + new Vector3(1, 0, 0)) 
            || VerLineFuncs.CheckIfThereIsLine(ListOfEnemies[i].transform.position, 1, ListOfEnemies[i].transform.position + new Vector3(0, 0, 1)) 
            || VerLineFuncs.CheckIfThereIsLine(ListOfEnemies[i].transform.position, -1, ListOfEnemies[i].transform.position + new Vector3(0, 0, -1))))
                ListOfEnemies[i] = null;
        }
        for (int i = 0; i < ListOfEnemies.Length; i++)
        {
            if (ListOfEnemies[i] != null)
                ListOfTransforms[i] = ListOfEnemies[i].transform;
            else
                ListOfTransforms[i] = null;
        }
        int [] Indexes = IsCrossing(ListOfEnemies);
        ReturnPositions(ListOfEnemies, Indexes);
        for (int i = 0; i < Indexes.Length; i++)
        {
            StartCoroutine("GettingSlower", ListOfEnemies[Indexes[i]]);
        }
     /*   for (int i = 0; i < ListOfEnemies.Length; i++)
        {
            if (ListOfEnemies[i] != null && (!CheckIfThereIsNodeToMove(ListOfEnemies[i]) || PlayerFuncs.IsThereGate(ListOfEnemies[i].transform) || PlayerFuncs.CheckIfThereIsMotEnemy(ListOfEnemies[i])))
                PlayerFuncs.StartCoroutine("RotateEnemies", ListOfEnemies[i]);
        }*/
        for (float i = 0; i < 1; i += 0.01f)
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
            for (int j = 0; j < ListOfEnemies.Length; j++)
            {
                if (ListOfEnemies[j] != null)
                {
                    /* try
                     {
                         ListOfEnemies[j].GetComponent<Animator>().SetBool("IsRunning", true);
                     }
                     catch
                     {
                         ListOfEnemies[j].GetComponentInChildren<Animator>().SetBool("IsRunning", true);
                     }*/
                    ListOfEnemies[j].GetComponentInChildren<Animator>().SetBool("IsRunning", true);
                   // Debug.Log(ListOfEnemies[j].GetComponentInChildren<Animator>().gameObject);
                    //animator.SetBool("IsRunning", true);
                    //Debug.Log(animator.GetBool("IsRunning"));
                    // animator.SetBool("IsRunning", false);
                    //animator.SetTrigger("IsRunning");
                    //Debug.Log("LOL");
                    //animator.SetBool("IsRunning", false);
                    // ListOfEnemies[j].GetComponent<Animator>().SetBool("IsRunning", true);
                    /* ListOfEnemies[j].GetComponent<Animator>().SetTarget(AvatarTarget.Root, 1.0f);
                     ListOfEnemies[j].GetComponent<Animator>().Update(0);
                     ListOfEnemies[j].transform.position = animator.targetPosition;*/
                    //ListOfEnemies[j].GetComponent<Animation>()["mixamo_com"].speed = 1f;
                    //if (ListOfEnemies[j].transform.rotation.eulerAngles.y == 0)
                    if (ListOfTransforms[j].rotation.eulerAngles.y == 0)
                    {
                         ListOfEnemies[j].transform.position += new Vector3(0, 0, 0.01f);
                      //  Debug.Log(i);
                       // ListOfEnemies[j].transform.position = new Vector3(ListOfTransforms[j].position.x, ListOfTransforms[j].position.y, ListOfTransforms[j].position.z + i / 2);
                     //   ListOfEnemies[j].transform.rotation = Quaternion.Euler(0, ListOfTransforms[j].rotation.y, 0);
                    }
                    if (ListOfEnemies[j].transform.rotation.eulerAngles.y == 90)
                        ListOfEnemies[j].transform.position += new Vector3(0.01f, 0, 0);
                    if (ListOfEnemies[j].transform.rotation.eulerAngles.y == 180)
                        ListOfEnemies[j].transform.position += new Vector3(0, 0, -0.01f);
                    if (ListOfEnemies[j].transform.rotation.eulerAngles.y == 270)
                        ListOfEnemies[j].transform.position += new Vector3(-0.01f, 0, 0);
                }
            }
            yield return new WaitForSeconds(0.004f);
        }
        /*Debug.Log(animator.GetBool("IsRunning"));
        animator.SetBool("IsRunning", false);
        Debug.Log(animator.GetBool("IsRunning"));*/
        // Debug.Log(XorY);
        for (int j = 0; j < ListOfEnemies.Length; j++)
        {
            if (ListOfEnemies[j] != null)
            {
                //ListOfEnemies[j].GetComponent<Animator>().SetBool("IsRunning", false);
                ListOfEnemies[j].GetComponentInChildren<Animator>().SetBool("IsRunning", false);
                // Debug.Log(ListOfEnemies[j].GetComponent<Animator>().Ge);
                //  ListOfEnemies[j].GetComponent<Animation>()["mixamo_com"].speed = 0f;
                //ListOfEnemies[j].GetComponent<Animator>().["mixamo_com"].speed = 0;
                // ListOfEnemies[j].GetComponent<Animator>().
                //animator["mixamo_com"].speed = 0f;
                //ListOfEnemies[j].transform.rotation = Quaternion.Euler(0, ListOfTransforms[j].rotation.y, 0);
                if (ListOfEnemies[j].transform.rotation.eulerAngles.y == 270 || ListOfEnemies[j].transform.rotation.eulerAngles.y == 90)
                    ListOfEnemies[j].transform.position = new Vector3(Mathf.Round(ListOfEnemies[j].transform.position.x), ListOfEnemies[j].transform.position.y, ListOfEnemies[j].transform.position.z);
                if (ListOfEnemies[j].transform.rotation.eulerAngles.y == 0 || ListOfEnemies[j].transform.rotation.eulerAngles.y == 180)
                    ListOfEnemies[j].transform.position = new Vector3(ListOfEnemies[j].transform.position.x, ListOfEnemies[j].transform.position.y, Mathf.Round(ListOfEnemies[j].transform.position.z));
                DestroyIfClose(ListOfEnemies[j]);
                //Debug.Log("HERE");
            }
        }
                // Debug.Log(Obj.transform.position);
                /* if (XorY == 'x')
                     Obj.transform.position = new Vector3(Mathf.Round(Obj.transform.position.x), Obj.transform.position.y, Obj.transform.position.z);
                 if (XorY == 'y')
                     Obj.transform.position = new Vector3(Obj.transform.position.x, Obj.transform.position.y, Mathf.Round(Obj.transform.position.z));*/
           // }
        //}
        //Debug.Log("HERE");
        PlayerFuncs.Waiting = false;
       // Debug.Log(ProjectionBehaviourFuncs);
        //if (ProjectionBehaviourHandler != null)
        //ProjectionBehaviourFuncs.Waiting = false;
        yield return null;
    }

    public IEnumerator LineMovingEnemyWalk(GameObject Obj)
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
           // animator.SetBool("IsRunning", true);
           // Debug.Log(animator.GetNextAnimatorStateInfo(1));
            if (Obj.transform.rotation.eulerAngles.y == 0)
                Obj.transform.position += new Vector3(0, 0, 0.2f);
            if (Obj.transform.rotation.eulerAngles.y == 90)
                Obj.transform.position += new Vector3(0.2f, 0, 0);
            if (Obj.transform.rotation.eulerAngles.y == 180)
                Obj.transform.position += new Vector3(0, 0, -0.2f);
            if (Obj.transform.rotation.eulerAngles.y == 270)
                Obj.transform.position += new Vector3(-0.2f , 0,0);
            //animator.SetBool("IsRunning", false);
            yield return new WaitForSeconds(0.1f);
        }
        //animator.SetBool("IsRunning", false);
        // Debug.Log(XorY);
        if (Obj.transform.rotation.eulerAngles.y == 270 || Obj.transform.rotation.eulerAngles.y == 90)
            Obj.transform.position = new Vector3(Mathf.Round(Obj.transform.position.x), Obj.transform.position.y, Obj.transform.position.z);
        if (Obj.transform.rotation.eulerAngles.y == 0 || Obj.transform.rotation.eulerAngles.y == 180)
            Obj.transform.position = new Vector3(Obj.transform.position.x, Obj.transform.position.y, Mathf.Round(Obj.transform.position.z));
        DestroyIfClose(Obj);
       // Debug.Log(Obj.transform.position);
        /* if (XorY == 'x')
             Obj.transform.position = new Vector3(Mathf.Round(Obj.transform.position.x), Obj.transform.position.y, Obj.transform.position.z);
         if (XorY == 'y')
             Obj.transform.position = new Vector3(Obj.transform.position.x, Obj.transform.position.y, Mathf.Round(Obj.transform.position.z));*/
        yield return null;
    }


    public bool CheckIfThereIsNodeToMove(GameObject Obj)
    {
        //Debug.Log(VerLineFuncs);
        /*Debug.Log(gameObject.activeInHierarchy);
        gameObject.SetActive(true);
        Debug.Log(gameObject.activeInHierarchy);*/
        Start();
        //Debug.Log(Obj.transform.rotation);
        if (Obj.transform.rotation.eulerAngles.y == 0)
        {
          //  XorY = 'y';
            //increment = 1;
            if (NodeFuncs.CheckIfNodeExist(Obj.transform.position, 'y', 1)
            && VerLineFuncs.CheckIfThereIsLine(Obj.transform.position, 1, Obj.transform.position + new Vector3(0, 0, 1)))
                return true;
        }
        if (Obj.transform.rotation.eulerAngles.y == 90)
        {
           // XorY = 'x';
            //increment = 1;
            if (NodeFuncs.CheckIfNodeExist(Obj.transform.position, 'x', 1)
            && HorLineFuncs.CheckIfThereIsLine(Obj.transform.position, 1, Obj.transform.position + new Vector3(1, 0, 0)))
                return true;
        }
        if (Obj.transform.rotation.eulerAngles.y == 180)
        {
           // XorY = 'y';
            //increment = -1;
            if (NodeFuncs.CheckIfNodeExist(Obj.transform.position, 'y', -1)
            && VerLineFuncs.CheckIfThereIsLine(Obj.transform.position, -1, Obj.transform.position + new Vector3(0, 0, -1)))
                return true;
        }
        if (Obj.transform.rotation.eulerAngles.y == 270)
        {
           // XorY = 'x';
           // increment = -1;
            if (NodeFuncs.CheckIfNodeExist(Obj.transform.position, 'x', -1)
            && HorLineFuncs.CheckIfThereIsLine(Obj.transform.position, -1, Obj.transform.position + new Vector3(-1, 0, 0)))
                return true;
        }
        return false;
    }

    public void LineMovingEnemyMove(GameObject Obj)
    {
        //Debug.Log(Obj);
        /* if (CheckIfThereIsNodeToMove(Obj))
             StartCoroutine("LineMovingEnemyWalk", Obj);
         else
         {
             TurnOtherWay(Obj);
             StartCoroutine("LineMovingEnemyWalk", Obj);
         }*/
        //Debug.Log(Obj);
        StartCoroutine("LineMovingEnemyWalk", Obj);
    }

   /* private bool CheckIfPlayerInfrontOfEnemy(GameObject player)
    {
        if (gameObject.transform.rotation.eulerAngles.y == 0
        && gameObject.transform.position.x == player.transform.position.x
        && gameObject.transform.position.z + 1 == player.transform.position.z
        && VerLineFuncs.CheckIfThereIsLine(gameObject.transform.position, 1, gameObject.transform.position + new Vector3(0, 0, 1)))
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
    }*/

    // Start is called before the first frame update
    void Start()
    {
        NodeFuncs = NodeHandler.GetComponent<Node>();
        VerLineFuncs = VerLineHandler.GetComponent<VerticalLine>();
        HorLineFuncs = HorLineHandler.GetComponent<HorizontalLine>();
        MotEnemyFuncs = MotionlessEnemyHandler.GetComponent<MotionlessEnemy>();
        PlayerFuncs = PlayerHandler.GetComponent<Player>();
        KnifeFuncs = KnifeHandler.GetComponent<ThrowKnife>();
        ProjectionBehaviourFuncs = ProjectionBehaviourHandler.GetComponent<ProjectionBehaviour>();
        animator = GetComponent<Animator>();
        //PlayerFuncs = PlayerHandler.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
       /* GameObject[] ListOfEnemies = GameObject.FindGameObjectsWithTag("LineMovingEnemy");
        for (int i = 0; i < ListOfEnemies.Length; i++)
        {
            for (int j = 0; j < ListOfEnemies.Length; j++)
            {
                if (i != j)
                {
                    //if (ListOfEnemies[i].transform.position.x == ListOfEnemies[j].transform.position.x && ListOfEnemies[i].transform.position.z == ListOfEnemies[j].transform.position.z)
                    if (Mathf.Abs(ListOfEnemies[i].transform.position.x - ListOfEnemies[j].transform.position.x) <= 0.4f && Mathf.Abs(ListOfEnemies[i].transform.position.z - ListOfEnemies[j].transform.position.z) <= 0.4f)
                    {
                        //ListOfEnemies[i].GetComponentInChildren<Transform>().localPosition = ListOfEnemies[i].transform.position + new Vector3(0.5f, 0, 0);
                        if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 0)
                            ListOfEnemies[i].transform.GetChild(0).position = ListOfEnemies[i].transform.position + new Vector3(0, 0, -0.2f);
                        if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 90)
                            ListOfEnemies[i].transform.GetChild(0).position = ListOfEnemies[i].transform.position + new Vector3(-0.2f, 0, 0);
                        if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 180)
                            ListOfEnemies[i].transform.GetChild(0).position = ListOfEnemies[i].transform.position + new Vector3(0, 0, 0.2f);
                        if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 270)
                            ListOfEnemies[i].transform.GetChild(0).position = ListOfEnemies[i].transform.position + new Vector3(0.2f, 0, 0);
                        //ListOfEnemies[i].transform.GetChild(0).position = ListOfEnemies[i].transform.position + new Vector3(0.5f, 0, 0);
                        // Debug.Log(ListOfEnemies[i].GetComponentInChildren<Transform>().gameObject) ;
                    }
                }
            }
        }*/
        //gameObject.transform.rotation = gameObject.
        /*   if (CheckIfThereIsNodeToMove())
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

        /*GameObject player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(MotEnemyFuncs.CheckifPlayerInfrontofEnemy(player));
        if (transform.position.x == player.transform.position.x
        && transform.position.z == player.transform.position.z
        && !MotEnemyFuncs.CheckIfFacing(player))
            Destroy(gameObject);
        if (MotEnemyFuncs.CheckifPlayerInfrontofEnemy(player))
            Application.LoadLevel(0);*/
    }
}
