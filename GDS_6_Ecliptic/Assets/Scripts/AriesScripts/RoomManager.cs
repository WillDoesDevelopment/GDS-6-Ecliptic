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

    public HubManager HM;

    private Vector3 PlayerStartPos;
    private Vector3 GoldRamStartPos;

    public GameObject ExitDoor;
    void Start()
    {
        PlayerStartPos = Player.transform.position;
        GoldRamStartPos = GoldSheep.transform.position;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
    
        if (DialogueEndcheck(Aries.transform.GetChild(6).gameObject))
        {
            
            ExitDoor.GetComponent<DoorScript>().DS.IsOpen = true;
            
            
        }
        if (DialogueEndcheck(GoldSheep.transform.GetChild(0).gameObject))
        {
            VFXCH.circleVFXStart();
            GoldSheep.GetComponent<Animator>().SetBool("Animate", true);
        }
        if (DialogueEndcheck(NormalSheep.transform.GetChild(0).gameObject))
        {
                                                                                // Once the dialogue component on the sheep is on the finished state it animates and gets hit by the arrow
            NormalSheep.GetComponent<Animator>().SetTrigger("Animate");

        }
    }

    public bool DialogueEndcheck(GameObject DialogueObj)
    {
        if (DialogueObj.GetComponent<DialogueTrigger>().dialogue.DialogueMode == Dialogue.DialogueState.Finished)
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
        Player.transform.position = PlayerStartPos;
        GameObject Temp = Instantiate(GoldSheep, GoldRamStartPos, Quaternion.identity);
        Destroy(GoldSheep);
        GoldSheep = Temp;

        Player.GetComponent<DialogueTrigger>().OnEventCheck();
        Player.GetComponent<DialogueTrigger>().OnEvent = false;

        Temp.GetComponent<Animator>().SetBool("Animate", false);
        Temp.GetComponent<DialogueTrigger>().dialogue.DialogueMode = Dialogue.DialogueState.NotStarted;
    }



}
