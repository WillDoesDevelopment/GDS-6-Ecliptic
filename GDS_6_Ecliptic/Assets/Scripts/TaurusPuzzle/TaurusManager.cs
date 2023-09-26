 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaurusManager : MonoBehaviour
{
    public int CollectedItems;

    public GameObject Player;

    public GameObject barrierObject;

    public HubManager HB;

    public AINavMesh ANM;

    public VFXCircleHandler VFXCH;

    public DoorScript DS;

    public DialogueTrigger dtEvent;
    private Vector3 PlayerStartPos;
    private Vector3 BullStartPos;
    public DialogueTrigger DoorDialogue;

    Renderer rend;

    private Artifact currentArtifact;

    public float p;
    void Start()
    {
        PlayerStartPos = Player.transform.position;
        BullStartPos = ANM.transform.position;
        rend = barrierObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

        CheckWinCondition();
        ArtifactCollected();
        Barrier();
    }

    public void Barrier()
    {        
        var t = Mathf.InverseLerp(8, 6, Vector3.Distance(transform.position, ANM.transform.position));
        p = t;
        if(t == 0)
        {
            barrierObject.SetActive(false);
        }
        else
        {
            barrierObject.SetActive(true);
        }

        rend.material.SetFloat("_Transparency", Mathf.Clamp(t, 0, 1));
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
        if (HO.GetComponent<Artifact>() != null && Vector3.Distance(this.transform.position, HO.transform.position) < 3)
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
