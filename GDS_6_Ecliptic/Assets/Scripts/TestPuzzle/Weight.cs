using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Weight : MonoBehaviour
{
    public float weight;
    public Rigidbody RB;
    public TextMeshProUGUI WeightTxt;

    public bool visible;

    public bool isColliding = false;
    private void Awake()
    {
        if (visible)
        {
            WeightTxt =  this.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            WeightTxt.text = weight.ToString();

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        isColliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }
}
