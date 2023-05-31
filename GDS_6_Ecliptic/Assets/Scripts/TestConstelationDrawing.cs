using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TestConstelationDrawing : MonoBehaviour
{
    public LayerMask layer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // this is option 1, This only tells us if the mouse is over a ui element (No 2d box collider needed) however does not retrieve Object info
/*        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            //Debug.Log("Working");

        }*/
        raycasting();

    }
    public void raycasting()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePosition.z = Mathf.Infinity;
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, layer);

        if (hit.collider != null)
        {
            Debug.Log(hit.transform.gameObject.name);
        }


    }
    public void OnMouseEnter()
    {
        // this only works when the script it on the UI elements. Less ideal
    }

}
