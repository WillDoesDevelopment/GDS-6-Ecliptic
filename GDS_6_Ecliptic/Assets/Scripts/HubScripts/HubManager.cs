using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubManager : MonoBehaviour
{
    public static bool DebugMode = true;
    public GameObject[] doors;

    public Animator TransitionAnim;

    public static int LevelNumber = 0;
    private GameObject player;

    private bool nextScene;
    private int DSRoomNum;
   
    // Start is called before the first frame update
    void Start()
    {
        //
        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nextScene)
        {
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Submit"))
            {
                Debug.Log("Scene Changing");
                SceneManager.LoadScene(DSRoomNum);
            }

        }

        DebugModeCheck();

    }

    public void DebugModeCheck()
    {
        if (DebugMode)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                SceneManager.LoadScene(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SceneManager.LoadScene(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SceneManager.LoadScene(4);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SceneManager.LoadScene(5);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SceneManager.LoadScene(6);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SceneManager.LoadScene(7);
            }
            /*if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                SceneManager.LoadScene(8);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                SceneManager.LoadScene(9);
            }*/

        }
    }


    // all the different ways of changing scenes and setting stages for repeated scenes
    public void SendToScene(DoorStatus DS)
    {
        
        if (DS.IsOpen == true)
        {
            Debug.Log("working");
            StartCoroutine(SendToSceneCoroutine(DS));
            
        }
    }
    public void SetGameStage(int stageNum)
    {
        //this is important for remembering what game stage we are in so that the hub room can respond accordingly
        LevelNumber = stageNum;
    }
    public void SendToHub(DoorStatus DS)
    {
        //this sends the player to a scene regardless of the value in DS, IE th hub room, In theory we could just use "Send to Scene" but this helps with readability amd allows for a different outro
        if (DS.IsOpen == true)
        {
            
            StartCoroutine(SendToHubCo());

        }
    }



    // all the coroutines below
    public IEnumerator SendToMainCo(int sceneNum)
    {
        Time.timeScale = 1;
        freezePlayerActions(player);
        Debug.Log("Happening");
        TransitionAnim.SetTrigger("Prompt");
        TransitionAnim.SetTrigger("Animate");
        yield return new WaitForSeconds(2f);
        Debug.Log("Working");
        
        SceneManager.LoadScene(0);
    }
    public IEnumerator SendToSceneCoroutine(DoorStatus DS)
    {
        freezePlayerActions(player);
        //Debug.Log("Happening");
        TransitionAnim.SetTrigger("Prompt");
        TransitionAnim.SetTrigger("Animate");
        yield return new WaitForSeconds(2f);
        
        // since adding the timer i needed to set these values so we can check every frame to see if the player presses enter
        nextScene = true;
        DSRoomNum = DS.SceneNum;
    }
    public IEnumerator SendToHubCo()
    {
        
        //Debug.Log("Happening");
        TransitionAnim.SetTrigger("Animate");
        
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);

    }



    // i put these in seperate functions because there are other instances when one wants to freeze player actions other than dialogue
    //for instance in an instructions or pause menu

    //in realiity this functionality doesnt belong in the Hub manager but it is convinient to put it here
    public static void freezePlayerActions(GameObject player)
    {
        if (player.GetComponent<Grapple3>() != null)
        {
            player.GetComponent<Grapple3>().enabled = false;

        }
    }
    public static void UnfreezePlayerActions(GameObject player)
    {
        if (player.GetComponent<Grapple3>() != null)
        {
            player.GetComponent<Grapple3>().enabled = true;
        }
        if (player.GetComponent<PickUpScript>() != null)
        {
            player.GetComponent<PickUpScript>().enabled = true;
        }
    }


}
