using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smth : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill.Length; i++)
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i] != null)
                //Destroy(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i]); //destroy every enemy thats in the list of dead enemies
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i].SetActive(false);
        }
    }
}
