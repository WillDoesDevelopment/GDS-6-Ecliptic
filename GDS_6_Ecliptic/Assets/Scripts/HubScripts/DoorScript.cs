using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    //viewable in editor
    public DoorStatus DS;

    //for collision checking
    public GameObject Player;
    
    // at the moment each door script needs to know about the hub manager
    public HubManager HM;
    void Start()
    {
        // instead of finding it in editor for each door
        HM = FindObjectOfType<HubManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if the door this is attached to collides with player, pass in DoorStatus into the corrisponding hub manager script
        if(collision.gameObject == Player)
        {
            HM.SendToScene(DS);
        }
    }
}
