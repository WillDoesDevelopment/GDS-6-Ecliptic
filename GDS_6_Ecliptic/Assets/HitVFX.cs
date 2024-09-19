using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitVFX : MonoBehaviour
{
    public Material[] glowAmt;
    public float shaderValue = 0;
    public bool isHit = false;

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
        if (isHit == false)
        {
            shaderValue = 0;
            glowAmt[0].SetFloat("_Glow_Amount", shaderValue);
            glowAmt[1].SetFloat("_Glow_Amount", shaderValue);
        }

        if (isHit == true)
        {
            shaderValue = 1;
            glowAmt[0].SetFloat("_Glow_Amount", shaderValue);
            glowAmt[1].SetFloat("_Glow_Amount", shaderValue);
        }
    }
}
