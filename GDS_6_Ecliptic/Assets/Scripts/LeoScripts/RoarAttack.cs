using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoarAttack : MonoBehaviour
{
    float timer = 5f;
    float speed = 3f;
    Vector3 lineStart;
    Vector3 lineEnd;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            Destroy(gameObject);
        }
        transform.position += transform.forward * speed * Time.deltaTime;

        lineStart = transform.position + transform.right * 2;
        lineEnd = transform.position - transform.right * 2;

        if (Physics.Linecast(lineStart, lineEnd, out RaycastHit hit, LayerMask.GetMask("Default")))
        {
            if(hit.transform.name == "Player")
            {
                if(hit.transform.gameObject.GetComponent<PlayerController>().playerState == PlayerState.Walk)
                {
                    hit.transform.gameObject.GetComponent<PlayerController>().playerState = PlayerState.Damage;
                }
                
            }            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(lineStart, lineEnd);
    }
}
