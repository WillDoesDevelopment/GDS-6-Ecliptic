using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sharkCollider : MonoBehaviour
{
    public PiscesManager m_Manager;

    // Start is called before the first frame update
    private void OnTriggerStay(Collider other) //first time
    {
        if (other.CompareTag("Player"))
        {       
            m_Manager.StartLerpToPosition();
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Angler"))
        {
            print("Angleeeer");
            m_Manager.sharkHit();
        }
    }

}
