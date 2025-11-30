using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class endingMan : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("General")]
    public GameObject[] endObj;
    public Dialogue[] dia;
    public PlayerController pc;
    public HubManager hm;
    public bool endStarted = false;
    private bool globDiaAct = false;

    [Header("Human Ending")]
    public string HumanScene;

    [Header("Star Ending")]
    public GameObject door;
    public GameObject hub;
    public GameObject globe;

    [ColorUsage(true, true)]
    public Color colour;
    public Material newDoorMat;

    public GameObject globeDia;

    // Update is called once per frame
    void Update()
    {
        if(HubManager.LevelNumber == 12 && globDiaAct == false)
        {
            StartCoroutine(endStartUp());
            globeDia.SetActive(false);
        }

        if (dia[0].DialogueMode == Dialogue.DialogueState.Finished) //when the chatter has stopped, spawn in both constellation and symbol to choose
        {
            endObj[2].SetActive(true);
            endObj[3].SetActive(true);
            
        }

        if (dia[1].DialogueMode == Dialogue.DialogueState.InProgress) //Picking the constellation
        {
            endObj[3].SetActive(false); //symbol is hidden

        }

        if (dia[1].DialogueMode == Dialogue.DialogueState.Finished && endStarted == false)
        {
            endObj[2].SetActive(false);
            endObj[3].SetActive(false); //constellation is also hidden
            globeDia.SetActive(true);
            StartCoroutine(God());
        }

        if (dia[2].DialogueMode == Dialogue.DialogueState.InProgress) //Picking the Earth Symbol
        {
            endObj[2].SetActive(false); //constellation is hidden
        }

        if (dia[2].DialogueMode == Dialogue.DialogueState.Finished && endStarted == false)
        {
            endObj[2].SetActive(false);
            endObj[3].SetActive(false); //symbol is also hidden
            StartCoroutine(Human()); //start human ending
        }

    }


    IEnumerator endStartUp()
    {
        endObj[0].SetActive(true);
        endObj[1].SetActive(true);
        pc.canWalk = false;
        yield return new WaitForSeconds(3f);
        endObj[4].SetActive(true);
        pc.canWalk = true;
        globDiaAct = true;
    }


    IEnumerator Human()
    {
        endStarted = true;
        pc.canWalk = false;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(HumanScene);

    }

    IEnumerator God()
    {
        endStarted = true;
        pc.canWalk = false;
        yield return new WaitForSeconds(3f);
        hub.GetComponent<Renderer>().material.SetColor("_EmissionColor", colour);
        globe.GetComponent<Renderer>().material.SetColor("_EmissionColor", colour);
        yield return new WaitForSeconds(3f);
        door.GetComponent<Renderer>().material = newDoorMat;
        pc.canWalk = true;

    }



}
