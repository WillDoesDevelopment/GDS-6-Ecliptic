using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// so you can see this in editor
//[System.Serializable]
[CreateAssetMenu(menuName = "data/dialogue")]
public class Dialogue: ScriptableObject
{
    // setting mode to not started
    public Dialogue()
    {
        DialogueMode = DialogueState.NotStarted;
        //formatDialogueLine()
    }


    [System.Serializable]
    public struct DialogueLine 
    { 
        // all the variables per line of dialogue
        public string sentence; 
        public DialogueType dialogueType;
        public AudioClip AS;
        public int OrderAndChaosVal;
        
        //public int OtherSpeaker;
    }

    // this is the origonal dialogue list
    public DialogueLine[] line; 
    public void formatDialogueLine(ref DialogueLine[] DL)
    {
        for (int i = 0; i<DL.Length; i++)
        {
            for(int s = 0; s < i; s++)
            {
                string sentence = DL[i].sentence;
                DL[i].sentence = " " + sentence;

            }
        }
    }

    // all required information for the dialogue
    public enum DialogueType
    {
        OtherDialogue,
        Monologue,
        Narration
    }
    //public string[] sentences;
    //public DialogueType[] dialogueType;

    public string monologueName;
    public Sprite MonologueImage;

    public string name;
    public Sprite DialogueImage;

    public enum DialogueState
    {
        NotStarted,
        InProgress,
        Finished
    }
    //[System.NonSerialized]
    public DialogueState DialogueMode = DialogueState.NotStarted;

    private void OnEnable()
    {
        DialogueMode = DialogueState.NotStarted;
    }
}
