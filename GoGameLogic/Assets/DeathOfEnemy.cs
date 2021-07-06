using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOfEnemy : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacter>().EnemiesKill.Length; i++)
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacter>().EnemiesKill[i] != null)
            {

                try
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacter>().EnemiesKill[i].transform.Find("Root/Hips/Spine_01/Spine_02/Spine_03/Neck/Head").GetChild(2).gameObject.SetActive(true);
                }
                catch
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacter>().EnemiesKill[i].transform.Find("Character_Cop_01/Root/Hips/Spine_01/Spine_02/Spine_03/Neck/Head").GetChild(2).gameObject.SetActive(true);
                }
                if (GameObject.FindObjectOfType<FillKnife>() != null)
                    GameObject.FindObjectOfType<FillKnife>().StartCoroutine("FillButton");
            }
        }  
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacter>().EnemiesKill.Length; i++)
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacter>().EnemiesKill[i] != null)
                Destroy(GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacter>().EnemiesKill[i]);
        }
    }
}
