using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GeminiManager : MonoBehaviour
{
  
    public GameObject EndDT;
    public GameObject Snake2;

    public AudioSource SuccessSND;

    private bool played = false;

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

        
    }

    
}
