using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LineMovingEnemy : MonoBehaviour
{
    public GameObject KnifeHandler; //canvas with knife throw script
    private Player PlayerFuncs; //player functions
    private ThrowKnife KnifeFuncs; //throw knife functions
    private bool IsKilling;

    public IEnumerator ReturnToMinusX(GameObject Enemy)
    {
        for (float i = 0; i < 1; i += 0.01f)
        {
            if (Enemy == null) // check if enemy destroyed
                yield break;
            Enemy.transform.GetChild(0).position += new Vector3(-0.0028f, 0, 0);
            yield return new WaitForSeconds(0.004f);
        }
        yield return new WaitForSeconds(0.05f);
        if (Enemy != null)
            Enemy.transform.GetChild(0).position = new Vector3(Mathf.Round(Enemy.transform.GetChild(0).position.x), Enemy.transform.GetChild(0).position.y, Enemy.transform.GetChild(0).position.z);
        yield return null;
    }

    public IEnumerator ReturnToX(GameObject Enemy)
    {
        for (float i = 0; i < 1; i += 0.01f)
        {
            if (Enemy == null)  // check if enemy destroyed
                yield break;
            Enemy.transform.GetChild(0).position += new Vector3(0.0028f, 0, 0);
            yield return new WaitForSeconds(0.004f);
        }
        yield return new WaitForSeconds(0.05f);
        if (Enemy != null)
            Enemy.transform.GetChild(0).position = new Vector3(Mathf.Round(Enemy.transform.GetChild(0).position.x), Enemy.transform.GetChild(0).position.y, Enemy.transform.GetChild(0).position.z);
        yield return null;
    }

    public IEnumerator ReturnToY(GameObject Enemy)
    {
        for (float i = 0; i < 1; i += 0.01f)
        {
            if (Enemy == null)  // check if enemy destroyed
                yield break;
            Enemy.transform.GetChild(0).position += new Vector3(0, 0, 0.0028f);
            yield return new WaitForSeconds(0.004f);
        }
        yield return new WaitForSeconds(0.05f);
        if (Enemy != null)
            Enemy.transform.GetChild(0).position = new Vector3(Enemy.transform.GetChild(0).position.x, Enemy.transform.GetChild(0).position.y, Mathf.Round(Enemy.transform.GetChild(0).position.z));
        yield return null;
    }

    public IEnumerator ReturnToMinusY(GameObject Enemy)
    {
        for (float i = 0; i < 1; i += 0.01f)
        {
            if (Enemy == null)  // check if enemy destroyed
                yield break;
            Enemy.transform.GetChild(0).position += new Vector3(0, 0, -0.0028f);
            yield return new WaitForSeconds(0.004f);
        }
        yield return new WaitForSeconds(0.05f);
        if (Enemy != null)
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

    private void ReturnPositions(GameObject[] ListOfEnemies, int[] Indexes) //Return positions of enemies after crossing
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

    IEnumerator GettingSlower(GameObject Enemy) //Slow down enemy before crossing
    {
        if (!(Mathf.Abs(Enemy.transform.GetChild(0).position.x - Mathf.Round(Enemy.transform.GetChild(0).position.x)) > 0.2f) //rotate visible part of enemy before crossing
        && !(Mathf.Abs(Enemy.transform.GetChild(0).position.z - Mathf.Round(Enemy.transform.GetChild(0).position.z)) > 0.2f))
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
        if (Time.timeScale == 1) // to proetct multiple killing
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (PlayerFuncs.KnifeIsReady == true) //if button is pressed
            {
                if (KnifeFuncs.CheckIfInRange(gameObject, player)) //if enemy is in range of killing
                {
                    Time.timeScale = 0.99f; // to protect multiple killing
                    PlayerFuncs.gameObject.GetComponent<Animator>().SetBool("IsTaunting", true); //activate player break-in animation
                    PlayerFuncs.StartCoroutine("StopBreaking", gameObject); //rotate player and enemy death
                    if (GameObject.FindObjectOfType<FillKnife>() != null)
                        GameObject.FindObjectOfType<FillKnife>().GetComponent<Image>().fillAmount = 0; //unfill button
                    PlayerFuncs.IsMovable = true; //player can move now
                }
                PlayerFuncs.KnifeIsReady = false; //player can't break-in more 
            }
        }
    }

    private bool CheckIfThereIsNeedToRotate(GameObject Enemy)
    {
        Quaternion localRotation = Enemy.transform.rotation;
        Enemy.transform.rotation = Quaternion.Euler(Enemy.transform.rotation.x, Utilities.Opposite(Enemy), Enemy.transform.rotation.z);
        if (Enemy == null || !Node.CheckIfThereIsNodeToMove(Enemy) //check all cases if enemy dont need to move
        || Utilities.IsThereGate(Enemy.transform)
        || Utilities.IsThereCamera(Enemy.transform)
        || Utilities.CheckIfThereIsMotEnemy(Enemy)
        || !(HorizontalLine.CheckIfThereIsLine(Enemy.transform.position, -1, Enemy.transform.position + new Vector3(-1, 0, 0))
        || HorizontalLine.CheckIfThereIsLine(Enemy.transform.position, 1, Enemy.transform.position + new Vector3(1, 0, 0))
        || VerticalLine.CheckIfThereIsLine(Enemy.transform.position, 1, Enemy.transform.position + new Vector3(0, 0, 1))
        || VerticalLine.CheckIfThereIsLine(Enemy.transform.position, -1, Enemy.transform.position + new Vector3(0, 0, -1))))
        {
            Enemy.transform.rotation = localRotation;
            return false;
        }
        Enemy.transform.rotation = localRotation;
        return true;
    }

    public IEnumerator LineMovingEnemyWalk2(GameObject[] ListOfEnemies) //Coroutine of enemies to walk
    {
        Transform[] ListOfTransforms = new Transform[ListOfEnemies.Length];
        GameObject[] ListBuf = new GameObject[ListOfEnemies.Length];
        ListOfEnemies.CopyTo(ListBuf , 0);
        for (int i = 0; i < ListOfEnemies.Length; i++)
        {
            if (ListOfEnemies[i] != null && (!Node.CheckIfThereIsNodeToMove(ListOfEnemies[i]) || Utilities.IsThereGate(ListOfEnemies[i].transform) 
            || Utilities.IsThereCamera(ListOfEnemies[i].transform) || Utilities.CheckIfThereIsMotEnemy(ListOfEnemies[i])))
            {
                //ListBuf[i] = null;
            }
            else
            {
                ListBuf[i] = null;
            }
        }
        int[] Indexes = Crossing.IsCrossing(ListBuf); //get indexes of enemies which is going to cross
        if (ReturnPositionsToTurn(ListBuf, Indexes) != 0) //return positions after crossing enemies
            yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < ListBuf.Length; i++)
        {
            if (ListBuf[i] != null)
            {
                if (ListOfEnemies[i].transform.rotation.eulerAngles.y == 0) //get childrens to same rotation
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
            if ((ListOfEnemies[i] != null && (!Node.CheckIfThereIsNodeToMove(ListOfEnemies[i]) || Utilities.IsThereGate(ListOfEnemies[i].transform) //if enemy have to place to move
            || Utilities.IsThereCamera(ListOfEnemies[i].transform) || Utilities.CheckIfThereIsMotEnemy(ListOfEnemies[i]))) && CheckIfThereIsNeedToRotate(ListOfEnemies[i]))
                PlayerFuncs.StartCoroutine("RotateEnemies", ListOfEnemies[i]); //rotate it
        }
        yield return new WaitForSeconds(0.8f);
        for (int i = 0; i < ListOfEnemies.Length; i++)
        {
            if (ListOfEnemies[i] == null || !Node.CheckIfThereIsNodeToMove(ListOfEnemies[i]) //check all cases if enemy dont need to move
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
        Indexes = Crossing.IsCrossing(ListOfEnemies); //get indexes of enemies which is need to return positions after crossing
        ReturnPositions(ListOfEnemies, Indexes); //return their positions
        for (int i = 0; i < Indexes.Length; i++)
        {
            StartCoroutine("GettingSlower", ListOfEnemies[Indexes[i]]); //slows enemies before crossing
        }
        for (int i = 0; i < ListOfEnemies.Length; i++)
        {
            if (ListOfEnemies[i] != null)
            {
                PlayerFuncs.CheckIfThereIsStairway(ListOfEnemies[i]); // if there is stairway move down or up on it
                FindObjectOfType<AudioManager>().Play("RobotWalk");
            }
        }
        // for (float i = 0; i < 1; i += 0.01f)
        for (float i = 0; i < 0.25f; i += 0.01f)
        {
            for (int j = 0; j < ListOfEnemies.Length; j++)
            {
                if (ListOfEnemies[j] != null)
                {
                    ListOfEnemies[j].GetComponentInChildren<Animator>().SetBool("IsRunning", true);
                    if (ListOfTransforms[j].rotation.eulerAngles.y == 0)
                        //ListOfEnemies[j].transform.position += new Vector3(0, 0, 0.01f);
                        ListOfEnemies[j].transform.position += new Vector3(0, 0, 0.04f);
                    if (ListOfEnemies[j].transform.rotation.eulerAngles.y == 90)
                        // ListOfEnemies[j].transform.position += new Vector3(0.01f, 0, 0);
                        ListOfEnemies[j].transform.position += new Vector3(0.04f, 0, 0);
                    if (ListOfEnemies[j].transform.rotation.eulerAngles.y == 180)
                        //ListOfEnemies[j].transform.position += new Vector3(0, 0, -0.01f);
                        ListOfEnemies[j].transform.position += new Vector3(0, 0, -0.04f);
                    if (ListOfEnemies[j].transform.rotation.eulerAngles.y == 270)
                        //ListOfEnemies[j].transform.position += new Vector3(-0.01f, 0, 0);
                        ListOfEnemies[j].transform.position += new Vector3(-0.04f, 0, 0);
                }
            }
            //yield return new WaitForSeconds(0.004f);
            yield return new WaitForSeconds(0.016f);
        }
        for (int i = 0; i < ListOfEnemies.Length; i++)
        {
            if (ListOfEnemies[i] != null)
            {
                if (Mathf.Abs(ListOfEnemies[i].transform.GetChild(0).rotation.eulerAngles.y - 347.4f) < 0.1f) //rotate enemies again to look better
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
                ListOfEnemies[j].GetComponentInChildren<Animator>().SetBool("IsRunning", false); //stop moving animations
                if (ListOfEnemies[j].transform.rotation.eulerAngles.y == 270 || ListOfEnemies[j].transform.rotation.eulerAngles.y == 90) //to avoid extra fraction
                    ListOfEnemies[j].transform.position = new Vector3(Mathf.Round(ListOfEnemies[j].transform.position.x), ListOfEnemies[j].transform.position.y, ListOfEnemies[j].transform.position.z);
                if (ListOfEnemies[j].transform.rotation.eulerAngles.y == 0 || ListOfEnemies[j].transform.rotation.eulerAngles.y == 180)
                    ListOfEnemies[j].transform.position = new Vector3(ListOfEnemies[j].transform.position.x, ListOfEnemies[j].transform.position.y, Mathf.Round(ListOfEnemies[j].transform.position.z));
            }
        }
        PlayerFuncs.IsWaiting = false; //player stops waiting for line moving enemies to turn
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
        if (Utilities.CheckifPlayerInfrontofEnemy(Player, gameObject) && !Utilities.IsThereGate(gameObject.transform) //check if player is infront of enemy and nothing interfere
        && !Utilities.IsThereCamera(gameObject.transform) && PlayerFuncs.LightsOffTurns <= 0 && IsKilling == false)
        {
            FindObjectOfType<TurnManager>().gameObject.SetActive(false);
            IsKilling = true;
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("IsKilling", true); //start killing animation
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("LineMovingEnemy").Length ; i++)
            {
                if (GameObject.FindGameObjectsWithTag("LineMovingEnemy")[i] != null)
                    GameObject.FindGameObjectsWithTag("LineMovingEnemy")[i].GetComponent<LineMovingEnemy>().StopAllCoroutines(); //stop all other coroutines 
            }
            PlayerFuncs.StartCoroutine("KillingAnimation", gameObject); //player is dying 
        }
    }
}
