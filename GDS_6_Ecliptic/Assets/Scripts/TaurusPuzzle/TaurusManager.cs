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

    public GameObject particleObject;
    public GameObject beamObject;

    public GameObject WinConditionSND;
    public GameObject GoodJobSND;

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
        ;
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
            WinConditionSND.SetActive(true);
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
        

        if(!HO.CompareTag("PickUp"))
        {
            //beamObject.SetActive(false);
            particleObject.SetActive(false);
        }
        else
        {
            /*
            beamObject.SetActive(true);
            beamObject.transform.position = (transform.position + HO.transform.position) / 2.0f;
            beamObject.transform.localScale = new Vector3(.05f, 0.05f, Vector3.Distance(transform.position, HO.transform.position));
            beamObject.transform.LookAt(transform.position);
            */
            particleObject.SetActive(true);
            particleObject.transform.position = HO.transform.position;
            particleObject.transform.LookAt(transform.position);


        }

        if (HO.GetComponent<Artifact>() != null && Vector3.Distance(this.transform.position, HO.transform.position) < 3)
        {
            HO.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            HO.transform.DetachChildren();
            CollectedItems += 1;
            if(CollectedItems <=3)
            {
                GoodJobSND.GetComponent<AudioSource>().Play();
            }
            Player.GetComponent<PickUpScript>().holding = false;
            //HO.gameObject.SetActive(false);
            HO.SetActive(false);
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
