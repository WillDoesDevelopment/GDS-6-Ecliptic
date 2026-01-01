using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "AquariusScripts/IngredientObj", order = 1)]
public class IngredientObj : ScriptableObject
{
    [SerializeField] private string ingName;

    public List<int> ingreIDNum;

    public GameObject ingreObj;

    public static event Action<int> onPlayerDeath;

    public string Name
    {
        get { return ingreObj.name; }
    }

}
