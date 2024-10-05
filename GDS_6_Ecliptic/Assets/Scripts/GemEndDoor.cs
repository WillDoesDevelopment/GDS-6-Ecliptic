using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemEndDoor : MonoBehaviour
{
    public GameObject Ophieno2;
    public GameObject SnakeDT;
    public GameObject polcasDT;
    public GameObject snek;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {

        if (SnakeDT.GetComponent<DialogueTrigger>().OnEvent == true && polcasDT.GetComponent<DialogueTrigger>().dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            SnakeDT.GetComponent<DialogueTrigger>().OnEventCheck();
            SnakeDT.GetComponent<DialogueTrigger>().OnEvent = false;
            snek.SetActive(false);
            print("Snek Hit");

        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Ophieno2.SetActive(false);
        }

    }
}
