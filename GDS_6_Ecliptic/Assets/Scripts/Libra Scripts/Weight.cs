using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Weight : MonoBehaviour
{
    // all components required (How heavy, Rigid Body and so on)
    public float weight;
    public Rigidbody RB;
    public TextMeshProUGUI WeightTxt;

    // do we know the weight of this block
    public bool visible;

    public bool isColliding = false;
    private void Awake()
    {
        if (visible)
        {
            // instead of doing this in editor
            WeightTxt =  this.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            WeightTxt.text = weight.ToString();

        }
    }

    // two simple collision checks
    private void OnCollisionStay(Collision collision)
    {
        isColliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }
}
