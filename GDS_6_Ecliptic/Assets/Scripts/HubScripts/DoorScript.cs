using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    //viewable in editor
    public DoorStatus DS;

    //for collision checking
    public GameObject Player;

    public float Radius = 5;

    public Animator ThisAnim;
    public int nextStage;

    
    // at the moment each door script needs to know about the hub manager
    public HubManager HM;
    void Start()
    {
        // instead of finding it in editor for each door
        HM = FindObjectOfType<HubManager>();

        ThisAnim = this.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (proximity() && DS.IsOpen ==true)
        {
            ThisAnim.SetBool("Animate", true);
        }
        else
        {
            ThisAnim.SetBool("Animate", false);
        }

        if (Vector3.Distance(transform.position, Player.transform.position) < 2)
        {
            HM.SetToLevel(nextStage);
            HM.SendToScene(DS);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*
        Debug.Log("Colliding");
        // if the door this is attached to collides with player, pass in DoorStatus into the corrisponding hub manager script
        if(collision.gameObject == Player && DS.IsOpen == true)
        {
            HM.SendToScene(DS);
        }
        */
    }

    public bool proximity()
    {
        //float playerDistZ = Player.transform.position.z - transform.position.z;
        //float playerDistX = Player.transform.position.x - transform.position.x;
        if (Vector3.Distance(transform.position, Player.transform.position) < Radius)
        {
            return true;
        }

        else
        {
            return false;
        }

        //Vector3.Distance(transform.position, Player.transform.position);
    }
}
