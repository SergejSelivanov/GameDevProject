using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalLine : MonoBehaviour
{
    public static bool CheckIfThereIsLine(Vector3 PlayerPos, int sign, Vector3 FinalPos)
    {
        HorizontalLine[] ListOfLines = GameObject.FindObjectsOfType<HorizontalLine>();
        for (int i = 0; i < ListOfLines.Length; i++)
        {
            if (ListOfLines[i].transform.position.x - 0.5 * sign == PlayerPos.x //check if there is line in needed destination
            && ListOfLines[i].transform.position.x + 0.5 * sign == FinalPos.x
            && ListOfLines[i].transform.position.z == PlayerPos.z)
                return true;
        }
        return false;
    }
}
