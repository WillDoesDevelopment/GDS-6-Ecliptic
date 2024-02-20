using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSheepScript : MonoBehaviour
{
    public Animator PlayerAnim;
    public Animator SheepMovmentAnim;
    bool canStep = true;

    //a random vol and pitch
    [Range(0f, 1f)] public float stepVolume = 0.8f;
    [Range(0f, 0.8f)] public float stepPitchVariance = 0.5f;

    // our footstep audio component and array of audio clips
    public AudioSource audioSource;
    public AudioClip[] stepArray;

    // for funny haha
    bool playDaftPunk = false;

    void Start()
    {
        
    }

    void Update()
    {
       if(transform.GetChild(0).GetComponent<Animator>().GetBool("Animate") == true)
        {
            FootStepCalc();
        }
    }


    //put this here for readability. Here FootStepCalc() checks how far through the animation we are and calls FootStep() function at the right time
    public void FootStepCalc()
    {
        var h = PlayerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        h = h - Mathf.Floor(h);
        //Steps
        if (h > 0.3f && h < 0.4f && canStep == true)
        {
            Footstep();

        }
        if (h > 0.6f && h < 0.7f)
        {
            canStep = true;
        }
        if (h > 0.8f && h < 0.9f && canStep == true)
        {
            Footstep();

        }
        if (h > 0.9f || h < 0.2f)
        {
            canStep = true;
        }
    }

    public void triggerAnim()
    {
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Animate2");
        canStep = false;
        audioSource.enabled = false;
    }

    void Footstep()
    {
        //Debug.Log("Step");
        canStep = false;
        if (audioSource != null && stepArray != null)
        {
            audioSource.clip = stepArray[Random.Range(0, stepArray.Length)];
            audioSource.pitch = 1 + Random.Range(-stepPitchVariance, stepPitchVariance);
            audioSource.volume = stepVolume;
            audioSource.PlayOneShot(audioSource.clip);

            //step.Play();
        }
    }


}
