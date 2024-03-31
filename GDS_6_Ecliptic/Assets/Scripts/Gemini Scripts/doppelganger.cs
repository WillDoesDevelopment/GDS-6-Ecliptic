using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doppelganger : MonoBehaviour
{
    public GameObject player;
    public float xOffset = 0;
    public float zOffset = 0;
    Vector3 offsetVector;

    public Animator copyAnim;
    // Start is called before the first frame update
    void Start()
    {
        offsetVector = new Vector3(xOffset, 0, zOffset);
        //copyAnim = player.GetComponent<PlayerController>().PlayerAnim;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offsetVector;
        transform.rotation = player.transform.rotation;

        copyAnim.SetBool("Walking", player.GetComponent<PlayerController>().walking);
        copyAnim.speed = player.GetComponent<PlayerController>().walkingSpeed;

    }
}
