using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VirgoManager : MonoBehaviour
{
    public DialogueTrigger DT;
    public GameObject VirtualCam;

    public GameObject credits;
    public GameObject title;
    public string level;
    public PlayerController CC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CreditsCheck();
    }

    public void CreditsCheck()
    {
        if (DT.dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            //Debug.Log("freezing");
            CC.playerState = PlayerState.Paused;
            VirtualCam.SetActive(true);
            StartCoroutine(loadMenu());
        }
    }
    IEnumerator loadMenu()
    {
        Debug.Log("Credits");
        yield return new WaitForSeconds(10f);
        credits.SetActive(true);
        title.SetActive(false);
        yield return new WaitForSeconds(90f);
        SceneManager.LoadScene(level);
    }
}
