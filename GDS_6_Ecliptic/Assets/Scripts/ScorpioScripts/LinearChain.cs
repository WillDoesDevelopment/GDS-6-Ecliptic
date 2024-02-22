using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearChain : MonoBehaviour
{
    public GameObject player;
    public GameObject chainStart;
    public float chainLength;
    Vector3 chainPosition;
    // Start is called before the first frame update
    void Start()
    {
        chainPosition = new Vector3(chainStart.transform.position.x, transform.position.y, chainStart.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        var currentLength = Vector3.Distance(transform.position, chainStart.transform.position);
        if(currentLength > chainLength)
        {
            transform.position = Vector3.MoveTowards(transform.position, chainPosition, Time.deltaTime * 10f);
            //grapple break here 
            //player.GetComponent<Grapple2>().RopeBreak();
            player.GetComponent<Grapple3>().RopeBreak();
        }
    }
}
