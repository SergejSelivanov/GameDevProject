using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalLine : MonoBehaviour
{
    // Start is called before the first frame update

    private VerticalLine[] GetListOfVerLines()
    {
        VerticalLine[] ListOfLines = GameObject.FindObjectsOfType<VerticalLine>();
        return ListOfLines;
    }

    public bool CheckIfThereIsLine(Vector3 PlayerPos, int sign, Vector3 FinalPos)
    {
        VerticalLine[] ListOfLines = GetListOfVerLines();
        for (int i = 0; i < ListOfLines.Length; i++)
        {
            if (ListOfLines[i].transform.position.z - 0.5 * sign == PlayerPos.z && ListOfLines[i].transform.position.z + 0.5 * sign == FinalPos.z && ListOfLines[i].transform.position.x == PlayerPos.x)
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
