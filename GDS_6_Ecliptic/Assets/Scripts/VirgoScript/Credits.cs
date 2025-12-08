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
    public Dialogue dia;
    public GameObject room;
    public bool creditsOn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (dia.DialogueMode == Dialogue.DialogueState.Finished && creditsOn == false)
        {
            StartCoroutine(titleOn());
        }


    }

    IEnumerator titleOn()
    {
        creditsOn = true;
        yield return new WaitForSeconds(3f);
        room.SetActive(false);
        yield return new WaitForSeconds(2f);
        title.SetActive(true);
        StartCoroutine(loadEnd());
    }

    //this runs through the credits animation, turns off the title and at the end of the credits reel, goes back to the first scene (the very start main menu of the game essentially resetting it)
    IEnumerator loadEnd()
    {
        
        credits.SetActive(true);
        yield return new WaitForSeconds(7f);
        title.SetActive(false);
        yield return new WaitForSeconds(83f);
        SceneManager.LoadScene(level);
    }
}
