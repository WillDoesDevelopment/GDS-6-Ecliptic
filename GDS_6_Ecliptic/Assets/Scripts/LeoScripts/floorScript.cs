using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorScript : MonoBehaviour
{

    public bool fall = false;
    float timer = 0;
    public float a = 0;
    float shakeSpeed = 4.5f;
    float shakeDist = 0.1f;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(fall == true)
        {
            timer += Time.deltaTime;
            if(timer < 4)   //Shake
            {
                a = Mathf.Abs(((shakeSpeed* timer + 0.5f) % 1f)*4f - 2f)-1f;                       //triangle wave
                transform.position = startPos + new Vector3(a * shakeDist, 0, 0);
            }
            if(timer > 4)   //Fall
            {
                transform.position = transform.position + Vector3.down * 2.0f * Time.deltaTime;
            }
            if(timer > 10)  //Deactivate
            {
                gameObject.SetActive(false);
            }
            
            
        }
    }

}
