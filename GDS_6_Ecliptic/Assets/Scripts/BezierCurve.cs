using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public Transform P0Object;
    public Transform P1Object;
    public Transform P2Object;



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
        P1 = P1Object.position;
        P2 = P2Object.position;


        LineRenderer.positionCount = 10;
        for (int i = 0; i < 10; i++)
        {
            PositionArray[i] = CalculateCurve(i / 9f, P0, P1, P2);
        }

            
        LineRenderer.SetPositions(PositionArray);


    }

    Vector3 CalculateCurve(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {        
        Vector3 y = ((1 - t)*(1-t) * P0) + (2 * (1 - t) * t * P1) + (t*t * P2);
        return y;
    }
}
