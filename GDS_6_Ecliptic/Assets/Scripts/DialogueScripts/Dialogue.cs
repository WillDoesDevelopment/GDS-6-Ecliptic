using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// so you can see this in editor
//[System.Serializable]
[CreateAssetMenu(menuName = "data/dialogue")]
public class Dialogue: ScriptableObject
{
    public DialogueLine[] line;
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
    // This is a constructor
    public Dialogue()
    {
        DialogueMode = DialogueState.NotStarted;
        //formatDialogueLine(line);
    }
    [System.Serializable]
    public struct DialogueLine 
    { 
        // all the variables per line of dialogue
        public string sentence; 
        public DialogueType dialogueType;
        public AudioClip AS;
        //public bool newDecision;
        //public bool IsDecision;
        //public int OrderAndChaosVal;
        
        //public int OtherSpeaker;
    }
    

    // this indents all the lins in our sentence 
    public void formatDialogueLine(ref DialogueLine[] DL)
    {
       //goes through every sentence in array
        for (int i = 0; i<DL.Length; i++) 
        {
            /* 
            //since the sentences are saved to memory permanently we need to use the trim function to get rid of the blank spaces in the data each time it runs
            DL[i].sentence = DL[i].sentence.Trim();


            // counter to keep trck of amount of spaces
            int indentCounter = 0;


            //if this is our first array entry, add one space
            if (i == 0) 
            {
                //DL[i].sentence = DL[i].sentence.Trim();
                string Tempsentence = DL[i].sentence;
                DL[i].sentence = " " + Tempsentence;
            }

            // else we check the characters in the previous sentence and add a value to the indent counter 
            else if (DL[i].IsDecision)
            {
                for (int k = i; k > 0; k--)
                {
                    if (DL[k].newDecision == true)
                    {
                        //Debug.Log(k + "is new decision");
                        foreach (char c in DL[k].sentence)
                        {
                            if (c == ' ')
                            {
                                indentCounter++;
                            }

                            for (int y = 0; y <= indentCounter; y++)
                            {
                                DL[i].sentence = DL[i].sentence.Trim();
                                string sentence = DL[i].sentence;
                                DL[i].sentence = " " + sentence;
                            }

                        }
                    }
                }
            }

            else
            {
                foreach (char c in DL[i - 1].sentence)
                {
                    if (c == ' ')
                    {
                        indentCounter++;
                    }
                    else
                    {
                        //Debug.Log(indentCounter);
                        break;
                    }

                }
                for (int k = 0; k <= indentCounter; k++)
                {
                    //Debug.Log("ran" + k + "times");
                    string sentence = DL[i].sentence;
                    DL[i].sentence = " " + sentence;
                }

            }
            
           *//* for(int s = 0; s < i; s++)
            {
                string Tempsentence = DL[i].sentence;
                DL[i].sentence = " " + Tempsentence;

            }*//*
            // this code is in here to make the sentances display withought indentation
            */
            //DL[i].sentence = DL[i].sentence.Trim();
            //Debug.Log(DL[i].sentence.Trim());
        }   
    }
    public void DialogueDecisionCheck(ref DialogueLine [] DL)
    {
        foreach(DialogueLine Dialogue in DL )
        {

        }
    }

    // all required information for the dialogue

    private void OnEnable() 
    {
        DialogueMode = DialogueState.NotStarted;
    }
    private void OnValidate()
    {
        formatDialogueLine(ref line);
        
    }
}
