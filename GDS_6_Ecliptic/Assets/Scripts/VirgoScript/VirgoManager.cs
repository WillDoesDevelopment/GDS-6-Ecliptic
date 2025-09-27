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
    public PlayerController CC;
	
    //Player controls. Saves hijacking the player controller.
    private Ecliptic02 InputController;
    
    //Player starts holding down "Accept" button.
    private float startTime;
    
    //Finished the credits screens.
    private bool finished = false;

    //Hold down button for x seconds to move to next scene.
    [SerializeField] private float waitTime = 5;

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

        if (finished)
        {
			if (InputController.Player.Accept.WasPressedThisFrame())
			{
				startTime = Time.time;
			}
			else if (InputController.Player.Accept.IsPressed())
			{
				TimeCheck();
			}
		}
    }

	private void TimeCheck()
	{
        //Are we at over X seconds? Sick, lit, epic. Next scene.
        if(Time.time - startTime >= waitTime)
        {
			SceneManager.LoadScene(level);
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
        yield return new WaitForSeconds(3f);
        title.SetActive(true);
        yield return new WaitForSeconds(3f);
		print("Going");
		credits.SetActive(true);
        finished = true;
	}
}
