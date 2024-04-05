using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doppelganger : MonoBehaviour
{
    public GameObject player;
    public float xOffset = 0;
    public float zOffset = 0;
    Vector3 offsetVector;

    public LayerMask wallLayer;
    public GameObject wallF;
    public GameObject wallB;
    public GameObject wallL;
    public GameObject wallR;

    float maxLength = 10.0f;

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

        var rayPos = transform.position;
        RaycastHit hit;

        Debug.DrawLine(rayPos, rayPos + Vector3.forward * maxLength, Color.green, 0.5f); 
        if (Physics.Raycast(new Ray(rayPos, Vector3.forward), out hit, maxLength, wallLayer))                      //Raycast forward
        {
            wallF.transform.position = hit.point - rayPos + player.transform.position;
        }
        else { wallF.transform.position = new Vector3(0, -10, 0); }

        Debug.DrawLine(rayPos, rayPos + Vector3.back * maxLength, Color.green, 0.5f);
        if (Physics.Raycast(new Ray(rayPos, Vector3.back), out hit, maxLength, wallLayer))                        //Raycast back
        {
            wallB.transform.position = hit.point - rayPos + player.transform.position;
        }
        else { wallB.transform.position = new Vector3(0, -10, 0); }

        Debug.DrawLine(rayPos, rayPos + Vector3.left * maxLength, Color.green, 0.5f);
        if (Physics.Raycast(new Ray(rayPos, Vector3.left), out hit, maxLength, wallLayer))                        //Raycast left
        {
            wallL.transform.position = hit.point - rayPos + player.transform.position;
        }
        else { wallL.transform.position = new Vector3(0, -10, 0); }

        Debug.DrawLine(rayPos, rayPos + Vector3.right * maxLength, Color.green, 0.5f);
        if (Physics.Raycast(new Ray(rayPos, Vector3.right), out hit, maxLength, wallLayer))                        //Raycast right
        {
            wallR.transform.position = hit.point - rayPos + player.transform.position;
        }
        else { wallR.transform.position = new Vector3(0, -10, 0); }
    }
}
