using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : WalkingHumanoid
{
    public GameObject PlayerHandler;
    private MainCharacter PlayerFuncs;
    private GameObject player;
    private bool flagToStop = false;

    public static bool CheckIfThereIsNodeToMove(GameObject Obj)
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

    public static int[] IsCrossing(GameObject[] ListOfEnemies)
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

    public static int Opposite(GameObject Obj)
    {
        if (Obj.transform.rotation.eulerAngles.y == 0)
            return (180);
        else if (Obj.transform.rotation.eulerAngles.y == 90)
            return (270);
        else if (Obj.transform.rotation.eulerAngles.y == 270)
            return (90);
        else 
            return (0);
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

    private void Start()
    {
        PlayerFuncs = PlayerHandler.GetComponent<MainCharacter>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //Debug.Log(player);
        if (flagToStop == false && CheckifPlayerInfrontofEnemy(player, gameObject) && PlayerFuncs.InvisibleSteps <= 0 && !MainCharacter.IsThereGate(gameObject.transform) && !MainCharacter.IsThereCamera(gameObject.transform) && PlayerFuncs.LightsOffTurns <= 0)
        {
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("IsKilling", true);
            player.GetComponent<MainCharacter>().StopAllCoroutines();
            PlayerFuncs.StartCoroutine("KillingAnimation", gameObject);
            flagToStop = true;
        }
    }
}
