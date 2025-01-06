using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaurusStage1 : TaurusStage

{
    
    public override void VReset(GameObject Player)
    {
        if(Vector3.Distance(Player.transform.position, bull.transform.position) < 1f)
        {
            Player.transform.position = PlayerResetPos;
                
        }
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
