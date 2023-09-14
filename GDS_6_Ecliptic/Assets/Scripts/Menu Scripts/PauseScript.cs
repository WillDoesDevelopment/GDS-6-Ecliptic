using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public static bool Paused = false;


    public GameObject PauseUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

    public void Resume()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }
    public void Pause()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
