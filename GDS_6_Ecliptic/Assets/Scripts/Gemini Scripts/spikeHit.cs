using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeHit : MonoBehaviour
{    
    public float width = 1f;
    public float length = 1f;

    public GameObject player;
    public GameObject doppleganger;
    public HitVFX playerHitVFX;
    public HitVFX dopplegangerHitVFX;

    float timer = 2f;
    bool hit = false;
    public AnimationCurve knockbackV;
    //public AnimationCurve knockbackH;

    public Vector3 Point2;
    Vector3 Point1;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, new Vector3(width, 1.0f, length) / 2, transform.rotation); //Hitbox for player
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject == player || hitCollider.gameObject == doppleganger)
            {
                if(player.GetComponent<PlayerController>().playerState == PlayerState.Walk)
                {
                    hit = true;
                    timer = 0f;
                    Point1 = player.transform.position;
                    playerHitVFX.GetComponent<HitVFX>().isHit = true;
                    player.GetComponent<PlayerController>().Damage();
                    player.GetComponent<PlayerController>().playerState = PlayerState.Freeze;
                    doppleganger.GetComponent<doppelganger>().fall = false;
                }                
            }
        }

        if(hit == true)
        {
            timer += Time.deltaTime;
            if(timer < 2)
            {
                player.transform.position = new Vector3(Mathf.Lerp(Point1.x, Point2.x, timer / 2), knockbackV.Evaluate(timer / 2) * 5f + Point2.y, Mathf.Lerp(Point1.z, Point2.z, timer / 2));
            }
            else
            {
                player.GetComponent<PlayerController>().playerState = PlayerState.Walk;
                doppleganger.GetComponent<doppelganger>().fall = true;
                hit = false;
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var p1 = transform.position + transform.forward * length / 2 + transform.right * width / 2;
        var p2 = transform.position - transform.forward * length / 2 + transform.right * width / 2;
        var p3 = transform.position - transform.forward * length / 2 - transform.right * width / 2;
        var p4 = transform.position + transform.forward * length / 2 - transform.right * width / 2;

        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);
    }
}
