using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GeminiManager : MonoBehaviour
{
  
    public GameObject EndDT;
    public GameObject Snake2;
    public GameObject SnekDt;

    public AudioSource SuccessSND;

    private bool played = false;
    private bool played2 = false;

    public GameObject animCanvas;
    public PlayerController cont;

    // Start is called before the first frame update
    void Start()
    {
        //VFXCH.circleVFXStart();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (EndDT.GetComponent<DialogueTrigger>().dialogue.DialogueMode == Dialogue.DialogueState.Finished && played == false)
        {
            SuccessSND.Play();
            played = true;
            Snake2.SetActive(true);
            

        }

        if(SnekDt.GetComponent<DialogueTrigger>().dialogue.DialogueMode == Dialogue.DialogueState.Finished && played2 == false)
        {
            StartCoroutine(gemAnimCanv());
        }

        
    }

    IEnumerator gemAnimCanv()
    {
        played2 = true;
        yield return new WaitForSeconds(1f);
        cont.canWalk = false;
        animCanvas.SetActive(true);
        yield return new WaitForSeconds(14f);
        animCanvas.SetActive(false);
        cont.canWalk = true;
        
    }

    
}
