using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovment : MonoBehaviour
{
    public GameObject target;
    public float lerpSpeed;

    // the two camera positions we smoothly move between
    public Vector3 offset1;
    public Vector3 offset2;
    public Vector3 offsetZ1;
    public Vector3 offsetZ2;

    // the two rotations we slowly go between
    public Vector3 Rot1;
    public Vector3 Rot2;
    public Vector3 RotZ1;
    public Vector3 RotZ2;

    // we need to know the position of iether end of the room so we know how much of pos 1 or pos 2 the camera should be (same as rotation)
    public Transform RoomLeftPos;
    public Transform RoomRightPos;
    public Transform RoomLeftPosZ;
    public Transform RoomRightPosZ;

    // player script
    public PlayerScript Ps;


    void Update()
    {

        LerpToTargetsV2();   
    }
    public void LerpToTarget()
    {
        Vector3 targetPos = offset1 + target.transform.position;
        this.transform.position = Vector3.Lerp(this.transform.position,targetPos, lerpSpeed);
    }

    public void LerpToTargets()
    {
        
        // the distance between each end of the room
        float totalDist = RoomRightPos.position.x - RoomLeftPos.position.x;

        // the distance of the player from the left or right
        float playerDist = (target.transform.position.x - RoomLeftPos.position.x)/totalDist;

        // calculates the change in rotation based on how far we are from the left or right of the room
        Vector3 RotDelta = Rot1 * playerDist + Rot2 * Mathf.Abs(playerDist - 1);
        this.transform.eulerAngles = RotDelta;

        // calculates positionally where the camera should be relative to the player
        Vector3 offsetDelta = offset1 * playerDist + offset2 * Mathf.Abs(playerDist-1);
        Vector3 targetPos = offsetDelta + target.transform.position;
        // this impliments the calculation and also lerps on the z axis
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, lerpSpeed);
    }
    public void LerpToTargetsV2()
    {

        // the distance between each end of the room
        float totalDistx = RoomRightPos.position.x - RoomLeftPos.position.x;
        float totalDistz = RoomRightPosZ.position.z - RoomLeftPosZ.position.z;

        // the distance of the player from the left or right
        float playerDist = (target.transform.position.x - RoomLeftPos.position.x) / totalDistx;
        float playerDistZ = (target.transform.position.z - RoomLeftPosZ.position.z) / totalDistz;

        // calculates the change in rotation based on how far we are from the left or right of the room
        Vector3 RotDelta = (Rot1 * playerDist + Rot2 * Mathf.Abs(playerDist - 1) + RotZ1 * playerDistZ+ RotZ2 * Mathf.Abs(playerDistZ - 1))/2;
        this.transform.eulerAngles = RotDelta;

        // calculates positionally where the camera should be relative to the player
        Vector3 offsetDelta = (offset1 * playerDist + offset2 * Mathf.Abs(playerDist - 1) + offsetZ1 * playerDistZ + offsetZ2 * Mathf.Abs(playerDistZ - 1))/2;
        Vector3 targetPos = offsetDelta + target.transform.position;
        // this impliments the calculation and also lerps on the z axis
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, lerpSpeed);
    }
}
