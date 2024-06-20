using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrierSegment : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;
    float startDist = 40f;
    float dist = 0;
    float speed = 10;    
    public bool move = false;

    Vector3 lineStart;
    Vector3 lineEnd1;
    Vector3 lineEnd2;

    public LayerMask LayerMask;
    

    // Start is called before the first frame update
    void Start()
    {
        endPos = transform.position;
        transform.position -= startDist * transform.forward;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(LayerMask.GetMask("Floor"));
        if (move)
        {
            dist += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPos, endPos, dist / startDist);

            lineStart = transform.position + transform.TransformDirection(new Vector3(0, -4, 20));
            lineEnd1 = transform.position + transform.TransformDirection(new Vector3(-20, -4, -15));
            lineEnd2 = transform.position + transform.TransformDirection(new Vector3(+20, -4, -15));
                       

            //if(Physics.Linecast(lineStart, lineEnd, out RaycastHit hit, LayerMask))
            while (Physics.Linecast(lineStart, lineEnd1, out RaycastHit hit1, LayerMask.GetMask("Floor")))            
            {
                hit1.transform.gameObject.GetComponent<floorScript>().fall = true;
                hit1.transform.gameObject.layer = LayerMask.GetMask("Default");                
            }

            while (Physics.Linecast(lineStart, lineEnd2, out RaycastHit hit2, LayerMask.GetMask("Floor")))
            {
                hit2.transform.gameObject.GetComponent<floorScript>().fall = true;
                hit2.transform.gameObject.layer = LayerMask.GetMask("Default");
            }


            if (dist > startDist)
            {
                move = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.blue;        
        Gizmos.DrawLine(lineStart, lineEnd1);
        Gizmos.DrawLine(lineStart, lineEnd2);

    }
}
