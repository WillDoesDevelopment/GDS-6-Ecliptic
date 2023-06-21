using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DisplayDictionary))]
public class SerialiseDictionary : Editor
{
    
    Dictionary<GameObject, float> KeyPairs;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        KeyPairs = new Dictionary<GameObject, float>();

        DisplayDictionary MyDictionary = (DisplayDictionary)target;
        KeyPairs = MyDictionary.keyValuePairs;

        foreach(KeyValuePair<GameObject,float> KVP in KeyPairs)
        {
            GUILayout.BeginHorizontal();

            MyDictionary.PlaceHolder = (GameObject)EditorGUILayout.ObjectField(KVP.Key, typeof(GameObject), true);
            //EditorGUILayout.FloatField(KVP.Value, typeof(float), true);

            GUILayout.EndHorizontal();
        }
    }

}
