using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaurusStage2 : TaurusStage
{


    public override void VReset(GameObject Player) 
    {
        Player.transform.position = PlayerResetPos;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetResetPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
