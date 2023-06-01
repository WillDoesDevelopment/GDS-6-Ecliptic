using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TestConstelationDrawing : MonoBehaviour
{
    public LayerMask layer;

    public GameObject FollowObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        raycasting();
        mouseFollow();
    }
    public void raycasting()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        if (raycastResults.Count > 0)
        {
            foreach (var go in raycastResults)
            {
                Debug.Log(go.gameObject.name, go.gameObject);
            }
        }
    }

    public void mouseFollow()
    {
        FollowObj.transform.position = Input.mousePosition;
    }
}
