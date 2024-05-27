using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{

    //Pause System which handles all the canvas's in the pause canvas

    public GameObject[] Pause;
    public GameObject dialouge;
    public GameObject[] settingsPanels;
    public EventSystem ES;
    public GameObject pBtn;
    public GameObject[] bBtn;
    public bool isKeyboard = false;
    public bool isXbox = false;
    public bool isPS = false;

    // Start is called before the first frame update
    void Start()
    {
        //makes sure that the pause menu is off on game start
        Pause[0].SetActive(true);
        //dialouge.SetActive(false);
        for (var i = 0; i < settingsPanels.Length; i++)
        {
            settingsPanels[i].SetActive(false);
        }

    }

    private void Update()
    {
        //These are the switches that turn the specific canvas's on and off depending on what input device is detected.
        if(this.gameObject != null)
        {
            Time.timeScale = 1f;
        }
        else if(this.gameObject != null)
        {
            Time.timeScale = 0f;
        }
        if(isKeyboard == true)
        {
            settingsPanels[1].SetActive(false);
            settingsPanels[2].SetActive(false);

            isXbox = false;
            isPS = false;

            settingsPanels[0].SetActive(true);
        }

        if (isXbox == true)
        {
            settingsPanels[0].SetActive(false);
            settingsPanels[2].SetActive(false);

            isKeyboard = false;
            isPS = false;

            settingsPanels[1].SetActive(true);
        }

        if (isPS == true)
        {
            settingsPanels[0].SetActive(false);
            settingsPanels[1].SetActive(false);

            isXbox = false;
            isKeyboard = false;

            settingsPanels[2].SetActive(true);
        }
    }

    public void BackBtn()
    {
        //This handles the back button on the canvases
        ES.firstSelectedGameObject = pBtn;

        for (var i = 0; i < settingsPanels.Length; i++)
        {
            settingsPanels[i].SetActive(false);
        }
        Pause[0].SetActive(true);
    }

    public void ResumeGame()
    {
        //This handles the resume btn
        ES.firstSelectedGameObject = pBtn;

        for (var i = 0; i < settingsPanels.Length; i++)
        {
            settingsPanels[i].SetActive(false);
        }

        Pause[0].SetActive(true);
        Pause[1].SetActive(false);
        //dialouge.SetActive(true);

    }

    public void QuitGame()
    {
        //this is the fucntion for the quit btn
        Application.Quit();
        print("quit");
    }

    public void Settings()
    {
        //This triggers the correct btn and canvas depending on the input device connected.
        settingsPanels[3].SetActive(true);
        Pause[0].SetActive(false);

        if (isKeyboard == true)
        {
            ES.firstSelectedGameObject = bBtn[0];
        }

        if (isXbox == true)
        {
            ES.firstSelectedGameObject = bBtn[1];
        }

        if (isPS == true)
        {
            ES.firstSelectedGameObject = bBtn[2];
        }

    }
}
