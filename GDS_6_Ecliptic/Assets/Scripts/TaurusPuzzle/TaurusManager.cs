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
        //GiftPickUpCheck();
        ArtifactCollected();
    }

/*    public void GiftPickUpCheck()
    {
        // everytime we pick up somthing we try get the artifact script and assign it to current artifact (Try get component returns a bool and spits out a variable (ie it spits out in this case a variable called artifact))
        GameObject HeldObj = Player.GetComponent<PickUpScript>().HoldingObj;


        if (HeldObj != null)
        {
            HeldObj.TryGetComponent<Artifact>(out Artifact artifact);
            currentArtifact = artifact;
*//*            if (HeldObj.CompareTag("PickUp"))
            {
                ANM.NavMeshPause = true;
            }*//*

        }
    }*/
    public void CheckWinCondition()
    {
        //DialogueTrigger dtEvent = null;
        if (CollectedItems == 4)
        {
            Debug.Log("running");
            DoorDialogue.gameObject.SetActive(false);
            VFXCH.circleVFXStart();
            dtEvent.gameObject.SetActive(true);

            /*            DialogueTrigger[] DT = this.GetComponents<DialogueTrigger>();
            foreach(DialogueTrigger dt in DT)
            {
                if (dt.OnEvent == true)
                {
                    dtEvent = dt;
                }
            }*/
            /*if(dtEvent.dialogue.DialogueMode == Dialogue.DialogueState.NotStarted)
            {
                dtEvent.OnEventCheck();
            }*/
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
