using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINavMesh : MonoBehaviour
{
    public NavMeshAgent NMA;
    void Update()
    {
        NMA.SetDestination(new Vector3(0, 0, 0));
    }
}
