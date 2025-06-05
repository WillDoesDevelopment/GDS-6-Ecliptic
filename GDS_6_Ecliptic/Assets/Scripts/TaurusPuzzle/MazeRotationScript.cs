using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRotationScript : MonoBehaviour
{
    public float rotSpeed;

    public DoorScript DS;

    // Start is called before the first frame update
    void Start()
    {
        //DS = GetComponent<DoorStatus>().IsOpen;
    }

    // Update is called once per frame

    private void Update()
    {
        /*
        if (DS.IsOpen == true)
        {
            
            print("DOING THE THING");
        }*/

        this.transform.eulerAngles += new Vector3(0, rotSpeed, 0);

    }

}
