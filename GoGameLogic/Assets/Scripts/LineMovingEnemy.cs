using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LineMovingEnemy : MonoBehaviour
{
    //public GameObject PlayerHandler;
    public GameObject KnifeHandler;
    private Player PlayerFuncs;
    private ThrowKnife KnifeFuncs;
    private Player player;
    GameObject player2;

    public int[] IsCrossing(GameObject[] ListOfEnemies)
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
            flag = false;
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

    private void ReturnPositions(GameObject[] ListOfEnemies, int[] Indexes)
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
                player.transform.rotation = Quaternion.Euler(0, (int)player.transform.rotation.eulerAngles.y + Diff / 30, 0);
                yield return new WaitForSeconds(0.0133f);
            }
            player.GetComponentInChildren<Animator>().SetInteger("IsRotating", 0);
            player.transform.rotation = Quaternion.Euler(0, RequiredAngle, 0);
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
        player.GetComponent<Animator>().SetBool("IsTaunting", false);
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1;
        yield return null;
    }

    private void OnMouseDown()
    {
        if (Time.timeScale == 1)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (PlayerFuncs.KnifeIsReady == true)
            {
                if (KnifeFuncs.CheckIfInRange(gameObject, player))
                {
                    Time.timeScale = 0.99f;
                    PlayerFuncs.gameObject.GetComponent<Animator>().SetBool("IsTaunting", true);
                    StartCoroutine("StopBreaking");
                    if (GameObject.FindObjectOfType<FillKnife>() != null)
                        GameObject.FindObjectOfType<FillKnife>().GetComponent<Image>().fillAmount = 0;
                    PlayerFuncs.IsMovable = true;
                }
                PlayerFuncs.KnifeIsReady = false;
            }
        }
    }

    public IEnumerator LineMovingEnemyWalk2(GameObject[] ListOfEnemies)
    {
        Transform[] ListOfTransforms = new Transform[ListOfEnemies.Length];
        GameObject[] ListBuf = new GameObject[ListOfEnemies.Length];
        ListOfEnemies.CopyTo(ListBuf , 0);
        for (int i = 0; i < ListOfEnemies.Length; i++)
        {
            if (ListOfEnemies[i] != null && (!Node.CheckIfThereIsNodeToMove(ListOfEnemies[i]) || Utilities.IsThereGate(ListOfEnemies[i].transform) || Utilities.IsThereCamera(ListOfEnemies[i].transform) || Utilities.CheckIfThereIsMotEnemy(ListOfEnemies[i])))
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
            yield return new WaitForSeconds(0.5f);
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
            if (ListOfEnemies[i] != null && (!Node.CheckIfThereIsNodeToMove(ListOfEnemies[i]) || Utilities.IsThereGate(ListOfEnemies[i].transform) || Utilities.IsThereCamera(ListOfEnemies[i].transform) || Utilities.CheckIfThereIsMotEnemy(ListOfEnemies[i])))
                PlayerFuncs.StartCoroutine("RotateEnemies", ListOfEnemies[i]);
        }
        // yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.8f);
        ////prover' potom vremya
        for (int i = 0; i < ListOfEnemies.Length; i++)
        {
            if (ListOfEnemies[i] == null || !Node.CheckIfThereIsNodeToMove(ListOfEnemies[i]) 
            || Utilities.IsThereGate(ListOfEnemies[i].transform)
            || Utilities.IsThereCamera(ListOfEnemies[i].transform)
            || Utilities.CheckIfThereIsMotEnemy(ListOfEnemies[i])
            || !(HorizontalLine.CheckIfThereIsLine(ListOfEnemies[i].transform.position, -1, ListOfEnemies[i].transform.position + new Vector3(-1, 0, 0)) 
            || HorizontalLine.CheckIfThereIsLine(ListOfEnemies[i].transform.position, 1, ListOfEnemies[i].transform.position + new Vector3(1, 0, 0)) 
            || VerticalLine.CheckIfThereIsLine(ListOfEnemies[i].transform.position, 1, ListOfEnemies[i].transform.position + new Vector3(0, 0, 1)) 
            || VerticalLine.CheckIfThereIsLine(ListOfEnemies[i].transform.position, -1, ListOfEnemies[i].transform.position + new Vector3(0, 0, -1))))
                ListOfEnemies[i] = null;
        }
        //Debug.Log(ListOfEnemies[0]);
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
                         ListOfEnemies[j].transform.position += new Vector3(0, 0, 0.01f);
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
                //DestroyIfClose(ListOfEnemies[j]);
            }
        }
        //PlayerFuncs.IsWaiting = false;
        PlayerFuncs.IsWaiting = false;
        yield return null;
    }

    void Start()
    {
        //PlayerFuncs = PlayerHandler.GetComponent<Player>();
        PlayerFuncs = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        KnifeFuncs = KnifeHandler.GetComponent<ThrowKnife>();
    }

    void FixedUpdate()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if (Utilities.CheckifPlayerInfrontofEnemy(Player, gameObject) && !Utilities.IsThereGate(gameObject.transform) && !Utilities.IsThereCamera(gameObject.transform) && PlayerFuncs.LightsOffTurns <= 0)
        {
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("IsKilling", true);
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("LineMovingEnemy").Length ; i++)
            {
                if (GameObject.FindGameObjectsWithTag("LineMovingEnemy")[i] != null)
                    GameObject.FindGameObjectsWithTag("LineMovingEnemy")[i].GetComponent<LineMovingEnemy>().StopAllCoroutines();
            }
            PlayerFuncs.StartKillingCoroutine(gameObject);
            
        }
    }
}
