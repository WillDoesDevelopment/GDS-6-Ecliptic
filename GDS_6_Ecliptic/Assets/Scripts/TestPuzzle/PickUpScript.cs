using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    // how far can things be to be picked up
    public float PickUpRadius;

    // an empty game object that represents where we want to hold things
    public GameObject PickUpPos;

    // stores the object we are holding
    public GameObject HoldingObj;

    // allows us to check if we are holding somthing or not
    public bool holding = false;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // on click we pick up the in range object
        Raycast();
    }
    public void Raycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            // syntax i found for raycasting from camera to mouse pos
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 3000))
            {
                //checks if the object is tagged correctly and we are not holding somthing already
                if (hit.transform.CompareTag("PickUp") && holding == false)
                {
                    HoldingObj = hit.transform.gameObject;
                    PickUp(HoldingObj);
                    holding = true;
                }
                //  if the user is holding somthing and clicks on it, we call the PutDown() function
                else if (holding == true && HoldingObj == hit.transform.gameObject)
                {
                    PutDown(HoldingObj);
                    holding = false;
                    HoldingObj = PickUpPos;
                }
                
            }
        }
    }

    // we check if it is withing proximity using the helper function, we then set the object to out holding pos and parent it, we check for an RB and then freeze all constraints
    public void PickUp(GameObject pickUpObj)
    {
        //bool ProxBool = ;

        if (Proximity(pickUpObj))
        {
            pickUpObj.transform.position = PickUpPos.transform.position;
            pickUpObj.transform.SetParent(PickUpPos.transform);

            Rigidbody RBTemp = pickUpObj.GetComponent<Rigidbody>();
            if (RBTemp != null)
            {
                RBTemp.constraints = RigidbodyConstraints.FreezeAll;
                //RBTemp.constraints = RigidbodyConstraints.FreezeRotation;
                //RBTemp.useGravity = false;
            }
        }
    }

    public bool Proximity(GameObject pickUpObj)
    {
        float playerDistZ = pickUpObj.transform.position.z - transform.position.z;
        float playerDistX = pickUpObj.transform.position.x - transform.position.x;
        if (Mathf.Sqrt(Mathf.Pow(playerDistX, 2) + Mathf.Pow(playerDistZ, 2)) < PickUpRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // here we reverse what we did in the pickUP function
    public void PutDown(GameObject HoldingObj)
    {
        PickUpPos.transform.DetachChildren();

        Rigidbody RBTemp = HoldingObj.GetComponent<Rigidbody>();
        if (RBTemp != null)
        {
            //Debug.Log("working");
            RBTemp.constraints  = RigidbodyConstraints.None;
        }
    }
}
