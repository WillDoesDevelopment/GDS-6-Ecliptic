using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blooood : MonoBehaviour
{
    public GameObject particleObject;
    public ParticleSystem system;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    private void Update()
    {
        var colliders = Physics.OverlapSphere(transform.position, .5f);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.tag == "Spike")
            {
                particleObject.transform.position = transform.position;
                
                system.Emit(2);
                break;
            }

        }
    }

}
