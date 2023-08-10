using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotate : MonoBehaviour
{
    public float rotSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", transform.position.x * rotSpeed);
    }
}
