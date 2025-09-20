using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VirgoManager : MonoBehaviour
{
    public DialogueTrigger DT;
  
    public GameObject VirgoDoor;

<<<<<<< HEAD
=======
    public GameObject credits;
    public GameObject title;
    public string level;
    public PlayerController CC;
    private bool isFinished = false;

>>>>>>> SMASH-DEMO
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
           
                
            
        }
    }

    public void CreditsCheck()
    {
        if (DT.dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
<<<<<<< HEAD
            VirgoDoor.GetComponent<Animator>().SetBool("Reveal", true);
        }
    }
=======
            CC.playerState = PlayerState.Paused;
            StartCoroutine(loadMenu());
        }
    }
    IEnumerator loadMenu()
    {
        Debug.Log("Credits");
        yield return new WaitForSeconds(3f);
        title.SetActive(true);
        yield return new WaitForSeconds(3f);
        isFinished = true;
        StartCoroutine(Bye());

    }

    IEnumerator Bye()
    {
        print("Going");
        credits.SetActive(true);
        yield return new WaitForSeconds(15f);
        SceneManager.LoadScene(level);
    }



>>>>>>> SMASH-DEMO

}
