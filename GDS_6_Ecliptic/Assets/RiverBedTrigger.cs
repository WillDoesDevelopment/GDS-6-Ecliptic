using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverBedTrigger : MonoBehaviour
{
    public PlayerController controller;
    public PiscesManager pm;

    private void OnTriggerStay(Collider other) //first time
    {
        if (other.CompareTag("Player"))
        {
            pm.PiscesReset();
            controller.Respawn();
        }

    }
}
