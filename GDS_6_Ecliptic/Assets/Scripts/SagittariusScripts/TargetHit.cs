using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHit : MonoBehaviour
{

    public Grapple3 gp;
    public SagManager sagManager;
    public LayerMask targetLayer;

    private void Update()
    {
        if (gp.target == this.gameObject)
        {
            print("Target has been hit");
            gp.RopeBreak();
            GetComponent<Collider>().enabled = false;
            sagManager.HitCounter++;
        }
    }
    
}
