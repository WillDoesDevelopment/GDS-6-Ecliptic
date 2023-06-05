using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovment : MonoBehaviour
{
    public GameObject target;
    public float lerpSpeed;

    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        LerpToTarget();   
    }
    public void LerpToTarget()
    {
        Vector3 targetPos = offset + target.transform.position;
        this.transform.position = Vector3.Lerp(this.transform.position,targetPos, lerpSpeed);
    }
}
