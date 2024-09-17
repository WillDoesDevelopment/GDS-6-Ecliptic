 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject NormalSheep;

    public GameObject Player;
    public GameObject GoldSheep;
    public GameObject Aries;
    public GameObject ResetRamObj;

    // object that activates vfx circle
    public VFXCircleHandler VFXCH;

    public DialogueTrigger TestDt;
    public HubManager HM;

    private Vector3 PlayerStartPos;
    private Vector3 GoldRamStartPos;
    private Quaternion GoldRamStartRot;

    public GameObject ExitDoor;

    // best way to play sounds on update is to toggle an active game object they belong to
    public GameObject GoldenRamSnd;
    public GameObject NormalRamSnd;

    public GameObject deadRamSnd;
    public GameObject SuccessSND;

    private bool isPlayed;
    void Start()
    {

        VFXCH.circleVFXStart();
        PlayerStartPos = Player.transform.position;
        GoldRamStartPos = GoldSheep.transform.parent.transform.position;
        GoldRamStartRot = GoldSheep.transform.parent.transform.rotation;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
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
        if(NormalSheep.gameObject.activeInHierarchy)
        {
            
            if (DialogueEndcheck(NormalSheep.GetComponent<DialogueTrigger>()))
            {
                NormalSheep.transform.parent.GetComponent<Animator>().SetTrigger("Animate");
                NormalSheep.transform.parent.GetChild(0).GetComponent<Animator>().SetTrigger("Animate");
            }

        }                                                           // Once the dialogue component on the sheep is on the finished state it animates and gets hit by the arrow
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
            if (DialogueEndcheck(ResetRamObj.GetComponent<DialogueTrigger>()))
            {
                resetGoldRam();
                ResetRamObj.GetComponent<DialogueTrigger>().dialogue.DialogueMode = Dialogue.DialogueState.NotStarted;
            }
        }
        else
        {
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
