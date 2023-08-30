using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leoCamera : MonoBehaviour
{

    public GameObject player;
    public GameObject lion;
    Vector3 offset;
    Vector3 offsetDir;
    float xSeperation = 0;
    //Vector3 _velocity;
    //public float SmoothTime = 1;
    //public float MaxSpeed = 1;



    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        offsetDir = Vector3.Normalize(offset);
    }

    // Update is called once per frame
    void Update()
    {
        xSeperation = Mathf.Abs(player.transform.position.x - lion.transform.position.x) / 4;
        transform.position = player.transform.position + offset + offsetDir * xSeperation;

        //transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + player.transform.forward * 2f + offset, ref _velocity, SmoothTime, MaxSpeed);

    }
}
