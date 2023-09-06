using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueIndicatorScript : MonoBehaviour
{
    public Animator DialogueIndicatorAnim;
    public DialogueTrigger DT;
    // Start is called before the first frame update
    void Start()
    {
        DialogueIndicatorAnim = gameObject.GetComponent<Animator>();
        DT = transform.parent.GetComponent<DialogueTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if(DT.dialogue.DialogueMode == Dialogue.DialogueState.NotStarted)
        {
            
            triggerIndicatorCheck();

        }
        else
        {
            DialogueIndicatorAnim.SetBool("Animate", false);
        }
    }
    public void triggerIndicatorCheck()
    {

        if (DT.Proximity())
        {
            //Debug.Log(DT.Proximity());  
            DialogueIndicatorAnim.SetBool("Animate", true);
        }
        else
        {
            DialogueIndicatorAnim.SetBool("Animate", false);
        }
    }
}
