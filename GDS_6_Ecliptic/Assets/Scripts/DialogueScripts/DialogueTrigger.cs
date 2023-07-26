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
    // Update is called once per frame
    void Start()
    {
        // instead of finding it in editor
        Dm = FindObjectOfType<DialogueManager>();
    }
    void Update()
    {

        if (dialogueMode == false)
        {
            // check if we are in range
            Proximity();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            // check if we press enter while in a dialogue mode
            Dm.NextDialogue();
        }
    }
    public void Proximity()
    {
        // checks if player is outside or within a radius
        float playerDistZ = player.transform.position.z - transform.position.z;
        float playerDistX = player.transform.position.x - transform.position.x;
        if (Mathf.Sqrt(Mathf.Pow(playerDistX, 2) + Mathf.Pow(playerDistZ, 2)) < radius)
        {
            // instructions for the player
            Dm.EnterPrompt();
            if (Input.GetKey(KeyCode.Return))
            {
                // we are in a dialogue, exit the prompt and start dialogue
                dialogueMode = true;
                Dm.EnterAnimExit();
                Dm.StartDialogue(dialogue);
            }
        }
        else
        {
            // if not in radius, exit the prompt
            Dm.EnterAnimExit();

        }
    }
}
