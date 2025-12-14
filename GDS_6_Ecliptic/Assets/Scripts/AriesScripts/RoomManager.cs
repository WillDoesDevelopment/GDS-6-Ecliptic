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
    public GameObject GoldRamFX;

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

    public Dialogue startDia;
    public DialogueTrigger startDiaDT;
    private bool isPlayed;
    public bool sheeped = false;
     

    //public bool isOrbed = false;
    void Start()
    {
        VFXCH.circleVFXStart();
        PlayerStartPos = Player.transform.position;
        GoldRamStartPos = GoldSheep.transform.parent.transform.position;
        GoldSheep.SetActive(true);
        GoldRamFX.SetActive(true);
        GoldRamStartRot = GoldSheep.transform.parent.transform.rotation;
        //Cursor.lockState = CursorLockMode.Locked;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        displayResetRam();
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
            if (GameObject.FindWithTag("Vfx") != null)
            {
                GameObject.FindWithTag("Vfx").gameObject.SetActive(false);
            }
            GoldSheep.transform.parent.GetComponent<Animator>().SetTrigger("AnimateTrig");
            GoldSheep.transform.parent.GetChild(0).GetComponent<Animator>().SetTrigger("Animate");
            
        }
  

        if (NormalSheep.gameObject.activeInHierarchy /*&& sheeped == false*/)
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
                //NormalSheep.GetComponentInChildren<DialogueTrigger>().dialogue.DialogueMode = Dialogue.DialogueState.Finished;
                //
            }

        }
        else
        {
            Debug.LogError("Made it here in one piece.");
            if (DialogueEndcheck(sheepDeathDT))
            {
                Debug.LogError("Death DT triggered");
                sheepDeathDT.gameObject.SetActive(false);
                GoldSheep.SetActive(true);
                GoldRamFX.SetActive(false);

            }
        }
        /*(if(blooood.tries >= 1)
        {
            startDia.DialogueMode = Dialogue.DialogueState.Finished;
        }*/
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
        Temp.transform.Find("SparkleFX 2").gameObject.SetActive(true);   

    }

    public void PlaySnd(GameObject AS)
    {
        //GameObject Temp = Instantiate(AS, GameObject.Find("AudioObjs").transform.position,Quaternion.identity);
        AS.SetActive(true);

    }


}
