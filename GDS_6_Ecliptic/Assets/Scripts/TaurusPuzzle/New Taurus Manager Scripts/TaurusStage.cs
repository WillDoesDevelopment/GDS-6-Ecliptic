using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaurusStage : MonoBehaviour
{
    public GameObject bull;
    public GameObject Artifact;
    public GameObject TargetObject;
    public GameObject BeamTarget;
    public GameObject ItemIndicator;
    public GameObject vfxCircle;
    public GameObject player;

    public GameObject[] pieces;
    public Vector3[] piecesResetPos;
    public int piecesCounter = 0;

    public DialogueTrigger DT;

    public Animator anim;

    public AudioSource GoodJobSND;

    public bool isOff = true;

    public Vector3 PlayerResetPos;
    public Vector3 ArtifactResetPos;
    public Vector3 BullResetPos;

    public GameObject barrierObject;
    public Renderer rend;
    public AINavMesh ANM;

    public MazeState maze;

    [SerializeField] private float barrierBuffer;

    // Start is called before the first frame update

    private void Start()
    {
        rend = barrierObject.GetComponent<Renderer>();
        
    }

    private void Update()
    {
        if(piecesCounter >= pieces.Length)
        {
            ArtifactAppear();
        }

        Barrier();

        if (DT.dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            RoomWinCondition(Artifact, player);
        }
        else
        {
            return;
        }
    }

    public void MazeActiveToggle(bool toggle)
    {
        if (ANM != null)
        {
            ANM.NavMeshPause = toggle;
        }
    }

    public void Barrier()
    {
        float bullDist = Vector3.Distance(barrierObject.transform.position, ANM.transform.position);
        
        if (bullDist >= barrierBuffer)
        {
            barrierObject.SetActive(false);
        }
        else
        {
            barrierObject.SetActive(true);
        }

        rend.material.SetFloat("_Transparency", Mathf.Clamp(bullDist, 0, 1));
    }

    public void ResetCheck(GameObject Player)
    {
        if (Vector3.Distance(Player.transform.position, bull.transform.position) < 1f)
        {
            player.GetComponent<blooood>().blood();

            //this teleports the player without being over written by the character controller
            Player.GetComponent<CharacterController>().enabled = false;
            Player.transform.position = PlayerResetPos;
            Player.GetComponent<CharacterController>().enabled = true;

            Artifact.transform.position = ArtifactResetPos;
            bull.transform.position = BullResetPos;

        }
    }

    public GameObject GetTargetObject()
    {
        return TargetObject;
    }

    public bool ArtifactCheck(GameObject Player, GameObject particleObject, int stageCounter, GameObject TargetObject)
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
        else if(pieces.Contains(HO))
        {
            ArtPieces(HO);
            return false;
        }
        else
        {
            anim.SetBool("Bob", false);
            ItemIndicator.SetActive(false);
            particleObject.SetActive(true);
            particleObject.transform.position = HO.transform.position;
            particleObject.transform.LookAt(BeamTarget.transform.position);
        }


        if (HO.GetComponent<Artifact>() != null && Vector3.Distance(this.TargetObject.transform.position, HO.transform.position) < 3)
        {          

            if (stageCounter <= 2)
            {
                this.TargetObject.transform.GetChild(1).GetComponent<DoorScript1>().DS.IsOpen = true;
                GoodJobSND.GetComponent<AudioSource>().Play();
                HO.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                HO.transform.DetachChildren();
                Player.GetComponent<PickUpScript>().holding = false;
                HO.gameObject.SetActive(false);
                HO.SetActive(false);
            }

            return true;

        }
        else
        {
            return false;
        }
    }

    public void StageWinCondition()
    { 
        vfxCircle.GetComponent<VFXCircleHandler>().circleVFXStart();    
    }

    public void ArtPieces(GameObject HO)
    {
        /*GameObject HO = player.GetComponent<PickUpScript>().HoldingObj;

        if (HO == null)
        {
            return;
        }

        if(pieces.Contains(HO))
        {*/
        Debug.Log("Checkpoint1");
        HO.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        Debug.Log("Checkpoint2");
        HO.transform.DetachChildren();
        Debug.Log("Checkpoint3");
        player.GetComponent<PickUpScript>().holding = false;
        Debug.Log("Checkpoint4");
        HO.SetActive(false);
        Debug.Log("Checkpoint5");
        piecesCounter++;
        Debug.Log("Checkpoint6");

        //}

    }

    public void ArtifactAppear()
    {
        Artifact.SetActive(true);
        anim.SetBool("Bob", true);
    }

    public void RoomWinCondition(GameObject HO, GameObject Player)
    {
        this.TargetObject.transform.GetChild(1).GetComponent<DoorScript>().DS.IsOpen = true;
        HO.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        HO.transform.DetachChildren();
        Player.GetComponent<PickUpScript>().holding = false;
        HO.gameObject.SetActive(false);
        HO.SetActive(false);
    }
    public void SetResetPos()
    {
        BullResetPos = bull.transform.position;
        ArtifactResetPos = Artifact.transform.position;

        for (int i = 0; i < pieces.Length; i++)
        {
            piecesResetPos[i] = pieces[i].transform.position;
        }
    }
}
