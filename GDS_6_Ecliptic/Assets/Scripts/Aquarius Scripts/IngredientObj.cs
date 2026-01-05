using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "AquariusScripts/IngredientObj", order = 1)]
public class IngredientObj : ScriptableObject
{
    [SerializeField] private IngredientChoice ingType;

    public List<int> ingreIDNum;

    public GameObject ingreObj;

    public IngredientChoice Type
    {
        get { return ingType; }
    }

    public GameObject IngreObjPrefab
    {
        get { return  ingreObj; }
    }

    public bool CompareList(List<int> list)
    {
        if (ingreIDNum.Count == list.Count)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Debug.Log("Index: " + list[i]);
                if (ingreIDNum[i] != list[i])
                    return false;
            }
            return true;
        }

        return false;
    }

}
