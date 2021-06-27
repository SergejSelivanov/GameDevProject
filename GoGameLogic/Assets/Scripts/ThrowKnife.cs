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

    public bool CheckIfInRange(GameObject Enemy, GameObject Player)
    {
        Object[] Gates = GameObject.FindGameObjectsWithTag("Gate");
        for (int j = 1; j <= KillingRange; j++)
        {
            RaycastHit hit;
            Ray ray = new Ray(Enemy.transform.position, Player.transform.position - Enemy.transform.position);
            Physics.Raycast(ray, out hit, 2);
            if (hit.collider != null)
            {
                for (int i = 0; i < Gates.Length; i++)
                {
                    if (hit.collider.gameObject == Gates[i])
                        return false;
                }
            }
            if (Enemy.transform.position.z == Player.transform.position.z 
            && (Enemy.transform.position.x + j == Player.transform.position.x || Enemy.transform.position.x - j == Player.transform.position.x))
                return true;
            if (Enemy.transform.position.x == Player.transform.position.x
            && (Enemy.transform.position.z + j == Player.transform.position.z || Enemy.transform.position.z - j == Player.transform.position.z))
                return true;
        }
        return false;
    }

    public void Throwknife()
    {
            if (PlayerFuncs.IsPlayerMovable == false)
            {
                PlayerFuncs.KnifeReady = false;
                PlayerFuncs.IsPlayerMovable = true;
                return;
            }
            PlayerFuncs.KnifeReady = true;
            PlayerFuncs.IsPlayerMovable = false;
    }

    void Start()
    {
        PlayerFuncs = PlayerHandler.GetComponent<Player>();
    }
}
