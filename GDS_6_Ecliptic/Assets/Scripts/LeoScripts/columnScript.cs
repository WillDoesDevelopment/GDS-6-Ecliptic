using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class columnScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool fall = false;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fall == true)
        {
            transform.position = transform.position - Vector3.up * Time.deltaTime * 4.0f;
            GetComponent<CapsuleCollider>().enabled = false;
            Destroy(gameObject, 4.0f);
        }
    }
}
