using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemDoor : MonoBehaviour
{
    public DoorScript DS;
    public GameObject EndDT;

    public GameObject Snake2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EndDT.GetComponent<DialogueTrigger>().dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            DS.DS.IsOpen = true;
            Snake2.GetComponent<Animator>().SetTrigger("Bye");
            return;
        }
    }
}
