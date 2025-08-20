using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weight2 : MonoBehaviour
{
    public float itemWeight;
    public bool isColliding = false;

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
