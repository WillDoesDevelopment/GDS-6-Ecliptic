using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour
{
    public float speed = 5.0f;
    public float destroyTime = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;


        destroyTime -= Time.deltaTime;
        if (destroyTime < 0)
        {
            Destroy(gameObject);
        }
              
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        if(collision.transform.gameObject.name == "Ram")
        {
            Debug.Log("Destroy");
            Destroy(gameObject);
        }
    }
}

