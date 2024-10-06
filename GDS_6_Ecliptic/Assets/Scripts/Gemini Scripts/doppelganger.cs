using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doppelganger : MonoBehaviour
{
    public GameObject player;
    public float xOffset = 0;
    public float zOffset = 0;

    public bool mirror = false;
    int mirrorInt = 1;
    
    float dopX;
    float dopY;
    float dopZ;

    Vector3 zeroY;
    float playerHeight;

    public LayerMask floorLayer;
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
        zeroY = new Vector3(1, 0, 1);
        playerHeight = 1.080006f;       
    }

    // Update is called once per frame
    void Update()
    {
        MirrorCheck();

        if(mirror)
        {
            dopX = -player.transform.position.x;
        }
        else
        {
            dopX = player.transform.position.x + xOffset;
        }
        dopZ = player.transform.position.z + zOffset;

        FallingCheck();
        transform.position = new Vector3(dopX, dopY, dopZ);
        
        if (mirror)
        {
            transform.rotation = new Quaternion(player.transform.rotation.x, -player.transform.rotation.y, player.transform.rotation.z, player.transform.rotation.w);
        }
        else
        {
            transform.rotation = player.transform.rotation;
        }
        dopZ = player.transform.position.z + zOffset;
        PlayerWalls();

        copyAnim.SetBool("Walking", player.GetComponent<PlayerController>().walking);
        copyAnim.speed = player.GetComponent<PlayerController>().walkingSpeed;  

    }


    public void PlayerWalls()
    {
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
            wallL.transform.position = (hit.point - rayPos) * mirrorInt + player.transform.position;
        }
        else { wallL.transform.position = new Vector3(0, -10, 0); }

        Debug.DrawLine(rayPos, rayPos + Vector3.right * maxLength, Color.green, 0.5f);
        if (Physics.Raycast(new Ray(rayPos, Vector3.right), out hit, maxLength, wallLayer))                        //Raycast right
        {
            wallR.transform.position = (hit.point - rayPos) * mirrorInt + player.transform.position;
        }
        else { wallR.transform.position = new Vector3(0, -10, 0); }
    }
    public void FallingCheck()
    {
        var rayPos = transform.position + new Vector3(0,1,0);
        RaycastHit hit;

        Debug.DrawLine(rayPos, rayPos + Vector3.down * maxLength, Color.green, 0.5f);
        if (Physics.Raycast(new Ray(rayPos, Vector3.down), out hit, maxLength, floorLayer))                      //Raycast forward
        {
            dopY = hit.point.y + playerHeight;
        }
                
    }

    void MirrorCheck()
    {
        Debug.DrawLine(new Vector3(-10,1,100), new Vector3(10,1,100),Color.cyan, 0.5f);
        if (transform.position.z > 110)
        {
            mirror = true;
            mirrorInt = -1;
        }
        else
        {
            mirror = false;
            mirrorInt = 1;
        }
    }
}
