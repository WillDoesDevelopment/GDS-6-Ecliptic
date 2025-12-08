using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linearScorpian : MonoBehaviour
{
    public GameObject ScorpDT;

    public GameObject player;
    public Transform point1;
    public Transform point2;
    public Vector3 line;
    Vector3 lineAngle;
    Vector3 playerVec;
    Vector3 limit1;
    Vector3 limit2;
    Vector3 targetPoint;
    float offset = 1.0f;
    float timer = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DTfollow();

        timer += Time.deltaTime;        //Time for pie
        if(timer > 6.28319)
        {
            timer -= 6.28319f;
        }

        /*
        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position, lineAngle), out hit, 50f))  //Raycast left
        {
            limit1 = hit.point - lineAngle * offset;
            Debug.DrawLine(hit.point, hit.point + transform.up, Color.blue, 0.01f);
        }
        if (Physics.Raycast(new Ray(transform.position, -lineAngle), out hit, 50f))  //Raycast right
        {
            limit2 = hit.point + lineAngle * offset;
            Debug.DrawLine(hit.point, hit.point + transform.up, Color.blue, 0.01f);
        }
        */

        targetPoint = GetClosestPointOnFiniteLine(player.transform.position, point1.position, point2.position);
        transform.position = targetPoint;
        transform.LookAt(new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z));
        
        
    }

    public void DTfollow()
    {
        ScorpDT.transform.position = transform.position;
    }

    Vector3 GetClosestPointOnFiniteLine(Vector3 point, Vector3 line_start, Vector3 line_end)
    {
        Vector3 line_direction = line_end - line_start;
        float line_length = line_direction.magnitude;
        line_direction.Normalize();
        float project_length = Mathf.Clamp(Vector3.Dot(point - line_start, line_direction), 0f, line_length);
        return line_start + line_direction * project_length;
    }
}
