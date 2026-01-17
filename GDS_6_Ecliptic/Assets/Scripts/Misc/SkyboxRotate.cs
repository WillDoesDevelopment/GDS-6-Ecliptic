using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotate : MonoBehaviour
{
    //public GameObject starhub;
    public float rotSpeed = 2f;
    //public float starSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float offset = Mathf.Lerp(-180, 180, 0) / ;
        //starhub.transform.EulerAngles = new Vector3(0, transform.position.x * starSpeed, 0);

        RenderSettings.skybox.SetFloat("_Rotation", transform.position.x * rotSpeed);
    }
}
