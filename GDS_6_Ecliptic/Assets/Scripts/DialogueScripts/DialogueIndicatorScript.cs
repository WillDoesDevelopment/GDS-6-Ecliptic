using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueIndicatorScript : MonoBehaviour
{
    public Animator DialogueIndicatorAnim;
    public DialogueTrigger DT;

    public bool JustIndicator;
    // Start is called before the first frame update
    void Start()
    {
        DialogueIndicatorAnim = gameObject.GetComponent<Animator>();
        DT = transform.parent.GetComponent<DialogueTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!JustIndicator)
        {
            DialogueStatusCheck();
        }
        else
        {
            triggerIndicatorCheck();
        }
    }
    public void triggerIndicatorCheck()
    {

        if (DT.Proximity())
        {
            //Debug.Log(DT.Proximity());  
            DialogueIndicatorAnim.SetBool("Animate", true);
            if (JustIndicator)
            {
                if(Input.GetKeyDown(KeyCode.Return) ^ Input.GetKeyUp(KeyCode.JoystickButton1))
                {
                    this.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            DialogueIndicatorAnim.SetBool("Animate", false);
        }
    }

    public void DialogueStatusCheck()
    {
        if (DT.dialogue.DialogueMode == Dialogue.DialogueState.NotStarted)
        {

            triggerIndicatorCheck();

        }
        else
        {
            DialogueIndicatorAnim.SetBool("Animate", false);
        }
    }
}
