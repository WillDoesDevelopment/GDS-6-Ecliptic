using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisePlatform : MonoBehaviour
{
    public GameObject proximityTrigger;
    public Vector3 endPos;
    Vector3 startPos;
    public float moveDuration = 4f;
    bool move = false;
    bool end = false;
    float timer = 0;
    float a = 0;
    float shakeSpeed = 4.5f;
    float shakeDist = 0.1f;
    public AnimationCurve raiseCurve;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (proximityTrigger.activeSelf == false && end == false)
        {
            move = true;
        }

        if (move == true)
        {
            timer += Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, moveDuration);                                            //Clamp timer to range
            a = Mathf.Abs(((shakeSpeed * timer + 0.5f) % 1f) * 4f - 2f) - 1f;                       //triangle wave
            
            transform.position = Vector3.Lerp(startPos, endPos, raiseCurve.Evaluate(timer/moveDuration)) + new Vector3(a * shakeDist, 0, 0); //Set position
            
            if (timer == moveDuration)                                                              //Stop movement
            {
                move = false;
                end = true;
            }

        }
    }
}
