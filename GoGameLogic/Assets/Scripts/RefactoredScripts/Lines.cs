using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Lines : MonoBehaviour
{
    public abstract bool CheckIfThereIsLine(Vector3 PlayerPos, int sign, Vector3 FinalPos);
}
