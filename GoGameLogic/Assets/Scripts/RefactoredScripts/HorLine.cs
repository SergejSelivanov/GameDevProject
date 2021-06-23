using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorLine : Lines
{
    public override bool CheckIfThereIsLine(Vector3 PlayerPos, int sign, Vector3 FinalPos)
    {
        HorLine[] ListOfLines = GameObject.FindObjectsOfType<HorLine>();
        for (int i = 0; i < ListOfLines.Length; i++)
        {
            if (ListOfLines[i].transform.position.x - 0.5 * sign == PlayerPos.x
            && ListOfLines[i].transform.position.x + 0.5 * sign == FinalPos.x
            && ListOfLines[i].transform.position.z == PlayerPos.z)
                return true;
        }
        return false;
    }
}
