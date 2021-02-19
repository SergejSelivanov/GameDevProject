using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // Start is called before the first frame update

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

    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
