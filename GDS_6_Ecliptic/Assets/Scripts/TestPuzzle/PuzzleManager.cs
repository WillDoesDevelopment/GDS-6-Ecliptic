using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PuzzleManager : MonoBehaviour
{
    public Collider[] weightsArr;

    public int TargetVal;
    // Start is called before the first frame update
    public TextMeshProUGUI ScaleValDisplay;
    public Vector3 StartPos;
    public PuzzleManager OtherScale;
    void Start()
    {
        StartPos = this.transform.position;
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
        
        //this.transform.position = StartPos - new Vector3(0, WeightVal/10, 0);
        ScaleValDisplay.text = WeightVal.ToString();
        //Debug.Log(WeightVal);
        if (ScaleValDisplay.text == TargetVal.ToString() && OtherScale.ScaleValDisplay.text == TargetVal.ToString())
        {
            this.GetComponent<Renderer>().material.color = Color.green;
        }
    }


}
