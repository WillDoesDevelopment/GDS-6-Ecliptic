using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraScaleBalance : MonoBehaviour
{
    //Door Variables
    public DoorScript doorScript;
    public MeshRenderer DoorMeshRenderer;
    public Material DoorOpenMat;
    public Gondola gondolaScript;

    //Permanent loadings
    public float deadLoadLeft;
    public float deadLoadRight;

    //Total load multiplier
    public float loadMultiplierLeft = 1f;
    public float loadMultiplierRight = 1f;

    // Physics.SphereOverlap creates an array of colliders and this is where we store them
    Collider[] weightsLeft;
    Collider[] weightsRight;

    // the dimensions of the SphereOverlap
    public float OverlapSphereRange;
    //public float OverlapSphereHeight;

    //Location to check for weights
    public Transform panLeftTransform;
    public Transform panRightTransform;

    //Weight layermask
    public LayerMask weightLayer;

    //Set to current version of grapple!!!!!!!!!!!!!!!!!!!!!!!!!!!! This is probably not ideal sorry- Gyles
    public Grapple4 playerGrapple;


    public float targetAngleDeg = 0f;
    public float CurrentRotZ;
    public float WeightVal = 0;
    
    //Keep track of previous angle
    bool prevAngleSide = false;
    bool thisAngleSide = false;

    //Situation checks
    public bool passAngle = false;
    public bool weightEqual = false;

    bool repeatLock = false;

    void Start()
    {
        CurrentRotZ = transform.localEulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (repeatLock == false)
        {            
            CheckAngle();
            SumWeight();

            if(passAngle && weightEqual)
            {
                OpenDoor();
            }
        }
    }

    void CheckAngle()
    {
        CurrentRotZ = transform.localEulerAngles.z;

        if (targetAngleDeg > CurrentRotZ)
        {
            CurrentRotZ += 360f;
        }

        thisAngleSide = targetAngleDeg + 180 > CurrentRotZ;
        passAngle = (thisAngleSide != prevAngleSide);        

        prevAngleSide = targetAngleDeg + 180 > CurrentRotZ;

    }

    void SumWeight()
    {
        WeightVal = -deadLoadLeft * loadMultiplierLeft + deadLoadRight * loadMultiplierRight;

        // populates our collider arrays
        weightsLeft = Physics.OverlapSphere(panLeftTransform.position, OverlapSphereRange, weightLayer);
        weightsRight = Physics.OverlapSphere(panRightTransform.position, OverlapSphereRange, weightLayer);

        // this checks that the weight is colliding near the scale and that the object is not currently being held, if this criterea is met we add the wieght to WeightVal
        foreach (Collider c in weightsLeft)
        {
            Weight2 weight = c.GetComponent<Weight2>();
            if (weight != null && weight.isColliding == true && c.gameObject != playerGrapple.target)
            {
                WeightVal -= (int)weight.itemWeight * loadMultiplierLeft;
            }
        }

        foreach (Collider c in weightsRight)
        {
            Weight2 weight = c.GetComponent<Weight2>();
            if (weight != null && weight.isColliding == true && c.gameObject != playerGrapple.target)
            {
                WeightVal += (int)weight.itemWeight * loadMultiplierRight;
            }
        }

        weightEqual = (WeightVal == 0);
        
    }

    void OpenDoor()
    {
        //Door
        Debug.Log("Open Door: " + doorScript.transform.gameObject);
        doorScript.DS.IsOpen = true;
        DoorMeshRenderer.material = DoorOpenMat;

        //Gondola
        gondolaScript.UnlockGondola();

        //Scale
        transform.localEulerAngles = new Vector3(0, 0, targetAngleDeg);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        repeatLock = true;
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere representing the sphere collider
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(panLeftTransform.position, OverlapSphereRange);
        Gizmos.DrawWireSphere(panRightTransform.position, OverlapSphereRange);
    }
}
