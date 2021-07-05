using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private LineMovingEnemy[] enemiesList;
    private GameObject[] EnemiesList;
    private Player player;

    public void EndPlayersTurn() //передаю ход врагам
    {
        GameObject[] LocalEnemiesList = new GameObject[EnemiesList.Length];
        EnemiesList.CopyTo(LocalEnemiesList, 0);
        if (player.LightsOffTurns <= 0 && enemiesList.Length != 0)
        {
            Debug.Log("A");
            for (int i = 0; i < enemiesList.Length; i++)
            {
                if (enemiesList[i] != null)
                {
                    StartCoroutine(enemiesList[i].LineMovingEnemyWalk2(LocalEnemiesList));
                    break;
                }
                else
                    EndEnemiesTurn();
            }
            
        }
        else
            EndEnemiesTurn();
    }

    public void EndEnemiesTurn()
    {
        player.IsWaiting = false;
    }

    private void Awake()
    {
        enemiesList = GameObject.FindObjectsOfType<LineMovingEnemy>();
        EnemiesList = GameObject.FindGameObjectsWithTag("LineMovingEnemy");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
}
