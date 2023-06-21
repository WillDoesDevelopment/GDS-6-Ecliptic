using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDictionary : MonoBehaviour
{
    [SerializeField]
    public Dictionary<GameObject, float> keyValuePairs = new Dictionary<GameObject, float>();

    public GameObject PlaceHolder;
    void Start()
    {
        keyValuePairs.Add(PlaceHolder, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
