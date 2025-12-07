using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    //this script handles the end credits of where our current public demo ends

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

    //this runs through the credits animation, turns off the title and at the end of the credits reel, goes back to the first scene (the very start main menu of the game essentially resetting it)
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
