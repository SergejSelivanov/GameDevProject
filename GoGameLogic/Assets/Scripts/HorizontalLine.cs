using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalLine : MonoBehaviour
{
    private HorizontalLine[] GetListOfHorLines()
    {
        HorizontalLine[] ListOfLines = GameObject.FindObjectsOfType<HorizontalLine>();
        return ListOfLines;
    }

    public bool CheckIfThereIsLine(Vector3 PlayerPos, int sign, Vector3 FinalPos)
    {
        HorizontalLine[] ListOfLines = GetListOfHorLines();
        for (int i = 0; i < ListOfLines.Length; i++)
        {
            if (ListOfLines[i].transform.position.x - 0.5 * sign == PlayerPos.x && ListOfLines[i].transform.position.x + 0.5 * sign == FinalPos.x && ListOfLines[i].transform.position.z == PlayerPos.z)
                return true;
        }
        return false;
    }



    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
