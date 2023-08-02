using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// so you can see this in editor
//[System.Serializable]
[CreateAssetMenu(menuName = "data/dialogue")]
public class Dialogue: ScriptableObject
{
    [System.Serializable]
    public struct DialogueLine 
    { 
        public string sentence; 
        public DialogueType dialogueType;
    }


    public DialogueLine[] line;
    // all required information for the dialogue
    public enum DialogueType
    {
        OtherDialogue,
        Monologue,
        Narration
    }
    public string[] sentences;
    public DialogueType[] dialogueType;

    public string monologueName;
    public Sprite MonologueImage;

    public string name;
    public Sprite DialogueImage;

}
