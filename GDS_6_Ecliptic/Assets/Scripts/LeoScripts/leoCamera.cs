using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leoCamera : MonoBehaviour
{

    public GameObject player;
    public GameObject lion;
    Vector3 offset;
    Vector3 offsetDir;
    Vector3 camCenter;
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
        if(lion.GetComponent<lionAI>().lionState != LionState.Falling)
        {
            xSeperation = Mathf.Abs(player.transform.position.x - lion.transform.position.x) / 4;
            camCenter = (3 * player.transform.position + lion.transform.position) / 4;
            transform.position = camCenter + offset + offsetDir * xSeperation;
        }
        else
        {
            transform.position = player.transform.position + offset;
        }


        
        //transform.position = player.transform.position + offset + offsetDir * xSeperation;

        //transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + player.transform.forward * 2f + offset, ref _velocity, SmoothTime, MaxSpeed);

    }
}
