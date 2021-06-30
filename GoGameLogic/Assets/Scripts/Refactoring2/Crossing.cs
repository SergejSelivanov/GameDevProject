using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossing : MonoBehaviour
{
    public static int[] IsCrossing(GameObject[] ListOfEnemies)
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
}
