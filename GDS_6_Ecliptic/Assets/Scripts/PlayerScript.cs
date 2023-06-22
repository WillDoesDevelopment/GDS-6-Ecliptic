using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Movement
    public float movSpeed = 0;

    float inputX;
    float inputZ;
    public float _rotation;
    float _velocity;
    public float yRotation;

    public Vector3 moveVector;

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
        //Input
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        //Translation
        moveVector = Vector3.ClampMagnitude(new Vector3(inputX, 0, inputZ), 1.0f) * movSpeed;
        RB.velocity = moveVector;

        //Rotation
        if(moveVector != Vector3.zero)
        {
            yRotation = Mathf.Atan2(moveVector.z, moveVector.x) * Mathf.Rad2Deg * -1f + 90f;
            _rotation = Mathf.SmoothDampAngle(_rotation, yRotation, ref _velocity, 0.1f);
            transform.rotation = Quaternion.Euler(0f, _rotation, 0f);
        }
        
    }
}
