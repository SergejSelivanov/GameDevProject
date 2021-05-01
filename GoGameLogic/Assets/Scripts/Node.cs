using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public GameObject PlayerHandler;
    public GameObject VerticalLineHandler;
    public GameObject HorizontalLineHandler;
    private Player PlayerFuncs;
    private VerticalLine verticalLine;
    private HorizontalLine horizontalLine;

    public GameObject ButtonUI;

    public int distance = 3;
    /*public GameObject playernhandler;
    private Player playern;*/
    //private bool flagGranade = true;

    /*public void ft_pressed_granade_button()
    {
        //flagGranade = false;
        if (flagGranade == true)
        {
            Time.timeScale = 0;
            flagGranade = false;
            return;
        }
        else if (flagGranade == false)
        {
            Time.timeScale = 1;
            flagGranade = true;
        }
        //Debug.Log(flagGranade);
    }*/

    /*private void ft_Throw_Granade(Vector3 coord)
    {
        bool thereIsVLinetop;
        bool thereIsVLinedown;
        bool thereIsVLineleft;
        bool thereIsVLineRight;

        thereIsVLinetop = verticalLine.CheckIfThereIsLine(coord, 1, coord + new Vector3(0, 0, 1));
        thereIsVLinedown = verticalLine.CheckIfThereIsLine(coord, -1, coord + new Vector3(0, 0, -1));
        thereIsVLineleft = horizontalLine.CheckIfThereIsLine(coord, -1, coord + new Vector3(-1, 0, 0));
        thereIsVLineRight = horizontalLine.CheckIfThereIsLine(coord, 1, coord + new Vector3(1, 0, 0));
        //Debug.Log("Top" + thereIsVLinetop);
        //Debug.Log("Down" + thereIsVLinedown);
        //Debug.Log("Left" + thereIsVLineleft);
        //Debug.Log("Right" + thereIsVLineRight);
        if (thereIsVLinetop == true)
            ft_check_hero_on_line("Top");
        if (thereIsVLinedown == true)
            ft_check_hero_on_line("Down");
        if (thereIsVLineleft == true)
            ft_check_hero_on_line("Left");
        if (thereIsVLineRight == true)
            ft_check_hero_on_line("Right");
    }

    private void ft_check_hero_on_line(string Sight)
    {

    }
    */

    private string ft_check_hero_on_line(Vector3 coord, Vector3 playerCoord)
    {
        if ((coord.x == playerCoord.x) && (playerCoord.z > coord.z))
            return ("Top");
        else if ((coord.x == playerCoord.x) && (playerCoord.z < coord.z))
            return ("Down");
        else if ((coord.z == playerCoord.z) && (playerCoord.x > coord.x))
            return ("Right");
        else if ((coord.z == playerCoord.z) && (playerCoord.x < coord.x))
            return ("Left");
        return ("Nothing");
    }

    private bool ft_check_hero_sight(string Sight)
    {
        if (Sight == "Top")
        {
            bool thereIsVLinetop;
            if (((int)PlayerHandler.transform.position.z - (int)transform.position.z) > distance)
                return (false);
            for (int i = (int)transform.position.z; i < (int)PlayerHandler.transform.position.z; i++)
            {
                thereIsVLinetop = verticalLine.CheckIfThereIsLine(new Vector3(transform.position.x, transform.position.y, i),
                                  1, new Vector3(transform.position.x, transform.position.y, i + 1));
                if (thereIsVLinetop == false)
                    return (false);
                //Debug.Log("Top");
            }
            if (IsThereGate2(PlayerHandler, (int)PlayerHandler.transform.position.z - (int)transform.position.z, 180))
                return (false);
            return (true);
        }
        else if (Sight == "Down")
        {
            bool thereIsVLinetop;
            if (((int)transform.position.z - (int)PlayerHandler.transform.position.z) > distance)
                return (false);
            for (int i = (int)PlayerHandler.transform.position.z; i < (int)transform.position.z; i++)
            {
                thereIsVLinetop = verticalLine.CheckIfThereIsLine(new Vector3(PlayerHandler.transform.position.x, PlayerHandler.transform.position.y, i),
                                  1, new Vector3(PlayerHandler.transform.position.x, PlayerHandler.transform.position.y, i + 1));
                if (thereIsVLinetop == false)
                    return (false);
                //Debug.Log("Down");
            }
            if (IsThereGate2(PlayerHandler, (int)transform.position.z - (int)PlayerHandler.transform.position.z, 0))
                return (false);
            return (true);
        }
        else if (Sight == "Left")
        {
            bool thereIsVLinetop;
            if (((int)transform.position.x - (int)PlayerHandler.transform.position.x) > distance)
                return (false);
            for (int i = (int)PlayerHandler.transform.position.x; i < (int)transform.position.x; i++)
            {
                thereIsVLinetop = horizontalLine.CheckIfThereIsLine(new Vector3(i, PlayerHandler.transform.position.y, PlayerHandler.transform.position.z),
                                  1, new Vector3(i + 1, PlayerHandler.transform.position.y, PlayerHandler.transform.position.z));
                if (thereIsVLinetop == false)
                    return (false);
               // Debug.Log("Left");
            }
            if (IsThereGate2(PlayerHandler, (int)transform.position.x - (int)PlayerHandler.transform.position.x, 90))
                return (false);
            return (true);
        }
        else if (Sight == "Right")
        {
            bool thereIsVLinetop;
            if (((int)PlayerHandler.transform.position.x - (int)transform.position.x) > distance)
                return (false);
            for (int i = (int)transform.position.x; i < PlayerHandler.transform.position.x; i++)
            {
                thereIsVLinetop = horizontalLine.CheckIfThereIsLine(new Vector3(i, transform.position.y, transform.position.z),
                                  1, new Vector3(i + 1, transform.position.y, transform.position.z));
                if (thereIsVLinetop == false)
                    return (false);
               // Debug.Log("Right");
            }
            if (IsThereGate2(PlayerHandler, (int)PlayerHandler.transform.position.x - (int)transform.position.x, 270))
                return (false);
            return (true);
        }
        return (false);
    }

    public bool IsThereGate2(GameObject player, int LenghtOfRay, int Rotation)
    {
        Quaternion OldRotation = player.transform.rotation;
        player.transform.rotation = Quaternion.Euler(0, Rotation, 0);
        Object[] Gates = GameObject.FindGameObjectsWithTag("Gate");
        RaycastHit Hit;
        Ray ray = new Ray(player.transform.position, player.transform.forward);
        Physics.Raycast(ray, out Hit, LenghtOfRay);
        if (Hit.collider != null)
        {
            for (int i = 0; i < Gates.Length; i++)
            {
                if (Hit.collider.gameObject == Gates[i])
                {
                    player.transform.rotation = OldRotation;
                    return true;
                }
            }
        }
        return false;
    }

    private void ft_check_mob_on_Line()
    {
        GameObject[] listOfEnemyAfk = GameObject.FindGameObjectsWithTag("LineMovingEnemy");
        GameObject[] listOfEnemyMoitenless = GameObject.FindGameObjectsWithTag("MotionlessEnemy");

        var listOfEnemy = new GameObject[listOfEnemyAfk.Length + listOfEnemyMoitenless.Length];
        listOfEnemyMoitenless.CopyTo(listOfEnemy, 0);
        listOfEnemyAfk.CopyTo(listOfEnemy, listOfEnemyMoitenless.Length);
        for (int i = 0; i < listOfEnemy.Length; i++)
        {
            string Sight;
            if (listOfEnemy[i].transform.position.x == transform.position.x || listOfEnemy[i].transform.position.z == transform.position.z)
            {
                Sight = ft_check_hero_on_line(transform.position, listOfEnemy[i].transform.position);
                if (ft_check_enemy_sight(Sight, listOfEnemy[i]))
                {
                    /*ft_rot_enemy(listOfEnemy[i]);*/
                    Vector3 newDir = Vector3.RotateTowards(listOfEnemy[i].transform.forward, transform.position - listOfEnemy[i].transform.position, 1, 0);
                    listOfEnemy[i].transform.rotation = Quaternion.LookRotation(newDir);
                }
            }
        }

    }

/*    private void ft_rot_enemy(GameObject Enemy)
    {

    }*/

    private bool ft_check_enemy_sight(string Sight, GameObject Enemy)
    {
        if (Sight == "Top")
        {
            bool thereIsVLinetop;
            for (int i = (int)transform.position.z; i < (int)Enemy.transform.position.z; i++)
            {
                thereIsVLinetop = verticalLine.CheckIfThereIsLine(new Vector3(transform.position.x, transform.position.y, i),
                                  1, new Vector3(transform.position.x, transform.position.y, i + 1));
                if (thereIsVLinetop == false)
                    return (false);
            }
            if (IsThereGate2(Enemy, (int)Enemy.transform.position.z - (int)transform.position.z, 180))
                return (false);
            return (true);
        }
        else if (Sight == "Down")
        {
            bool thereIsVLinetop;
            for (int i = (int)Enemy.transform.position.z; i < (int)transform.position.z; i++)
            {
                thereIsVLinetop = verticalLine.CheckIfThereIsLine(new Vector3(Enemy.transform.position.x, Enemy.transform.position.y, i),
                                  1, new Vector3(Enemy.transform.position.x, Enemy.transform.position.y, i + 1));
                if (thereIsVLinetop == false)
                    return (false);
            }
            if (IsThereGate2(Enemy, (int)transform.position.z - (int)Enemy.transform.position.z, 0))
                return (false);
            return (true);
        }
        else if (Sight == "Left")
        {
            bool thereIsVLinetop;
            for (int i = (int)Enemy.transform.position.x; i < (int)transform.position.x; i++)
            {
                thereIsVLinetop = horizontalLine.CheckIfThereIsLine(new Vector3(i, Enemy.transform.position.y, Enemy.transform.position.z),
                                  1, new Vector3(i + 1, Enemy.transform.position.y, Enemy.transform.position.z));
                if (thereIsVLinetop == false)
                    return (false);
            }
            if (IsThereGate2(Enemy, (int)transform.position.x - (int)Enemy.transform.position.x, 90))
                return (false);
            return (true);
        }
        else if (Sight == "Right")
        {
            bool thereIsVLinetop;
            for (int i = (int)transform.position.x; i < Enemy.transform.position.x; i++)
            {
                thereIsVLinetop = horizontalLine.CheckIfThereIsLine(new Vector3(i, transform.position.y, transform.position.z),
                                  1, new Vector3(i + 1, transform.position.y, transform.position.z));
                if (thereIsVLinetop == false)
                    return (false);
            }
            if (IsThereGate2(Enemy, (int)Enemy.transform.position.x - (int)transform.position.x, 270))
                return (false);
            return (true);
        }
        return (false);
    }

    private void OnMouseDown()
    {
        string Sight;
        //Debug.Log(PlayerFuncs.IsflagGranade);
        //if (PlayerFuncs.IsflagGranade == false)
       // Debug.Log("PRESSED");
        if (PlayerFuncs.IsflagGranade == true)
        {
            //ft_Throw_Granade(transform.position);
            Sight = ft_check_hero_on_line(transform.position, PlayerFuncs.transform.position);
            //Debug.Log(ft_check_hero_sight(Sight));
            //Debug.Log(Sight);
            if (ft_check_hero_sight(Sight))
            {
                ft_check_mob_on_Line();
                // PlayerFuncs.IsflagGranade = true;
                PlayerFuncs.IsflagGranade = false;
                Time.timeScale = 1;
                GameObject.FindGameObjectWithTag("GranadeButton").SetActive(false);
            }
        }
    }

    /*public bool IsflagGranade
    {
        get
        {
            return flagGranade;
        }
        set
        {
            flagGranade = value;
        }
    }*/

    private Node[] GetListOfNodes()
    {
        Node[] ListOfNodes = GameObject.FindObjectsOfType<Node>();
        return ListOfNodes;
    }

    public bool CheckIfNodeExist(Vector3 PlayerCoord, char XorY, int increment)
    {
        Node[] ListOfNodes = GetListOfNodes();
        for (int i = 0; i < ListOfNodes.Length; i++)
        {
            if (XorY == 'x')
                if (ListOfNodes[i].transform.position.x == PlayerCoord.x + increment 
                && ListOfNodes[i].transform.position.z == PlayerCoord.z)
                    return true;
            if (XorY == 'y')
                if (ListOfNodes[i].transform.position.z == PlayerCoord.z + increment 
                && ListOfNodes[i].transform.position.x == PlayerCoord.x)
                    return true;
        }
        return false;
    }

    void Start()
    {
        PlayerFuncs = PlayerHandler.GetComponent<Player>();
        verticalLine = VerticalLineHandler.GetComponent<VerticalLine>();
        horizontalLine = HorizontalLineHandler.GetComponent<HorizontalLine>();
    }
    // Update is called once per frame
    void Update()
    {
       //Debug.Log(flagGranade);
    }
}
