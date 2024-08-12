using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeoDoor : MonoBehaviour
{
    public GameObject otherDoor;
    public GameObject[] objectIgnoreArray;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject ignoreobject in objectIgnoreArray)
        {
            Physics.IgnoreCollision(ignoreobject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount > 0)
        {
            //otherDoor.transform.eulerAngles = new Vector3(0, -transform.rotation.eulerAngles.y, 0);
        }
    }
}
