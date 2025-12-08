using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introCineCamScript : MonoBehaviour
{
    //This script handles the intro cameras, shader animation and rig animation

    public Animator anim;
    public Animator anim2;
    public GameObject plyer;
    public GameObject star;
    public string levelName;
    public Material[] glowAmt;
    public float amtStart = 1;
    public AudioSource snd;
    public AudioSource snd2;

    public OrreryScript orreryScript;

    private float shaderValue = 1f;
    private float change = 0.01f;

    private bool StartSpin = false;

    public GameObject[] Doors;
    public Material[] doorMats;
    void Start()
    {
        //collects all the shaders parameters to start the animation
        glowAmt[0].GetFloat("_Glow_Amount");
        glowAmt[1].GetFloat("_Glow_Amount");
        StartCoroutine(panCam()); //starts the first camera that follows the orb
        amtStart = 2;
        doorMats[5].DisableKeyword("_EMISSION");
        doorMats[6].DisableKeyword("_EMISSION");
        InvokeRepeating("AmtGo", 0.01f, 0.15f);
    }

    private void Update()
    {
        //starts the orrery script
        if (StartSpin)
        {
            orreryScript.AmbientSpin();
        }
        
    }

    IEnumerator panCam()
    {
        //This starts the follow cam which follows the glowing orb and switches the orb out between the character, orb and the shader animation.
        yield return new WaitForSeconds(2f);
        anim2.SetTrigger("FadeIn");
        yield return new WaitForSeconds(4f);
        plyer.SetActive(true);
        star.SetActive(false);
        //yield return new WaitForSeconds(2f);
        anim.SetTrigger("Pan");

        StartCoroutine(outro());
    }

    IEnumerator outro()
    {

        //outro function, this plays the final animation of ophie 

        //OPHIE DISCOVERS HE HAS HANDS
        yield return new WaitForSeconds(6f);
        snd2.Play();
        
        yield return new WaitForSeconds(7f);
        DoorsOn();
        snd.Play(); // INTRO OBSERVATORY SND PLAYS HERE
        anim.SetTrigger("End"); // STOP THAT! ZOOM CAMERA OUT
        //yield return new WaitForSeconds(f);
        StartSpin = true;   
        
        yield return new WaitForSeconds(9f);
        SceneManager.LoadScene(2); // GOES TO HUB AFTER THAT SOUNDS DONE


    }

    public void AmtGo()
    {
        //This triggers the shader animation lerp that changes the glow from the material 1 to 0.
            shaderValue -= change;
            glowAmt[0].SetFloat("_Glow_Amount", shaderValue);
            glowAmt[1].SetFloat("_Glow_Amount", shaderValue);


    }

    public void DoorsOn()
    {
        //This turns the emission on for all the doors, the platform and the podium.
        Doors[0].GetComponent<MeshRenderer>().material = doorMats[0];
        Doors[1].GetComponent<MeshRenderer>().material = doorMats[1];
        Doors[2].GetComponent<MeshRenderer>().material = doorMats[2];
        Doors[3].GetComponent<MeshRenderer>().material = doorMats[3];
        Doors[4].GetComponent<MeshRenderer>().material = doorMats[4];

        doorMats[5].EnableKeyword("_EMISSION");
        doorMats[6].EnableKeyword("_EMISSION");
    }

}
