using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple4 : MonoBehaviour
{
    Ecliptic02 InputController;
    public GameObject target;
    public GameObject outline;
    Vector3 playerDelta = new Vector3(0, 0, 0);
    Vector3 playerPosPrev = new Vector3(0, 0, 0);
    bool usesGravity = false;        
    bool m_HitDetect;        
    RaycastHit m_Hit;
    public Vector3 debugDistance; //for checking target distance

    public GameObject startPoint;    
    public GameObject scope;
    public GameObject head;
    public GameObject body;
    public GameObject pointObject;
    public float length = 8f;
    public float fireSpeed = 20f;
    public float t = 1;
    public float ropeWidth = 0.15f;
    public float maxLength = 10f;
    float scopeTimer = 0;
    float castLength = 0;
    bool casting = false;
    bool castComplete = false;
    bool retract = false;
    public LayerMask grappleLayer;
    public AudioClip[] grappleSnd;
    public AudioSource source;

    Transform playerPoint;
    Vector3 grapplePoint;
    

    public float explosionForce = 500f;
    public float explosionRadius = 5f;

    // Start is called before the first frame update
    void Start()
    {
        InputController = new Ecliptic02();
        InputController.Enable();

        playerPosPrev = transform.position;
        
        scope.SetActive(false);
        head.SetActive(false);
        body.SetActive(false);
        OutlineHide();
    }

    // Update is called once per frame
    void Update()
    {       

        if (InputController.Player.Accept.IsPressed() && target == null)                      //Press A button
        {
            DrawScope();
        }

        if (InputController.Player.Accept.WasReleasedThisFrame() && target == null)           //Release A button
        {
            GrappleHitCheck();
        }

        if (InputController.Player.Back.IsPressed() && target != null)                        //Press B button
        {
            RopeBreak();
        }
        
                
        //______v3 code_________ Gyles should double check this
        

        //Casting
        SnakeActiveFunc();

        //Retracting
        SnakeRetractCheck();

        //Grapple Follow
        GrappleFollowCheck();

        playerPosPrev = transform.position;
    }

    void SetupTarget()
    {
        if(target.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
        {
            usesGravity = rigidbody.useGravity;
            rigidbody.useGravity = false;
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
        target.transform.position += new Vector3(0, 0.4f, 0);       //move target up slighlty (Change if needed)
    }

    void ReleaseTarget()
    {
        if (target.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
        {
            rigidbody.useGravity = usesGravity;
            rigidbody.constraints = RigidbodyConstraints.None;
        }        
        target = null;
    }

    void MoveTarget()
    {
        playerDelta = transform.position - playerPosPrev;                       //Change in player position
        
        if (playerDelta != Vector3.zero)                                        //If player moved
        {
                                                                                //Boxcast with scale of target
            m_HitDetect = Physics.BoxCast(target.transform.position, target.transform.localScale * 0.5f, playerDelta, out m_Hit, target.transform.rotation, Vector3.Magnitude(playerDelta));
            if (m_HitDetect)
            {
                
                Debug.Log("Hit : " + m_Hit.collider.name);
                Debug.Log("Hit at " + Vector3.Angle(-playerDelta, m_Hit.normal) + " degrees");

                float incidence = Vector3.Angle(-playerDelta, m_Hit.normal);     //Check if target hit an object at enough angle to change holding location
                if (incidence > 20)
                {
                    Vector3 direction = Vector3.Normalize(Vector3.ProjectOnPlane(playerDelta, m_Hit.normal));

                    float depth = Vector3.Magnitude(playerDelta * (1 - m_Hit.distance));
                    var targetPenLength = depth / Mathf.Cos(Mathf.Deg2Rad * (90 - incidence));

                    target.transform.position += direction * targetPenLength;            //Move target along collided surface
                }

                target.transform.position += m_Hit.distance * playerDelta;      //Move target remaining straight distance
            }
            else
            {
                target.transform.position += playerDelta;                       //Move target with player
            }
        }

        debugDistance = target.transform.position - transform.position;
    }

    void OutlineCreate()
    {
        outline.SetActive(true);
        outline.transform.parent = target.transform;
        outline.GetComponent<MeshFilter>().mesh = target.GetComponent<MeshFilter>().mesh;
        outline.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        outline.transform.localPosition = Vector3.zero;
        outline.transform.localRotation = Quaternion.identity;
    }

    void OutlineHide()
    {
        outline.SetActive(false);
    }




    public void DrawScope()
    {
        if (target == null && casting == false)    //Draw indicator
        {
            scope.SetActive(true);
            scope.transform.position = transform.position;
            scope.transform.rotation = transform.rotation;

            scopeTimer += Time.deltaTime;

            for (int i = 0; i < scope.transform.childCount; i++)                                                //Display arrows sequentially
            {
                scope.transform.GetChild(i).gameObject.SetActive(scopeTimer * 20f > i);
            }
        }        
    }

    public void GrappleHitCheck()
    {
        scope.SetActive(false);
        scopeTimer = 0;

        //Cast on release
        
        casting = true;
        head.SetActive(true);
        body.SetActive(true);
        var rayPos = transform.position;
        RaycastHit hit;
        

        if (Physics.Raycast(new Ray(rayPos, transform.forward), out hit, maxLength))                        //Raycast forward
        {
            if (grappleLayer == (grappleLayer | (1 << hit.transform.gameObject.layer)))
            {
                //Succesfully hit grappleable object
                Debug.Log("grapple object hit");                
                target = hit.transform.gameObject;                                                              //Set target object                    
                pointObject.transform.position = hit.point;
                pointObject.transform.parent = target.transform;
                if (target.GetComponent<BreakObject>() == null && target.GetComponent<columnScript>() == null)  //Check for physics based targets
                {
                    OutlineCreate();
                    SetupTarget();
                }
                
            }
            else
            {
                //Hit other object
                Debug.Log("other object hit");
                pointObject.transform.position = hit.point;
            }
        }
        else
        {
            //Hit no object
            Debug.Log("no object hit");
            pointObject.transform.position = rayPos + transform.forward * maxLength;
        }

        
    }

    public void SnakeActiveFunc()
    {
        playerPoint = startPoint.transform;
        grapplePoint = pointObject.transform.position;
        length = Vector3.Distance(transform.position, grapplePoint); //Can change to hand location (playerPoint.position) if needed
        if (casting == true)
        {

            if (castComplete == false)
            {
                //Stretch out
                head.transform.position = Vector3.Lerp(playerPoint.position, grapplePoint, (castLength / length) - 0.01f);
                head.transform.LookAt(grapplePoint);

                body.transform.position = Vector3.Lerp(playerPoint.position, grapplePoint, (castLength / length / 2f) - 0.01f); ;
                body.transform.localScale = new Vector3(1f, 1f, castLength);
                body.transform.LookAt(grapplePoint);

                castLength += Time.deltaTime * fireSpeed;

                if (castLength > length)
                {
                    source.clip = grappleSnd[0];
                    source.Play();
                    castLength = length;
                    castComplete = true;
                }
            }
            else if (target == null)
            {
                //Return
                head.transform.position = Vector3.Lerp(playerPoint.position, grapplePoint, (castLength / length) - 0.01f);
                head.transform.LookAt(grapplePoint);

                body.transform.position = Vector3.Lerp(playerPoint.position, grapplePoint, (castLength / length / 2f) - 0.01f); ;
                body.transform.localScale = new Vector3(1f, 1f, castLength);
                body.transform.LookAt(grapplePoint);

                castLength -= Time.deltaTime * fireSpeed;

                if (castLength < 0f)
                {
                    casting = false;
                    castComplete = false;
                    castLength = 0;
                    head.SetActive(false);
                    body.SetActive(false);
                }
            }
            else
            {
                t = length / maxLength;
                if (t > 1.2)
                {
                    RopeBreak();//break rope
                }

                //Check if target is breakable
                else if (target.GetComponent<BreakObject>() != null)
                {
                    SmashInteraction(); //break object
                }

                //Check if target is column
                else if (target.GetComponent<columnScript>())
                {
                    if(t>1) //Break column if stretched
                    {
                        CollumnInteraction();
                    }                    
                }
                else //Move target
                {
                    MoveTarget();
                }
                
                
            }
        }
    }

    public void SnakeRetractCheck()
    {
        if (retract)
        {
            head.transform.position = Vector3.Lerp(playerPoint.position, grapplePoint, (castLength / length) - 0.01f);
            head.transform.LookAt(grapplePoint);

            body.transform.position = Vector3.Lerp(playerPoint.position, grapplePoint, (castLength / length / 2f) - 0.01f); ;
            body.transform.localScale = new Vector3(1f, 1f, castLength);
            body.transform.LookAt(grapplePoint);

            castLength -= Time.deltaTime * fireSpeed;

            if (castLength < 0f)
            {
                //Reset grapple
                target = null;
                retract = false;
                casting = false;
                castComplete = false;
                t = 0;
                castLength = 0;
                head.SetActive(false);
                body.SetActive(false);
            }
        }
    }

    public void GrappleFollowCheck()
    {
        if (target != null && castComplete == true)
        {
            head.transform.position = Vector3.Lerp(playerPoint.position, grapplePoint, 0.99f);
            head.transform.LookAt(grapplePoint);

            body.transform.position = (playerPoint.position + grapplePoint) / 2.0f;
            body.transform.localScale = new Vector3(1f, 1f, length);
            body.transform.LookAt(grapplePoint);
        }
    }

    public void CollumnInteraction()
    {
        if (t > 1)
        {
            target.GetComponent<columnScript>().fall = true;
            target.GetComponent<columnScript>().FallUpdate();
            RopeBreak();
            ExplosionForce();
            target = null;
        }
    }

    public void SmashInteraction()
    {
        target.GetComponent<BreakObject>().smash = true;
        RopeBreak();
        target = null;
    }

    public void RopeBreak()
    {
        OutlineHide();
        ReleaseTarget();
        source.clip = grappleSnd[1];
        source.Play();
        castComplete = false;
        casting = false;
        retract = true;             //Start retracting
    }

    void ExplosionForce()
    {
        Vector3 explosionPos = target.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius);
        }
    }
}
