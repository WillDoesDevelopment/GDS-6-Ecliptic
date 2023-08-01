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
        //CurrentNavPos = positionQueue.Peek();
        if (this.transform.position == CurrentNavPos)
        {

        }
        NMA.SetDestination(new Vector3(0, 0, 0));
    }
}
