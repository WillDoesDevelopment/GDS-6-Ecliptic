using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaurusStage : MonoBehaviour
{
    public GameObject bull;
    public GameObject Artifact;


    public Vector3 PlayerResetPos;
    public Vector3 ArtifactResetPos;
    public Vector3 BullResetPos;
    
    // Start is called before the first frame update
    public abstract void VReset(GameObject Player);


    public void ResetCheck(GameObject Player)
    {
        if (Vector3.Distance(Player.transform.position, bull.transform.position) < 1f)
        {
            Player.GetComponent<CharacterController>().enabled = false;
            Player.transform.position = PlayerResetPos;
            Player.GetComponent<CharacterController>().enabled = true;
            Debug.Log(Player.transform.position);

            Artifact.transform.position = ArtifactResetPos;
            bull.transform.position = BullResetPos;

        }
    }

    public void CollectArtifact()
    {

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

    private void Start()
    {
        
    }

    public void Update()
    {
        TaurusStages[taurusStageCounter].ResetCheck(Player);
    }

    
    public void ResetCheck()
    {
        //this function needs to check the players location, bulls location and 
    }

}
