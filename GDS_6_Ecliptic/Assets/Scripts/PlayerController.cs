using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public Animator PlayerAnim;
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
    public GameObject pauseMenu;
    public bool isPaused = false;
      
    //Spawn
    Vector3 spawnPoint;


    // Start is called before the first frame update
    void Start()
    {

        isPaused = false;

        // Spawn
        spawnPoint = transform.position;

        // Movement
        controller = GetComponent<CharacterController>();                
        
    }
    
    void Respawn()
    {
        //controller.enabled = false;
        //transform.position = spawnPoint;
        //controller.enabled = true;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    

    // Update is called once per frame
    void Update()
    {
        //Exit
/*        if (Input.GetKey("escape") && isPaused == false || Input.GetButton("Cancel") && isPaused == false)
        {
                pauseMenu.SetActive(true);
                canWalk = false;
                //isPaused = true;
            
        }

        if(pauseMenu.activeInHierarchy == false )
        {
            
            canWalk = true;
        }*/

        /*if (Input.GetKey("escape") && isPaused == true || Input.GetButton("Cancel") && isPaused == true)
        {
            pauseMenu.SetActive(false);
            canWalk = true;
            isPaused = false;

        }*/

        //Movement
        #region Movement




        

        if(canWalk)
        {
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
                airTime = 0;
            }
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
                PlayerAnim.SetBool("Walking", true);
                yRotation = Mathf.Atan2(moveVector.z, moveVector.x) * Mathf.Rad2Deg * -1f + 90f;
                _rotation = Mathf.SmoothDampAngle(_rotation, yRotation, ref _velocity, 0.1f);
                transform.rotation = Quaternion.Euler(0f, _rotation , 0f);
            }
            else
            {
                PlayerAnim.SetBool("Walking", false);
            }
        }
        else
        {
         
        }
    }      
}
