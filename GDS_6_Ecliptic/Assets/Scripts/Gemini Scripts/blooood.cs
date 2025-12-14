using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blooood : MonoBehaviour
{
    public GameObject particleObject;
    public ParticleSystem system;
    public HitVFX hitVFX;
    public GameObject player;
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
                hitVFX.GetComponent<HitVFX>().isHit = true;
                particleObject.transform.position = transform.position;
                player.GetComponent<PlayerController>().Damage();

                system.Emit(2);
                break;
            }

        }
    }

    public void blood()
    {
        hitVFX.GetComponent<HitVFX>().isHit = true;
        particleObject.transform.position = transform.position;
        player.GetComponent<PlayerController>().Damage();
        //player.GetComponent<PlayerController>().Knockback();

        system.Emit(2);
    }

}
