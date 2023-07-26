using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burstScript : MonoBehaviour
{

    public ParticleSystem system;

    // Start is called before the first frame update
    void Start()
    {
        system.Emit(10);
        Destroy(gameObject, 1.0f);
    }
    
}
