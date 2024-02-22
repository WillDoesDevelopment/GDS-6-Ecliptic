using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpianWalk : MonoBehaviour
{
    public GameObject player;
    public AudioSource AudioSource;
    Vector3 pastPosition;
    Vector3[] Positions = new Vector3[4];
    int i = 0;

    float timer = 1f;
    bool pause = true;

    // Start is called before the first frame update
    void Start()
    {
        pastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <0)
        {
            timer += 0.2f;

            if (transform.position != pastPosition)
            {
                
                if(pause && Vector3.Distance(transform.position, player.transform.position) < 10)
                {
                    Debug.Log("sfx play");
                    AudioSource.Play();
                    pause = false;
                }
                
                
                pastPosition = transform.position;
            }            
            else if(!pause)
            {
                pause = true;
                AudioSource.Pause();
                Debug.Log("sfx pause");
            }

        }
    }

}
