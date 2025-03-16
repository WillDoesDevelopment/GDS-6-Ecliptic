using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeoRoom : MonoBehaviour
{

    public HubManager hubManager;
    public DialogueTrigger EndDialogue;
    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EndDialogue.dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            door.SetActive(true);
            //hubManager.SendToHub();
            //hubManager.SetGameStage(6);
        }
    }
}
