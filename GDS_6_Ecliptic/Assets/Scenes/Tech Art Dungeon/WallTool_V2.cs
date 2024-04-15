using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTool_V2 : MonoBehaviour
{
    private Vector3 mPosA;
    private Vector3 objPosA;
    public GameObject wall;
    public GameObject column;

    public List<GameObject> pilars = new List<GameObject>();
    private Vector3 instPos;
    private float lerpVal;
    private float dist;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Places a column
        if (Input.GetMouseButtonDown(0))
        {
            mPosA = Input.mousePosition ;
            mPosA.z = 25.0f;
         

            objPosA = Camera.main.ScreenToWorldPoint(mPosA);
            GameObject col = Instantiate(column, objPosA, column.transform.rotation);
            pilars.Add(col);
            
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            wallSegments();
        }

    }

    void wallSegments()
    {

            for(int i = 0; i < pilars.Count; i++)
            {
                Instantiate(wall, pilars[i].transform.position, transform.rotation);
            }
    }
}
