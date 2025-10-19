using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class VirgoManager : MonoBehaviour
{
    public DialogueTrigger DT;
    public GameObject VirtualCam;

    public GameObject credits;
    public GameObject title;
    public string level;

    private bool stop = false;
	
    //Player controls. Saves hijacking the player controller.
    private Ecliptic02 InputController;


	// Start is called before the first frame update.
	void Start()
    {
		InputController = new Ecliptic02();
		InputController.Enable();
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
            
            StartCoroutine(loadMenu());
            return;
            
        }
    }
    IEnumerator loadMenu()
    {
        
        Debug.Log("Credits");
        yield return new WaitForSeconds(3f);
        title.SetActive(true);
        yield return new WaitForSeconds(3f);
		print("Going");
        credits.SetActive(true);
        yield return new WaitForSeconds(85f);
        reLoad();

    }

    void reLoad()
    {

            SceneManager.LoadScene(level);

    }
}
