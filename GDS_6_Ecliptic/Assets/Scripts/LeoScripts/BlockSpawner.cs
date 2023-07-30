using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject Prefab;
    public float scale = 1;
    public int width = 10;
    public int length = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        Debug.Log("spawn");
        ClearObjects();

        for (var i = 0; i < length; i++)
        {
            for (var j = 0; j < width; j++)
            {
                var newObject = Instantiate(Prefab);                                                    //Create Arrow                
                newObject.transform.localScale = Vector3.one * scale;                                   //Scale
                newObject.transform.position = new Vector3(i * scale, 0, j * scale);                    //Location
                newObject.transform.parent = transform;                                                 //Set as child


            }
        }


        Physics.SyncTransforms();
    }


    [ContextMenu("Clear Objects")]
    public void ClearObjects()
    {
        Debug.Log("clear");
        Debug.Log(transform.gameObject);

        //Credit: Lachlan Sleight
        //We iterate backwards through children, since if we go forwards we end up changing child indices
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }


}
