using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubCamAnim : MonoBehaviour
{

    public Animator anim;
    public GameObject DT;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ZoomIn());
    }

    private void Update()
    {
        if (DT.GetComponent<DialogueTrigger>().dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            StartCoroutine(ZoomOut());
        }
        else
        {
            return;
        }
    }

    IEnumerator ZoomIn()
    {
        yield return new WaitForSeconds(3.5f);
        anim.SetTrigger("Enhance");
    }

    IEnumerator ZoomOut()
    {
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("Too Much");

    }
}

