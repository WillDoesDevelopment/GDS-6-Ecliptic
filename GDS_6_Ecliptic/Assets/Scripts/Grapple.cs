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

    // Start is called before the first frame update
    void Start()
    {
        rope.SetActive(false);
        scope.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        //Cast
        if(target == null)
        {
            if (Input.GetMouseButton(0))    //Draw indicator
            {
                scope.SetActive(true);
                scope.transform.position = (transform.position + transform.forward * maxLength/2.0f);                
                scope.transform.LookAt(transform.position);
            }
            else
            {
                scope.SetActive(false);
            }


            if (Input.GetMouseButtonUp(0))  //Cast on release
            {
                var rayPos = transform.position;
                RaycastHit hit;
                Debug.DrawLine(rayPos, rayPos + transform.forward * maxLength, Color.blue, 0.5f);                   //Draw debug line for cast

                if (Physics.SphereCast(new Ray(rayPos, transform.forward), 2.0f, out hit, maxLength - 2.0f, grappleLayer)) //Raycast forward
                {
                    Debug.DrawLine(hit.point, hit.point + transform.up * 2f, Color.green, 0.5f);                    //Draw debug line on hit
                    target = hit.transform.gameObject;                                                              //Set target object
                    rope.SetActive(true);
                }

            }

        }

        

        //Release
        if (Input.GetMouseButtonDown(1))
        {
            target = null;
            rope.SetActive(false);
        }





        //Object follow

        if (target != null)
        {
            playerXZ = new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
            t = length / Vector3.Distance(target.transform.position, transform.position);
            if (t < 1)
            {
                //target.transform.LookAt(playerXZ);            
            }
            target.transform.position = Vector3.Lerp(playerXZ, target.transform.position, t);

            //Rope
            playerPoint = transform;
            grapplePoint = target.transform;

            rope.transform.position = (playerPoint.position + grapplePoint.position) / 2.0f;
            rope.transform.localScale = new Vector3(ropeWidth, ropeWidth, Vector3.Distance(playerPoint.position, grapplePoint.position));
            rope.transform.LookAt(grapplePoint);

            if (target.GetComponent<columnScript>() != null)
            {
                Debug.Log("almost");

                if (t<1)
                {
                    target.GetComponent<columnScript>().fall = true;
                    target = null;
                    Debug.Log("fall");
                    rope.SetActive(false);
                }
            }
        }

            

    }
}
