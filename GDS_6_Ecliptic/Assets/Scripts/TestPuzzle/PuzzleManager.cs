using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PuzzleManager : MonoBehaviour
{
    public Animator EvenAnim;
    //Checks the coroutine if its running as to avoid calling it every frame
    private bool isRunning = false;

    // Physics.SphereOverlap creates an array of colliders and this is where we store them
    public Collider[] weightsArr;

    // the diametre of the SphereOverlap
    public float OverlapSphereRange;

    // was used as the origonal win condition of the puzzle, has changed since
    public int TargetVal;

    // for displaying the weight on the scale in UI form, may be obsolete now
    public TextMeshProUGUI ScaleValDisplay;

    // needed to know the initial position for which to change the scale height
    public Vector3 StartPos;

    //Needed a refrence to the other scale script, there are two instances of this script in the scene
    public PuzzleManager OtherScale;

    public PickUpScript PUS;

    private int WeightVal = 0;
    void Start()
    {
        StartPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //the weight that is on our scale
        WeightVal = 0;

        // populates our collider array
        weightsArr = Physics.OverlapSphere(this.transform.position, OverlapSphereRange);

        // this checks that the weight is colliding near the scale and that the object is not currently being held, if this criterea is met we add the wieght to WeightVal
        foreach (Collider c in weightsArr)
        {
            Weight weight = c.GetComponent<Weight>();
            if (weight != null && c.GetComponent<Weight>().isColliding == true && c.gameObject != PUS.HoldingObj )
            {
                WeightVal += (int)weight.weight;
            }
        }
        //provided the coroutine is not running, run it and pass in WeightVal as an overload
        if(isRunning == false  )
        {
            if(WeightVal <= 10)
            {
                StartCoroutine(MoveScales(WeightVal));
            }
            else
            {
                StartCoroutine(MoveScales(10));

            }
        }
        //displays our Weight
        UnknownBlockCheck();


        

        //the test for our obsolete win condition
        if (WeightVal == OtherScale.WeightVal && WeightVal!=0)
        {
            EvenAnim.SetBool("Even", true);
        }
        else
        {
            EvenAnim.SetBool("Even", false);
        }
    }

    // the couroutine that moves the scales relative to the weight
    public  IEnumerator MoveScales(int WeightVal )
    {
        isRunning = true;
        Vector3 yTranslation = new Vector3(0, (float)WeightVal / 10, 0);
        foreach (Collider c in weightsArr)
        {
            if(c.GetComponent<Weight>() != null && c.gameObject != PUS.HoldingObj)
            {
                Debug.Log("Working" + yTranslation);
                c.transform.parent = this.transform;

            }
        }
        this.transform.position = StartPos - yTranslation;
        yield return new  WaitForSeconds(1);
        
        //this.transform.DetachChildren();
        
        isRunning = false;


    }

    public void UnknownBlockCheck()
    {
        bool UnknownBlock = false;
        foreach (Collider c in weightsArr)
        {
            Weight W = c.GetComponent<Weight>();
            if (W != null && W.visible == false)
            {
                UnknownBlock = true;
                ScaleValDisplay.text = "?";
            }
        }
        if (UnknownBlock == false)
        {
            ScaleValDisplay.text = WeightVal.ToString();
        }
    }
}
