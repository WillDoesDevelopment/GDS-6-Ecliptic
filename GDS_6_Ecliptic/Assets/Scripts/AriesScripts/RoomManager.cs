using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject NormalSheep;
    public GameObject GoldSheep;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PuzzleStartCheck();
        StartledSheepCheck();
    }

    void PuzzleStartCheck()
    {
        if (GoldSheep.GetComponent<DialogueTrigger>().enabled != true)
        {
            GoldSheep.GetComponent<Animator>().SetTrigger("Animate");
        }
    }

    void StartledSheepCheck()
    {
        if (NormalSheep.GetComponent<DialogueTrigger>().enabled != true)
        {
            NormalSheep.GetComponent<Animator>().SetTrigger("Animate");
            

        }
    }
}
