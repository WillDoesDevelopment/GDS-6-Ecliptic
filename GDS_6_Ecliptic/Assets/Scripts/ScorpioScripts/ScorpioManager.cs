using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public DialogueTrigger EndDialogue;
    public DoorScript DS;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EndDialogue.dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            DS.DS.IsOpen = true;
        }
    }
}