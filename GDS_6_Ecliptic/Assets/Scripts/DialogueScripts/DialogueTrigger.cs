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

    // all Dialogue triggers need to know about the Dialogue manages, same as the hub manager
    public DialogueManager Dm;

    // self explanitory
    private bool dialogueMode = false;

    public bool OnStart = false;
    public bool OnEvent = false;
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
            Dm.StartDialogue(dialogue);
            dialogueMode = true;
        }
    }

    public void DialogueModeCheck()
    {
        
        if (dialogueMode == false)
        {
            // check if we are in range
            if (Proximity())
            {
                //Debug.Log("In range");
                Dm.proximityBool = true;
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    TriggerDialogue();
                    
                }

            }

        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            EndDialogueCheck();

            Dm.NextDialogue();
        }
    }
    public void EndDialogueCheck()
    {
        if (Dm.Sentences.Count == 0)
        {
            this.GetComponent<DialogueTrigger>().enabled = false;
        }
    }

    public void TriggerDialogue()
    {
        dialogueMode = true;
        Dm.EnterAnimExit();
        Dm.StartDialogue(dialogue);
    }

    public void OnEventCheck()
    {
        if (OnEvent && dialogueMode == false)
        {
            radius = 0;
            TriggerDialogue();
        }
    }
}
