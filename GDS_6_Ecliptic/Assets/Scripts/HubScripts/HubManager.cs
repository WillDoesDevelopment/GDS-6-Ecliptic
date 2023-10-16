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
                SceneManager.LoadScene(DSRoomNum);
            }

        }
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

        //InDialogueCheck();
    }
    // wher called it accesses a Door status script and loads the designated scene
    public void SendToScene(DoorStatus DS)
    {
        
        if (DS.IsOpen == true)
        {
            Debug.Log("working");
            StartCoroutine(SendToSceneCoroutine(DS));
            
        }
    }
    public void AddOneToLevel()
    {
        LevelNumber += 1;
    }

    public void SetGameStage(int stageNum)
    {
        LevelNumber = stageNum;
    }
    public void SendToHub(DoorStatus DS)
    {
        if (DS.IsOpen == true)
        {
            
            StartCoroutine(SendToHubCo());

        }
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
        Debug.Log("freezing");
        if (player.GetComponent<Grapple3>() != null)
        {
            player.GetComponent<Grapple3>().enabled = false;
            //player.GetComponent<Grapple3>().rope.SetActive(false);
        }
/*        if (player.GetComponent<PickUpScript>() != null)
        {
            player.GetComponent<PickUpScript>().enabled = false;
        }*/
    }

    public static void UnfreezePlayerActions(GameObject player)
    {
        Debug.Log("unfreezing");
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
