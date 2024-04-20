using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowSpawn2 : MonoBehaviour
{

    public GameObject Prefab;
    public float spawnRate;
    public float spawnDelay;
    public Vector3 spawnOffset;
    //public float yRot;
    public float speed;
    float sTime;
    public float destroyTime = 10;

    public AudioSource ArrowSound;



    // Start is called before the first frame update
    void Start()
    {
        sTime = spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        sTime -= Time.deltaTime;

        if(sTime < 0)
        {
            //ArrowSound.Play();
            var newObject = Instantiate(Prefab);                                                    //Create Arrow
            newObject.transform.position = transform.position + spawnOffset;                        //Location
            //newObject.transform.localScale = Vector3.one * 0.4f * Random.Range(0.8f, 1.2f);       //Scale
            newObject.transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);         //Rotation
            

            //newObject.transform.parent = transform;                                               //Parent
            newObject.GetComponent<arrowScript>().speed = speed;                                    //Speed
            newObject.GetComponent<arrowScript>().destroyTime = destroyTime;                        //Destory

            sTime += 1 / spawnRate;                                                                 //Reset timer
        }

        

    }
}
