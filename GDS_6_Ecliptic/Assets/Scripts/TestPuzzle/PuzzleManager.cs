using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public Collider[] weightsArr;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        weightsArr = Physics.OverlapSphere(Vector3.zero, 10);

        foreach (Collider c in weightsArr)
        {
            if(c.GetComponent<Weight>() != null)
            {

                Debug.Log(c.GetComponent<Weight>().isColliding);
            }
        }
    }



}
