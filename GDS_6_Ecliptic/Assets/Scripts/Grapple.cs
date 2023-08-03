using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    public GameObject target;
    public float length = 10f;
    public float t = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t = length / Vector3.Distance(target.transform.position, transform.position);
        if(t < 1)
        {
            target.transform.LookAt(transform);            
        }
        target.transform.position = Vector3.Lerp(transform.position, target.transform.position, t);



    }
}
