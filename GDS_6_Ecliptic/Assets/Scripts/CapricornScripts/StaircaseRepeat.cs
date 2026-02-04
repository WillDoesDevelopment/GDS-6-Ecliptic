using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class StaircaseRepeat : MonoBehaviour
{
    public GameObject player;
    PlayerController playerController;
    CharacterController characterController;
    public GameObject teleportStart;
    public GameObject teleportEnd;
    public GameObject[] stairSegments;
    public GameObject[] decorationPrefabs;
    public float range = 4f;
    public int repeatCount = 0;
    Vector3 teleportDelta;

    //When to start dialogue
    public int triggerTime1;
    public DialogueTrigger dialogueTrigger1;
    public int triggerTime2;
    public DialogueTrigger dialogueTrigger2;
    public int triggerTime3;
    public DialogueTrigger dialogueTrigger3;

    // Start is called before the first frame update
    void Start()
    {
        characterController = player.GetComponent<CharacterController>();
        playerController = player.GetComponent<PlayerController>();
        teleportDelta = teleportEnd.transform.position - teleportStart.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            Teleport();
            repeatCount += 1;
            if(repeatCount == triggerTime1) { StartDialogue(dialogueTrigger1); }
            if(repeatCount == triggerTime2) { StartDialogue(dialogueTrigger2); }
            if(repeatCount == triggerTime3) { StartDialogue(dialogueTrigger3); }
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
        GameObject newObject = Instantiate(decorationPrefabs[Random.Range(0,decorationPrefabs.Length)], stairSegments[stairSegments.Length-1].transform.position + new Vector3( 0f, 1.5f, Random.Range(-2f, 2f)), Quaternion.identity);
        newObject.transform.parent = stairSegments[stairSegments.Length - 1].transform;        
    }
    
    void StartDialogue(DialogueTrigger dt)
    {
        playerController.playerState = PlayerState.Dialogue;        

        if (dt != null)
        {
            dt.OnEvent = false;
            dt.TriggerDialogue();
        }
    }
    
    private void OnDrawGizmos()
    {
        // Set the color with custom alpha
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
