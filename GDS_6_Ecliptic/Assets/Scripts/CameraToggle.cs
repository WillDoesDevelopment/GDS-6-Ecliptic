using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraToggle : MonoBehaviour
{
    public GameObject VirtualCamera;
    public DialogueTrigger DT;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ToggleCamera();
    }

    public void ToggleCamera()
    {
        if(DT.dialogue.DialogueMode == Dialogue.DialogueState.InProgress)
        {
            VirtualCamera.gameObject.SetActive(true);
        }
        else
        {
            VirtualCamera.gameObject.SetActive(false);
        }
    }

}
