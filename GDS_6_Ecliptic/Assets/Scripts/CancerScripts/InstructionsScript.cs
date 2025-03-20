using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsScript : MonoBehaviour
{
    private Animator InstructionsAnim;

    public PlayerController playerController;
    public PlayerState newPlayerState = PlayerState.Dialogue;
    public HubManager HB;
    public DialogueTrigger dialogueTrigger;
    // Start is called before the first frame update
    void Start()
    {
        InstructionsAnim = this.GetComponent<Animator>();
        newPlayerState = PlayerState.Dialogue;
        // when instructions appear, freeze player movement and actions     
        playerController.canWalk = false;
        HubManager.freezePlayerActions(playerController.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        fadeOut();
    }

    public void fadeOut()
    {
        if(Input.GetKeyDown(KeyCode.Return) ^ Input.GetKeyUp(KeyCode.JoystickButton1))
        {
            //Time.timeScale = 1f;
            InstructionsAnim.SetTrigger("Animate");
        }
    }
    public void enablePlayer()
    {
        HubManager.UnfreezePlayerActions(playerController.gameObject);
        playerController.canWalk = true;
        newPlayerState = PlayerState.Walk;

        if (dialogueTrigger != null)
        {
            dialogueTrigger.OnEvent = false;
            dialogueTrigger.TriggerDialogue();
        }
    }
}
