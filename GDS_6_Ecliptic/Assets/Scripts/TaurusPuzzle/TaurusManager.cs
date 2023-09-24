 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaurusManager : MonoBehaviour
{
    public int CollectedItems;

    public GameObject Player;

    public HubManager HB;

    public AINavMesh ANM;

    public VFXCircleHandler VFXCH;

    public DoorScript DS;

    private Vector3 PlayerStartPos;
    private Vector3 BullStartPos;
    public DialogueTrigger DoorDialogue;
    void Start()
    {
        PlayerStartPos = Player.transform.position;
        BullStartPos = ANM.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        CheckWinCondition();
        GiftPickUpCheck();
        ArtifactCollected();
    }

    public void GiftPickUpCheck()
    {
        GameObject HeldObj = Player.GetComponent<PickUpScript>().HoldingObj;
        if (HeldObj != null)
        {
            if (HeldObj.CompareTag("PickUp"))
            {
                HeldObj.GetComponent<DialogueTrigger>().OnEventCheck();
                HeldObj.GetComponent<DialogueTrigger>().OnEvent = false;
                ANM.NavMeshPause = true;
                if (HeldObj.GetComponent<DialogueTrigger>().dialogue.DialogueMode == Dialogue.DialogueState.Finished)
                {
                    ANM.NavMeshPause = false;
                }
            }

        }
    }
    public void CheckWinCondition()
    {
        DialogueTrigger dtEvent = null;
        if (CollectedItems == 4)
        {
            DoorDialogue.gameObject.SetActive(false);
            VFXCH.circleVFXStart();
            DialogueTrigger[] DT = this.GetComponents<DialogueTrigger>();
            foreach(DialogueTrigger dt in DT)
            {
                if (dt.OnEvent == true)
                {
                    dtEvent = dt;
                }
            }
            if(dtEvent.dialogue.DialogueMode == Dialogue.DialogueState.NotStarted)
            {
                dtEvent.OnEventCheck();
            }
            if(dtEvent.dialogue.DialogueMode == Dialogue.DialogueState.Finished)
            {
                DS.DS.IsOpen = true;
                /*HB.SetGameStage(2);
                HB.SendToHub();*/

            }
            // here we will check if dialogue is done
        }
    }
    public void ArtifactCollected()
    {
        GameObject HO = Player.GetComponent<PickUpScript>().HoldingObj;
        if(HO == null)
        {
            return;
        }
        if (HO.GetComponent<Artifact>() != null && Vector3.Distance(this.transform.position, HO.transform.position) < 2)
        {
            CollectedItems += 1;
            Player.GetComponent<PickUpScript>().holding = false;
            //HO.gameObject.SetActive(false);
            Destroy(HO);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
    }
    public void Resetting()
    {
        Player.GetComponent<DialogueTrigger>().OnEventCheck();
        Player.GetComponent<DialogueTrigger>().OnEvent = false;

        Debug.Log(PlayerStartPos);
        Player.transform.position = PlayerStartPos; 
        Debug.Log(Player.transform.position);

        if (Player.transform.position == PlayerStartPos)
        {
            ANM.transform.position = BullStartPos;

        }
    }
}
