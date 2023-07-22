using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public DoorStatus DS;

    public GameObject Player;

    public HubManager HM;
    void Start()
    {
        HM = FindObjectOfType<HubManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == Player)
        {
            HM.SendToScene(DS);
        }
    }
}
