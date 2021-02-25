using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ThrowKnife : MonoBehaviour
{
    public GameObject PlayerHandler;
    public GameObject ButtonUI;
    private Player PlayerFuncs;
    
    public int KillingRange = 2;

    private GameObject[] InsertArrays(GameObject[] arr1, GameObject[] arr2)
    {
        GameObject[] RetArray = new GameObject[arr1.Length + arr2.Length];
        int j = 0;
        for (int i = 0; i < arr1.Length; i++)
        {
            RetArray[j] = arr1[i];
            j++;
        }
        for (int i = 0; i < arr2.Length; i++)
        {
            RetArray[j] = arr2[i];
            j++;
        }
        return RetArray;
    }

    public void Throwknife()
    {
        GameObject[] ListOfMovingEnemies = GameObject.FindGameObjectsWithTag("LineMovingEnemy");
        GameObject[] ListOfMotionlessEnemies = GameObject.FindGameObjectsWithTag("MotionlessEnemy");
        //GameObject[] ListOfEnemies = new GameObject[ListOfMotionlessEnemies.Length + ListOfMovingEnemies.Length];
        GameObject[] ListOfEnemies = InsertArrays(ListOfMovingEnemies, ListOfMotionlessEnemies);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        // for (int i = 0; i < ListOfMovingEnemies.Length; i++)
        for (int j = 1; j <= KillingRange; j++)
        {
            //for (int j = 0; j < KillingRange; j++)
            for (int i = 0; i < ListOfEnemies.Length; i++)
            {
               // Debug.Log(ListOfEnemies[i].transform.position.z);
                if (player.transform.rotation.eulerAngles.y == 0
                && ListOfEnemies[i].transform.position.x == player.transform.position.x
                && ListOfEnemies[i].transform.position.z - j == player.transform.position.z)
                {
                    Destroy(ListOfEnemies[i]);
                    PlayerFuncs.SkillSetter = 0;
                    ButtonUI.SetActive(false);
                    return;
                }
                if (player.transform.rotation.eulerAngles.y == 90
                && ListOfEnemies[i].transform.position.z == player.transform.position.z
                && ListOfEnemies[i].transform.position.x - j == player.transform.position.x)
                {
                    Destroy(ListOfEnemies[i]);
                    PlayerFuncs.SkillSetter = 0;
                    ButtonUI.SetActive(false);
                    return;
                }
                if (player.transform.rotation.eulerAngles.y == 180
                && ListOfEnemies[i].transform.position.x == player.transform.position.x
                && ListOfEnemies[i].transform.position.z + j == player.transform.position.z)
                {
                    Destroy(ListOfEnemies[i]);
                    PlayerFuncs.SkillSetter = 0;
                    ButtonUI.SetActive(false);
                    return;
                }
                if (player.transform.rotation.eulerAngles.y == 270
                && ListOfEnemies[i].transform.position.z == player.transform.position.z
                && ListOfEnemies[i].transform.position.x + j == player.transform.position.x)
                {
                    Destroy(ListOfEnemies[i]);
                    PlayerFuncs.SkillSetter = 0;
                    ButtonUI.SetActive(false);
                    return;
                }
            }
        }
       /* PlayerFuncs.SkillSetter = 0;
        ButtonUI.SetActive(false);*/
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerFuncs = PlayerHandler.GetComponent<Player>();
        ButtonUI.SetActive(false);
         //GUI.enabled = false;
        //GUI.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PlayerFuncs.SkillSetter);
        if (PlayerFuncs.SkillSetter == 1)
            ButtonUI.SetActive(true);
    }
}
