using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class dialogueEvent : MonoBehaviour
{
    [SerializeField] public UnityEvent evt;

    // Start is called before the first frame update
    void Start()
    {
        if(evt == null)
        {
            evt = new UnityEvent();
        }
    }

    public bool OnDialogueTriggered()
    {
        return true;
    }
}
