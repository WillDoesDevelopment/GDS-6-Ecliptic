using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float movSpeed = 0;

    public Rigidbody RB;
    // Start is called before the first frame update
    void Start()
    {
        RB = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movment();
    }

    private void movment()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //transform.position += Vector3.left*Time.deltaTime * movSpeed;
            //RB.AddForce(Vector3.left * movSpeed);
            RB.velocity = Vector3.left * movSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //transform.position += Vector3.back * Time.deltaTime * movSpeed;
            //RB.AddForce(Vector3.back * movSpeed);
            RB.velocity = Vector3.back * movSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //transform.position += Vector3.right * Time.deltaTime * movSpeed;
            //RB.AddForce(Vector3.right * movSpeed);
            RB.velocity = Vector3.right * movSpeed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            //transform.position += Vector3.forward * Time.deltaTime * movSpeed;
            //RB.AddForce(Vector3.forward * movSpeed);
            RB.velocity =Vector3.forward * movSpeed;
        }
    }
}
