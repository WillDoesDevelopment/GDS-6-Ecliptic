using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScorpioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public DialogueTrigger EndDialogue;
    public DoorScript DS;
    public VFXCircleHandler vfx;
    public GameObject WinConditionSND;
    public GameObject SuccessSND;
    public string level;
    void Start()
    {
        vfx.circleVFXStart();
    }

    // Update is called once per frame
    void Update()
    {
        if(EndDialogue.dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            WinConditionSND.SetActive(true);
            DS.DS.IsOpen = true;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(level);
        print("Hit");
    }

}
