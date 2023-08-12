using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCamera : MonoBehaviour
{

    public GameObject player;
    Vector3 offset;
    Vector3 _velocity;
    public float SmoothTime = 1;
    public float MaxSpeed = 1;


    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;

        //transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + player.transform.forward * 2f + offset, ref _velocity, SmoothTime, MaxSpeed);

    }
}
