using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZCamera : MonoBehaviour
{

    public GameObject player;
    public GameObject doppelganger;
    Vector3 offset;
    //Vector3 _velocity;
    //public float SmoothTime = 1;
    //public float MaxSpeed = 1;
    //[Range(0f, 1f)] public float XLerp = 0;
    //public float centerX = 0;


    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - (player.transform.position + doppelganger.transform.position)/2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (player.transform.position + doppelganger.transform.position) / 2 + offset;
        //transform.position = new Vector3(Mathf.Lerp(transform.position.x, centerX, XLerp), transform.position.y, transform.position.z);

       
    }
}
