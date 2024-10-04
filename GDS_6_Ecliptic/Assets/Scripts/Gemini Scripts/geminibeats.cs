using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class geminibeats : MonoBehaviour
{
    public GameObject Ophieno2;
    public DialogueTrigger DT;
    public Dialogue prevDia;
    public BoxCollider BC;
    public VFXCircleHandler VFXCH;
    public GameObject purpleOrb;

    // Start is called before the first frame update
    void Start()
    {
        BC = GetComponent<BoxCollider>();
        VFXCH.circleVFXStart();

    }

    private void Update()
    {
        if (prevDia.DialogueMode == Dialogue.DialogueState.Finished)
        {
            BC.isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (DT.OnEvent == true && BC.isTrigger == true)
        {
            DT.OnEventCheck();
            Ophieno2.SetActive(true);
            purpleOrb.SetActive(true);
            DT.OnEvent = false;
        }

    }
}
