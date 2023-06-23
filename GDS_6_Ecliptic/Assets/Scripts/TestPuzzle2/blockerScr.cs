using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockerScr : MonoBehaviour
{

    public Transform playerTransform;
    public Transform pivotPoint;

    public float radius = 10;
    public float speed = 15;
    float radialSpeed = 0;
    public float angle = 180;

    // Start is called before the first frame update
    void Start()
    {
        radialSpeed = speed * 360 / (2f * 3.14159f * radius);
    }

    // Update is called once per frame
    void Update()
    {
        radialSpeed = speed * 360 / (2f * 3.14159f * radius);

        //angle += Time.deltaTime * radialSpeed;


        //Check left or right
        {
            Vector3 targetDir = playerTransform.position - transform.position;
            Vector3 forward = transform.forward;
            float playerAngle = Vector3.SignedAngle(targetDir, forward, Vector3.up);
            if (playerAngle <= 0.0F)
            {
                //Turn Right
                angle += Time.deltaTime * radialSpeed;
            }
            else
            {
                //Turn Left
                angle -= Time.deltaTime * radialSpeed;
            }
        }


        if (angle > 360) { angle -= 360; }
        if (angle < 0) { angle += 360; }

        transform.position = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), 0, Mathf.Sin(Mathf.Deg2Rad * angle)) * radius;
        transform.LookAt(pivotPoint);
        

        

    }
}
