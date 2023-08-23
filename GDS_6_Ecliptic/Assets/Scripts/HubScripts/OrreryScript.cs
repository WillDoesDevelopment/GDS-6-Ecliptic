using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrreryScript : MonoBehaviour
{
    public BridgeScript BS;
    
    public GameObject[] OrreryArms;
    public float[] RandomRotations;

    public DialogueTrigger[] StartDialoguetriggers;
    public float LerpSpeed;

    public static int SceneCounter;

    //public DialogueTrigger.DialogueState DialogueState = new DialogueTrigger.DialogueState();
    public DialogueTrigger DT;
    // Start is called before the first frame update

    void Start()
    {
        Cursor.lockState = CursorLockMode.None; //.........................................................Cursor
        StartDialogue();
        BS.Disconnect();
        
        // creates the arrays of random rotations for the Orrery
        /*RandomRotations = new float[OrreryArms.Length];
        for (int i = 0; i< OrreryArms.Length; i++)
        {
            RandomRotations[i] = Random.Range(-0.1f, 0.1f);
        }*/
    }

    // Update is called once per frame
    void Update()
    {

        
        if(DT.dialogue.DialogueMode != Dialogue.DialogueState.Finished)
        {

            AmbientSpin();

        }
        else
        {
            SpinToPosition();
        }
    }

    public void AmbientSpin()
    {
        for (int i = 0; i < OrreryArms.Length; i++)
        {
            OrreryArms[i].transform.eulerAngles += new Vector3(0, RandomRotations[i], 0);
        }
    }
    public void SpinToPosition()
    {
        for (int i = 0; i < OrreryArms.Length; i++)
        {
            if (i != HubManager.LevelNumber)
            {
                //Debug.Log("Working");
                OrreryArms[i].transform.eulerAngles = Vector3.Lerp(OrreryArms[i].transform.eulerAngles, new Vector3(0, 27 * (i), 0), LerpSpeed);

            }
            else
            {

                //Debug.Log(OrreryArms[HubManager.LevelNumber]);
                OrreryArms[HubManager.LevelNumber].transform.eulerAngles = Vector3.Lerp(OrreryArms[HubManager.LevelNumber].transform.eulerAngles, new Vector3(0, 90, 0), LerpSpeed);
                BS.door = OrreryArms[HubManager.LevelNumber].transform.GetChild(0).gameObject;

                BS.Connect();
                //Debug.Log("connect");

            }

        }
    }

    public void StartDialogue()
    {
        StartDialoguetriggers[HubManager.LevelNumber].gameObject.SetActive(true);
    }
}
