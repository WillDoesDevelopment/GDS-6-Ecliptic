using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lionAI : MonoBehaviour
{
    public LionState lionState = LionState.Cutscene;
    public HubManager hubManager;
    public RoomManager RM;
    PlayerController playerController;
    public GameObject player;
    public GameObject ColumnParent;
    public GameObject particle1;
    public GameObject particle2;
    public GameObject particle3;
    public GameObject barrier;
    public GameObject roarObject;
    public DialogueTrigger StartDialogue;
    public DialogueTrigger EndDialogue;
    public DialogueTrigger DeathDialogue;
    public float barrierMaxScale = 20;
    public float speed = 1;
    float inv = 1;
    float angle = 0;
    Vector3 forward;
    Vector3 target;
    Vector3 pounceTarget;
    float rayDist = 10;
    public int stage = 1;
    int difficulty = 2;
    public int columnCount = 7;
    public float rotationSpeed = 4;
    float timer = 10;
    public float followTime = 10f;
    public float chargeTime = 3f;
    public float waitTime = 2f;
    public float pounceTime = 2.5f;
    public float pounceDistance = 14f;
    public float roarTime = 2.5f;
    bool roar1Lock = false;
    bool roar2Lock = false;
    bool roar3Lock = false;
    bool roar4Lock = false;
    bool roar5Lock = false;
    public int stateCounter = 0;

    //Floor collapse
    float floorTimer = 0.0f;
    float x;
    float y;
    float z;
    Vector3 Pos;


    LayerMask hitObject;
    // Start is called before the first frame update
    void Start()
    {
        RM = FindObjectOfType<RoomManager>();
        playerController = player.GetComponent<PlayerController>();


    }

    // Update is called once per frame
    void Update()
    {
        Difficulty();
        //StartDialogue.OnEventCheck();
        //StartDialogue.OnEvent = false;

        if(columnCount <4 && columnCount > 0)
        {
            stage = 2;
        }
        if(columnCount == 0)
        {
            lionState = LionState.Defeat;
            columnCount = -1;
        }

        if (StartDialogue.dialogue.DialogueMode == Dialogue.DialogueState.Finished && lionState == LionState.Cutscene)
        {
            forward = transform.forward;
            target = player.transform.position;
            particle1.SetActive(false);
            particle2.SetActive(false);
            particle3.SetActive(false);
            barrier.SetActive(false);
            lionState = LionState.Follow;
            timer = followTime;
        }

        if (lionState == LionState.Follow)          //Following
        {
            Follow();
        }

        else if(lionState == LionState.Charge)           //Charging
        {
            Charge();
        }

        else if (lionState == LionState.Wait)            //Waiting
        {
            Wait();
        }

        else if (lionState == LionState.Pounce)          //Pouncing
        {
            Pounce();
        }

        else if (lionState == LionState.Roar)          //Pouncing
        {
            Roar();
        }

        else if (lionState == LionState.Defeat)          //Defeat
        {
            Defeat();
        }

        else if (lionState == LionState.Falling)          //Falling
        {
            Falling();
        }

        if (ColumnParent.transform.childCount == 0 && lionState != LionState.Defeat)                                //Level Ends
        {
            x = Mathf.Floor(transform.position.x + 0.5f);
            y = transform.position.y;
            z = Mathf.Floor(transform.position.z + 0.5f);
            Pos = new Vector3(x, y, z);
            lionState = LionState.Defeat;
        }
        /*
        if (lionState == LionState.Defeat)     //Collapse
        {
            particle1.SetActive(false);
            particle2.SetActive(false);
            barrier.SetActive(true);
            barrier.transform.position = Pos;
            var a = Mathf.Clamp(floorTimer * 20f, 0, barrierMaxScale);
            barrier.transform.localScale = new Vector3(a, barrierMaxScale, a);

            floorTimer += Time.deltaTime;
            if (floorTimer > 0.0)
            {
                collapse(3.0f);
            }
            if (floorTimer > 0.5)
            {
                collapse(4.0f);
            }
            if (floorTimer > 1.0)
            {
                collapse(5.0f);
            }
            if (floorTimer > 1.5)
            {
                collapse(6.0f);
            }
            if (floorTimer > 4.0f)
            {
                gameObject.SetActive(false);
            }

            transform.position = transform.position - Vector3.up * Time.deltaTime * 2.0f;            
            //Destroy(gameObject, 4.0f);
        }
        */

        //Collision    
        Collider[] hitColliders1 = Physics.OverlapBox(transform.position + transform.forward * 2f, new Vector3(1.25f, 1.0f, 1.25f)); //Hitbox for player
        foreach (var hitCollider in hitColliders1)
        {            
            if (hitCollider.gameObject == player)                                           
            {
                if(playerController.playerState == PlayerState.Walk) //Player takes damage
                {
                    playerController.Damage();
                }
                                
            }
        }
        Collider[] hitColliders2 = Physics.OverlapBox(transform.position, new Vector3(1.0f, 1.0f, 1.0f)); //Hitbox for columns. Hitboxes need rotation.........
        foreach (var hitCollider in hitColliders2)
        {
            if (hitCollider.gameObject.layer == LayerMask.NameToLayer("Grapple"))            //collumn falls
            {                      
                hitCollider.gameObject.GetComponent<columnScript>().fall = true;
            }  
        }
        
    }

    void Cutscene()
    {

    }

    void Follow()
    {
        particle3.SetActive(false);
        rayDist = Vector3.Distance(transform.position, player.transform.position) - 3.0f;
        var rayPos = transform.position;
        RaycastHit hit;

        forward = Vector3.Normalize(Vector3.Scale(player.transform.position - transform.position, new Vector3(1, 0, 1)));
        angle = 0.0f;

        for (var i = 0; i < 100; i++)
        {
            Debug.DrawLine(rayPos, rayPos + forward * 2.0f, Color.blue, 0.01f);
            if (Physics.SphereCast(new Ray(rayPos, forward), 2.0f, out hit, rayDist))
            {
                inv = inv * -1.0f;
                angle += 1.0f;
                forward = Quaternion.Euler(0, angle * inv, 0) * forward;
            }
            else
            {
                target = rayPos + forward * 10.0f;
                break;
            }
        }
        if (rayDist > 1)
        {
            transform.LookAt(target);
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        }

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (stateCounter == 1)
            {
                stateCounter = 0;
                timer = chargeTime;
                lionState = LionState.Charge;
            }
            else if (Random.Range(0, 2) == 1)
            {
                stateCounter = 0;
                timer = chargeTime;
                lionState = LionState.Charge;
            }
            else
            {
                stateCounter = 1;
                timer = roarTime;
                lionState = LionState.Roar;
            }           
            
        }
    }
    void Charge()
    {
        particle1.SetActive(true);
        forward = Vector3.Normalize(Vector3.Scale(player.transform.position - transform.position, new Vector3(1, 0, 1)));

        Quaternion targetRotation = Quaternion.LookRotation(forward, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            pounceTarget = transform.position + transform.forward * pounceDistance;
            timer = waitTime;
            lionState = LionState.Wait;
        }
    }
    void Pounce()
    {
        particle2.SetActive(false);
        particle3.SetActive(true);
        transform.position = Vector3.MoveTowards(transform.position, pounceTarget, Time.deltaTime * 40.0f);
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = followTime - difficulty;
            lionState = LionState.Follow;
        }


        RaycastHit hit;                                                                     //raycast for wall boundary
        if (Physics.Raycast(new Ray(transform.position, transform.forward), out hit, 5.0f)) //Raycast forward
        {
            GameObject Temp = hit.collider.gameObject;
            if (Temp.layer == LayerMask.NameToLayer("Wall"))                                //Check if hitting wall Layer
            {
                pounceTarget = transform.position;                                          //Stop moving
                                                                                            //timer = followTime;
                                                                                            //state = 1;
            }
        }
    }

    void Roar()
    {
        timer -= Time.deltaTime;

        if (timer < 2.0f && roar1Lock == false && difficulty > 2)
        {
            RoarSpawn(80);
            roar1Lock = true;
        }
        if (timer < 1.7f && roar2Lock == false && difficulty > 1)
        {
            RoarSpawn(40);
            roar2Lock = true;
        }
        if (timer < 1.4f && roar3Lock == false)
        {
            RoarSpawn(0);
            roar3Lock = true;
        }
        if (timer < 1.1f && roar4Lock == false && difficulty > 1)
        {
            RoarSpawn(-40);
            roar4Lock = true;
        }
        if (timer < 0.8f && roar5Lock == false && difficulty > 2)
        {
            RoarSpawn(-80);
            roar5Lock = true;
        }
        if (timer < 0)
        {
            roar1Lock = false;
            roar2Lock = false;
            roar3Lock = false;
            roar4Lock = false;
            roar5Lock = false;
            timer = followTime - difficulty;
            lionState = LionState.Follow;
        }

    }

    void RoarSpawn(float roarAngle)
    {
        var newObject = Instantiate(roarObject);                                                            //Create Object
        newObject.transform.position = transform.position + new Vector3(0, -1, 0);                          //Location
        //newObject.transform.localScale = Vector3.one;                                                       //Scale        
        newObject.transform.eulerAngles = new Vector3(0, transform.rotation.eulerAngles.y + roarAngle, 0);  //Rotation
    }
    void Wait()
    {
        particle1.SetActive(false);
        particle2.SetActive(true);
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = pounceTime;
            lionState = LionState.Pounce;
        }
    }
    void Defeat()
    {
        playerController.playerState = PlayerState.Autowalk;
        playerController.AutoWalkDestination = new Vector3(30, 0, 30);  //Autowalk destination
        lionState = LionState.Falling;
    }

    void Falling()
    {
        transform.position = transform.position - Vector3.up * Time.deltaTime * 2.0f;
        //Add in end condition
    }

    void Difficulty()
    {
        if(stage == 1)
        {
            if(playerController.health == 1)
            {
                difficulty = 1;
            }
            else
            {
                difficulty = 2;
            }
        }
        else
        {
            if (playerController.health == 1)
            {
                difficulty = 2;
            }
            else
            {
                difficulty = 3;
            }
        }
    }
    public void collapse(float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(Pos, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.layer == LayerMask.NameToLayer("Floor"))
            {
                hitCollider.transform.GetComponent<floorScript>().fall = true;
                EndDialogue.OnEventCheck();
                EndDialogue.OnEvent = false;

                
            }

        }

    }
    public IEnumerator WaitForX(int x)
    {
        yield return new WaitForSeconds(x);
        
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + transform.forward * 2f, new Vector3(2.5f, 2.0f, 2.5f));    //Draw for debugging player hit

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(2.0f, 2.0f, 2.0f));                             //Draw for debugging column hit
    }
}
