using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BridgeTriggerG : MonoBehaviour
{
    float timeElapsed;
    public float lerpDuration = 5;

    public Renderer rend1;
    public Renderer rend2;
    public float shaderTimer = 115;
    bool isColliding = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if(isColliding == true)
        {
            shaderTimer = Mathf.Lerp(110, 170, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
        }

        rend1.material.SetFloat("_Timer", shaderTimer);
        rend2.material.SetFloat("_Timer", shaderTimer);

    }

    public void OnTriggerEnter(Collider other)
    {
        isColliding = true;
    }
   

}
