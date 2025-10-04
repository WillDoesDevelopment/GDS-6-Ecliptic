using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueTrigger : MonoBehaviour
{
    // seen in editor
    public Dialogue dialogue;

    // to checkproximity
    public GameObject player;

    // how large is proximity
    public float radius;

    // all Dialogue triggers need to know about the DialogueManager, same as the hub manager
    public DialogueManager Dm;

    // for different dialogue circumstances
    public bool OnStart = false;
    public bool OnEvent = false;

    // attempt to intergrate a Lambda
    public bool IsDone => dialogue.DialogueMode == Dialogue.DialogueState.Finished;
    public bool IsTrigger => !OnStart && !OnEvent;

	PlayerController PlayerCon;

	void Awake()
    {
        // instead of finding it in editor
        Dm = FindObjectOfType<DialogueManager>();
        player = GameObject.Find("Player");
    }
    private void Start()
    {
        PlayerCon = FindObjectOfType<PlayerController>();
		OnStartCheck();
    }
    void Update()
    {
            DialogueModeCheck();
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
        if (OnStart)
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
                if (PlayerCon.InputController.Player.Accept.triggered)
                {
                    TriggerDialogue();
                }

            }
            
        }
        else if (dialogue.DialogueMode == Dialogue.DialogueState.InProgress)
        {
            
            if (PlayerCon.InputController.Player.Accept.triggered)
            {
                //Debug.Log("CallingNextDialogue");
                // end dialogue must be done first otherwise our Next dialogue in dialogue manager will check for no sentences left and stop the dialogue before we can exit the dialogue in dialogue trigger
               if(Dm.CoroutineRunning == false)
                {
                    //Debug.Log("endingDialogue");
                    Dm.EndDialogueCheck(dialogue);
                }
                Dm.NextDialogue(dialogue);
            }

        }
        

    }

    // is called by iether, OnStartCheck(), OnEventCheck(), or DialogueModeCheck()
    public void TriggerDialogue()
    {
        DialogueManager.InDialogue = true;
        Dm.EnterAnimExit();
        Dm.StartDialogue(dialogue);
    }

    // can only be called from other functions, must have OnEvent bool true
    public void OnEventCheck()
    {
        if (OnEvent)
        {
            radius = 0;
            TriggerDialogue();
        }
    }
}
