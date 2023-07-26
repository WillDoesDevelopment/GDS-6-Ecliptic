using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// since it is searialised, when a public door status is made in another script it can be seen in editor
[System.Serializable]
public class DoorStatus 
{
    // package of information each door needs to pass on to the Hub manager
    public int SceneNum;
    public bool IsOpen;
}
