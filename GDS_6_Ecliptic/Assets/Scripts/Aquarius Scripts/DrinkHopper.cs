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
            var obj = other.gameObject;
            AbsorbIngredient(obj);
        }
    }
}

