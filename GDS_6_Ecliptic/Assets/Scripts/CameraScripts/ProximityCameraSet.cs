using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ProximityCameraSet : MonoBehaviour
{
    public GameObject player;    
    public float width = 1f;
    public float length = 1f;
    private GameObject[] vCameras;
    public CinemachineVirtualCamera setCamera;
    

    void Start()
    {
        vCameras = GameObject.FindGameObjectsWithTag("Vcam");
        
    }


    private void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, new Vector3(width, 1.0f, length) / 2, transform.rotation); //Hitbox for player
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject == player)
            {
                foreach (GameObject cam in vCameras)
                {

                    cam.GetComponent<CinemachineVirtualCamera>().Priority = 10;
                }
                setCamera.Priority = 11;
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var p1 = transform.position + transform.forward * length / 2 + transform.right * width / 2;
        var p2 = transform.position - transform.forward * length / 2 + transform.right * width / 2;
        var p3 = transform.position - transform.forward * length / 2 - transform.right * width / 2;
        var p4 = transform.position + transform.forward * length / 2 - transform.right * width / 2;

        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);
    }
}
