using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaurusOrbus : MonoBehaviour
{

    public GameObject EndDT;
    public Animator anim;
    public GameObject orb;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EndDT.GetComponent<DialogueTrigger>().dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            anim.GetComponent<Animator>().SetTrigger("Walk");
            StartCoroutine(TauOrb());
        }
    }

    IEnumerator TauOrb()
    {
        print("Started");
        yield return new WaitForSeconds(20.0f);
        Destroy(orb);
    }
}
