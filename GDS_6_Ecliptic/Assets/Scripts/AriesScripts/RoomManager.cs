using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject NormalSheep;
    public DialogueTrigger DeadSheepDialogue;
    public GameObject Arrow;
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
            HM.AddOneToLevel();
            HM.SendToHub();
        }
        if (DialogueEndcheck(GoldSheep))
        {
            GoldSheep.GetComponent<Animator>().SetTrigger("Animate");
        }
        if (DialogueEndcheck(NormalSheep))
        {
                                                                                // Once the dialogue component on the sheep is on the finished state it animates and gets hit by the arrow
            NormalSheep.GetComponent<Animator>().SetTrigger("Animate");

        }
    }

    public bool DialogueEndcheck(GameObject DialogueObj)
    {
        if (DialogueObj.GetComponent<DialogueTrigger>().dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Reset()
    {
        
    }



}
