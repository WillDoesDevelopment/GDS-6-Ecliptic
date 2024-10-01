using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class OrbMovementAries : MonoBehaviour
{
    public Animator OrbAnim;
    public Animator MoveAnim;
    public GameObject RM;

    void Start()
    {

    }

    public void OrbIdle()
    {
        OrbAnim.GetComponent<Animator>().SetTrigger("Idle");
    }

    public void OrbFinish()
    {
        OrbAnim.GetComponent<Animator>().SetTrigger("Idle");
        RM.GetComponent<RoomManager>().isOrbed = true;
    }

    public void OrbMove()
    {
        OrbAnim.GetComponent<Animator>().SetTrigger("Move");
        MoveAnim.GetComponent<Animator>().SetTrigger("orbMoving");
    }

}
