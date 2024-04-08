using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTool : MonoBehaviour
{
    private Vector3 mPosA;
    private Vector3 objPosA;

    private Vector3 mPosB;
    private Vector3 objPosB;

    public GameObject wall;
    public GameObject column;

    private Vector3 instPos;
    private float lerpVal;
    private float dist;
    private int segments;
    private bool isOn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mPosA = Input.mousePosition;
            mPosA.z = 25.0f;

            objPosA = Camera.main.ScreenToWorldPoint(mPosA);
            Instantiate(column, objPosA, Quaternion.identity);
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            mPosB = Input.mousePosition;
            mPosB.z = 25.0f;

            objPosB = Camera.main.ScreenToWorldPoint(mPosB);
            Instantiate(column, objPosB, Quaternion.identity);
            wallSegments();

        }

        if(isOn == true)
        {
            
            isOn = false;
        }

    }


    void wallSegments()
    {
        segments = Mathf.RoundToInt(Vector3.Distance(objPosA, objPosB) / 0.5f);
        dist = 1 / segments;
        for(int i = 0; i < segments; i++)
        {
            lerpVal += dist;
            instPos = Vector3.Lerp(objPosA, objPosB, lerpVal);
            Instantiate(wall, instPos, Quaternion.identity);
            return;
        }
    }
}
