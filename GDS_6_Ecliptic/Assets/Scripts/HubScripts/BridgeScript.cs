using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour
{
    public GameObject bridge1;
    public GameObject bridge2;
    public GameObject openWall;
    public GameObject door;
    public GameObject psStars;
    Renderer rend1;
    Renderer rend2;

    public Vector3 doorOffset;
    float totalLength;
    float bridge1Length = 4f;
    float shaderTimer;


    // Start is called before the first frame update
    void Start()
    {
        bridge1.SetActive(false);
        bridge2.SetActive(false);
        psStars.SetActive(false);
        openWall.SetActive(true);
        rend1 = bridge1.GetComponent<Renderer>();
        rend2 = bridge2.GetComponent<Renderer>();
        Connect();
    }

    // Update is called once per frame
    void Update()
    {
        if (shaderTimer < 50)
        {
            shaderTimer += Time.deltaTime * 1.5f;
            rend1.material.SetFloat("_Timer", shaderTimer);
            rend2.material.SetFloat("_Timer", shaderTimer);
        }
    }

    [ContextMenu("Connect")]
    public void Connect()
    {
        Debug.Log("connected");
        bridge1.SetActive(true);
        bridge2.SetActive(true);
        psStars.SetActive(true);
        openWall.SetActive(false);

        shaderTimer = transform.position.z-1;     //Z coordinate of start of bridge
        rend1.material.SetFloat("_Timer", shaderTimer);
        rend2.material.SetFloat("_Timer", shaderTimer);
        shaderTimer = psStars.transform.position.z-1;

        totalLength = Vector3.Distance(bridge1.transform.position, door.transform.position + doorOffset);
        bridge2.transform.localScale = new Vector3(1, 1, totalLength - bridge1Length);
        bridge1.transform.LookAt(door.transform.position + doorOffset);

        totalLength = Vector3.Distance(psStars.transform.position, door.transform.position - doorOffset);
        psStars.transform.localScale = new Vector3(1, 1, totalLength + bridge1Length);
        psStars.transform.LookAt(door.transform.position - doorOffset);
    }

    [ContextMenu("Disconnect")]
    public void Disconnect()
    {
        bridge1.SetActive(false);
        bridge2.SetActive(false);
        psStars.SetActive(false);
        openWall.SetActive(true);

    }


}
