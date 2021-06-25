using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    IEnumerator StopBreaking()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int RequiredAngle = 0;
        if (player.transform.position.x > gameObject.transform.position.x)
            RequiredAngle = 270;
        else if (player.transform.position.x < gameObject.transform.position.x)
            RequiredAngle = 90;
        else if (player.transform.position.z > gameObject.transform.position.z)
            RequiredAngle = 180;
        else
            RequiredAngle = 0;
        int playerangle = (int)player.transform.rotation.eulerAngles.y;
        int Diff = RequiredAngle - playerangle;
        if (Diff == 270 || Diff == -270)
            Diff = -Diff % 180;
        if (Diff != 0)
        {
            player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 1);
            for (int i = 0; i < 30; i++)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y + Diff / 30, 0);
                yield return new WaitForSeconds(0.0133f);
            }
            player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
            gameObject.transform.rotation = Quaternion.Euler(0, RequiredAngle, 0);
        }
        yield return new WaitForSeconds(1);
        for (int i = 0; i < PlayerFuncs.EnemiesKill.Length; i++)
        {
            if (PlayerFuncs.EnemiesKill[i] == null)
            {
                PlayerFuncs.EnemiesKill[i] = gameObject;
                break;
            }
        }
        gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("IsDead", true);
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0.6f;
        PlayerHandler.GetComponent<Animator>().SetBool("IsTaunting", false);
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1;
        yield return null;
    }

    private void OnMouseDown()
    {
        if (Time.timeScale == 1)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (PlayerFuncs.KnifeReady == true)
            {
                if (KnifeFuncs.CheckIfInRange(gameObject, player))
                {
                    Time.timeScale = 0.99f;
                    PlayerFuncs.gameObject.GetComponent<Animator>().SetBool("IsTaunting", true);
                    StartCoroutine("StopBreaking");
                    if (GameObject.FindObjectOfType<FillKnife>() != null)
                        GameObject.FindObjectOfType<FillKnife>().GetComponent<Image>().fillAmount = 0;
                    PlayerFuncs.IsPlayerMovable = true;
                }
                PlayerFuncs.KnifeReady = false;
            }
        }
    }

    public bool CheckifPlayerInfrontofEnemy(GameObject player, GameObject Enemy)
    {
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
        if ((CheckifPlayerInfrontofEnemy(player, Obj) || (player.transform.position.x == Obj.transform.position.x && player.transform.position.z == Obj.transform.position.z)) && PlayerFuncs.Invisible <= 0 && !PlayerFuncs.IsThereGate(Obj.transform))
        {
            Obj.transform.GetChild(0).GetComponent<Animator>().SetBool("IsKilling", true);
            PlayerFuncs.StartCoroutine("KillingAnimation", Obj);
            return;
        }
        if (player.transform.position.x == Obj.transform.position.x
            && player.transform.position.z == Obj.transform.position.z && PlayerFuncs.Invisible >= 0)
            Destroy(Obj);
        if (Projection != null)
        {
            if (Projection.transform.position.x == Obj.transform.position.x
            && Projection.transform.position.z == Obj.transform.position.z && PlayerFuncs.Invisible >= 0)
                Destroy(Obj);
            if (CheckifPlayerInfrontofEnemy(Projection, Obj) && !PlayerFuncs.IsThereGate(Obj.transform))
            {
                PlayerFuncs.ProjectionActive = false;
                Projection.SetActive(false);
            }
        }
    }

    public int Opposite(GameObject Obj)
    {
        if (Obj.transform.rotation.eulerAngles.y == 0)
            return (180);
        else if (Obj.transform.rotation.eulerAngles.y == 90)
            return (270);
        else if (Obj.transform.rotation.eulerAngles.y == 180)
            return (0);
        else if (Obj.transform.rotation.eulerAngles.y == 270)
            return (90);
        return (0);
    }

    public void TurnOtherWay(GameObject Obj)
    {
        if (Obj.transform.rotation.eulerAngles.y == 0)
            Obj.transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (Obj.transform.rotation.eulerAngles.y == 90)
            Obj.transform.rotation = Quaternion.Euler(0, 270, 0);
        else if (Obj.transform.rotation.eulerAngles.y == 180)
            Obj.transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (Obj.transform.rotation.eulerAngles.y == 270)
            Obj.transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    private int[] IsCrossing(GameObject[] ListOfEnemies)
    {
        int[] IndexArray = new int[ListOfEnemies.Length * 2];
        for (int i = 0; i < IndexArray.Length; i++)
            IndexArray[i] = 50;
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
                            for (int k = 0; k < IndexArray.Length; k++)
                            {
                                if (IndexArray[k] == 50)
                                {
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

    private int ReturnPositionsToTurn(GameObject[] ListOfEnemies, int[] Indexes)
    {
        int ret = 0;
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
                if (ListOfEnemies[i].transform.GetChild(0).position.x - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.x) < 0)
                {
                    ret++;
                    ListOfEnemies[i].transform.GetChild(0).GetComponent<Animator>().SetBool("IsRunning", true);
                    StartCoroutine("ReturnToX", ListOfEnemies[i]);
                }
                if (ListOfEnemies[i].transform.GetChild(0).position.x - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.x) > 0)
                {
                    ret++;
                    ListOfEnemies[i].transform.GetChild(0).GetComponent<Animator>().SetBool("IsRunning", true);
                    StartCoroutine("ReturnToMinusX", ListOfEnemies[i]);
                }
                if (ListOfEnemies[i].transform.GetChild(0).position.z - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.z) < 0)
                {
                    ret++;
                    ListOfEnemies[i].transform.GetChild(0).GetComponent<Animator>().SetBool("IsRunning", true);
                    StartCoroutine("ReturnToY", ListOfEnemies[i]);
                }
                if (ListOfEnemies[i].transform.GetChild(0).position.z - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.z) > 0)
                {
                    ret++;
                    ListOfEnemies[i].transform.GetChild(0).GetComponent<Animator>().SetBool("IsRunning", true);
                    StartCoroutine("ReturnToMinusY", ListOfEnemies[i]);
                }
                if (ret != 0)
                {
                    if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 0)
                        ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, -45, 0);
                    if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 90)
                        ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 45, 0);
                    if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 180)
                        ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 135, 0);
                    if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 270)
                        ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 235, 0);
                }      
            }
        }
        return ret;
    }

    private void ReturnPositions(GameObject[] ListOfEnemies, int [] Indexes)
    {
        bool flag = false;
        bool NeedToRotate = false;
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
                if (ListOfEnemies[i].transform.GetChild(0).position.x - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.x) < 0)
                {
                    NeedToRotate = true;
                    StartCoroutine("ReturnToX", ListOfEnemies[i]);
                }
                if (ListOfEnemies[i].transform.GetChild(0).position.x - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.x) > 0)
                {
                    NeedToRotate = true;
                    StartCoroutine("ReturnToMinusX", ListOfEnemies[i]);
                }
                if (ListOfEnemies[i].transform.GetChild(0).position.z - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.z) < 0)
                {
                    NeedToRotate = true;
                    StartCoroutine("ReturnToY", ListOfEnemies[i]);
                }
                if (ListOfEnemies[i].transform.GetChild(0).position.z - Mathf.Round(ListOfEnemies[i].transform.GetChild(0).position.z) > 0)
                {
                    NeedToRotate = true;
                    StartCoroutine("ReturnToMinusY", ListOfEnemies[i]);
                }
                if (NeedToRotate == true)
                {
                    if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 0)
                        ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, -12.6f, 0);
                    if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 90)
                        ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 77.4f, 0);
                    if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 180)
                        ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 167.4f, 0);
                    if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 270)
                        ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 257.4f, 0);
                }
            }
        }
    }

    public IEnumerator GettingSlower(GameObject Enemy)
    {
        if (!(Mathf.Abs(Enemy.transform.GetChild(0).position.x - Mathf.Round(Enemy.transform.GetChild(0).position.x)) > 0.2f) && !(Mathf.Abs(Enemy.transform.GetChild(0).position.z - Mathf.Round(Enemy.transform.GetChild(0).position.z)) > 0.2f))
        {
            for (float i = 0; i < 1; i += 0.01f)
            {
                if (Enemy.transform.rotation.eulerAngles.y == 0)
                {
                    Enemy.transform.GetChild(0).rotation = Quaternion.Euler(0, 12.6f, 0);
                    Enemy.transform.GetChild(0).position += new Vector3(0.0028f, 0, -0.0028f);
                }
                if (Enemy.transform.rotation.eulerAngles.y == 90)
                {
                    Enemy.transform.GetChild(0).rotation = Quaternion.Euler(0, 102.6f, 0);
                    Enemy.transform.GetChild(0).position += new Vector3(-0.0028f, 0, -0.0028f);
                }
                if (Enemy.transform.rotation.eulerAngles.y == 180)
                {
                    Enemy.transform.GetChild(0).rotation = Quaternion.Euler(0, 192.6f, 0);
                    Enemy.transform.GetChild(0).position += new Vector3(-0.0028f, 0, 0.0028f);
                }
                if (Enemy.transform.rotation.eulerAngles.y == 270)
                {
                    Enemy.transform.GetChild(0).rotation = Quaternion.Euler(0, 282.6f, 0);
                    Enemy.transform.GetChild(0).position += new Vector3(0.0028f, 0, 0.0028f);
                }
                yield return new WaitForSeconds(0.004f);
            }
        }
        if (Mathf.Abs(Enemy.transform.GetChild(0).rotation.eulerAngles.y - 12.6f) < 0.1f)
            Enemy.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);
        if (Mathf.Abs(Enemy.transform.GetChild(0).rotation.eulerAngles.y - 102.6f) < 0.1f)
            Enemy.transform.GetChild(0).rotation = Quaternion.Euler(0, 90, 0);
        if (Mathf.Abs(Enemy.transform.GetChild(0).rotation.eulerAngles.y - 192.6f) < 0.1f)
            Enemy.transform.GetChild(0).rotation = Quaternion.Euler(0, 180, 0);
        if (Mathf.Abs(Enemy.transform.GetChild(0).rotation.eulerAngles.y - 282.6f) < 0.1f)
            Enemy.transform.GetChild(0).rotation = Quaternion.Euler(0, 270, 0);
        yield return null;
    }

    public IEnumerator LineMovingEnemyWalk2(GameObject[] ListOfEnemies)
    {
        Transform[] ListOfTransforms = new Transform[ListOfEnemies.Length];
        GameObject[] ListBuf = new GameObject[ListOfEnemies.Length];
        ListOfEnemies.CopyTo(ListBuf , 0);
        for (int i = 0; i < ListOfEnemies.Length; i++)
        {
            if (ListOfEnemies[i] != null && (!CheckIfThereIsNodeToMove(ListOfEnemies[i]) || PlayerFuncs.IsThereGate(ListOfEnemies[i].transform) || PlayerFuncs.IsThereCamera(ListOfEnemies[i].transform) || PlayerFuncs.CheckIfThereIsMotEnemy(ListOfEnemies[i])))
            {
                //ListBuf[i] = null;///gavno
            }
            else
            {
                ListBuf[i] = null;
            }
        }
        int[] Indexes = IsCrossing(ListBuf);
        if (ReturnPositionsToTurn(ListBuf, Indexes) != 0)
        {
            yield return new WaitForSeconds(0.5f);
        }
        for (int i = 0; i < ListBuf.Length; i++)
        {
            if (ListBuf[i] != null)
            {
                if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 0)
                    ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);
                if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 90)
                    ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 90, 0);
                if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 180)
                    ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 180, 0);
                if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 270)
                    ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 270, 0);
                ListBuf[i].transform.GetChild(0).GetComponent<Animator>().SetBool("IsRunning", false);
            }
        }
        for (int i = 0; i < ListOfEnemies.Length; i++)
        {
            if (ListOfEnemies[i] != null && (!CheckIfThereIsNodeToMove(ListOfEnemies[i]) || PlayerFuncs.IsThereGate(ListOfEnemies[i].transform) || PlayerFuncs.IsThereCamera(ListOfEnemies[i].transform) || PlayerFuncs.CheckIfThereIsMotEnemy(ListOfEnemies[i])))
            {
                PlayerFuncs.StartCoroutine("RotateEnemies", ListOfEnemies[i]);
            }
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < ListOfEnemies.Length; i++)
        {
            if (ListOfEnemies[i] == null || !CheckIfThereIsNodeToMove(ListOfEnemies[i]) 
            || PlayerFuncs.IsThereGate(ListOfEnemies[i].transform)
            || PlayerFuncs.IsThereCamera(ListOfEnemies[i].transform)
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
        Indexes = IsCrossing(ListOfEnemies);
        ReturnPositions(ListOfEnemies, Indexes);
        for (int i = 0; i < Indexes.Length; i++)
        {
            StartCoroutine("GettingSlower", ListOfEnemies[Indexes[i]]);
        }
        for (int i = 0; i < ListOfEnemies.Length; i++)
        {
            if (ListOfEnemies[i] != null)
                PlayerFuncs.CheckIfThereIsStairway(ListOfEnemies[i]);
        }
        for (float i = 0; i < 1; i += 0.01f)
        {
            for (int j = 0; j < ListOfEnemies.Length; j++)
            {
                if (ListOfEnemies[j] != null)
                {
                    ListOfEnemies[j].GetComponentInChildren<Animator>().SetBool("IsRunning", true);
                    if (ListOfTransforms[j].rotation.eulerAngles.y == 0)
                    {
                         ListOfEnemies[j].transform.position += new Vector3(0, 0, 0.01f);
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
        for (int i = 0; i < ListOfEnemies.Length; i++)
        {
            if (ListOfEnemies[i] != null)
            {
                if (Mathf.Abs(ListOfEnemies[i].transform.GetChild(0).rotation.eulerAngles.y - 347.4f) < 0.1f)
                    ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);
                if (Mathf.Abs(ListOfEnemies[i].transform.GetChild(0).rotation.eulerAngles.y - 77.4f) < 0.1f)
                    ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 90, 0);
                if (Mathf.Abs(ListOfEnemies[i].transform.GetChild(0).rotation.eulerAngles.y - 167.4f) < 0.1f)
                    ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 180, 0);
                if (Mathf.Abs(ListOfEnemies[i].transform.GetChild(0).rotation.eulerAngles.y - 257.4f) < 0.1f)
                    ListOfEnemies[i].transform.GetChild(0).rotation = Quaternion.Euler(0, 270, 0);
            }
        }
        for (int j = 0; j < ListOfEnemies.Length; j++)
        {
            if (ListOfEnemies[j] != null)
            {
                ListOfEnemies[j].GetComponentInChildren<Animator>().SetBool("IsRunning", false);
                if (ListOfEnemies[j].transform.rotation.eulerAngles.y == 270 || ListOfEnemies[j].transform.rotation.eulerAngles.y == 90)
                    ListOfEnemies[j].transform.position = new Vector3(Mathf.Round(ListOfEnemies[j].transform.position.x), ListOfEnemies[j].transform.position.y, ListOfEnemies[j].transform.position.z);
                if (ListOfEnemies[j].transform.rotation.eulerAngles.y == 0 || ListOfEnemies[j].transform.rotation.eulerAngles.y == 180)
                    ListOfEnemies[j].transform.position = new Vector3(ListOfEnemies[j].transform.position.x, ListOfEnemies[j].transform.position.y, Mathf.Round(ListOfEnemies[j].transform.position.z));
                DestroyIfClose(ListOfEnemies[j]);
            }
        }
        PlayerFuncs.Waiting = false;
        yield return null;
    }

    public IEnumerator LineMovingEnemyWalk(GameObject Obj)
    {
        for (float i = 0; i < 1; i += 0.2f)
        {
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
        if (Obj.transform.rotation.eulerAngles.y == 270 || Obj.transform.rotation.eulerAngles.y == 90)
            Obj.transform.position = new Vector3(Mathf.Round(Obj.transform.position.x), Obj.transform.position.y, Obj.transform.position.z);
        if (Obj.transform.rotation.eulerAngles.y == 0 || Obj.transform.rotation.eulerAngles.y == 180)
            Obj.transform.position = new Vector3(Obj.transform.position.x, Obj.transform.position.y, Mathf.Round(Obj.transform.position.z));
        DestroyIfClose(Obj);
        yield return null;
    }


    public bool CheckIfThereIsNodeToMove(GameObject Obj)
    {
        Start();
        if (Obj.transform.rotation.eulerAngles.y == 0)
        {
            if (NodeFuncs.CheckIfNodeExist(Obj.transform.position, 'y', 1)
            && VerLineFuncs.CheckIfThereIsLine(Obj.transform.position, 1, Obj.transform.position + new Vector3(0, 0, 1)))
                return true;
        }
        if (Obj.transform.rotation.eulerAngles.y == 90)
        {
            if (NodeFuncs.CheckIfNodeExist(Obj.transform.position, 'x', 1)
            && HorLineFuncs.CheckIfThereIsLine(Obj.transform.position, 1, Obj.transform.position + new Vector3(1, 0, 0)))
                return true;
        }
        if (Obj.transform.rotation.eulerAngles.y == 180)
        {
            if (NodeFuncs.CheckIfNodeExist(Obj.transform.position, 'y', -1)
            && VerLineFuncs.CheckIfThereIsLine(Obj.transform.position, -1, Obj.transform.position + new Vector3(0, 0, -1)))
                return true;
        }
        if (Obj.transform.rotation.eulerAngles.y == 270)
        {
            if (NodeFuncs.CheckIfNodeExist(Obj.transform.position, 'x', -1)
            && HorLineFuncs.CheckIfThereIsLine(Obj.transform.position, -1, Obj.transform.position + new Vector3(-1, 0, 0)))
                return true;
        }
        return false;
    }

    public void LineMovingEnemyMove(GameObject Obj)
    {
        StartCoroutine("LineMovingEnemyWalk", Obj);
    }

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
    }

    void FixedUpdate()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject projection = GameObject.FindGameObjectWithTag("Projection");
        if (MotEnemyFuncs.CheckifPlayerInfrontofEnemy(player, gameObject) && PlayerFuncs.Invisible <= 0 && !PlayerFuncs.IsThereGate(gameObject.transform) && !PlayerFuncs.IsThereCamera(gameObject.transform) && PlayerFuncs.LightOffTurns <= 0)
        {
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("IsKilling", true);
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("LineMovingEnemy").Length ; i++)
            {
                if (GameObject.FindGameObjectsWithTag("LineMovingEnemy")[i] != null)
                    GameObject.FindGameObjectsWithTag("LineMovingEnemy")[i].GetComponent<LineMovingEnemy>().StopAllCoroutines();
            }
            PlayerFuncs.StartCoroutine("KillingAnimation", gameObject);
        }
        if (projection != null && MotEnemyFuncs.CheckifPlayerInfrontofEnemy(projection, gameObject) && !PlayerFuncs.IsThereGate(gameObject.transform) && !PlayerFuncs.IsThereCamera(gameObject.transform) && PlayerFuncs.LightOffTurns <= 0)
        {
            Application.LoadLevel(0);
        }
    }
}
