using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorCollapse : MonoBehaviour
{
    float timer = 0.0f;
    float x;
    float y;
    float z;
    Vector3 Pos;


    // Start is called before the first frame update
    void Start()
    {
        x = Mathf.Floor(transform.position.x + 0.5f);
        y = transform.position.y;
        z = Mathf.Floor(transform.position.z + 0.5f);
        Pos = new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1.0)
        {
            collapse(2.0f);            
        }
        if (timer > 1.5)
        {
            collapse(3.0f);
        }
        if (timer > 2.0)
        {
            collapse(4.0f);
        }
        if (timer > 2.5)
        {
            collapse(5.0f);
        }
    }

    public void collapse(float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(Pos, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.layer == LayerMask.NameToLayer("Floor"))
            {
                hitCollider.transform.GetComponent<floorScript>().solid = false;
            }

        }

    }
}
