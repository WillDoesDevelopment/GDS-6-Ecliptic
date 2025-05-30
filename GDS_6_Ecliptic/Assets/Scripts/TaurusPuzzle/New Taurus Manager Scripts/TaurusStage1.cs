using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaurusStage1 : TaurusStage
{
    public override void VReset(GameObject Player)
    {
        Player.transform.position = PlayerResetPos;
    }
    void Start()
    {
        SetResetPos();
    }

    // Update is called once per frame
    void Update()
    {
        Barrier();
    }
}
