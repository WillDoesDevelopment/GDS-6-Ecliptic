using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrinkHopper : MonoBehaviour
{
    public GameEventObj eObj;

    public void AbsorbIngredient(GameObject obj)
    {
        eObj.TriggerEvent(obj);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            var obj = other.GetComponent<GameObject>();
            AbsorbIngredient(obj);
        }
    }

    public bool CheckDrinkContents(List<IngredientChoice> A, List<IngredientChoice> B)
    {
        if (A.Count != B.Count)
        {
            return false;
        }

        List<IngredientChoice> ASort = A.ToList();
        ASort.Sort();
        List<IngredientChoice> BSort = B.ToList();
        BSort.Sort();

        for (int i = 0; i < A.Count; i++)
        {
            if (ASort[i] != BSort[i])
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        return false;

    }
}

