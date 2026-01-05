using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrinkHopper : MonoBehaviour
{
    public GameEvent eTrigger;

    public void AbsorbIngredient(GameObject obj)
    {
        eTrigger.TriggerEvent();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            AbsorbIngredient(other.gameObject);
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

