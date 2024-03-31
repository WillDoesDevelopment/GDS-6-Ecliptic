using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linearOscilation : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    //public Transform mirroredObject;
    public AnimationCurve curve;                    //input curve in editor    
    public float travelTime = 5;
    float timer = 0;
    float a = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * a;

        if(timer > travelTime)
        {
            timer = travelTime + travelTime - timer;    //any extra time is carried over
            a = -a;                                     //reverse direction
        }
        if (timer < 0f)
        {
            timer = -timer;                             //any extra time is carried over
            a = -a;                                     //reverse direction
        }
        timer = Mathf.Clamp(timer, 0, travelTime);      //clamp just in case. This shouldn't take affect, but maybe the framerate is bad.

        transform.position = Vector3.Lerp(point1.position, point2.position, curve.Evaluate(timer / travelTime));    //lerp between points based on timer input to animation curve
    }
}
