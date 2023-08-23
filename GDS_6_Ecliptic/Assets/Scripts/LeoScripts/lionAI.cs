using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lionAI : MonoBehaviour
{
    public HubManager hubManager;
    public RoomManager RM;
    public GameObject Player;
    public GameObject ColumnParent;
    public GameObject particle1;
    public GameObject particle2;
    public GameObject particle3;
    public GameObject barrier;
    public float barrierMaxScale = 20;
    public float speed = 1;
    float inv = 1;
    float angle = 0;
    Vector3 forward;
    Vector3 target;
    Vector3 pounceTarget;
    float rayDist = 10;
    public int state = 2;
    public float rotationSpeed = 4;
    float timer = 10;
    public float followTime = 10f;
    public float chargeTime = 3f;
    public float waitTime = 2f;
    public float pounceTime = 2.5f;
    public float pounceDistance = 14f;

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
        forward = transform.forward;
        target = Player.transform.position;
        particle1.SetActive(false);
        particle2.SetActive(false);
        particle3.SetActive(false);
        barrier.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(state == 1)          //Following
        {
            particle3.SetActive(false);
            rayDist = Vector3.Distance(transform.position, Player.transform.position) - 3.0f;
            var rayPos = transform.position;
            RaycastHit hit;

            forward = Vector3.Normalize(Vector3.Scale(Player.transform.position - transform.position, new Vector3(1, 0, 1)));
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
            if(timer < 0) 
            {
                timer = chargeTime;
                state = 2; 
            }
        }

        if(state == 2)          //Charging
        {
            particle1.SetActive(true);
            forward = Vector3.Normalize(Vector3.Scale(Player.transform.position - transform.position , new Vector3(1,0,1)));            
            
            Quaternion targetRotation = Quaternion.LookRotation(forward, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            timer -= Time.deltaTime;
            if (timer < 0)
            {
                pounceTarget = transform.position + transform.forward * pounceDistance;
                timer = waitTime;
                state = 3;
            }
        }

        if (state == 3)         //Waiting
        {
            particle1.SetActive(false);
            particle2.SetActive(true);
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = pounceTime;
                state = 4;
            }
        }

        if (state == 4)         //Pouncing
        {
            particle2.SetActive(false);
            particle3.SetActive(true);
            transform.position = Vector3.MoveTowards(transform.position, pounceTarget, Time.deltaTime * 40.0f);
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = followTime;
                state = 1;
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

        if(ColumnParent.transform.childCount == 0 && state != 5)
        {
            x = Mathf.Floor(transform.position.x + 0.5f);
            y = transform.position.y;
            z = Mathf.Floor(transform.position.z + 0.5f);
            Pos = new Vector3(x, y, z);
            state = 5;
        }

        if (state == 5)     //Collapse
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

            transform.position = transform.position - Vector3.up * Time.deltaTime * 2.0f;            
            Destroy(gameObject, 4.0f);
        }

        //Collision    
        Collider[] hitColliders1 = Physics.OverlapBox(transform.position + transform.forward * 2f, new Vector3(2.5f, 2.0f, 2.5f)); //Hitbox for player
        foreach (var hitCollider in hitColliders1)
        {            
            if (hitCollider.gameObject == Player)                                           //Room restarts
            {
                //RM.Reset();
                Debug.Log("Restart room");                                                  //Need restart function here.....................................
            }
        }
        Collider[] hitColliders2 = Physics.OverlapBox(transform.position, new Vector3(1.2f, 2.0f, 1.2f)); //Hitbox for columns. Hitboxes need rotation.........
        foreach (var hitCollider in hitColliders2)
        {
            if (hitCollider.gameObject.layer == LayerMask.NameToLayer("Column"))            //collumn falls
            {                      
                hitCollider.gameObject.GetComponent<columnScript>().fall = true;
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
                hitCollider.transform.GetComponent<floorScript>().solid = false;
                StartCoroutine(WaitForX(3));
            }

        }

    }
    public IEnumerator WaitForX(int x)
    {
        yield return new WaitForSeconds(x);
        hubManager.SendToHub();
        hubManager.SetGameStage(3);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + transform.forward * 2f, new Vector3(2.5f, 2.0f, 2.5f));    //Draw for debugging

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(1.2f, 2.0f, 1.2f));                             //Draw for debugging
    }
}
