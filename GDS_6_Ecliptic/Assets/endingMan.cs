using System.Collections;
using System.Collections.Generic;
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

    [Header("Human Ending")]
    public string HumanScene;

    [Header("Star Ending")]
    public GameObject door;
    public GameObject hub;
    public GameObject globe;

    public Color colour;
    public Material newDoorMat;

    public GameObject globeDia;

    void Start()
    {
        endObj[0].SetActive(true);
        endObj[1].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (dia[1].DialogueMode == Dialogue.DialogueState.Finished) //when the chatter has stopped, spawn in both constellation and symbol to choose
        {
            endObj[2].SetActive(true);
            endObj[3].SetActive(true);
        }

        if (dia[2].DialogueMode == Dialogue.DialogueState.InProgress) //Picking the constellation
        {
            endObj[3].SetActive(false); //symbol is hidden

            if (dia[2].DialogueMode == Dialogue.DialogueState.Finished)
            {
                endObj[2].SetActive(false); //constellation is also hidden
            }
        }

        if (dia[3].DialogueMode == Dialogue.DialogueState.InProgress) //Picking the Earth Symbol
        {
            endObj[2].SetActive(false); //constellation is hidden

            if (dia[3].DialogueMode == Dialogue.DialogueState.Finished)
            {
                endObj[3].SetActive(false); //symbol is also hidden
                StartCoroutine(Human()); //start human ending
            }
        }

    }

    IEnumerator Human()
    {
        pc.canWalk = false;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(HumanScene);

    }

    IEnumerator God()
    {
        pc.canWalk = false;
        yield return new WaitForSeconds(3f);
        hub.GetComponent<Material>().SetColor("_EmissionColor", colour);
        globe.GetComponent<Material>().SetColor("_EmissionColor", colour);

    }



}
