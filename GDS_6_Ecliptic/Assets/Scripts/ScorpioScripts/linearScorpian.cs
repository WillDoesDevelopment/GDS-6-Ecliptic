using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linearScorpian : MonoBehaviour
{
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
        lineAngle = Vector3.Normalize(point1.position - point2.position);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;        //Time for pie
        if(timer > 6.28319)
        {
            timer -= 6.28319f;
        }

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

        
        playerVec = player.transform.position - point1.position;                            //Vector to player
        line = Vector3.Project(playerVec, lineAngle);                                       //Vector to closest point to player on line angle

        Debug.DrawLine(point1.position, point1.position + line, Color.green, 0.01f);

        targetPoint = point1.position + line + 0.5f * Mathf.Sin(timer * 3.0f) * lineAngle;
        targetPoint = new Vector3(Mathf.Clamp(targetPoint.x, Mathf.Min(limit1.x, limit2.x), Mathf.Max(limit1.x, limit2.x)),targetPoint.y, Mathf.Clamp(targetPoint.z, Mathf.Min(limit1.z, limit2.z), Mathf.Max(limit1.z, limit2.z))); //omg i hope this works

        transform.position = targetPoint;
        transform.LookAt(new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z));
        
        
    }
}
