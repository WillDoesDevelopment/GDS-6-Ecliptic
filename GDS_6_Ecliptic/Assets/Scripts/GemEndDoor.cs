using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemEndDoor : MonoBehaviour
{
    public GameObject Ophieno2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Ophieno2.SetActive(false);
        }

    }
}
