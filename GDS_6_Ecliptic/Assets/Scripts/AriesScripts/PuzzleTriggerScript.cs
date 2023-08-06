using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTriggerScript : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<DialogueTrigger>().enabled != true)
        {
            this.GetComponent<Animator>().SetTrigger("Animate");
        }
    }
}
