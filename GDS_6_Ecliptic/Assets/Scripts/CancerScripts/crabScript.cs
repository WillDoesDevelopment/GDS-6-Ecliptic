using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crabScript : MonoBehaviour
{

    public GameObject player;
    public bool squish = false;
    public Vector3 colliderOffset;
    public Vector3 colliderScale;

    public HubManager HM;
    public DoorScript DS;

    //public DialogueTrigger EndDialogue;
    public VFXCircleHandler VFXCH;

    // need to know about both dialogue prefabs in the scene
    public GameObject crabDT;
    public GameObject EndDT;

    public GameObject AngrySnds;
    public GameObject WinConditionSND;
    public GameObject SuccessSND;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Collision    
        Collider[] hitColliders1 = Physics.OverlapBox(transform.position + colliderOffset, colliderScale / 2f); //Hitbox for player
        foreach (var hitCollider in hitColliders1)
        {
            if (hitCollider.gameObject == player)                                           
            {
                crabDT.SetActive(false); // if we dont talk to the crab before squishing it we want to deactivate the dialogue
                if (player.GetComponent<PlayerController>().airTime > 0.5)
                {
                    AngrySnds.SetActive(true);
                    GetComponent<Animator>().SetTrigger("Animate");//squashes crab
                    VFXCH.circleVFXStart(); //dialouge circle stuff :)
                    EndDT.SetActive(true);// we want to activate the dialogue prefab
                    SuccessSND.SetActive(true);

                }
            }
        }
       
        if (EndDT.GetComponent<DialogueTrigger>().dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            GetComponent<Animator>().SetTrigger("Animate2");
            WinConditionSND.SetActive(true);
            // if the end dialogue is done, open the door
            DS.DS.IsOpen = true;
            //Destroy(gameObject);
/*            HM.SendToHub();
            HM.SetGameStage(4);*/
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + colliderOffset, colliderScale);    //Draw for debugging player hit

    }

}
