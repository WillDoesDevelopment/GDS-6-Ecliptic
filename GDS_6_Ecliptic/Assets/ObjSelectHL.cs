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
    Ray ray;
    // Update is called once per frame
    void Update()
    {


        if(isSelected == false)
        {
            gameObject.GetComponent<Renderer>().material.GetColor("_Highlight_Colour");
            gameObject.GetComponent<Renderer>().material.SetColor("_Highlight_Colour", HLColours[0]);
        }

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            isSelected = false;
            if (hit.transform.gameObject.tag == "Selectable")
            {
                isSelected = true;
                this.gameObject.GetComponent<Renderer>().material.GetColor("_Highlight_Colour");
                this.gameObject.GetComponent<Renderer>().material.SetColor("_Highlight_Colour", HLColours[1]);
                print("Hit" + this.gameObject.name);
            }   
            else
            {
                return;
                
            }
            
        }

    }

}
