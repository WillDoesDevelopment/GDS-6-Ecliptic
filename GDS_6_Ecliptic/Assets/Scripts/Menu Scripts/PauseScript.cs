using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseScript : MonoBehaviour
{
    public static bool Paused = false;

    public EventSystem eventSystem;

    public GameObject PauseUI;
    public GameObject SettingsFirstSelect;
    public GameObject PauseFirstSelect;

    public AudioSource PauseSnd;
    public AudioSource SelectSnd;
    public AudioSource PressedSnd;

    private GameObject curentlySelected;
    // Start is called before the first frame update
    void Start()
    {
        eventSystem = EventSystem.current;
    }

    // Update is called once per frame
    void Update()
    {
        SelectedSNDCheck();
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
           //if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            if(eventSystem.currentSelectedGameObject != curentlySelected)
            {
                curentlySelected = eventSystem.currentSelectedGameObject;
                Debug.Log("working");
                SelectSnd.Play();
            }

        }
    }
    public void Resume()
    {
        PauseSnd.Play();
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }
    public void Pause()
    {
        PauseSnd.Play();
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeFirstSelected()
    {
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
}
