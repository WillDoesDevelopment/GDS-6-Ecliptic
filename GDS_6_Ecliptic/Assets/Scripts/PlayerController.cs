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
    bool canStep = true;
    public AudioSource audioSource;
    public AudioClip[] stepArray;
    bool playDaftPunk = false;
      
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
    
    void Footstep()
    {
        Debug.Log("Step");
        canStep = false;
        if(audioSource != null && playDaftPunk)
        {
            audioSource.clip = stepArray[Random.Range(0, stepArray.Length)];
            audioSource.PlayOneShot(audioSource.clip);

            //step.Play();
        }        
    }

    //public IEnumerator FootStep

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            playDaftPunk = !playDaftPunk;
        }

        

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
                PlayerAnim.speed = Vector3.Magnitude(Vector3.ClampMagnitude(new Vector3(inputX, 0, inputZ), 1.0f));
                var h = PlayerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
                h = h - Mathf.Floor(h);
                //Steps
                if(h>0.32f && h < 0.4f && canStep == true)
                {
                    Footstep();

                }
                if(h > 0.6f && h < 0.7f)
                {
                    canStep = true;
                }
                if (h > 0.82f && h < 0.9f && canStep == true)
                {
                    Footstep();

                }
                if (h > 0.9f || h < 0.2f)
                {
                    canStep = true;
                }



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
            PlayerAnim.SetBool("Walking", false);
        }
    }      
}
