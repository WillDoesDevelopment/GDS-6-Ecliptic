using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemEndDoor : MonoBehaviour
{
    public GameObject Ophieno2;
    public GameObject polcasDT;
    public GameObject finalDT;
    public VFXCircleHandler VFXCH;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {


        if (polcasDT.GetComponent<DialogueTrigger>().dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            finalDT.GetComponent<DialogueTrigger>().OnEventCheck();
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Ophieno2.SetActive(false);
            VFXCH.circleVFXStart();
        }

    }
}
