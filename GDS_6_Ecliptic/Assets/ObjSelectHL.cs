using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjSelectHL : MonoBehaviour
{
    public Material Mat;
    public Color[] HLColours;
    public bool isSelected = false;

    private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {


        if(isSelected == false)
        {
            gameObject.GetComponent<MeshRenderer>().material.GetColor("_Highlight_Colour");
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_Highlight_Colour", HLColours[0]);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit))
        {

            if (hit.transform.CompareTag("Selectable"))
            {
                isSelected = true;
                gameObject.GetComponent<MeshRenderer>().material.GetColor("_Highlight_Colour");
                gameObject.GetComponent<MeshRenderer>().material.SetColor("_Highlight_Colour", HLColours[1]);

            }   
            else
            {
                return;
            }
            isSelected = false;
        }

    }
}
