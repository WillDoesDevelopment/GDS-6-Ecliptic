using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalram : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<DialogueTrigger>().dialogue.DialogueMode = Dialogue.DialogueState.NotStarted;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ramram()
    {
        this.gameObject.GetComponent<DialogueTrigger>().dialogue.DialogueMode = Dialogue.DialogueState.NotStarted;
    }
}
