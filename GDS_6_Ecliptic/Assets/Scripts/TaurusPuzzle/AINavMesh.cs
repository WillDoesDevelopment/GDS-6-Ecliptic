using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINavMesh : MonoBehaviour
{
    public GameObject Player;
    public int visionDist;

    public NavMeshAgent NMA;
    private Vector3 CurrentNavPos;

    public Queue<Transform> positionQueue = new Queue<Transform>();
    public Transform[] positions;

    public DialogueTrigger StartDialogue;
    public bool NavMeshPause = false;

    private void Awake()
    {
        foreach (Transform t in positions)
        {
            positionQueue.Enqueue(t);
        }
        //Debug.Log(positionQueue.Count);
    }
    void Update()
    {
        if(StartDialogue.dialogue.DialogueMode == Dialogue.DialogueState.Finished && NavMeshPause == false)
        {

            GetComponent<Renderer>().material.color = Color.red;
            CurrentNavPos = positionQueue.Peek().position;
            if(Proximity(Player.transform.position, visionDist))
            {
                CurrentNavPos = Player.transform.position;
                GetComponent<Renderer>().material.color = Color.black;
            }
            else if (Proximity(CurrentNavPos, 1))
            {
                Transform temp = positionQueue.Dequeue();
                positionQueue.Enqueue(temp);
            
            }
            NMA.SetDestination(CurrentNavPos);
        }
        else
        {
            
            NMA.SetDestination(this.transform.position);
        }
    }

    public bool Proximity( Vector3 NavPos, float radius)
    {
        float playerDistZ = NavPos.z - transform.position.z;
        float playerDistX = NavPos.x - transform.position.x;
        if (Mathf.Sqrt(Mathf.Pow(playerDistX, 2) + Mathf.Pow(playerDistZ, 2)) < radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == Player)
        {
            Debug.Log("GG loser");
        }
    }
}
