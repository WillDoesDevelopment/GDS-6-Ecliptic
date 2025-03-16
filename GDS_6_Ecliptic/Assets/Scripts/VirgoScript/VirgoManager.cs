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
    private bool isFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CreditsCheck();

        if (Input.GetKey(KeyCode.Return) ^ Input.GetKey(KeyCode.JoystickButton1) && isFinished == true)
        {
           
                StartCoroutine(Bye());
            
        }
    }

    public void CreditsCheck()
    {
        if (DT.dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            CC.playerState = PlayerState.Paused;
            StartCoroutine(loadMenu());
        }
    }
    IEnumerator loadMenu()
    {
        Debug.Log("Credits");
        credits.SetActive(true);
        yield return new WaitForSeconds(1f);
        isFinished = true;

    }

    IEnumerator Bye()
    {
        print("Going");
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(level);
    }




}
