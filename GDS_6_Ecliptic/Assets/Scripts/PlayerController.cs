using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Movement
    CharacterController controller;
    float inputX;
    float inputY;
    float inputZ;
    public Vector3 moveVector;
    Vector3 moveDirection = Vector3.zero;
    public float _rotation;
    float _velocity;
    public float yRotation;    
    [Range(0f, 10f)] public float speed = 5;        
    [Range(0f, 20f)] public float gravity = 10;
    [Range(0f, 100f)] public float maxFallSpeed = 10;
    public float airTime = 0;
    bool grounded = false;    
    public bool canWalk = true;
      
    //Spawn
    Vector3 spawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //HI SRRY CURSOR BUG GO BRRR

        // Spawn
        spawnPoint = transform.position;

        // Movement
        controller = GetComponent<CharacterController>();                
        
    }
    
    void Respawn()
    {
        controller.enabled = false;
        transform.position = spawnPoint;
        controller.enabled = true;
    }
    

    // Update is called once per frame
    void Update()
    {
        //Exit
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        

        //Movement
        #region Movement

                 
        if (grounded == false)
        {
            airTime += Time.deltaTime;
            inputY += -gravity * Time.deltaTime;
        }
        else
        {
            airTime = 0;
            inputY = -0.25f;
        }

        if (airTime > 10)       //Respawns if falling for too long
        {
            Respawn();
        }

        

        if(canWalk)
        {
            inputY = Mathf.Clamp(inputY, -maxFallSpeed, maxFallSpeed);
            inputX = Input.GetAxis("Horizontal");
            inputZ = Input.GetAxis("Vertical");

            moveDirection = new Vector3(inputX, 0, inputZ);
            moveDirection = Vector3.ClampMagnitude(moveDirection, 1.0f) * speed;
            moveDirection = new Vector3(moveDirection.x, inputY, moveDirection.z);
                   
            grounded = (controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0; //credit benjamin esposito First Person Drifter


            #endregion Movement


            //rotation input
            moveVector = Vector3.ClampMagnitude(new Vector3(inputX, 0, inputZ), 1.0f) * speed *Time.deltaTime;
            if (moveVector != Vector3.zero)
            {
                yRotation = Mathf.Atan2(moveVector.z, moveVector.x) * Mathf.Rad2Deg * -1f + 90f;
                _rotation = Mathf.SmoothDampAngle(_rotation, yRotation, ref _velocity, 0.1f);
                transform.rotation = Quaternion.Euler(0f, _rotation , 0f);
            }
        }
    }      
}
