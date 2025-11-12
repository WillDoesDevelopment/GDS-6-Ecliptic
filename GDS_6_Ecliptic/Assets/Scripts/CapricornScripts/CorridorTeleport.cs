using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class CorridorTeleport : MonoBehaviour
{
    public GameObject player;
    public GameObject startCorridor;
    public GameObject endCorridor;
    CharacterController characterController;
    float range = 5f;
    // Start is called before the first frame update
    void Start()
    {
        characterController = player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,player.transform.position) < range)
        {
            Teleport();
        }
    }

    void Teleport()
    {
        characterController.enabled = false;
        player.transform.position += endCorridor.transform.position - startCorridor.transform.position;
        characterController.enabled = true;
    }

    private void OnDrawGizmos()
    {
        // Set the color with custom alpha
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
