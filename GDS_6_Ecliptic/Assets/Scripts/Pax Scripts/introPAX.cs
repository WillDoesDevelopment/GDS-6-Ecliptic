using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introPAX : MonoBehaviour
{
    //This script handles the intro cameras, shader animation and rig animation
    public GameObject plyer;
    public string levelName;
  
    public AudioSource snd;
    public AudioSource snd2;

    public OrreryScript orreryScript;

    private bool StartSpin = false;

    public GameObject[] Doors;
    public Material[] doorMats;
    void Start()
    {

        StartCoroutine(outro());
    }

    private void Update()
    {
        //starts the orrery script
        if (StartSpin)
        {
            orreryScript.AmbientSpin();
        }
        
    }

 

    IEnumerator outro()
    {

        yield return new WaitForSeconds(6f);
        snd2.Play();
        
        yield return new WaitForSeconds(7f);
        DoorsOn();
        snd.Play(); // INTRO OBSERVATORY SND PLAYS HERE
        StartSpin = true;   
        
        yield return new WaitForSeconds(9f);
        SceneManager.LoadScene(2); // GOES TO HUB AFTER THAT SOUNDS DONE


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
