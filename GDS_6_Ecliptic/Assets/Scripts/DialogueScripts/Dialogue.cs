using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// so you can see this in editor
[System.Serializable]
public class Dialogue 
{
    
    // all required information for the dialogue
    public enum DialogueType
    {
        OtherDialogue,
        Monologue,
        Narration
    }
    public string[] sentences;
    public DialogueType[] dialogueType;
    public string name;
    public Sprite DialogueImage;

}
