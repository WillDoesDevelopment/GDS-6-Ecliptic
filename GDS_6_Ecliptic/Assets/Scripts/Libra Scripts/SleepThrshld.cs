using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepThrshld : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody Rigidbody;
    public float slpThreshold;
    void Start()
    {
        Rigidbody = transform.GetComponent<Rigidbody>();
        Rigidbody.sleepThreshold = slpThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
