using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartOnContact : MonoBehaviour
{
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }
        
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collide");
        if(other.gameObject == Player)
        {
            Debug.Log("hit player");
            Debug.Log("Respawning");

            Player.GetComponent<PlayerController>().Respawn();
        }
    }
}
