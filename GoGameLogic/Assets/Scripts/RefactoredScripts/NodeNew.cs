using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeNew : MonoBehaviour
{
    public static bool CheckIfNodeExist(Vector3 PlayerCoord, char XorY, int increment)
    {
        GameObject[] ListOfNodes = GameObject.FindGameObjectsWithTag("Node");
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
