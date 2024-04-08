using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTool : MonoBehaviour
{
    private Vector3 mPos;
    private Vector3 objPos;

    public GameObject wall;
    public GameObject column;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mPos = Input.mousePosition;
            mPos.z = 25.0f;

            objPos = Camera.main.ScreenToWorldPoint(mPos);
            Instantiate(column, objPos, Quaternion.identity);
        }

        if (Input.GetMouseButtonUp(0))
        {
            mPos = Input.mousePosition;
            mPos.z = 25.0f;

            objPos = Camera.main.ScreenToWorldPoint(mPos);
            Instantiate(column, objPos, Quaternion.identity);
        }
    }
}
