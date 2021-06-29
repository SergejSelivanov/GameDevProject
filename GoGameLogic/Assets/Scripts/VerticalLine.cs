using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalLine : MonoBehaviour
{
    public static bool CheckIfThereIsLine(Vector3 PlayerPos, int sign, Vector3 FinalPos)
    {
        VerticalLine[] ListOfLines = GameObject.FindObjectsOfType<VerticalLine>();
        for (int i = 0; i < ListOfLines.Length; i++)
        {
            if (ListOfLines[i].transform.position.z - 0.5 * sign == PlayerPos.z 
            && ListOfLines[i].transform.position.z + 0.5 * sign == FinalPos.z 
            && ListOfLines[i].transform.position.x == PlayerPos.x)
                return true;
        }
        return false;
    }
}
