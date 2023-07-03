using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PuzzleManager : MonoBehaviour
{
    public Collider[] weightsArr;
    // Start is called before the first frame update
    public TextMeshProUGUI ScaleValDisplay;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float WeightVal = 0;
        weightsArr = Physics.OverlapSphere(this.transform.position, 2);

        foreach (Collider c in weightsArr)
        {
            Weight weight = c.GetComponent<Weight>();
            if (weight != null && c.GetComponent<Weight>().isColliding == true)
            {
                WeightVal += weight.weight;
            }
        }
        ScaleValDisplay.text = WeightVal.ToString();
        //Debug.Log(WeightVal);
    }


}
