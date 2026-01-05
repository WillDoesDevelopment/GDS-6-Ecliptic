using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static aquaManager;
[CreateAssetMenu(fileName = "Data", menuName = "AquariusScripts/Recipe", order = 1)]
public class Recipe : ScriptableObject
{
    [SerializeField] private string recipeName;
    public GameObject recPanel;
    public List<IngredientChoice> ingredients;

}
