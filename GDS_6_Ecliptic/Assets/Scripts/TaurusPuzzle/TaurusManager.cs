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

    public DialogueTrigger dtEvent;
    private Vector3 PlayerStartPos;
    private Vector3 BullStartPos;
    public DialogueTrigger DoorDialogue;

    private Artifact currentArtifact;
    void Start()
    {
        PlayerStartPos = Player.transform.position;
        BullStartPos = ANM.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        CheckWinCondition();

        ArtifactCollected();
    }

    public void CheckWinCondition()
    {
        //DialogueTrigger dtEvent = null;
        if (CollectedItems == 4)
        {
            ANM.NMA.SetDestination(ANM.transform.position);

            DoorDialogue.gameObject.SetActive(false);
            VFXCH.circleVFXStart();
            dtEvent.gameObject.SetActive(true);
            // here we will check if dialogue is done
            if(dtEvent.dialogue.DialogueMode == Dialogue.DialogueState.Finished)
            {
                DS.DS.IsOpen = true;


            }
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

// resetting is called by the bull script
    public void Resetting()
    {
        Player.GetComponent<DialogueTrigger>().OnEventCheck();
        Player.GetComponent<DialogueTrigger>().OnEvent = false;

        //character controller overwrites position, needs to be disabled before player can go back to the start
        Player.GetComponent<CharacterController>().enabled = false;
        Player.transform.position = PlayerStartPos;
        Player.GetComponent<CharacterController>().enabled = true;

        if(currentArtifact != null)
        {
            currentArtifact.ResetArtifact();
        }
        /*if(Player.GetComponent<PickUpScript>().HoldingObj.GetComponent<Artifact>() != null)
        {

        }*/

        if (Player.transform.position == PlayerStartPos)
        {
            ANM.transform.position = BullStartPos;

        }
    }
}
