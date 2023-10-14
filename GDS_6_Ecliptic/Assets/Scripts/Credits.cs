using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{

    public GameObject credits;
    public GameObject title;
    public string level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void creditsactive()
    {
        loadMenu();

    }

    IEnumerator loadMenu()
    {
        Debug.Log("Credits");
        yield return new WaitForSeconds(10f);
        credits.SetActive(true);
        title.SetActive(false);
        yield return new WaitForSeconds(90f);
        SceneManager.LoadScene(level);
    }
}
