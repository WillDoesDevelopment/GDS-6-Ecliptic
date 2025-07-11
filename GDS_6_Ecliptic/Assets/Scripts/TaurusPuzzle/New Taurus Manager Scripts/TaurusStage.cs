using System.Collections;
using System.Collections.Generic;
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

    public DialogueTrigger DT;

    public Animator anim;

    public bool isOff = true;

    public Vector3 PlayerResetPos;
    public Vector3 ArtifactResetPos;
    public Vector3 BullResetPos;

    public GameObject barrierObject;
    public Renderer rend;
    public AINavMesh? ANM;

    public MazeState maze;

    [SerializeField] private float barrierBuffer;

    // Start is called before the first frame update

    private void Start()
    {
        rend = barrierObject.GetComponent<Renderer>();
        anim.SetBool("Bob", true);
    }

    private void Update()
    {
        Barrier();

        if (DT.dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            RoomWinDondition(Artifact, player);
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
        /*
         * 1. Calc Distance to Bull
         * 
         * 2. Subtract Radius
         * 
         * 3. Convert to percentage?
         */

        float bullDist = Vector3.Distance(barrierObject.transform.position, ANM.transform.position);
        
        if (bullDist >= barrierBuffer)
        {
            barrierObject.SetActive(false);
            print(bull + "... is getting here");
        }
        else
        {
            barrierObject.SetActive(true);
            print(bull + "... is NOT getting here");
        }

        print(bull + "is" + bullDist);
        

        rend.material.SetFloat("_Transparency", Mathf.Clamp(bullDist, 0, 1));
    }

    public void ResetCheck(GameObject Player)
    {
        if (Vector3.Distance(Player.transform.position, bull.transform.position) < 1f)
        {
            Player.GetComponent<PlayerController>().Damage();
            Player.GetComponent<DialogueTrigger>().OnEventCheck();
            Player.GetComponent<DialogueTrigger>().OnEvent = false;

            //this teleports the player without being over written by the character controller
            Player.GetComponent<CharacterController>().enabled = false;
            Player.transform.position = PlayerResetPos;
            Player.GetComponent<CharacterController>().enabled = true;


            Debug.Log(Player.transform.position);

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
        else
        {
            anim.SetBool("Bob", false);
            ItemIndicator.SetActive(false);
            particleObject.SetActive(true);
            particleObject.transform.position = HO.transform.position;
            particleObject.transform.LookAt(BeamTarget.transform.position);

        }
        //Debug.Log(HO.GetComponent<Artifact>() != null && Vector3.Distance(this.TargetObject.transform.position, HO.transform.position) < 3);

        if (HO.GetComponent<Artifact>() != null && Vector3.Distance(this.TargetObject.transform.position, HO.transform.position) < 3)
        {           /*            CollectedItems += 1;
                        if (CollectedItems <= 3)
                        {
                            GoodJobSND.GetComponent<AudioSource>().Play();
                        }*/
            //Debug.Log(HO.GetComponent<Artifact>() != null && Vector3.Distance(this.TargetObject.transform.position, HO.transform.position) < 3);
            

            if (stageCounter <= 2)
            {
                this.TargetObject.transform.GetChild(1).GetComponent<DoorScript>().DS.IsOpen = true;
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

        vfxCircle.SetActive(true);
        Debug.Log("I got to here!");
       
    }

    public void RoomWinDondition(GameObject HO, GameObject Player)
    {
        print("Activated Now!");
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
    }
}
