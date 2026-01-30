using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairMovment : MonoBehaviour
{

    //Only works on the z axis. Restrict other movment in the rigidbody. Sorry - Gyles

    public float zPosLock;
    bool stairLock = false;

    public GameObject[] objectIgnoreArray;

    public AudioSource stairsnd;
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
        if(transform.position.z < zPosLock && !stairLock)
        {
            stairsnd.Play();
            transform.position = new Vector3(transform.position.x, transform.position.y, zPosLock);
            int LayerDefault = LayerMask.NameToLayer("Default");
            gameObject.layer = LayerDefault;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            stairLock = true;
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y - 5f, zPosLock), new Vector3(transform.position.x, transform.position.y + 5f, zPosLock));
        
    }
}
