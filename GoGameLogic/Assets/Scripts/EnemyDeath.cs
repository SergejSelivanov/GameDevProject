using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (GameObject.FindGameObjectWithTag("Test") != null)
            GameObject.FindGameObjectWithTag("Test").SetActive(false);
        for (int i = 0; i < GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill.Length; i++)
        {
            if (GameObject.FindGameObjectWithTag("Test2") != null)
                GameObject.FindGameObjectWithTag("Test2").SetActive(false);
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i] != null)
            {
                if (GameObject.FindGameObjectWithTag("Test3") != null)
                    GameObject.FindGameObjectWithTag("Test3").SetActive(false);
                try
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i].transform.
                    Find("Root/Hips/Spine_01/Spine_02/Spine_03/Neck/Head").GetChild(2).gameObject.SetActive(true); //activateexplosion if its motionless enemy
                }
                catch
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i].transform.
                    Find("Character_Cop_01/Root/Hips/Spine_01/Spine_02/Spine_03/Neck/Head").GetChild(2).gameObject.SetActive(true); //activate explosion of line moving enemy
                }
                if (GameObject.FindObjectOfType<FillKnife>() != null)
                    GameObject.FindObjectOfType<FillKnife>().StartCoroutine("FillButton");
            }
        }    
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill.Length; i++)
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i] != null)
                Destroy(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i]); //destroy enemy
        } 
    }
}
