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
    public DialogueTrigger EndDialogue;
    public VFXCircleHandler VFXCH;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Collision    
        Collider[] hitColliders1 = Physics.OverlapBox(transform.position + transform.up * 2f, new Vector3(1.25f, 1.0f, 1.25f)); //Hitbox for player
        foreach (var hitCollider in hitColliders1)
        {
            if (hitCollider.gameObject == player)                                           
            {
                if (player.GetComponent<PlayerController>().airTime > 0.5)
                {
                    GetComponent<Animator>().SetTrigger("Animate");
                    VFXCH.circleVFXStart(); //dialouge circle stuff :)
                    
                    EndDialogue.OnEventCheck();
                    EndDialogue.OnEvent = false;
                    

                }
            }
        }

        if (EndDialogue.dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            HM.SendToHub();
            HM.SetGameStage(4);
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + colliderOffset, colliderScale);    //Draw for debugging player hit

    }

}
