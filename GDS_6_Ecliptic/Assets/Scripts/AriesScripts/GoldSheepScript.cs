using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSheepScript : MonoBehaviour
{
    public Animator PlayerAnim;
    public Animator SheepMovmentAnim;
    bool canStep = true;
    [Range(0f, 1f)] public float stepVolume = 0.8f;
    [Range(0f, 0.8f)] public float stepPitchVariance = 0.5f;
    public AudioSource audioSource;
    public AudioClip[] stepArray;
    bool playDaftPunk = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


       if(transform.GetChild(0).GetComponent<Animator>().GetBool("Animate") == true)
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
