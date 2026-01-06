using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTester : MonoBehaviour
{
    [SerializeField] private GameEventInt ge;

    [SerializeField] private int num;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            ge.TriggerEvent(num);
        }
    }
}
