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
        base.OnInspectorGUI();
        //TempDict = new Dictionary<GameObject, float>();
        DisplayDictionary displayDictionary = (DisplayDictionary)target;
       

        //placeHolder2 = (GameObject)EditorGUILayout.ObjectField(placeHolder2, typeof(GameObject), true);
        if(true)
        {
            foreach (KeyValuePair<GameObject,float> KVP in displayDictionary.TestDict)
            {
               GUILayout.BeginHorizontal();
                placeHolder = (GameObject)EditorGUILayout.ObjectField(KVP.Key, typeof(GameObject), true);
                EditorGUILayout.FloatField(KVP.Value);
               GUILayout.EndHorizontal();
                if(placeHolder != KVP.Key)
                {
                    float val = KVP.Value;
                    displayDictionary.TestDict[placeHolder] = val;
                    displayDictionary.TestDict.Remove(KVP.Key);
                    Debug.Log("value changed");
                }
            }

        }
        placeHolder2 = (GameObject)EditorGUILayout.ObjectField(placeHolder2, typeof(GameObject), true);
        F = EditorGUILayout.FloatField(F);

        if (GUILayout.Button("Add To Dictionary"))
        {
            AddToDict(displayDictionary, placeHolder2, F);
        }
    }

    public void AddToDict(DisplayDictionary DD, GameObject GM, float F)
    {
        DD.TestDict.Add(placeHolder2, F);
        Debug.Log(DD.TestDict.Count);
    }

}
