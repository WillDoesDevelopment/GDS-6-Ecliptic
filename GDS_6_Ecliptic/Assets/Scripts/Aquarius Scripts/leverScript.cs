using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class leverScript : MonoBehaviour
{
    public GameEvent eTrigger;

    [SerializeField] private int leverNum;

    public void LeverNumberQueue()
    {
        eTrigger.TriggerEvent();
    }
   
}
