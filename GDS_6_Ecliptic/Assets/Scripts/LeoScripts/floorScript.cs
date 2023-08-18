using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorScript : MonoBehaviour
{

    public bool solid = true;
    public bool obstacle = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(solid == false)
        {
            transform.position = transform.position + Vector3.down * 2.0f * Time.deltaTime;
            
        }
    }

}
