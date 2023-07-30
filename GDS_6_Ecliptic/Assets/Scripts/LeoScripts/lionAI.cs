using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lionAI : MonoBehaviour
{
    public GameObject Player;
    float inv = 1;
    float angle = 0;
    Vector3 forward;
    Vector3 target;
    float rayDist = 10;

    
    LayerMask hitObject;
    // Start is called before the first frame update
    void Start()
    {
        forward = transform.forward;
        target = Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rayDist = Vector3.Distance(transform.position, Player.transform.position)-3.0f;

        var rayPos = transform.position;
        RaycastHit hit;
        //hitObject = LayerMask.NameToLayer("Wall");

        forward = Vector3.Normalize(Player.transform.position - transform.position);
        //target = Player.transform.position;
        angle = 0.0f;

        for (var i = 0; i < 100; i++)
        {
            Debug.DrawLine(rayPos, rayPos + forward * 2.0f, Color.blue, 0.01f);
            if (Physics.SphereCast(new Ray(rayPos, forward), 2.0f, out hit, rayDist))  
            {
                //target = hit.transform.position;
                //hitObject = hit.collider.gameObject.layer;
                inv = inv * -1.0f;
                angle += 1.0f;
                forward = Quaternion.Euler(0, angle * inv, 0) * forward;
            }
            else
            {
                target = rayPos + forward * 10.0f;
                break;
            }

        }
        if(rayDist > 1)
        {
            transform.LookAt(target);
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime);
        }
        

    }
}
