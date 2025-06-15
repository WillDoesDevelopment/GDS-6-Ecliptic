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

    public Animator anim;

    public bool isOff = true;

    public Vector3 PlayerResetPos;
    public Vector3 ArtifactResetPos;
    public Vector3 BullResetPos;

    public GameObject barrierObject;
    public Renderer rend;
    public AINavMesh? ANM;

    public MazeState maze;

    // Start is called before the first frame update

    private void Start()
    {
        rend = barrierObject.GetComponent<Renderer>();
        anim.SetBool("Bob", true);
    }

    private void Update()
    {
        Barrier();
    }

    public void MazeActiveToggle(bool toggle)
    {
        if (ANM != null)
        {
            ANM.NavMeshPause = toggle;
            Debug.Log("I'm a little bull and I've hurt my knee... " + toggle);
        }
    }

    public void Barrier()
    {
        var t = Mathf.InverseLerp(8, 6, Vector3.Distance(barrierObject.transform.position, ANM.transform.position));
        ;
        if (t == 0)
        {
            barrierObject.SetActive(false);
        }
        else
        {
            barrierObject.SetActive(true);
        }

        rend.material.SetFloat("_Transparency", Mathf.Clamp(t, 0, 1));
    }

    public void ResetCheck(GameObject Player)
    {
        if (Vector3.Distance(Player.transform.position, bull.transform.position) < 1f)
        {

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
        {
            Debug.Log(HO.GetComponent<Artifact>() != null && Vector3.Distance(this.TargetObject.transform.position, HO.transform.position) < 3);
            this.TargetObject.transform.GetChild(1).GetComponent<DoorScript>().DS.IsOpen = true;
            HO.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            HO.transform.DetachChildren();
            /*            CollectedItems += 1;
                        if (CollectedItems <= 3)
                        {
                            GoodJobSND.GetComponent<AudioSource>().Play();
                        }*/
            Player.GetComponent<PickUpScript>().holding = false;
            HO.gameObject.SetActive(false);
            HO.SetActive(false);
            return true;
        }
        else
        {
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
