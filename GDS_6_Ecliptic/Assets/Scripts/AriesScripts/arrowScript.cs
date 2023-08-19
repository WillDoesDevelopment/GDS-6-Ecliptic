using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour
{
    public float speed = 5.0f;
    public float destroyTime = 10;
    float arrowLength = 1.3f;
    public GameObject burstPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        transform.position += transform.forward * speed * Time.deltaTime;

        //Time destroy safeguard
        destroyTime -= Time.deltaTime;
        if (destroyTime < 0)
        {
            Destroy(gameObject);
        }

        //Collision check
        var rayPos = transform.position - transform.forward * arrowLength * 0.5f;       //Rear point of arrow
        RaycastHit hit;
        Debug.DrawLine(rayPos, rayPos + transform.forward * arrowLength, Color.blue, 0.01f);
        if (Physics.Raycast(new Ray(rayPos, transform.forward), out hit, arrowLength))  //Raycast forward
        {
            Debug.DrawLine(rayPos, rayPos + transform.up, Color.green, 0.01f);

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))         //Check if hitting wall
            {
                Burst();
                Destroy(gameObject);        
            }
            if (hit.collider.gameObject.name == "Player")                               //Check if hitting Player
            {
                Burst();
                Debug.Log("Hit Player");
                Destroy(gameObject);
            }
            if (hit.collider.gameObject.name == "Ram")                                  //Check if hitting Ram
            {
                Burst();                
                Destroy(gameObject);
            }

        }
    }


    void Burst()
    {
        var newObject = Instantiate(burstPrefab);                                                    //Create Arrow
        newObject.transform.position = transform.position;
    }
    
    

}

