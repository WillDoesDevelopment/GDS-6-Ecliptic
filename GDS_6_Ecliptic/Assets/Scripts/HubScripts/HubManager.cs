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
    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
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
                SceneManager.LoadScene(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SceneManager.LoadScene(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SceneManager.LoadScene(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SceneManager.LoadScene(4);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SceneManager.LoadScene(5);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SceneManager.LoadScene(6);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                SceneManager.LoadScene(7);
            }

        }
    }
    // wher called it accesses a Door status script and loads the designated scene
    public void SendToScene(DoorStatus DS)
    {
        
        if (DS.IsOpen == true)
        {
           
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
    public void SendToHub()
    {
        SceneManager.LoadScene(1);
    }

    public IEnumerator SendToSceneCoroutine(DoorStatus DS)
    {
        //Debug.Log("Happening");
        TransitionAnim.SetTrigger("Animate");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(DS.SceneNum);
    }
}
