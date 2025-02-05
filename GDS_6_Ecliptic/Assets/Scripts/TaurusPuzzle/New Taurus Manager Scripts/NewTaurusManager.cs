using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaurusStage : MonoBehaviour
{
    public GameObject bull;
    public GameObject Artifact;
    public GameObject TargetObject;


    public Vector3 PlayerResetPos;
    public Vector3 ArtifactResetPos;
    public Vector3 BullResetPos;
    
    // Start is called before the first frame update
    public abstract void VReset(GameObject Player);


    public void ResetCheck(GameObject Player)
    {
        if (Vector3.Distance(Player.transform.position, bull.transform.position) < 1f)
        {

            Player.GetComponent<DialogueTrigger>().OnEventCheck();
            Player.GetComponent<DialogueTrigger>().OnEvent = false;

            //this telliports the player without being over written by the character controller
            Player.GetComponent<CharacterController>().enabled = false;
            Player.transform.position = PlayerResetPos;
            Player.GetComponent<CharacterController>().enabled = true;


            Debug.Log(Player.transform.position);

            Artifact.transform.position = ArtifactResetPos;
            bull.transform.position = BullResetPos;

        }
    }

    public bool ArtifactCheck(GameObject Player, GameObject particleObject, int stageCounter)
    {
        GameObject HO = Player.GetComponent<PickUpScript>().HoldingObj;
        if (HO == null)
        {
            return false;
        }
        if (!HO.CompareTag("PickUp"))
        {
            particleObject.SetActive(false);
        }
        else
        {
            particleObject.SetActive(true);
            particleObject.transform.position = HO.transform.position;
            particleObject.transform.LookAt(transform.position);
            
        }
        if (HO.GetComponent<Artifact>() != null && Vector3.Distance(TargetObject.transform.position, HO.transform.position) < 3)
        {
            TargetObject.transform.GetChild(0).GetComponent<DoorScript>().DS.IsOpen = true;
            HO.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            HO.transform.DetachChildren();
/*            CollectedItems += 1;
            if (CollectedItems <= 3)
            {
                GoodJobSND.GetComponent<AudioSource>().Play();
            }*/
            Player.GetComponent<PickUpScript>().holding = false;
            //HO.gameObject.SetActive(false);
            HO.SetActive(false);
            return true;
        }
        else {
            return false;
        }
    }

    public void StageWinCondition()
    {

    }

    public void RoomWinDondition()
    {

    }
    public void SetResetPos()
    {
        BullResetPos = bull.transform.position;
        ArtifactResetPos = Artifact.transform.position;
    }

}





public class NewTaurusManager : MonoBehaviour
{
    public TaurusStage[] TaurusStages;
    public int taurusStageCounter;

    public GameObject Player;
    public GameObject particleObject;

    private void Start()
    {
        
    }

    public void Update()
    {
        Debug.Log(taurusStageCounter);
        TaurusStages[taurusStageCounter].ResetCheck(Player);
        if (TaurusStages[taurusStageCounter].ArtifactCheck(Player, particleObject, taurusStageCounter))
        {
            if (taurusStageCounter < 2)
            {
                taurusStageCounter += 1;

            }
            else
            {
                return;
            }
        }

    }

    
    public void ResetCheck()
    {
        //this function needs to check the players location, bulls location and 
    }

}
