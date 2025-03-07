using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexSpawner : MonoBehaviour
{
    public GameObject Prefab;
    public float scale = 1;
    public int width = 10;
    public int length = 10;
    public bool circle = false;
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
                var newObject = Instantiate(Prefab);                                                    //Create Object
                if (j % 2 == 0)                                                                         //Check if even
                {
                    newObject.transform.position = new Vector3(i * scale, 0, j * scale * 0.866025f);            //Location
                }
                else
                {
                    newObject.transform.position = new Vector3((i+ 0.5f) * scale, 0, j * scale * 0.866025f);    //Location
                }
                                       
                newObject.transform.localScale = Vector3.one * scale;                                   //Scale                
                newObject.transform.parent = transform;                                                 //Set as child

                if (circle == true)
                {
                    var r = length * scale / 2f;
                    var x = newObject.transform.position.x;
                    var y = newObject.transform.position.z;
                    if (y > Mathf.Sqrt(r * r - (x - r) * (x - r))+ r)
                    {
                        DestroyImmediate(newObject);
                    }

                    if (y < -Mathf.Sqrt(r * r - (x - r) * (x - r)) + r)
                    {
                        DestroyImmediate(newObject);
                    }
                }
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
