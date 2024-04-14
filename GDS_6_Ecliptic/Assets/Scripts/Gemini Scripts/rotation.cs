using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    public bool rotate = true;
    public float rotSpeedX = 0;
    public float rotSpeedY = 0;
    public float rotSpeedZ = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate == true)
        {
            transform.Rotate(rotSpeedX * Time.deltaTime, rotSpeedY * Time.deltaTime, rotSpeedZ * Time.deltaTime);
        }
    }
}
