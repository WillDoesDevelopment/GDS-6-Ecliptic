using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copyTransform : MonoBehaviour
{
    public Transform copyPosition;

    // Update is called once per frame
    void Update()
    {
        transform.position = copyPosition.position;
        transform.rotation = copyPosition.rotation;
    }
}
