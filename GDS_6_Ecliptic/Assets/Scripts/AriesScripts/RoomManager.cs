using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject NormalSheep;
    //public DialogueTrigger NoramlSheepDeath;

    public GameObject GoldSheep;
    public GameObject Aires;

    public HubManager HM;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueEndcheck(Aires))
        {
            HM.SendToHub();
        }
        if (DialogueEndcheck(GoldSheep))
        {
            GoldSheep.GetComponent<Animator>().SetTrigger("Animate");
        }
        if (DialogueEndcheck(NormalSheep))
        {
            NormalSheep.GetComponent<Animator>().SetTrigger("Animate");
            DialogueTrigger[] DT = NormalSheep.GetComponents<DialogueTrigger>();
            foreach(DialogueTrigger dt in DT)
            {
                dt.OnEventCheck();
            }
        }
    }
    public bool DialogueEndcheck(GameObject DialogueObj)
    {
        if (DialogueObj.GetComponent<Dialogue>().DialogueMode == Dialogue.DialogueState.Finished)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
