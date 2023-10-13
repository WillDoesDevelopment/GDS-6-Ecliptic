using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
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

    private void Awake()
    {

        controllerCheck();
 
    }

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        controllerCheck();
    }

    public void LoadGame()
    {
        
        anim2.GetComponent<Animator>().SetTrigger("Woosh");
        StartCoroutine(Load());
        
    }

    public void CtrLoad()
    {
        canvas[0].SetActive(false);
        canvas[1].SetActive(true);
    }
    public void SaveLoad()
    {
        canvas[0].SetActive(false);
        canvas[2].SetActive(true);
    }

    public void BackBtn()
    {
        canvas[1].SetActive(false);
        canvas[0].SetActive(true);
    }

    public void BackMenuBtn()
    {
        canvas[2].SetActive(false);
        canvas[0].SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        print("Quit!");
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(2f);
        anim.GetComponent<Animator>().SetTrigger("Boop");
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(level);
    }

    public void ChangeFirstSelected()
    {
        if (eventSystem.currentSelectedGameObject == SettingsFirstSelect)
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

    public void controllerCheck()
    { 

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
