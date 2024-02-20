using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script belongs to each arrow
public class arrowScript : MonoBehaviour
{
    public float speed = 5.0f;
    public float destroyTime = 10;
    float arrowLength = 1.3f;
    public GameObject burstPrefab;

    public RoomManager RM;

    void Start()
    {
        RM = FindObjectOfType<RoomManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        transform.position += transform.forward * speed * Time.deltaTime;

        //Time destroy safeguard
        destroyTime -= Time.deltaTime;
        if (destroyTime < 0)
        {
            Destroy(gameObject);
        }

        //Collision check
        var rayPos = transform.position - transform.forward * arrowLength * 0.5f;       //Rear point of arrow
        RaycastHit hit;
        Debug.DrawLine(rayPos, rayPos + transform.forward * arrowLength, Color.blue, 0.01f);
        if (Physics.Raycast(new Ray(rayPos, transform.forward), out hit, arrowLength))  //Raycast forward
        {
            Debug.DrawLine(rayPos, rayPos + transform.up, Color.green, 0.01f);
            GameObject Temp = hit.collider.gameObject;

            WallCollisionCheck(Temp);
            PlayerColissionCheck(Temp);
            RamCollisionCheck(Temp);

        }
    }
    public void WallCollisionCheck(GameObject Temp)
    {
        if (Temp.layer == LayerMask.NameToLayer("Wall"))         //Check if hitting wall
        {
            Burst();
            Destroy(gameObject);
        }
    }
    public void PlayerColissionCheck(GameObject Temp)
    {
        if (Temp.CompareTag("Player"))                               //Check if hitting Player
        {
            Burst();
            Debug.Log("Hit Player");
            RM.Reset();
        }
    }

    public void RamCollisionCheck(GameObject Temp)
    {
        if (Temp.CompareTag("Destroyable"))                                  //Check if hitting Ram
        {
            Burst();

            Debug.Log("Hit Ram");

            Temp.SetActive(false);

            Temp.GetComponent<DialogueTrigger>().OnEventCheck();
            Temp.GetComponent<DialogueTrigger>().OnEvent = false;


            RM.PlaySnd(RM.deadRamSnd);
        }
    }
    void Burst()
    {
        var newObject = Instantiate(burstPrefab);                                                    //Create Arrow
        newObject.transform.position = transform.position;
        Destroy(newObject, 1f);
    }
    
    

}

