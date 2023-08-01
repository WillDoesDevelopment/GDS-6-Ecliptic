using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINavMesh : MonoBehaviour
{
    public NavMeshAgent NMA;
    private Vector3 CurrentNavPos;

    public Queue<Transform> positionQueue = new Queue<Transform>();
    public Transform[] positions;

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
        CurrentNavPos = positionQueue.Peek().position;
        if (Proximity(CurrentNavPos))

        {
            Transform temp = positionQueue.Dequeue();
            positionQueue.Enqueue(temp);
            
        }
        NMA.SetDestination(CurrentNavPos);
    }

    public bool Proximity( Vector3 NavPos)
    {
        float playerDistZ = NavPos.z - transform.position.z;
        float playerDistX = NavPos.x - transform.position.x;
        if (Mathf.Sqrt(Mathf.Pow(playerDistX, 2) + Mathf.Pow(playerDistZ, 2)) < 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
