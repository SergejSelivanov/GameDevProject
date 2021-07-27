using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public static GameObject FindNode(Node[] Nodes, int X, int Z)
    {
        for (int i = 0; i < Nodes.Length; i++)
        {
            if (Nodes[i].transform.position.x == X && Nodes[i].transform.position.z == Z) //if node is in definite position
                return (Nodes[i].gameObject);
        }
        return (null);
    }

    public static bool CheckIfThereIsNodeToMove(GameObject Obj)
    {
        if (Obj.transform.rotation.eulerAngles.y == 0)
        {
            if (CheckIfNodeExist(Obj.transform.position, 'y', 1)
            && VerticalLine.CheckIfThereIsLine(Obj.transform.position, 1, Obj.transform.position + new Vector3(0, 0, 1))) //check if there is node in front of object
                return true;
        }
        if (Obj.transform.rotation.eulerAngles.y == 90)
        {
            if (CheckIfNodeExist(Obj.transform.position, 'x', 1)
            && HorizontalLine.CheckIfThereIsLine(Obj.transform.position, 1, Obj.transform.position + new Vector3(1, 0, 0)))
                return true;
        }
        if (Obj.transform.rotation.eulerAngles.y == 180)
        {
            if (CheckIfNodeExist(Obj.transform.position, 'y', -1)
            && VerticalLine.CheckIfThereIsLine(Obj.transform.position, -1, Obj.transform.position + new Vector3(0, 0, -1)))
                return true;
        }
        if (Obj.transform.rotation.eulerAngles.y == 270)
        {
            if (CheckIfNodeExist(Obj.transform.position, 'x', -1)
            && HorizontalLine.CheckIfThereIsLine(Obj.transform.position, -1, Obj.transform.position + new Vector3(-1, 0, 0)))
                return true;
        }
        return false;
    }

    public static bool CheckIfNodeExist(Vector3 PlayerCoord, char XorY, int increment)
    {
        Node[] ListOfNodes = GameObject.FindObjectsOfType<Node>();
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
}
