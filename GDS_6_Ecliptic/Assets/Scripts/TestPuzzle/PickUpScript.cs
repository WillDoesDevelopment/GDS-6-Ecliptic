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

    public LayerMask PickUpLayer;

    public bool Raycasting  = false;
    public bool ProximityPickUp = false;

    public AudioSource PickUpSnd;

    public Collider[] ObjColliders;
    //private  Collider[] ObjColliders;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(holding == false)
        {
            HoldingObj = PickUpPos.gameObject;
        }
        // on click we pick up the in range object
        if (ProximityPickUp)
        {
            PickUpInProximity();
        }
        if (Raycasting)
        {
            Raycast();
        }
    }
    public void Raycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            // syntax i found for raycasting from camera to mouse pos
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 3000, PickUpLayer))
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

    public void PickUpInProximity()
    {
        ObjColliders = Physics.OverlapSphere(this.transform.position, PickUpRadius);
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Submit"))
        {
            Debug.Log(HoldingObj);
            if (nearestPickUp() != null && nearestPickUp().CompareTag("PickUp") && holding == false)
            {
                PickUpSnd.Play();
                Debug.Log("running");
                HoldingObj = nearestPickUp();
                PickUp(nearestPickUp());
                holding = true;
            }
            else if (holding == true)
            {
                //PutDown(HoldingObj);
            }
            
        }
    }
    // we check if it is withing proximity using the helper function, we then set the object to out holding pos and parent it, we check for an RB and then freeze all constraints
    public void PickUp(GameObject pickUpObj)
    {
        //bool ProxBool = ;
        pickUpObj.transform.position = PickUpPos.transform.position;
        pickUpObj.transform.SetParent(PickUpPos.transform);

        if(pickUpObj.GetComponent<Rigidbody>() != null)
        {
            Rigidbody RBTemp = pickUpObj.GetComponent<Rigidbody>();
            if (RBTemp != null)
            {
                RBTemp.constraints = RigidbodyConstraints.FreezeAll;
                //RBTemp.constraints = RigidbodyConstraints.FreezeRotation;
                //RBTemp.useGravity = false;
            }

        }
        
    }

    public float Dist(GameObject player, GameObject other)
    {
        return Vector3.Distance(player.transform.position, other.transform.position);
    }
    public GameObject nearestPickUp()
    {
        GameObject nearest = null;

        foreach (Collider c in ObjColliders)
        {
            //Debug.Log(nearest);
            if(c.CompareTag("PickUp"))
            {
                if (nearest == null )
                {
                    nearest = c.gameObject;
                }
                else if (Dist(this.gameObject, c.gameObject) < Dist(this.gameObject, nearest))
                {
                    Debug.Log("is this distance " + Dist(this.gameObject, c.gameObject) + "smalller than this distance" + Dist(this.gameObject, nearest));
                    nearest = c.gameObject;
                    Debug.Log(nearest + "the closest OBJ");
                    //Debug.Log(nearest);
                }
            }
        }
        //Debug.Log(nearest);
        return nearest;
    }

    // here we reverse what we did in the pickUP function
    public void PutDown(GameObject HoldingObj)
    {
        HoldingObj = PickUpPos.gameObject;
        Debug.Log(HoldingObj);
        PickUpPos.transform.DetachChildren();
        holding = false;
        
        Rigidbody RBTemp = HoldingObj.GetComponent<Rigidbody>();
        if (RBTemp != null)
        {
            //Debug.Log("working");
            RBTemp.constraints  = RigidbodyConstraints.None;
        }
    }
}
