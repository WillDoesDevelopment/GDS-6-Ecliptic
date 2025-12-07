using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mazeTrigger : MonoBehaviour
{
    public bool mazeisOn = false;
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        mazeisOn = true;
    }
}
