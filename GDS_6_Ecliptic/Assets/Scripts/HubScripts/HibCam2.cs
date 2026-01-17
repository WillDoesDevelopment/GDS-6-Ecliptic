using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HibCam2 : MonoBehaviour
{
    public GameObject player;
    
    public Vector3 targetPos;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = player.transform.position;

        targetPos = Vector3.Scale(targetPos, new Vector3(1, 0, 1));
       
        targetPos = Vector3.Normalize(targetPos);
       
        transform.position = (15 * targetPos) + new Vector3(0, 6, 0);

        transform.LookAt(player.transform);
        
        
        
    }
}
