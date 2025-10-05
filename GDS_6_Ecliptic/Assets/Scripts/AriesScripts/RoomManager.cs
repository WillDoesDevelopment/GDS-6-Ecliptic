 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("Ram Objects")]
    public GameObject NormalSheep;
    public GameObject Player;
    public GameObject GoldSheep;
    public GameObject Aries;
    public GameObject ResetRamObj;
    public GameObject Resetdialogue;
    public GameObject ResetSmallRam;
    public GameObject NsheepAnim;

    [Header("Orb Objects")]
    //public GameObject startDia;
    //public GameObject orb;

    [Header("VFX & Managers")]
    // object that activates vfx circle
    public VFXCircleHandler VFXCH;
    public DialogueTrigger TestDt;
    public DialogueTrigger sheepDeathDT;

    public HubManager HM;

    [Header("Positions")]
    private Vector3 PlayerStartPos;
    private Vector3 GoldRamStartPos;
    private Quaternion GoldRamStartRot;

    [Header("Door Objects")]
    public GameObject ExitDoor;

    [Header("Audio")]
    // best way to play sounds on update is to toggle an active game object they belong to
    public GameObject GoldenRamSnd;
    public GameObject NormalRamSnd;

    public GameObject deadRamSnd;
    public GameObject SuccessSND;

    private bool isPlayed;
    public bool sheeped = false;
    //public bool isOrbed = false;
    void Start()
    {
        //orb.GetComponent<OrbMovementAries>().OrbIdle();
        VFXCH.circleVFXStart();
        PlayerStartPos = Player.transform.position;
        GoldRamStartPos = GoldSheep.transform.parent.transform.position;
        GoldSheep.SetActive(true);
        GoldRamStartRot = GoldSheep.transform.parent.transform.rotation;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        displayResetRam();
        //Debug.Log("TestDT" + TestDt);
        //Debug.Log(Aries.GetComponent<DialogueTrigger>());
        if (DialogueStartedCheck(GoldSheep.GetComponent<DialogueTrigger>()))
        {
            PlaySnd(GoldenRamSnd);
        }
        if (DialogueStartedCheck(NormalSheep.GetComponent<DialogueTrigger>()))
        {
            PlaySnd(NormalRamSnd);
        }

        if (DialogueEndcheck(Aries.GetComponent<DialogueTrigger>()))
        {
            ExitDoor.GetComponent<DoorScript>().DS.IsOpen = true;

            SuccessSND.SetActive(true);
        }
        
        if (DialogueEndcheck(GoldSheep.GetComponent<DialogueTrigger>()))
        {
            VFXCH.circleVFXStart(); 
            GoldSheep.transform.parent.GetComponent<Animator>().SetTrigger("AnimateTrig");
            GoldSheep.transform.parent.GetChild(0).GetComponent<Animator>().SetTrigger("Animate");
            
        }

        /* (DialogueEndcheck(startDia.GetComponent<DialogueTrigger>()) && isOrbed == false)
        {
            orb.GetComponent<OrbMovementAries>().OrbMove();

        }*/

        if (NormalSheep.gameObject.activeInHierarchy && sheeped == false)
        {
            
            if (DialogueEndcheck(NormalSheep.GetComponent<DialogueTrigger>()))
            {
                GoldSheep.SetActive(false);
                //print("Baby sheep");
                NormalSheep.GetComponent<Animator>().SetTrigger("Animate");
                NsheepAnim.GetComponent<Animator>().SetTrigger("Animate");
                sheeped = true;
                //NormalSheep.GetComponentInChildren<Animator>().SetTrigger("Animate");
                //NormalSheep.transform.parent.GetChild(0).GetComponent<Animator>().SetTrigger("Animate");
                //NormalSheep.transform.parent.GetChild(1).GetComponent<DialogueTrigger>().dialogue.DialogueMode = Dialogue.DialogueState.NotStarted;
                //
            }

        }
        else
        {
            if (DialogueEndcheck(sheepDeathDT))
            {
                //print("Death DT triggered");
                sheepDeathDT.gameObject.SetActive(false);
                GoldSheep.SetActive(true);
            }
        }
        // Once the dialogue component on the sheep is on the finished state it animates and gets hit by the arrow
    }

    public bool DialogueEndcheck(DialogueTrigger DialogueObj)
    {
        
        if (DialogueObj.dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool DialogueStartedCheck(DialogueTrigger DialogueObj)
    {

        if (DialogueObj.dialogue.DialogueMode == Dialogue.DialogueState.InProgress)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void displayResetRam()
    {
        if (Vector3.Distance(ResetRamObj.transform.position, GoldSheep.transform.position) > 5)
        {
            ResetRamObj.GetComponent<Animator>().SetBool("FadeIn", true);
            ResetRamObj.GetComponent<DialogueTrigger>().enabled = true;
            Resetdialogue.SetActive(true);
            
            if (DialogueEndcheck(ResetRamObj.GetComponent<DialogueTrigger>()))
            {
                resetGoldRam();
                Resetdialogue.GetComponent<DialogueTrigger>().dialogue.DialogueMode = Dialogue.DialogueState.NotStarted;

            }
        }
        else
        {
            Resetdialogue.SetActive(false);
            ResetRamObj.GetComponent<Animator>().SetBool("FadeIn", false);
            ResetRamObj.GetComponent<DialogueTrigger>().enabled = false;
        }
    }
    public void Reset()
    {
        PlaySnd(deadRamSnd);


        Player.transform.position = PlayerStartPos;

        // golden ram being reset
        resetGoldRam();

        Player.GetComponent<DialogueTrigger>().OnEventCheck();
        Player.GetComponent<DialogueTrigger>().OnEvent = false;

        
        //turn off audios
        deadRamSnd.SetActive(false);
        NormalRamSnd.SetActive(false);
        GoldenRamSnd.SetActive(false);


    }

    public void resetGoldRam()
    {
       
        GameObject Temp = Instantiate(GoldSheep.transform.parent.gameObject, GoldRamStartPos, GoldRamStartRot);
        Destroy(GoldSheep.transform.parent.gameObject);
        GoldSheep = Temp.transform.Find("DialogueTriggerPrefab").gameObject;
        Temp.GetComponent<Animator>().SetBool("Animate", false);
        Temp.transform.Find("DialogueTriggerPrefab").gameObject.GetComponent<DialogueTrigger>().dialogue.DialogueMode = Dialogue.DialogueState.NotStarted;
    }

    public void PlaySnd(GameObject AS)
    {
        //GameObject Temp = Instantiate(AS, GameObject.Find("AudioObjs").transform.position,Quaternion.identity);
        AS.SetActive(true);

    }


}
