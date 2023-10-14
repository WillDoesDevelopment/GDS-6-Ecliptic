using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public DialogueTrigger EndDialogue;
    public DoorScript DS;
    public VFXCircleHandler vfx;
    public GameObject WinConditionSND;
    public GameObject SuccessSND;
    void Start()
    {
        vfx.circleVFXStart();
    }

    // Update is called once per frame
    void Update()
    {
        if(EndDialogue.dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            WinConditionSND.SetActive(true);
            DS.DS.IsOpen = true;
        }
    }
}
