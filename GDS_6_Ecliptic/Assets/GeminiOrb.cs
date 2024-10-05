using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeminiOrb : MonoBehaviour
{
    [Header("Dialogue Triggers")]
    public GameObject VFXDT;
    public GameObject FwdDT;
    public GameObject EndDT;

    [Header("Animators")]
    public Animator AnimatorL;
    public Animator AnimatorR;

    [Header("Orb Objects")]
    public GameObject[] orbs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (VFXDT.GetComponent<DialogueTrigger>().dialogue.DialogueMode == Dialogue.DialogueState.InProgress)
        {
            AnimatorL.GetComponent<Animator>().SetTrigger("Circle");
        }

        if (FwdDT.GetComponent<DialogueTrigger>().dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            AnimatorL.GetComponent<Animator>().ResetTrigger("Circle");
            AnimatorL.GetComponent<Animator>().SetTrigger("Fwd");
            AnimatorR.GetComponent<Animator>().SetTrigger("FwdR");
        }

        if (EndDT.GetComponent<DialogueTrigger>().dialogue.DialogueMode == Dialogue.DialogueState.InProgress)
        {
            orbs[0].SetActive(false);
            orbs[1].SetActive(false);
        }
    }
}
