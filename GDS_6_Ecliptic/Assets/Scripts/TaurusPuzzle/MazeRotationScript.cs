using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRotationScript : MonoBehaviour
{
    public float rotSpeed;

    public mazeTrigger mt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        
        if (mt.mazeisOn == true)
        {
            this.transform.eulerAngles += new Vector3(0, rotSpeed, 0);
            print("DOING THE THING");
        }

        else
        {
            return;
        }

        

    }

}
