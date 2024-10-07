using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int health = 3;
    public GameObject mainCamera;
    public Animator PlayerAnim;
    //Movement
    CharacterController controller;
    float inputX;
    float inputY;
    float inputZ;
    public Vector3 moveVector;
    Vector3 moveDirection = Vector3.zero;
    public Vector3 AutoWalkDestination;
    public float _rotation;
    float _velocity;
    public float yRotation;
    //[Range(0f, 100f)] public float animSpeed = 8;
    float animSpeed = 0.2f;
    [Range(0f, 10f)] public float speed = 5;
    [Range(0f, 20f)] public float gravity = 10;
    [Range(0f, 100f)] public float maxFallSpeed = 10;
    public float airTime = 0;
    bool grounded = false;
    public PlayerState playerState = PlayerState.Walk;
    public bool canWalk = true;
    public bool walking = false;
    public float walkingSpeed = 0;
    public GameObject pauseMenu;
    public bool isPaused = false;
    bool canStep = true;
    [Range(0f, 1f)] public float stepVolume = 0.8f;
    [Range(0f, 0.8f)] public float stepPitchVariance = 0.5f;
    public AudioSource audioSource;
    public AudioClip[] stepArray;
    float timer = 0;
    public float knockbackTime = 2f;
    public float knockbackDist = 10f;
    public AnimationCurve knockbackV;
    public AnimationCurve knockbackH;
    Vector3 knockbackStartPos;
    Vector3 knockbackEndPos;

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

        //Placeholder values
        knockbackStartPos = transform.position;
        knockbackEndPos = transform.position + Vector3.left * 10f;

    }




    // Update is called once per frame
    void Update()
    {
        //Debug remove for build
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            //playerState = PlayerState.Knockback;
            //Damage();
        }

        //if(canWalk)

        if (playerState == PlayerState.Walk)
        {
            PlayerInput();
            Movement();
        }
        else
        {

            PlayerAnim.SetBool("Walking", false);
            walking = false;
        }

        if (playerState == PlayerState.Paused)
        {

        }
        else if (playerState == PlayerState.Autowalk)
        {
            AutoWalk();
            Movement();
        }
        else if (playerState == PlayerState.Dialogue)
        {

        }
        else if (playerState == PlayerState.Freeze)
        {

        }
        else if (playerState == PlayerState.Knockback)
        {
            Knockback();
        }
        else if (playerState == PlayerState.Damage)
        {
            Damage();
        }
    }

    void PlayerInput()
    {
        FallingCheck();

        inputY = Mathf.Clamp(inputY, -maxFallSpeed, maxFallSpeed);
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");
    }
    void Movement()
    {
        //translation
        moveDirection = new Vector3(inputX, 0, inputZ);
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1.0f) * speed;
        moveDirection = new Vector3(moveDirection.x, inputY, moveDirection.z);

        //camera correction
        if(playerState == PlayerState.Walk & mainCamera != null)
        {
            moveDirection = Quaternion.Euler(0, mainCamera.transform.eulerAngles.y,0) * moveDirection;
        }
        grounded = (controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0; //credit benjamin esposito First Person Drifter



        //rotation input
        moveVector = Vector3.ClampMagnitude(new Vector3(inputX, 0, inputZ), 1.0f) * speed * Time.deltaTime;
        //camera correction
        if (playerState == PlayerState.Walk & mainCamera != null)
        {
            moveVector = Quaternion.Euler(0, mainCamera.transform.eulerAngles.y, 0) * moveVector;
        }
        if (moveVector != Vector3.zero)
        {
            PlayerAnim.SetBool("Walking", true);
            walking = true; // Used for doppelganger
            //walkingSpeed = Vector3.Magnitude(Vector3.ClampMagnitude(new Vector3(inputX, 0, inputZ), 1.0f));
            //walkingSpeed = Vector3.Magnitude(moveVector) * animSpeed;
            walkingSpeed = Vector3.Magnitude(Vector3.ClampMagnitude(new Vector3(inputX, 0, inputZ), 1.0f)) * animSpeed * speed;
            PlayerAnim.speed = walkingSpeed;

            FootStepCheck();

            yRotation = Mathf.Atan2(moveVector.z, moveVector.x) * Mathf.Rad2Deg * -1f + 90f;
            _rotation = Mathf.SmoothDampAngle(_rotation, yRotation, ref _velocity, 0.1f);
            transform.rotation = Quaternion.Euler(0f, _rotation, 0f);
        }
        else
        {
            PlayerAnim.SetBool("Walking", false);
            walking = false;
        }
    }

    public void FallingCheck()
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

        if (airTime > 8)       //Respawns if falling for too long
        {
            Respawn();
            airTime = 0;
        }
    }
    public void FootStepCheck()
    {
        var h = PlayerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        h = h - Mathf.Floor(h);
        //Steps
        if (h > 0.3f && h < 0.4f && canStep == true)
        {
            Footstep();

        }
        if (h > 0.6f && h < 0.7f)
        {
            canStep = true;
        }
        if (h > 0.8f && h < 0.9f && canStep == true)
        {
            Footstep();

        }
        if (h > 0.9f || h < 0.2f)
        {
            canStep = true;
        }

    }

    public void AutoWalk()
    {
        //Simple linear auto walk function
        Vector3 xz = new Vector3(1, 0, 1);
        Vector3 autoDir = Vector3.Normalize(Vector3.Scale(AutoWalkDestination, xz) - Vector3.Scale(transform.position, xz));

        var a = Vector3.Distance(Vector3.Scale(AutoWalkDestination, xz), Vector3.Scale(transform.position, xz)); //Distance to destination
        var b = Mathf.InverseLerp(0.0f, 2.0f, a);   //Slow movement when within 2m of destination
        var c = Mathf.Lerp(0.2f, 1.0f, b);          //Slow by this factor

        inputX = autoDir.x * c;
        inputZ = autoDir.z * c;
        if(a < 0.1) { playerState = PlayerState.Dialogue; } //Stop when within 0.1m of destination
    }

    void Knockback()
    {
        timer += Time.deltaTime;

        //Add player falling animation here

        var a = Mathf.Clamp01(knockbackH.Evaluate(timer / knockbackTime));

        transform.position = Vector3.Lerp(knockbackStartPos, knockbackEndPos, a);
        transform.position += new Vector3(0, knockbackV.Evaluate(a), 0);
        //Debug.Log(a);

        if(a == 1f)
        {
            timer = 0;
            playerState = PlayerState.Walk;
        }
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
        //Debug.Log("Step");
        canStep = false;
        if (audioSource != null && stepArray != null)
        {
            audioSource.clip = stepArray[Random.Range(0, stepArray.Length)];
            audioSource.pitch = 1 + Random.Range(-stepPitchVariance, stepPitchVariance);
            //audioSource.volume = stepVolume;
            audioSource.PlayOneShot(audioSource.clip);

        }
    }

    public void Damage()
    {
        health -= 1;
        if(health == 0)
        {
            Respawn();
            //Restart or faint or something
        }
        playerState = PlayerState.Knockback;
        knockbackStartPos = transform.position;
        knockbackEndPos = transform.position - transform.forward * knockbackDist;
        Knockback();
    }
}
