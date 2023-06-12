using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjSelectHL : MonoBehaviour
{
    public Material hlMat;
    public Material ogMat;

    private Transform highlight;
    private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {

        if(highlight != null)
        {
            highlight.GetComponent<MeshRenderer>().material = ogMat;
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit))
        {
            highlight = hit.transform;
            if (highlight.CompareTag("Selectable"))
            {
                if (highlight.GetComponent<MeshRenderer>().material != hlMat)
                {
                    ogMat = highlight.GetComponent<MeshRenderer>().material;
                    highlight.GetComponent<MeshRenderer>().material = hlMat;
                }
            }   
            else
            {
                highlight = null;
            }
        }

    }
}
