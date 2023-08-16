using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueTrigger : MonoBehaviour
{
    /*public enum DialogueState
    {
        NotStarted,
        InProgress,
        Finished
    }
    public DialogueState DialogueMode = DialogueState.NotStarted;*/
    // seen in editor
    public Dialogue dialogue;

    // to checkproximity
    public GameObject player;

    // how large is proximity
    public float radius;

    // all Dialogue triggers need to know about the Dialogue manages, same as the hub manager
    public DialogueManager Dm;

    // self explanitory
    //private bool dialogueMode = false;

    public bool OnStart = false;
    public bool OnEvent = false;

    public bool IsDone => dialogue.DialogueMode == Dialogue.DialogueState.Finished;
    public bool IsTrigger => !OnStart && !OnEvent;

    void Awake()
    {
        // instead of finding it in editor
        Dm = FindObjectOfType<DialogueManager>();

    }
    private void Start()
    {
        OnStartCheck();
    }
    void Update()
    {
       
            DialogueModeCheck();

        
        //OnEventCheck();

    }
    public bool Proximity()
    {
        // checks if player is outside or within a radius
        float playerDistZ = player.transform.position.z - transform.position.z;
        float playerDistX = player.transform.position.x - transform.position.x;
        if (Mathf.Sqrt(Mathf.Pow(playerDistX, 2) + Mathf.Pow(playerDistZ, 2)) < radius)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public void OnStartCheck()
    {
        if (OnStart == true)
        {
            TriggerDialogue();
        }
    }

    public void DialogueModeCheck()
    {
        
        if (dialogue.DialogueMode == Dialogue.DialogueState.NotStarted)
        {
            // check if we are in range
            if (Proximity())
            {
                //Debug.Log("In range");
                Dm.proximityBool = true;
                if (Input.GetKeyDown(KeyCode.Return) ^ Input.GetKeyUp(KeyCode.JoystickButton1))
                {
                    TriggerDialogue();
                    
                    
                }

            }

        }
        else if (dialogue.DialogueMode == Dialogue.DialogueState.InProgress)
        {
            player.GetComponent<PlayerController>().canWalk = false;
            if (Input.GetKeyDown(KeyCode.Return) ^ Input.GetKeyUp(KeyCode.JoystickButton1))
            {
                // end dialogue must be done first otherwise our Next dialogue in dialogue manager will check for no sentences left and stop the dialogue before we can exit the dialogue in dialogue trigger
                Dm.EndDialogueCheck(dialogue);
                Dm.NextDialogue(dialogue);
            }

        }
        else
        {
            player.GetComponent<PlayerController>().canWalk = true;
        }

    }
   /* public void EndDialogueCheck()
    {
        if (Dm.Sentences.Count == 0)
        {
            //Debug.Log("end sentence");
            dialogue.DialogueMode = Dialogue.DialogueState.Finished;
            //dialogueMode = false;
            //this.GetComponent<DialogueTrigger>().enabled = false;
        }
    }*/

    public void TriggerDialogue()
    {
        
        Dm.EnterAnimExit();
        Dm.StartDialogue(dialogue);
    }

    public void OnEventCheck()
    {
        if (OnEvent)
        {
            radius = 0;
            TriggerDialogue();
        }
    }
}
