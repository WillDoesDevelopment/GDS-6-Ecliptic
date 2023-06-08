using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovment : MonoBehaviour
{
    public GameObject target;
    public float lerpSpeed;

    public Vector3 offset1;
    public Vector3 offset2;

    public Vector3 Rot1;
    public Vector3 Rot2;

    public Transform RoomLeftPos;
    public Transform RoomRightPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Rot1 = this.transform.eulerAngles;
        LerpToTargets();   
    }
    public void LerpToTarget()
    {
        Vector3 targetPos = offset1 + target.transform.position;
        this.transform.position = Vector3.Lerp(this.transform.position,targetPos, lerpSpeed);
    }

    public void LerpToTargets()
    {
        

        float totalDist = RoomRightPos.position.x - RoomLeftPos.position.x;
        float playerDist = (target.transform.position.x - RoomLeftPos.position.x)/totalDist;


        Vector3 RotDelta = Rot1 * playerDist + Rot2 * Mathf.Abs(playerDist - 1);
        this.transform.eulerAngles = RotDelta;

        Vector3 offsetDelta = offset1 * playerDist + offset2 * Mathf.Abs(playerDist-1);
        Vector3 targetPos = offsetDelta + target.transform.position;
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, lerpSpeed);
    }
}
