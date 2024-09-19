using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitVFX : MonoBehaviour
{
    public Material[] glowAmt;
    public float shaderValue = 0;
    public bool isHit = false;
    public float hitLength = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //collects all the shaders parameters to start the animation
        glowAmt[0].GetFloat("_Glow_Amount");
        glowAmt[1].GetFloat("_Glow_Amount");
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isHit == true)
        {
            StartCoroutine(Hit());
        }
    }

    IEnumerator Hit()
    {
        shaderValue = 1;
        glowAmt[0].SetFloat("_Glow_Amount", shaderValue);
        glowAmt[1].SetFloat("_Glow_Amount", shaderValue);

        yield return new WaitForSeconds(hitLength);

        shaderValue = 0;
        glowAmt[0].SetFloat("_Glow_Amount", shaderValue);
        glowAmt[1].SetFloat("_Glow_Amount", shaderValue);

        yield return new WaitForSeconds(hitLength);

        shaderValue = 1;
        glowAmt[0].SetFloat("_Glow_Amount", shaderValue);
        glowAmt[1].SetFloat("_Glow_Amount", shaderValue);

        yield return new WaitForSeconds(hitLength);

        shaderValue = 0;
        glowAmt[0].SetFloat("_Glow_Amount", shaderValue);
        glowAmt[1].SetFloat("_Glow_Amount", shaderValue);

        yield return new WaitForSeconds(hitLength);

        shaderValue = 1;
        glowAmt[0].SetFloat("_Glow_Amount", shaderValue);
        glowAmt[1].SetFloat("_Glow_Amount", shaderValue);

        yield return new WaitForSeconds(hitLength);

        shaderValue = 0;
        glowAmt[0].SetFloat("_Glow_Amount", shaderValue);
        glowAmt[1].SetFloat("_Glow_Amount", shaderValue);

        yield return new WaitForSeconds(hitLength);
        isHit = false;

    }
    
}
