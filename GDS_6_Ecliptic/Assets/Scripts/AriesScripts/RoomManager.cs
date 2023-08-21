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
    }

    // Update is called once per frame
    void Update()
    {
    
        if (DialogueEndcheck(Aries))
        {
            VFXCH.circleVFXStart();
            ExitDoor.GetComponent<DoorScript>().DS.IsOpen = true;
            HM.AddOneToLevel();
            
        }
        if (DialogueEndcheck(GoldSheep))
        {
            GoldSheep.GetComponent<Animator>().SetBool("Animate", true);
        }
        if (DialogueEndcheck(NormalSheep))
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
