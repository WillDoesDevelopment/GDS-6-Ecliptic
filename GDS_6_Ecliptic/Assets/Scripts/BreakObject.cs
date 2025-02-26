using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
    // Start is called before the first frame update
    public bool smash = false;

    /*                                                          Shader VFX
    public Material mat;
    private float alphaFloat = 0f;
    private float change = 0.2f;
    */

    float timer = 0f;

    bool stage1Lock = false;
    bool stage2Lock = false;
    bool stage3Lock = false;

    public bool explodeForce = false;
    public float explosionForce = 150f;
    public float explosionRadius = 1f;

    void Start()
    {
        /*                                                      Shader VFX
        alphaFloat = 0f;
        mat.GetFloat("_Clip_Amt");
        mat.SetFloat("_Clip_Amt", alphaFloat);
        */
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

        if (timer > 3 && stage2Lock == false)
        {
            BreakStage2();
            stage2Lock = true;
        }

        if (timer > 4 && stage3Lock == false)
        {
            BreakStage3();
            stage3Lock = true;

        }
    }

    void BreakStage1() //Break
    {     
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
    }

    void BreakStage2() //Dissolve
    {
        //InvokeRepeating("DissolveTrigger", 0.5f, 10f);        Shader VFX

    }

    void BreakStage3() //Deactivate
    {        
        gameObject.SetActive(false);
    }

    /*                                                          Shader VFX
    void DissolveTrigger()
    {
        mat.GetFloat("_Clip_Amt");

        alphaFloat += change;
        mat.SetFloat("_Clip_Amt", alphaFloat);
    }
    */

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

