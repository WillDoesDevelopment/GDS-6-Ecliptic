using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lionAI : MonoBehaviour
{
    public GameObject Player;
    public float speed = 1;
    float inv = 1;
    float angle = 0;
    Vector3 forward;
    Vector3 target;
    Vector3 pounceTarget;
    float rayDist = 10;
    public int state = 2;
    public float rotationSpeed = 4;
    float timer = 10;
    public float followTime = 10f;
    public float chargeTime = 3f;
    public float waitTime = 2f;
    public float pounceTime = 2.5f;


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
        if(state == 1)          //Following
        {
            rayDist = Vector3.Distance(transform.position, Player.transform.position) - 3.0f;
            var rayPos = transform.position;
            RaycastHit hit;

            forward = Vector3.Normalize(Vector3.Scale(Player.transform.position - transform.position, new Vector3(1, 0, 1)));
            angle = 0.0f;

            for (var i = 0; i < 100; i++)
            {
                Debug.DrawLine(rayPos, rayPos + forward * 2.0f, Color.blue, 0.01f);
                if (Physics.SphereCast(new Ray(rayPos, forward), 2.0f, out hit, rayDist))
                {
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
            if (rayDist > 1)
            {
                transform.LookAt(target);
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            }

            timer -= Time.deltaTime;
            if(timer < 0) 
            {
                timer = chargeTime;
                state = 2; 
            }
        }

        if(state == 2)          //Charging
        {
            forward = Vector3.Normalize(Vector3.Scale(Player.transform.position - transform.position , new Vector3(1,0,1)));
              
            
            Quaternion targetRotation = Quaternion.LookRotation(forward, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            timer -= Time.deltaTime;
            if (timer < 0)
            {
                pounceTarget = transform.position + transform.forward * 10;
                timer = waitTime;
                state = 3;
            }
        }

        if (state == 3)         //Waiting
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = pounceTime;
                state = 4;
            }
        }

        if (state == 4)         //Pouncing
        {
            transform.position = Vector3.MoveTowards(transform.position, pounceTarget, Time.deltaTime * 40.0f);
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = followTime;
                state = 1;
            }
        }

        //Collision

        Collider[] hitColliders = Physics.OverlapBox(transform.position, new Vector3(1f, 1.5f, 1f));
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.layer == LayerMask.NameToLayer("Column"))
            {
                hitCollider.transform.gameObject.SetActive(false);
            }

        }

    }
}
