using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple3 : MonoBehaviour
{

    public GameObject target;
    public GameObject rope;
    public GameObject scope;
    public GameObject head;
    public GameObject body;
    public GameObject pointObject;
    public float length = 8f;
    public float fireSpeed = 10f;
    public float t = 1;
    public float ropeWidth = 0.15f;
    public float maxLength = 10f;
    float castLength = 0;
    bool headAttatch = false;
    public LayerMask grappleLayer;

    Transform playerPoint;
    Vector3 grapplePoint;
    Vector3 playerXZ;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rope.SetActive(false);        
        scope.SetActive(false);
        head.SetActive(false);
        body.SetActive(false);

        rend = rope.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

        //Cast
        if (target == null)
        {
            if (Input.GetMouseButton(0) ^ Input.GetKey(KeyCode.JoystickButton1) ^ Input.GetKey(KeyCode.Return))    //Draw indicator
            {
                scope.SetActive(true);
                scope.transform.position = (transform.position + transform.forward * maxLength / 2.0f);
                scope.transform.LookAt(transform.position);
            }
            else
            {
                scope.SetActive(false);
            }


            if (Input.GetMouseButtonUp(0) ^ Input.GetKeyUp(KeyCode.JoystickButton1) ^ Input.GetKeyUp(KeyCode.Return))  //Cast on release
            {
                var rayPos = transform.position;// - transform.forward;
                RaycastHit hit;
                Debug.DrawLine(rayPos, rayPos + transform.forward * maxLength, Color.blue, 0.5f);                   //Draw debug line for cast

                if (Physics.Raycast(new Ray(rayPos, transform.forward), out hit, maxLength, grappleLayer))         //Raycast forward
                {
                    Debug.DrawLine(hit.point, hit.point + transform.up * 2f, Color.green, 0.5f);                    //Draw debug line on hit
                    target = hit.transform.gameObject;                                                              //Set target object                    
                    pointObject.transform.position = hit.point;
                    pointObject.transform.parent = target.transform;
                    //rope.SetActive(true);
                    head.SetActive(true);
                    body.SetActive(true);
                }

            }

            

        }



        //Release
        if (Input.GetMouseButtonDown(1) ^ Input.GetKeyDown(KeyCode.JoystickButton0) ^ Input.GetKeyDown(KeyCode.RightShift))
        {
            RopeBreak();
        }





        //Object follow

        if (target != null)
        {
            playerPoint = transform;
            grapplePoint = pointObject.transform.position;
            length = Vector3.Distance(playerPoint.position, grapplePoint);

            //Target
            playerXZ = new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
            t = length / maxLength;                                                             // % of max length extended


            //Throw animation
            if(headAttatch == false)
            {
                head.transform.position = Vector3.Lerp(playerPoint.position, grapplePoint, (castLength / length) - 0.01f);
                head.transform.LookAt(grapplePoint);

                body.transform.position = Vector3.Lerp(playerPoint.position, grapplePoint, (castLength / length / 2f) - 0.01f); ;
                body.transform.localScale = new Vector3(1f, 1f, castLength);
                body.transform.LookAt(grapplePoint);

                castLength += Time.deltaTime * fireSpeed;                

                if (castLength > length)
                {
                    castLength = length;
                    headAttatch = true;
                    castLength = 0;
                }
            }
            //Move snake
            else
            {
                head.transform.position = Vector3.Lerp(playerPoint.position, grapplePoint, 0.99f);
                head.transform.LookAt(grapplePoint);

                body.transform.position = (playerPoint.position + grapplePoint) / 2.0f;
                body.transform.localScale = new Vector3(1f, 1f, length);
                body.transform.LookAt(grapplePoint);
            }
            
            
            //Force

            float f = Mathf.Clamp(t - 1f, 0, 0.2f) * 5f;                                       // force modifier when 1.0 < t < 1.2

            var forceVec = Vector3.Normalize(playerXZ - target.transform.position) * Time.deltaTime * f * 100000f;    //Calculate force            
            target.GetComponent<Rigidbody>().AddForceAtPosition(forceVec, pointObject.transform.position);            //Add force

            //Rope
            /*
            rope.transform.position = (playerPoint.position + grapplePoint) / 2.0f;
            rope.transform.localScale = new Vector3(ropeWidth, ropeWidth, length);
            rope.transform.LookAt(grapplePoint);
            

            rend.material.SetFloat("_Stretch_Amount", Mathf.Clamp(t, 0, 1));
            */

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

            if (t > 1.2f)                                                                        // Break if stretched
            {
                RopeBreak();
            }
        }




    }

    public void RopeBreak()
    {
        target = null;
        //rope.SetActive(false);        
        head.SetActive(false);
        body.SetActive(false);
        headAttatch = false;
        castLength = 0;
    }
}