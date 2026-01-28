using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class StaircaseRepeat : MonoBehaviour
{
    public GameObject player;
    CharacterController characterController;
    public GameObject teleportStart;
    public GameObject teleportEnd;
    public GameObject[] stairSegments;
    public GameObject[] decorationPrefabs;
    public float range = 4f;
    Vector3 teleportDelta;

    // Start is called before the first frame update
    void Start()
    {
        characterController = player.GetComponent<CharacterController>();
        teleportDelta = teleportEnd.transform.position - teleportStart.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            Teleport();
        }
    }

    void Teleport()
    {
        characterController.enabled = false;
        player.transform.position += teleportDelta;
        characterController.enabled = true;
        
        foreach (Transform child in stairSegments[0].transform)
        {
            Destroy(child.gameObject);
        }

        for(int i = 1; i < stairSegments.Length; i++)
        {
            while (stairSegments[i].transform.childCount > 0)
            {                
                stairSegments[i].transform.GetChild(0).transform.position += teleportDelta;
                stairSegments[i].transform.GetChild(0).parent = stairSegments[i - 1].transform;
            }


        }

        SpawnTopDecoration();
    }

    void SpawnTopDecoration()
    {
        GameObject newObject = Instantiate(decorationPrefabs[Random.Range(0,decorationPrefabs.Length)], stairSegments[stairSegments.Length-1].transform.position + new Vector3(Random.Range(-2f,2f), 1.5f, 0f), Quaternion.identity);
        newObject.transform.parent = stairSegments[stairSegments.Length - 1].transform;        
    }
    private void OnDrawGizmos()
    {
        // Set the color with custom alpha
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
