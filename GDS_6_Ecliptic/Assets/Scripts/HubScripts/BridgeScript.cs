using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour
{
    public GameObject bridge1;
    public GameObject bridge2;
    public GameObject door;

    public Vector3 doorOffset;
    float totalLength;
    float bridge1Length = 4f;

    // Start is called before the first frame update
    void Start()
    {
        bridge1.SetActive(false);
        bridge2.SetActive(false);
        Connect();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("Connect")]
    public void Connect()
    {
        bridge1.SetActive(true);
        bridge2.SetActive(true);
        totalLength = Vector3.Distance(bridge1.transform.position, door.transform.position + doorOffset);
        bridge2.transform.localScale = new Vector3(1, 1, totalLength - bridge1Length);
        bridge1.transform.LookAt(door.transform.position + doorOffset);
    }

    [ContextMenu("Disconnect")]
    public void Disconnect()
    {
        bridge1.SetActive(false);
        bridge2.SetActive(false);
    }


}
