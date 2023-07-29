using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubManager : MonoBehaviour
{
    public GameObject[] doors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // wher called it accesses a Door status script and loads the designated scene
    public void SendToScene(DoorStatus DS)
    {
        if(DS.IsOpen == true)
        {
            SceneManager.LoadScene(DS.SceneNum);
        }
    }

    public void SendToHub()
    {
        SceneManager.LoadScene(0);
    }
}
