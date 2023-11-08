using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    //public static bool Controller = true;
    public static bool Paused = false;

    public EventSystem eventSystem;

    public GameObject PauseUI;
    public GameObject SettingsFirstSelect;
    public GameObject PauseFirstSelect;

    public AudioSource PauseSnd;
    public AudioSource SelectSnd;
    public AudioSource PressedSnd;

    private GameObject curentlySelected;

    public GameObject[] controlsCanvas;

    public HubManager HM;

    public bool Quitting = false;
    // Start is called before the first frame update
    void Start()
    {
        HM = GameObject.FindObjectOfType<HubManager>();
        eventSystem = EventSystem.current;
        ControllerOrNot();
    }

    // Update is called once per frame
    void Update()
    {
        ControllerOrNot();
        SelectedSNDCheck();

        // when pause button is pressed we pause or unpause
        if (Input.GetKeyDown(KeyCode.Escape)  || Input.GetButtonDown("Cancel") )
        {
            if (Paused == false)
            {
                Pause();
            }
            else
            {
                Resume();
            }

        }
    }
    public void SelectedSNDCheck()
    {
        if(Paused == true)
        {
            //plays the sound if the button has changed
            if(eventSystem.currentSelectedGameObject != curentlySelected)
            {
                curentlySelected = eventSystem.currentSelectedGameObject;
                //Debug.Log("working");
                SelectSnd.Play();
            }

        }
    }
    public void Resume()
    {
        // gets rid of menu. will become an animation later
        PauseSnd.Play();
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }
    public void Pause()
    {
        // brings up the pause menu. will be an animation later
        PauseSnd.Play();
        ControllerOrNot();
        PauseUI.SetActive(true);
        if (!Quitting)
        {
            Time.timeScale = 0f;
        }
        Paused = true;
    }
    public void Quit()
    {
        // quits the game
        //Application.Quit();
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        HM.SendToMainCo(0);
    }

    public void ChangeFirstSelected()
    {
        // since we are using controller and there is multiple menus, we need to change the initial selected button
        if(eventSystem.currentSelectedGameObject == SettingsFirstSelect)
        {
            Debug.Log("ChangeBack");
            eventSystem.SetSelectedGameObject(PauseFirstSelect);

        }
        else
        {
            Debug.Log("Change");
            eventSystem.SetSelectedGameObject(SettingsFirstSelect);
        }
    }

    public void ControllerOrNot()
    {
        // detects if controller and sets the appropriate things active and inactive 
        Input.GetJoystickNames();
        if (Input.GetJoystickNames().Length == 0)
        {
            //print("Keyboard");
            controlsCanvas[0].SetActive(false);
            controlsCanvas[1].SetActive(true);
        }
        else
        {
            //print("Controller");
            controlsCanvas[0].SetActive(true);
            controlsCanvas[1].SetActive(false);
        }
    }
}
