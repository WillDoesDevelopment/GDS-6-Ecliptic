 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    public GameObject NormalSheep;

    public GameObject Player;
    public GameObject GoldSheep;
    public GameObject Aries;
    public VFXCircleHandler VFXCH;

    public DialogueTrigger TestDt;
    public HubManager HM;

    private Vector3 PlayerStartPos;
    private Vector3 GoldRamStartPos;
    private Quaternion GoldRamStartRot;

    public GameObject ExitDoor;

    public AudioSource GoldenRamSnd;
    public AudioSource NormalRamSnd;
    public AudioSource deadRamSnd;
    
    void Start()
    {
        PlayerStartPos = Player.transform.position;
        GoldRamStartPos = GoldSheep.transform.parent.transform.position;
        GoldRamStartRot = GoldSheep.transform.parent.transform.rotation;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("TestDT" + TestDt);
        //Debug.Log(Aries.GetComponent<DialogueTrigger>());
        if (DialogueEndcheck(Aries.GetComponent<DialogueTrigger>()))
        {
            ExitDoor.GetComponent<DoorScript>().DS.IsOpen = true;
            
            
        }
        if (DialogueEndcheck(GoldSheep.GetComponent<DialogueTrigger>()))
        {
            VFXCH.circleVFXStart();
            GoldSheep.transform.parent.GetComponent<Animator>().SetBool("Animate", true);
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

    public void Reset()
    {
        //deadRamSnd.Play();
        Player.transform.position = PlayerStartPos;
        GameObject Temp = Instantiate(GoldSheep.transform.parent.gameObject, GoldRamStartPos, GoldRamStartRot);
        Destroy(GoldSheep.transform.parent.gameObject);        
        GoldSheep = Temp.transform.Find("DialogueTriggerPrefab").gameObject;

        Player.GetComponent<DialogueTrigger>().OnEventCheck();
        Player.GetComponent<DialogueTrigger>().OnEvent = false;

        Temp.GetComponent<Animator>().SetBool("Animate", false);        
        Temp.transform.Find("DialogueTriggerPrefab").gameObject.GetComponent<DialogueTrigger>().dialogue.DialogueMode = Dialogue.DialogueState.NotStarted;
    }



}
