using System.Collections;
using System.Collections.Generic;
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

    //public DialogueTrigger.DialogueState DialogueState = new DialogueTrigger.DialogueState();
    public DialogueTrigger DT;
    // Start is called before the first frame update
    public Sprite[] FadeOutSprites;
    void Start()
    {
        BS.door = OrreryArms[HubManager.LevelNumber].transform.GetChild(0).gameObject;
        Cursor.lockState = CursorLockMode.None; //.........................................................Cursor
        StartDialogue();
        BS.Disconnect();
        connected = false;

        ActivateDiscs();

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
        discs[HubManager.LevelNumber].SetActive(true);
        constellations[HubManager.LevelNumber].SetActive(true);
        if (t>1f)
        {
            

            if (connected == false)
            {
                BS.Connect();
                connected = true;
            }

            for (int i = 0; i < OrreryArms.Length; i++) //Stop doors overlapping main door from 80* to 100*
            {
                if (BS.door != OrreryArms[i].transform.GetChild(0).gameObject)
                {
              
                    if(OrreryArms[i].transform.eulerAngles.y > 80f - (12f-i) && OrreryArms[i].transform.eulerAngles.y < 100f + (12f-i))
                    {
                        OrreryArms[i].transform.eulerAngles += new Vector3(0, RandomRotations[i], 0) * Time.deltaTime;
                    }                    
                }
            }
        }
        else
        {
            for (int i = 0; i < OrreryArms.Length; i++)
            {
                if (i != HubManager.LevelNumber)        //Other doors keep rotating
                {
                    OrreryArms[i].transform.eulerAngles += new Vector3(0, RandomRotations[i] * Time.deltaTime, 0);
                }
                else
                {
                    t += Time.deltaTime * 0.6f;                //t to 90* 0-1   
                    var u = -(t - 2) * t;               //convert to parabola
                    OrreryArms[i].transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(startY, 90, u), 0); //Main door move to 90* when t = 1 
                    //Debug.Log(t);

                }
            }

        }
        
        //if (OrreryArms[HubManager.LevelNumber].transform.eulerAngles.y == 90f)



        /*
        for (int i = 0; i < OrreryArms.Length; i++)
        {
            if (i != HubManager.LevelNumber)
            {
                //Debug.Log("Working");
                OrreryArms[i].transform.eulerAngles = Vector3.Lerp(OrreryArms[i].transform.eulerAngles, new Vector3(0, 27 * (i), 0), LerpSpeed);

            }
            else   //This is called every frame and porbably shouldn't be..............................................................................
            {

                //Debug.Log(OrreryArms[HubManager.LevelNumber]);
                OrreryArms[HubManager.LevelNumber].transform.eulerAngles = Vector3.Lerp(OrreryArms[HubManager.LevelNumber].transform.eulerAngles, new Vector3(0, 90, 0), LerpSpeed);
                BS.door = OrreryArms[HubManager.LevelNumber].transform.GetChild(0).gameObject;

                if(connected == false)
                {
                    BS.Connect();
                    connected = true;
                }
                
                //Debug.Log("connect");

            }

        }
        */
    }
    

    public void StartDialogue()
    {

          StartDialoguetriggers[HubManager.LevelNumber].gameObject.SetActive(true);
        
    }

    public void SetFadeOutImage()
    {

    }
}
