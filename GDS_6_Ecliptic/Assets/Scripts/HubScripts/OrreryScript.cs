using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class OrreryScript : MonoBehaviour
{
    public BridgeScript BS;
    bool connected = false;
    
    public GameObject[] OrreryArms;
    public float[] RandomRotations;

    public DialogueTrigger[] StartDialoguetriggers;
    public float LerpSpeed;

    public static int SceneCounter;
    
    float t = 0;
    float startY;

    public GameObject[] discs;
    public GameObject[] constellations;

    public bool startMode = false;
    bool targetDoorActivated = false;
    //public DialogueTrigger.DialogueState DialogueState = new DialogueTrigger.DialogueState();
    public DialogueTrigger DT;
    // Start is called before the first frame update

    void Start()
    {
      
        BS.door = OrreryArms[HubManager.LevelNumber].transform.GetChild(0).gameObject;
        Cursor.lockState = CursorLockMode.None; //.........................................................Cursor
        StartDialogue();
        BS.Disconnect();
        connected = false;

        ActivateDiscs();
        DeactivateOrreryDoors();

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
        if (startMode == false)
        {
            if (DT.dialogue.DialogueMode != Dialogue.DialogueState.Finished)
            {
                AmbientSpin();

            }
            else
            {
                SpinToPosition();
            }

        } 

    }
    public void ActivateDiscs()
    {
        for (int i = 0; i< HubManager.LevelNumber; i++)
        {
            discs[i].SetActive(true);
        }
    }

    public void AmbientSpin()
    {

        for (int i = 0; i < OrreryArms.Length; i++)
        {
            OrreryArms[i].transform.eulerAngles += new Vector3(0, RandomRotations[i] * Time.deltaTime, 0); //Check with will if this is based on framerate..................
        }
        startY = OrreryArms[HubManager.LevelNumber].transform.eulerAngles.y;
    }
    public void SpinToPosition()
    {
        /*
        if (HubManager.LevelNumber > discs.Length)
        {            
        }
        */

        if(!targetDoorActivated)
        {
            ActivateTargetDoor(HubManager.LevelNumber);
            targetDoorActivated = true;
        }
        
        if (t<1f)
        {
            for (int i = 0; i < OrreryArms.Length; i++)
            {
                if (i != HubManager.LevelNumber)        //Other doors keep rotating
                {
                    OrreryArms[i].transform.eulerAngles += new Vector3(0, RandomRotations[i] * Time.deltaTime, 0);
                }
                else
                {
                    t += Time.deltaTime * 0.6f;              //t to 90* 0-1   
                    t = Mathf.Clamp01(t);
                    var u = -(t - 2) * t;                    //convert to parabola (0,0) to (1,1)
                    OrreryArms[i].transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(startY, 90, u), 0); //Main door move to 90* when t = 1 
                    //Debug.Log(t);

                }
            }
        }

        if(t>=1f)
        {
            if (connected == false)
            {
                BS.Connect();
                connected = true;
                discs[HubManager.LevelNumber].SetActive(true);
                constellations[HubManager.LevelNumber].SetActive(true);
            }

            for (int i = 0; i < OrreryArms.Length; i++) //Stop doors overlapping main door from 80* to 100*
            {
                if (BS.door != OrreryArms[i].transform.GetChild(0).gameObject)
                {

                    if (OrreryArms[i].transform.eulerAngles.y > 80f - 2* (12f - i) && OrreryArms[i].transform.eulerAngles.y < 100f + 2* (12f - i)) //spread doors out more when closer to the center
                    {
                        OrreryArms[i].transform.eulerAngles += new Vector3(0, RandomRotations[i], 0) * Time.deltaTime;
                    }
                }
            }
        }
    }
    

    public void StartDialogue()
    {

          StartDialoguetriggers[HubManager.LevelNumber].gameObject.SetActive(true);
        
    }

    public void DeactivateOrreryDoors()
    {
        foreach (var t in OrreryArms)
        {
            var doorScript = t.transform.GetChild(0).GetChild(1).gameObject.GetComponent<DoorScript>(); //I'm sorry about this mess. I'm a better programmer now from when we started the project - Gyles
            doorScript.EnterRadius = -1;    //negative distances should be unobtainable.
            doorScript.AnimateRadius = -1;
        }
    }

    public void ActivateTargetDoor(int doorNumber)
    {
        var doorScript = OrreryArms[doorNumber].transform.GetChild(0).GetChild(1).gameObject.GetComponent<DoorScript>();
        doorScript.EnterRadius = 2;
        doorScript.AnimateRadius = 8;

    }
}
