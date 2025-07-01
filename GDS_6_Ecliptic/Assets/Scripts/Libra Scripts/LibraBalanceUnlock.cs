using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraBalanceUnlock : MonoBehaviour
{
    public DoorScript DoorScriptV;
    public MeshRenderer DoorMeshRenderer;
    public Rigidbody ScaleRididbody;
    public Material DoorOpenMat;

    bool repeatLock = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.eulerAngles.z);
        if (repeatLock == false)
        {
            if (transform.eulerAngles.z < 5 || transform.eulerAngles.z > 355)
            {
                if (ScaleRididbody.IsSleeping() == true)
                {
                    DoorScriptV.DS.IsOpen = true;
                    DoorMeshRenderer.material = DoorOpenMat;
                    repeatLock = true;
                }
            }            
        }        
    }
}
