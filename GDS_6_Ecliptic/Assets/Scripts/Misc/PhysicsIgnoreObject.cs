using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsIgnoreObject : MonoBehaviour
{
    public GameObject[] objectIgnoreArray;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject ignoreobject in objectIgnoreArray)
        {
            Physics.IgnoreCollision(ignoreobject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
}
