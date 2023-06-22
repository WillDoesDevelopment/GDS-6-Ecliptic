using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DisplayDictionary))]
public class SerialiseDictionary : Editor
{
    float F = 0;
    GameObject placeHolder;   
    GameObject placeHolder2;   
    Dictionary<GameObject, float> TempDict;

    public override void OnInspectorGUI()
    {
        // runs all the normal GUI the monobehaviour target script would run
        base.OnInspectorGUI();

        //sets our target script we are editing to displayDictionary, we need to decair this before the key word Target as target is by default and object and not a script

        DisplayDictionary displayDictionary = (DisplayDictionary)target;      

        // runs through all keyValPairs in our refrenced dictionary
        foreach (KeyValuePair<GameObject,float> KVP in displayDictionary.TestDict)
        {
            // this is to display our keys and values side by side in editor
            GUILayout.BeginHorizontal();
            // for convinience i have made the editable fields inside the ChangeKey and ChangeVal funcs so i do not have to pass in as many variables
            ChangeKey(KVP, displayDictionary);
            ChangeValue(KVP, displayDictionary);

            // this ends horrisontal mode 
            GUILayout.EndHorizontal();
        }

        //we keep our button and fields used for adding to the dictionary in here. We also call the AddToDict function in here
        AddToDictDisplay(displayDictionary);

    }

    public void AddToDict(DisplayDictionary DD, GameObject GM, float F)
    {
        foreach(KeyValuePair<GameObject, float> KVP in DD.TestDict)
        {
            if(placeHolder2 == KVP.Key)
            {
                break;
            }
            
        }
        DD.TestDict.Add(placeHolder2, F);
        Debug.Log(DD.TestDict.Count);
    }
    public void ChangeValue(KeyValuePair<GameObject,float> KVP, DisplayDictionary displayDictionary) 
    { 
        F = EditorGUILayout.FloatField(KVP.Value);
        if (F != KVP.Value)
        {
            displayDictionary.TestDict[placeHolder] =  F;
            Debug.Log("value changed");

        }

    }
    public void ChangeKey(KeyValuePair<GameObject, float> KVP, DisplayDictionary displayDictionary)
    {
        placeHolder = (GameObject)EditorGUILayout.ObjectField(KVP.Key, typeof(GameObject), true);
        if(placeHolder != KVP.Key)
        {
            float val = KVP.Value;
            displayDictionary.TestDict[placeHolder] = val;
            displayDictionary.TestDict.Remove(KVP.Key);
        }

    }

    public void AddToDictDisplay(DisplayDictionary displayDictionary)
    {
        placeHolder2 = (GameObject)EditorGUILayout.ObjectField(placeHolder2, typeof(GameObject), true);
        F = EditorGUILayout.FloatField(F);

        if (GUILayout.Button("Add To Dictionary"))
        {
            AddToDict(displayDictionary, placeHolder2, F);
        }

    }
}
