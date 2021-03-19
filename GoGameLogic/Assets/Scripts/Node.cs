using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    /*public GameObject playernhandler;
    private Player playern;*/
    private bool flagGranade = true;

    public void ft_pressed_granade_button()
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
    }

    private void OnMouseDown()
    {
        Debug.Log(flagGranade);
        if (flagGranade == false)
        {
            Debug.Log(transform.position.x);
            Debug.Log(transform.position.y);
            Debug.Log(transform.position.z);
        }
    }

    public bool IsflagGranade
    {
        get
        {
            return flagGranade;
        }
        set
        {
            flagGranade = value;
        }
    }

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
        //playern = playernhandler.GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
       //Debug.Log(flagGranade);
    }
}
