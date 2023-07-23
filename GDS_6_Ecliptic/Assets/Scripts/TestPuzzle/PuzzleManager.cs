using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PuzzleManager : MonoBehaviour
{
    private bool isRunning = false;
    public Collider[] weightsArr;

    public float OverlapSphereRange;
    public int TargetVal;
    // Start is called before the first frame update
    public TextMeshProUGUI ScaleValDisplay;
    public Vector3 StartPos;
    public PuzzleManager OtherScale;

    public PickUpScript PUS;
    void Start()
    {
        StartPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        int WeightVal = 0;
        weightsArr = Physics.OverlapSphere(this.transform.position, OverlapSphereRange);

        foreach (Collider c in weightsArr)
        {
            Weight weight = c.GetComponent<Weight>();
            if (weight != null && c.GetComponent<Weight>().isColliding == true && c.gameObject != PUS.HoldingObj )
            {
                WeightVal += (int)weight.weight;
            }
        }
        
        if(isRunning == false  )
        {
            StartCoroutine(MoveScales(WeightVal));
        }

        ScaleValDisplay.text = WeightVal.ToString();

        if (ScaleValDisplay.text == TargetVal.ToString() && OtherScale.ScaleValDisplay.text == TargetVal.ToString())
        {
            this.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    public  IEnumerator MoveScales(int WeightVal )
    {
        isRunning = true;
        Vector3 yTranslation = new Vector3(0, (float)WeightVal / 50, 0);
        foreach (Collider c in weightsArr)
        {
            if(c.GetComponent<Weight>() != null && c.gameObject != PUS.HoldingObj)
            {
                Debug.Log("Working" + yTranslation);
                c.transform.parent = this.transform;

            }
        }
        this.transform.position = StartPos - yTranslation;
        
        //this.transform.DetachChildren();
        
        isRunning = false;

        yield return new  WaitForSeconds(1);

    }


}
