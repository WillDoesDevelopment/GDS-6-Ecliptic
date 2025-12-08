using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    //basic main menu script

    public string level;

    public GameObject[] canvas;

    public GameObject[] controllerCanvas;

    public Animator anim;
    public Animator anim2;
    public GameObject orb;

    public EventSystem eventSystem;

    public GameObject PauseUI;
    public GameObject SettingsFirstSelect;
    public GameObject PauseFirstSelect;

    private bool connected = false;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void OnBeforeSplashScreen()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }



    private void Awake()
    {
        //checks if the controller is connected
        controllerCheck();
        Cursor.lockState = CursorLockMode.Locked;
 
    }

    public void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        controllerCheck();
        Debug.Log(eventSystem.currentSelectedGameObject);
        if (eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(PauseFirstSelect);
        }
    }

    //the various btn functions that are called with the corrosponding btn
    public void LoadGame(int SceneNumber)
    {
        HubManager.LevelNumber = SceneNumber;

        
        anim2.GetComponent<Animator>().SetTrigger("Woosh");
        SceneManager.LoadScene(1);
        //StartCoroutine(Load());
        
    }

    public void ActivateDemoCanvas()
    {
        canvas[2].SetActive(true);

        canvas[1].SetActive(false);
        canvas[0].SetActive(false);
    }
    public void CtrLoad()
    {
        canvas[1].SetActive(true);
        
        canvas[0].SetActive(false);
        //canvas[2].SetActive(false);
    }
    /*public void SaveLoad()
    {
        canvas[0].SetActive(false);
        canvas[2].SetActive(true);
    }*/

    public void BackBtn()
    {
        canvas[0].SetActive(true);

        canvas[1].SetActive(false);
        //canvas[2].SetActive(false);
    }

    /*public void BackMenuBtn()
    {
        canvas[2].SetActive(false);
        canvas[0].SetActive(true);
    }*/

    public void QuitGame()
    {
        //SceneManager.LoadScene(0);
        Application.Quit();
        print("Quit!");
    }

    IEnumerator Load()
    {
        //actually loads the game and has some animations to go with
        yield return new WaitForSeconds(2f);
        anim.GetComponent<Animator>().SetTrigger("Boop");
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(level);
    }

    public void ChangeFirstSelected(GameObject firstSelected)
    {
        eventSystem.SetSelectedGameObject(firstSelected);
        //Using the new input system for UI, swaps the selected btn depending on which canvas is active
        /*if (eventSystem.currentSelectedGameObject == SettingsFirstSelect)
        {
            //Debug.Log("ChangeBack");
            eventSystem.SetSelectedGameObject(PauseFirstSelect);

        }
        else
        {
            //Debug.Log("Change");
            eventSystem.SetSelectedGameObject(SettingsFirstSelect);
        }*/
    }

    public void controllerCheck()
    {
        // checks if the controller is connected
            var controllers = Input.GetJoystickNames();
            if (connected && controllers.Length > 0)
            {
                connected = false;
                
                controllerCanvas[0].SetActive(true);
                controllerCanvas[1].SetActive(false);
            Debug.Log("Connected");

            }
            else if (!connected && controllers.Length == 0)
            {
                connected = true;
                controllerCanvas[0].SetActive(false);
                controllerCanvas[1].SetActive(true);
                Debug.Log("Disconnected");
            }
        }

}
