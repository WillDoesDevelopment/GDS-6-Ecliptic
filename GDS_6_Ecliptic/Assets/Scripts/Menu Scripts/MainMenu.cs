using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string level;

    public GameObject[] canvas;

    public Animator anim;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadGame()
    {
        canvas[0].SetActive(false);
        anim.GetComponent<Animator>().SetTrigger("Click");
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
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(level);
    }


}
