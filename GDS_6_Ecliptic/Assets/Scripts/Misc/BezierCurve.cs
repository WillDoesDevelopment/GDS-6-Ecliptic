using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public GameObject player;
    public GameObject block;

    public Transform P0Object;
    public Transform P1Object;
    public Transform P2Object;

    public float maxLength = 5f;

    float d;
    float a;
    public float t;

    Vector3 P0;
    Vector3 P1;
    Vector3 P2;

    Vector3[] PositionArray = new Vector3[10];

    public LineRenderer LineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        P0 = P0Object.position;        
        P2 = P2Object.position;

        d = Vector3.Distance(P0, P2);
        a = Mathf.Pow(maxLength / 2f, 2f) - (d / 2f) * (d / 2f);
        a = Mathf.Clamp(a, 0, maxLength);
        a = Mathf.Sqrt(a);
        a = Mathf.Pow(a, 1.5f) * 0.6f;     //approximation of a to get consistant rope length. True rope length would need lots of calculus.
        P1Object.position = (P0 + P2) / 2 + new Vector3(0, -a, 0);

        P1 = P1Object.position;


        LineRenderer.positionCount = 10;
        for (int i = 0; i < 10; i++)
        {
            PositionArray[i] = CalculateCurve(i / 9f, P0, P1, P2);
        }
                    
        LineRenderer.SetPositions(PositionArray);

        t = d / maxLength;                                                             // % of max length extended
        if (d > maxLength)
        {
            AddForce();
            //transform.position = Vector3.MoveTowards(transform.position, chainPosition, Time.deltaTime * 10f);
            //grapple break here 
            //player.GetComponent<Grapple2>().RopeBreak();
            //player.GetComponent<Grapple3>().RopeBreak();
        }
    }

    Vector3 CalculateCurve(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {        
        Vector3 y = ((1 - t)*(1-t) * P0) + (2 * (1 - t) * t * P1) + (t*t * P2);
        return y;
    }

    void AddForce()
    {
        
        if(t>1.1)
        {
            player.GetComponent<Grapple2>().RopeBreak();
            player.GetComponent<Grapple3>().RopeBreak();

        }
        float f = Mathf.Clamp(t - 1f, 0, 0.1f) * 10f;                                       // force modifier when 1.0 < t < 1.1

        var forceVec = Vector3.Normalize(P0 - P2) * Time.deltaTime * f * 50000f;    //Calculate force            
        block.GetComponent<Rigidbody>().AddForceAtPosition(forceVec, P2);            //Add force
        block.transform.position = Vector3.MoveTowards(block.transform.position, P0, Time.deltaTime * 10f);
    }
}
