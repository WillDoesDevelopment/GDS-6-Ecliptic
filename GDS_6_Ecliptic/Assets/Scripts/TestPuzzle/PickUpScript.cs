using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public float PickUpRadius;
    public GameObject PickUpPos;

    public GameObject HoldingObj;
    public bool holding = false;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

                if (hit.transform.CompareTag("PickUp") && holding == false)
                {
                    HoldingObj = hit.transform.gameObject;
                    PickUp(HoldingObj);
                    holding = true;
                }
                else if (holding == true && HoldingObj == hit.transform.gameObject)
                {
                    PutDown(HoldingObj);
                    holding = false;
                }
                
            }
        }
    }
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

    public void PutDown(GameObject HoldingObj)
    {
        PickUpPos.transform.DetachChildren();

        Rigidbody RBTemp = HoldingObj.GetComponent<Rigidbody>();
        if (RBTemp != null)
        {
            Debug.Log("working");
            RBTemp.constraints  = RigidbodyConstraints.None;
            //HoldingObj = null;
        }
    }
}
