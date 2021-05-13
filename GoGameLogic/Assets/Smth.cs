using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smth : StateMachineBehaviour
{
     ///GameObject.FindGameObjectWithTag("Player").GetComponent<Player>;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //    
        for (int i = 0; i < GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill.Length; i++)
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i] != null)
            {
                //GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i].transform.GetChild(22).gameObject.SetActive(true);
                //Debug.Log(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i].transform.GetChild(22).gameObject);
                Destroy(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i]);
            }
        }
        //GameObject Enemy = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[0];
        //GameObject.Destroy();
       // Destroy(Enemy);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    /*override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject Enemy = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemyTokill;
        //GameObject.Destroy();
        Destroy(Enemy);
        //    
        //Destroy(PlayerFuncs.EnemyTokill);
    }*/

    // OnStateMove is called right after Animator.OnAnimatorMove()
   /* override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //    // Implement code that processes and affects root motion
        GameObject Enemy = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemyTokill;
        //GameObject.Destroy();
        Destroy(Enemy);
    }*/

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
