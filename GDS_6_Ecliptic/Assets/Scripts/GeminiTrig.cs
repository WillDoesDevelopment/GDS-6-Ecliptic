using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeminiTrig : MonoBehaviour
{
    public GameObject WinConditionSND;
    public GameObject prevDia;
    public GameObject[] dialoguecanv;
    public Sprite[] playerNew;
    public Sprite[] playerOld;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void Update()
    {
        if (prevDia.GetComponent<DialogueTrigger>().dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            dialoguecanv[2].GetComponent<RectTransform>().transform.localScale = new Vector3(0.04f, 0.32f, 0.32f);
            dialoguecanv[0].GetComponent<Image>().sprite = playerOld[0];
            dialoguecanv[1].GetComponent<Image>().sprite = playerOld[1];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
            print("HIT PLAYER");
            dialoguecanv[2].GetComponent<RectTransform>().transform.localScale = new Vector3(0.02f, 0.16f, 0.16f);
            WinConditionSND.GetComponent<AudioSource>().Play();
            dialoguecanv[0].GetComponent<Image>().sprite = playerNew[0];
            dialoguecanv[1].GetComponent<Image>().sprite = playerNew[1];

    }
}
