using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeminiManager : MonoBehaviour
{
    public DoorScript DS;

    //public DialogueTrigger EndDialogue;
    public VFXCircleHandler VFXCH;

    // need to know about both dialogue prefabs in the scene
    //public GameObject startDT;
    public GameObject EndDT;

    public GameObject WinConditionSND;
    public GameObject SuccessSND;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (EndDT.GetComponent<DialogueTrigger>().dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            SuccessSND.ge;
            DS.DS.IsOpen = true;
 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gemini") 
        {
            WinConditionSND.SetActive(true);
        }
        
    }
}
