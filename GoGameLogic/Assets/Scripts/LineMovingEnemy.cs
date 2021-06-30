using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LineMovingEnemy : MonoBehaviour
{
    public GameObject KnifeHandler;
    private Player PlayerFuncs;
    private ThrowKnife KnifeFuncs;

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
                    PlayerFuncs.StartCoroutine("StopBreaking", gameObject);
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
        int[] Indexes = Crossing.IsCrossing(ListBuf);
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
        for (int i = 0; i < ListOfEnemies.Length; i++)
        {
            if (ListOfEnemies[i] != null)
                ListOfTransforms[i] = ListOfEnemies[i].transform;
            else
                ListOfTransforms[i] = null;
        }
        Indexes = Crossing.IsCrossing(ListOfEnemies);
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
            }
        }
        PlayerFuncs.IsWaiting = false;
        yield return null;
    }

    void Start()
    {
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
            //PlayerFuncs.StartKillingCoroutine(gameObject);
            PlayerFuncs.StartCoroutine("KillingAnimation", gameObject);
        }
    }
}
