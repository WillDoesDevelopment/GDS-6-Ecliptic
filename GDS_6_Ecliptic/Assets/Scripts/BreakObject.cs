using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
    // Start is called before the first frame update
    public bool smash = false;

    Renderer[] allChildren;

    float timer = 0f;
    public float stage2Time = 3.0f;
    public float stage3Time = 4.0f;

    bool stage1Lock = false;
    //bool stage2Lock = false;
    bool stage3Lock = false;

    public bool explodeForce = false;
    public float explosionForce = 150f;
    public float explosionRadius = 1f;

    void Start()
    {        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (smash == true)
        {
            SmashUpdate();            
        }       

    }

    public void SmashUpdate()
    {
        timer += Time.deltaTime;

        if (stage1Lock == false)
        {
            BreakStage1();
            stage1Lock = true;
        }

        if (timer > stage2Time)
        {
            BreakStage2();            
        }

        if (timer > stage3Time && stage3Lock == false)
        {
            BreakStage3();
            stage3Lock = true;

        }
    }

    void BreakStage1() //Break
    {     
        //Should add layer filter for physics interaction

        if (TryGetComponent(out MeshCollider MC))
        {
            MC.enabled = false;
        }
        if (TryGetComponent(out MeshRenderer MR))
        {
            MR.enabled = false;
        }
        if (TryGetComponent(out BoxCollider BC))
        {
            BC.enabled = false;
        }

        transform.GetChild(0).gameObject.SetActive(true);
        if(explodeForce)
        {
            ExplosionForce();
        }

        allChildren = GetComponentsInChildren<Renderer>();
    }

    void BreakStage2() //Dissolve
    {                      
        foreach (Renderer rend in allChildren)
        {
            rend.material.SetFloat("_Clip_Amt", Mathf.InverseLerp(stage2Time, stage3Time, timer));//Shader VFX  
        }
    }

    void BreakStage3() //Deactivate
    {        
        gameObject.SetActive(false);
    }
    
    void ExplosionForce()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius);
        }
    }
}

