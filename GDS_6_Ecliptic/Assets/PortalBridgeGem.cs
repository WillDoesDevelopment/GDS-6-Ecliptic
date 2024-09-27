using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBridgeGem : MonoBehaviour
{
    [Header("Player Objects")]
    public GameObject[] skinMats;
    public GameObject[] clothesMats;

    [Header("Materials")]
    public Material[] invertedMats;
    public Material[] normalMats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            for(int i = 0; i < skinMats.Length; i++)
            {
                print("Skin!");
                skinMats[i].GetComponent<Renderer>().material = invertedMats[1];
            }

            for (int i = 0; i < clothesMats.Length; i++)
            {
                print("Clothes!");
                clothesMats[i].GetComponent<Renderer>().material = invertedMats[0];
            }

        }

        if (gameObject.tag == "EndBridge" && other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < skinMats.Length; i++)
            {
                skinMats[i].GetComponent<Renderer>().material = normalMats[1];
            }

            for (int i = 0; i < clothesMats.Length; i++)
            {
                clothesMats[i].GetComponent<Renderer>().material = normalMats[0];
            }
        }

    }


}
