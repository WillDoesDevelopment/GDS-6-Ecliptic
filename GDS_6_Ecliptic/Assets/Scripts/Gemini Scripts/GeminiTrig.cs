using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GeminiTrig : MonoBehaviour
{
    public GameObject WinConditionSND;
    public GameObject prevDia;
    public GameObject polcasDT;
    public GameObject[] dialoguecanv;
    public Sprite[] playerNew;
    public Sprite[] playerOld;
    public PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void Update()
    {
        if (prevDia.GetComponent<DialogueTrigger>().dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            dialoguecanv[2].GetComponent<RectTransform>().transform.localScale = new Vector3(0.03f, 0.24f, 0.24f);
            dialoguecanv[0].GetComponent<Image>().sprite = playerOld[0];
            dialoguecanv[1].GetComponent<Image>().sprite = playerOld[1];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
            print("HIT PLAYER");
            if(polcasDT.GetComponent<DialogueTrigger>().dialogue.DialogueMode == Dialogue.DialogueState.NotStarted)
            {
                polcasDT.GetComponent<DialogueTrigger>().OnEventCheck();
            }
            
            dialoguecanv[2].GetComponent<RectTransform>().transform.localScale = new Vector3(0.02f, 0.16f, 0.16f);
            WinConditionSND.GetComponent<AudioSource>().Play();
            dialoguecanv[0].GetComponent<Image>().sprite = playerNew[0];
            dialoguecanv[1].GetComponent<Image>().sprite = playerNew[1];
        

    }


}
