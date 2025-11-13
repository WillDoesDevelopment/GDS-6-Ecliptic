using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sharkCollider : MonoBehaviour
{
    public PiscesManager m_Manager;
    

    // Start is called before the first frame update
    private void OnTriggerStay(Collider other) //first time
    {
        if (other.CompareTag("Player") && m_Manager.go == false)
        {       
            m_Manager.StartLerpToPosition();
        }

        if(other.CompareTag("Player") && m_Manager.go == true)
        {
            m_Manager.EndLerpToPosition();
        }

    }

    private void OnTriggerEnter(Collider other) //first time
    {
        
        if (other.CompareTag("Angler"))
        {
            other.GetComponent<AnglerManager>().spin = true;
            GameObject obj = other.gameObject;
            StartCoroutine(m_Manager.sharkHit(obj));

        }
    }

}
