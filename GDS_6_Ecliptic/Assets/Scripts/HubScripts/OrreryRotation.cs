using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrreryRotation : MonoBehaviour
{

    public GameObject[] OrreryArms;
    public float[] RandomRotations;

    public float LerpSpeed;

    public static int SceneCounter;

    //public DialogueTrigger.DialogueState DialogueState = new DialogueTrigger.DialogueState();
    public GameObject DT;
    // Start is called before the first frame update
    void Start()
    {
        if(DT == null)
        {
            DT = GameObject.FindObjectOfType<DialogueTrigger>().gameObject;
            
        }
        
        RandomRotations = new float[OrreryArms.Length];
        for (int i = 0; i< OrreryArms.Length; i++)
        {
            RandomRotations[i] = Random.Range(-.1f, .1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
/*        if(DT == null)
        {
            Debug.Log("is null");
        }*/
        
        if(DT.GetComponent<DialogueTrigger>().dialogue.DialogueMode != Dialogue.DialogueState.Finished)
        {

            for (int i = 0; i < OrreryArms.Length; i++)
            {
                OrreryArms[i].transform.eulerAngles += new Vector3(0, RandomRotations[i], 0);
            }

        }
        else
        {
            for (int i = 0; i < OrreryArms.Length; i++)
            {
                if (i != 1)
                {
                    //Debug.Log("Working");
                    OrreryArms[i].transform.eulerAngles = Vector3.Lerp(OrreryArms[i].transform.eulerAngles,new Vector3(0, 27*(i), 0),LerpSpeed);

                }
                else
                {

                    Debug.Log(OrreryArms[1]);
                    OrreryArms[1].transform.eulerAngles = Vector3.Lerp(OrreryArms[1].transform.eulerAngles, new Vector3(0, 90, 0), LerpSpeed);
                }

            }
        }
    }
}
