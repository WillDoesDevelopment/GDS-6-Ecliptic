using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SagManager : MonoBehaviour
{

    public VFXCircleHandler VFX;
    public int HitCounter = 0;
    public DialogueTrigger dt;
    public DoorScript Door;

    // Update is called once per frame
    void Update()
    {
        if(HitCounter >= 2)
        {
            VFX.circleVFXStart();

            if(dt.dialogue.DialogueMode == Dialogue.DialogueState.Finished)
            {
                Door.DS.IsOpen = true;
            }
        }
    }
}
