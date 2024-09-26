using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class geminibeats : MonoBehaviour
{
    public GameObject Ophieno2;
    public DialogueTrigger DT;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (DT.OnEvent == true)
        {
            DT.OnEventCheck();
            Ophieno2.SetActive(true);
            DT.OnEvent = false;

        }
    }
}
