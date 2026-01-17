using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    public GameObject target;
    public GameObject rope;
    public GameObject scope;
    public float length = 10f;
    public float t = 1;
    public float ropeWidth = 1;
    public float maxLength = 10f;    
    public LayerMask grappleLayer;

    Transform playerPoint;
    Transform grapplePoint;
    Vector3 playerXZ;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rope.SetActive(false);
        scope.SetActive(false);

        rend = rope.GetComponent<Renderer>();                
    }

    // Update is called once per frame
    void Update()
    {
        
        //Cast
        if(target == null)
        {
            if (Input.GetMouseButton(0) ^ Input.GetKey(KeyCode.JoystickButton1) ^ Input.GetKey(KeyCode.Return))    //Draw indicator
            {
                scope.SetActive(true);
                scope.transform.position = (transform.position + transform.forward * maxLength/2.0f);                
                scope.transform.LookAt(transform.position);
            }
            else
            {
                scope.SetActive(false);
            }


            if (Input.GetMouseButtonUp(0) ^ Input.GetKeyUp(KeyCode.JoystickButton1) ^ Input.GetKeyUp(KeyCode.Return))  //Cast on release
            {
                var rayPos = transform.position - transform.forward;
                RaycastHit hit;
                Debug.DrawLine(rayPos, rayPos + transform.forward * maxLength, Color.blue, 0.5f);                   //Draw debug line for cast

                if (Physics.SphereCast(new Ray(rayPos, transform.forward), 2.0f, out hit, maxLength - 3.0f, grappleLayer)) //Raycast forward
                {
                    Debug.DrawLine(hit.point, hit.point + transform.up * 2f, Color.green, 0.5f);                    //Draw debug line on hit
                    target = hit.transform.gameObject;                                                              //Set target object
                    rope.SetActive(true);
                }

            }

        }

        

        //Release
        if (Input.GetMouseButtonDown(1) ^ Input.GetKeyDown(KeyCode.JoystickButton0) ^ Input.GetKeyDown(KeyCode.RightShift))
        {
            target = null;
            rope.SetActive(false);
        }
        
        

        

        //Object follow

        if (target != null)
        {
            playerPoint = transform;
            grapplePoint = target.transform;
            length = Vector3.Distance(playerPoint.position, grapplePoint.position);

            //Target
            playerXZ = new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
            t = maxLength / length;
            if (t < 1)
            {
                //target.transform.LookAt(playerXZ);            
            }
            target.transform.position = Vector3.Lerp(playerXZ, target.transform.position, t);

            //Rope
            rope.transform.position = (playerPoint.position + grapplePoint.position) / 2.0f;
            rope.transform.localScale = new Vector3(ropeWidth, ropeWidth, length);
            rope.transform.LookAt(grapplePoint);


            rend.material.SetFloat("_Stretch_Amount", length / maxLength);


            //column interaction
            if (target.GetComponent<columnScript>() != null)
            {
                //Debug.Log("almost");

                if (t<1)
                {
                    target.GetComponent<columnScript>().fall = true;
                    target = null;
                    //Debug.Log("fall");
                    rope.SetActive(false);
                }
            }
        }

            

    }
}
