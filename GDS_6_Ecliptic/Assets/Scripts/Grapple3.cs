using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple3 : MonoBehaviour
{

    public GameObject target;
    //public GameObject rope;
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

    Transform playerPoint;
    Vector3 grapplePoint;
    Vector3 playerXZ;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        //rope.SetActive(false);        
        scope.SetActive(false);
        head.SetActive(false);
        body.SetActive(false);

        //rend = rope.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

        //Cast
        if (target == null && casting == false)
        {
            if (Input.GetMouseButton(0) ^ Input.GetKey(KeyCode.JoystickButton1) ^ Input.GetKey(KeyCode.Return))    //Draw indicator
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
            else
            {
                scope.SetActive(false);
                scopeTimer = 0;
            }


            if (Input.GetMouseButtonUp(0) ^ Input.GetKeyUp(KeyCode.JoystickButton1) ^ Input.GetKeyUp(KeyCode.Return))  //Cast on release
            {                
                casting = true;
                head.SetActive(true);
                body.SetActive(true);
                var rayPos = transform.position;
                RaycastHit hit;
                //Debug.DrawLine(rayPos, rayPos + transform.forward * maxLength, Color.blue, 0.5f);                   //Draw debug line for cast

                if (Physics.Raycast(new Ray(rayPos, transform.forward), out hit, maxLength))                        //Raycast forward
                {                    
                    if (grappleLayer == (grappleLayer | (1 << hit.transform.gameObject.layer)))
                    {
                        //Succesfully hit grappleable object
                        Debug.Log("grapple object hit");

                        //Debug.DrawLine(hit.point, hit.point + transform.up * 2f, Color.green, 0.5f);                    //Draw debug line on hit
                        target = hit.transform.gameObject;                                                              //Set target object                    
                        pointObject.transform.position = hit.point;
                        pointObject.transform.parent = target.transform;
                        //rope.SetActive(true);
                        
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

        }

        //Release
        if (Input.GetMouseButtonDown(1) ^ Input.GetKeyDown(KeyCode.JoystickButton0) ^ Input.GetKeyDown(KeyCode.RightShift))
        {
            RopeBreak();
        }

        //Casting
        playerPoint = transform;
        grapplePoint = pointObject.transform.position;
        length = Vector3.Distance(playerPoint.position, grapplePoint);

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
                    castLength = length;
                    castComplete = true;                    
                }
            }
            else if(target == null)
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
                //Attatch and Pull Object
                t = length / maxLength;
                AddForce();
            }
        }

        //Retracting
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

        //Grapple Follow
        if (target != null && castComplete == true)
        {
            head.transform.position = Vector3.Lerp(playerPoint.position, grapplePoint, 0.99f);
            head.transform.LookAt(grapplePoint);

            body.transform.position = (playerPoint.position + grapplePoint) / 2.0f;
            body.transform.localScale = new Vector3(1f, 1f, length);
            body.transform.LookAt(grapplePoint);
        }





        

        if (target != null)
        {
            //column interaction
            if (target.GetComponent<columnScript>() != null)
            {
                //Debug.Log("almost");

                if (t > 1)
                {
                    target.GetComponent<columnScript>().fall = true;
                    RopeBreak();                   
                }
            }

            // Break if stretched
            if (t > 1.2f)                                                                        
            {
                RopeBreak();
            }
        }
    }

    void AddForce()
    {
        //t = length / maxLength;                                                             // % of max length extended
        playerXZ = new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
        float f = Mathf.Clamp(t - 1f, 0, 0.2f) * 5f;                                       // force modifier when 1.0 < t < 1.2

        var forceVec = Vector3.Normalize(playerXZ - target.transform.position) * Time.deltaTime * f * 100000f;    //Calculate force            
        target.GetComponent<Rigidbody>().AddForceAtPosition(forceVec, pointObject.transform.position);            //Add force
    }

    public void RopeBreak()
    {        
        castComplete = false;
        casting = false;
        retract = true;             //Start retracting
    }
}
