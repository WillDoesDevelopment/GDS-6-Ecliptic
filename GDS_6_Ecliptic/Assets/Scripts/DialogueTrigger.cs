using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public GameObject player;

    public float radius;

    public DialogueManager Dm;

    private bool dialogueMode = false;
    // Update is called once per frame
    void Start()
    {
        Dm = FindObjectOfType<DialogueManager>();
    }
    void Update()
    {
        if (dialogueMode == false)
        {
            Proximity();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Dm.NextDialogue();
        }
    }
    public void Proximity()
    {
        
        float playerDistZ = player.transform.position.z - transform.position.z;
        float playerDistX = player.transform.position.x - transform.position.x;
        if (Mathf.Sqrt(Mathf.Pow(playerDistX, 2) + Mathf.Pow(playerDistZ, 2)) < radius)
        {
            Dm.EnterPrompt();
            if (Input.GetKey(KeyCode.Return))
            {
                dialogueMode = true;
                Dm.EnterAnimExit();
                Dm.StartDialogue(dialogue);
            }
        }
        else
        {
            Dm.EnterAnimExit();
        }
    }
}
