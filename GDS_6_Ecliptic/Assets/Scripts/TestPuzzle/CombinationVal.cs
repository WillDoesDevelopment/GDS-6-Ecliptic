using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationVal : MonoBehaviour
{
    public int Val = 1;
    void Update()
    {
        if (Val > 9)
        {
            Val = 1;
        }
    }
}
