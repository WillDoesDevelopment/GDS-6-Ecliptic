using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaDialogueChanger : MonoBehaviour
{
    public DialogueTrigger trigger;

    public void ChangeDialogue(Dialogue d)
    {
        trigger.dialogue = d;
    }
}
