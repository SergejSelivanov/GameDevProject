using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : WalkingHumanoid
{

    public bool CheckIfThereIsNodeToMove(GameObject Obj)
    {
       // Start();
        if (Obj.transform.rotation.eulerAngles.y == 0)
        {
            if (NodeNew.CheckIfNodeExist(Obj.transform.position, 'y', 1)
            && VerLine.CheckIfThereIsLine(Obj.transform.position, 1, Obj.transform.position + new Vector3(0, 0, 1)))
                return true;
        }
        if (Obj.transform.rotation.eulerAngles.y == 90)
        {
            if (NodeNew.CheckIfNodeExist(Obj.transform.position, 'x', 1)
            && HorLine.CheckIfThereIsLine(Obj.transform.position, 1, Obj.transform.position + new Vector3(1, 0, 0)))
                return true;
        }
        if (Obj.transform.rotation.eulerAngles.y == 180)
        {
            if (NodeNew.CheckIfNodeExist(Obj.transform.position, 'y', -1)
            && VerLine.CheckIfThereIsLine(Obj.transform.position, -1, Obj.transform.position + new Vector3(0, 0, -1)))
                return true;
        }
        if (Obj.transform.rotation.eulerAngles.y == 270)
        {
            if (NodeNew.CheckIfNodeExist(Obj.transform.position, 'x', -1)
            && HorLine.CheckIfThereIsLine(Obj.transform.position, -1, Obj.transform.position + new Vector3(-1, 0, 0)))
                return true;
        }
        return false;
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

    public IEnumerator LineMovingEnemyWalk2(GameObject[] ListOfEnemies)
    {
        Transform[] ListOfTransforms = new Transform[ListOfEnemies.Length];
        GameObject[] ListBuf = new GameObject[ListOfEnemies.Length];
        ListOfEnemies.CopyTo(ListBuf, 0);
        for (int i = 0; i < ListOfEnemies.Length; i++)
        {
            if (ListOfEnemies[i] != null && (!CheckIfThereIsNodeToMove(ListOfEnemies[i]) || MainCharacter.IsThereGate(ListOfEnemies[i].transform) || MainCharacter.IsThereCamera(ListOfEnemies[i].transform) || MainCharacter.CheckIfThereIsMotEnemy(ListOfEnemies[i])))
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
            if (ListOfEnemies[i] != null && (!CheckIfThereIsNodeToMove(ListOfEnemies[i]) || PlayerFuncs.IsThereGate(ListOfEnemies[i].transform) || PlayerFuncs.IsThereCamera(ListOfEnemies[i].transform) || PlayerFuncs.CheckIfThereIsMotEnemy(ListOfEnemies[i])))
                PlayerFuncs.StartCoroutine("RotateEnemies", ListOfEnemies[i]);
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
                DestroyIfClose(ListOfEnemies[j]);
            }
        }
        PlayerFuncs.Waiting = false;
        yield return null;
    }

    public static bool CheckifPlayerInfrontofEnemy(GameObject player, GameObject Enemy)
    {
        if (Enemy.transform.rotation.eulerAngles.y == 0
        && Enemy.transform.position.x == player.transform.position.x
        && Enemy.transform.position.z + 1 == player.transform.position.z
        && VerLine.CheckIfThereIsLine(Enemy.transform.position, 1, Enemy.transform.position + new Vector3(0, 0, 1)))
            return true;
        if (Enemy.transform.rotation.eulerAngles.y == 90
        && Enemy.transform.position.z == player.transform.position.z
        && Enemy.transform.position.x + 1 == player.transform.position.x
        && HorLine.CheckIfThereIsLine(Enemy.transform.position, 1, Enemy.transform.position + new Vector3(1, 0, 0)))
            return true;
        if (Enemy.transform.rotation.eulerAngles.y == 180
        && Enemy.transform.position.x == player.transform.position.x
        && Enemy.transform.position.z - 1 == player.transform.position.z
        && VerLine.CheckIfThereIsLine(Enemy.transform.position, -1, Enemy.transform.position + new Vector3(0, 0, -1)))
            return true;
        if (Enemy.transform.rotation.eulerAngles.y == 270
        && Enemy.transform.position.z == player.transform.position.z
        && Enemy.transform.position.x - 1 == player.transform.position.x
        && HorLine.CheckIfThereIsLine(Enemy.transform.position, -1, Enemy.transform.position + new Vector3(-1, 0, 0)))
            return true;
        return false;
    }

    public override void WalkDown()
    {
        throw new System.NotImplementedException();
    }

    public override void WalkLeft()
    {
        throw new System.NotImplementedException();
    }

    public override void WalkRight()
    {
        throw new System.NotImplementedException();
    }

    public override void WalkUp()
    {
        throw new System.NotImplementedException();
    }
}
