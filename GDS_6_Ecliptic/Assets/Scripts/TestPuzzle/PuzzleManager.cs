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
        float WeightVal = 0;
        weightsArr = Physics.OverlapSphere(this.transform.position, 5);

        foreach (Collider c in weightsArr)
        {
            Weight weight = c.GetComponent<Weight>();
            if (weight != null && c.GetComponent<Weight>().isColliding == true)
            {
                WeightVal += weight.weight;
            }
        }
        Debug.Log(WeightVal);
    }


}
