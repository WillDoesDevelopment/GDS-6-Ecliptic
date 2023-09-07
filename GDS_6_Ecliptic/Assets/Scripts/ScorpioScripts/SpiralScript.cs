using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralScript : MonoBehaviour
{
    public Animator anim;
    
    [Range(0, 0.98f)] public float animPercent;

    void Start()
    {
        //anim = gameObject.GetComponent<Animation>();

    }

    // Update is called once per frame
    void Update()
    {
        //anim["Scorpion2"].time = animPercent;
        anim.SetFloat("MotionPercent", animPercent);
    }
}
