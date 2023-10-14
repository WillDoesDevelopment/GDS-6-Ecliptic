using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairBottom : MonoBehaviour
{

    //Only works on the x axis. Restrict other movment in the rigidbody. Sorry - Gyles

    public float xPosLock;
    bool stairLock = false;

    public GameObject[] objectIgnoreArray;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject ignoreobject in objectIgnoreArray)
        {
            Physics.IgnoreCollision(ignoreobject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > xPosLock && !stairLock)
        {
            transform.position = new Vector3(xPosLock, transform.position.y, transform.position.z);
            int LayerDefault = LayerMask.NameToLayer("Default");
            gameObject.layer = LayerDefault;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            stairLock = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(xPosLock, transform.position.y - 5f, transform.position.z), new Vector3(xPosLock, transform.position.y + 5f, transform.position.z));

    }
}
