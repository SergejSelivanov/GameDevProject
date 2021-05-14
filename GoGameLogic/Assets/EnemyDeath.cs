using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

   /* IEnumerator Death(GameObject Enemy)
    {
        yield return new WaitForSeconds(1);
        Destroy(Enemy);
    }*/

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        for (int i = 0; i < GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill.Length; i++)
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i] != null)
            {
                //Debug.Log(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i].transform.Find("Character_Cop_01/Root/Hips/Spine_01/Spine_02/Spine_03/Neck/Head"));
                //GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i].transform.Find("")
                // Debug.Log("Hey");
                //GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i].transform.GetChild(22).gameObject.SetActive(true);
                try
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i].transform.Find("Root/Hips/Spine_01/Spine_02/Spine_03/Neck/Head").GetChild(2).gameObject.SetActive(true);
                }
                catch
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i].transform.Find("Character_Cop_01/Root/Hips/Spine_01/Spine_02/Spine_03/Neck/Head").GetChild(2).gameObject.SetActive(true);
                }
                if (GameObject.FindObjectOfType<FillKnife>() != null)
                    GameObject.FindObjectOfType<FillKnife>().StartCoroutine("FillButton");
                //new WaitForSeconds(1);
                //Debug.Log(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i].transform.GetChild(22).gameObject);
                // Destroy(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i]);
            }
        }
        //    
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i].transform.GetChild(22).gameObject.SetActive(true);
        //    
        //Debug.Log("Hey");
        for (int i = 0; i < GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill.Length; i++)
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i] != null)
            {
               // Debug.Log("Hey");
                //GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i].transform.GetChild(22).gameObject.SetActive(true);
                //Debug.Log(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i].transform.GetChild(22).gameObject);
                Destroy(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().EnemiesKill[i]);
            }
        }
        //Debug.Log("Hey");
        //    
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
