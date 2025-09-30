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

    public int StageNumber;
    public bool BackToHub;

    public Renderer rend;
    public Material[] mat;

    //public AudioSource DoorSnd;
    
    // at the moment each door script needs to know about the hub manager
    public HubManager HM;
    void Start()
    {
        // instead of finding it in editor for each door
        //HM = FindObjectOfType<HubManager>();

        ThisAnim = this.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        DoorOpenCheck();

        DoorEnteredCheck();

    }

    private void FixedUpdate()
    {
        if(DS.IsOpen == true)
        {
            rend.material = mat[0];
        }
    }
    public void DoorOpenCheck()
    {
        if (proximity(Radius) && DS.IsOpen == true)
        {
            rend.material = mat[0];
            ThisAnim.SetBool("Animate", true); 

        }
        else
        {
            ThisAnim.SetBool("Animate", false);
            rend.material = mat[1];
        }
    }
    public void DoorEnteredCheck()
    {
        if (proximity(2))
        {
            if (BackToHub)
            {
                HM.SendToHub(DS);
                HM.SetGameStage(StageNumber);
            }
            else
            {
                HM.SendToScene(DS);
                HM.SetGameStage(StageNumber);
            }
        }
    }

    public bool proximity(float Rad)
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < Rad)
        {
            return true;
        }

        else
        {
            return false;
        }


    }

}
