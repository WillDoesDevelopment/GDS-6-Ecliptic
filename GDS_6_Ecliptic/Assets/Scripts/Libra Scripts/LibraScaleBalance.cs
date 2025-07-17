using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraScaleBalance : MonoBehaviour
{
    //Door Variables
    public DoorScript doorScript;
    public MeshRenderer DoorMeshRenderer;
    public Material DoorOpenMat;

    //Permanent loadings
    public float deadLoadLeft;
    public float deadLoadRight;

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

    //Scale behaviour parameters
    public float successAngleDeg = 0f;
    public float maxAngleDeg = 20f;
    public float speed = 10f;

    
    public float targetAngleDeg;
    public float CurrentRotZ;
    public float WeightVal = 0;

    bool repeatLock = false;

    void Start()
    {
        CurrentRotZ = transform.localEulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if(repeatLock == false)
        {
            SumWeight();
            SetTarget();
            MoveScale();
        }        
    }

    void SumWeight()
    {
        WeightVal = -deadLoadLeft + deadLoadRight;

        // populates our collider arrays
        weightsLeft = Physics.OverlapSphere(panLeftTransform.position, OverlapSphereRange, weightLayer);
        weightsRight = Physics.OverlapSphere(panRightTransform.position, OverlapSphereRange, weightLayer);

        // this checks that the weight is colliding near the scale and that the object is not currently being held, if this criterea is met we add the wieght to WeightVal
        foreach (Collider c in weightsLeft)
        {
            Weight2 weight = c.GetComponent<Weight2>();
            if (weight != null && weight.isColliding == true && c.gameObject != playerGrapple.target)
            {
                WeightVal -= (int)weight.itemWeight;
            }
        }

        foreach (Collider c in weightsRight)
        {
            Weight2 weight = c.GetComponent<Weight2>();
            if (weight != null && weight.isColliding == true && c.gameObject != playerGrapple.target)
            {
                WeightVal += (int)weight.itemWeight;
            }
        }
    }

    void SetTarget()
    { 
     if(WeightVal == 0)
        {
            targetAngleDeg = successAngleDeg;
        }
        else
        {
            targetAngleDeg = -Mathf.Sign(WeightVal) * maxAngleDeg;
        }
    }

    void MoveScale()
    {        
        CurrentRotZ = Mathf.MoveTowardsAngle(CurrentRotZ, targetAngleDeg, speed * Time.deltaTime);
        transform.localEulerAngles = new Vector3(0, 0, CurrentRotZ);        

        if(transform.localEulerAngles.z == targetAngleDeg)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        doorScript.DS.IsOpen = true;
        DoorMeshRenderer.material = DoorOpenMat;
        repeatLock = true;
    }
 
}
