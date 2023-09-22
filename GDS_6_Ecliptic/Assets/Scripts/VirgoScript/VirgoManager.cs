using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirgoManager : MonoBehaviour
{
    public DialogueTrigger DT;

    public GameObject VirtualCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CreditsCheck();
    }

    public void CreditsCheck()
    {
        if (DT.dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            VirtualCam.SetActive(true);
        }
    }
}
