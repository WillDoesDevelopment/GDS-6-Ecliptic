using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraRoomManager : MonoBehaviour
{
    //what needs to happen in the Libra room??? 
    //and what components do we need to know about???
    //want these combinations to rotate 360/9  degrees when the player presses "return" infront of the combination
    public GameObject[] Combination;

    public int[] CombinationNumbers;

    public GameObject player;

    public Animator CombinationsAnim;

    public HubManager HM;

    public DialogueTrigger EndDialogue;
    void Start()
    {
        
    }

    void Update()
    {
        if(EndDialogue.OnEvent != false)
        {
            RotateCombination(ReturnNearestCombination());

        }
        CheckWinCondition();
    }

    // Every frame we check if the nearest combination object is close enough and if it should be rotated
    public void RotateCombination(GameObject Combination)
    {
        if (Dist(player, Combination) < 3)
        {
            //Debug.Log(Combination.name);
            CombinationsAnim.SetBool(Combination.name, true);
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Submit"))
            {
                Combination.transform.Rotate(0, 0, -360 / 9);
                Combination.GetComponent<CombinationVal>().Val += 1;
            }
        }
        else
        {
            CombinationsAnim.SetBool(Combination.name, false);
        }
    }

    // if all the vals in the combination objects are correct, you win
    public void CheckWinCondition()
    {
        if (Combination[0].GetComponent<CombinationVal>().Val == 2 && Combination[1].GetComponent<CombinationVal>().Val == 4 && Combination[2].GetComponent<CombinationVal>().Val == 7)
        {
            foreach(GameObject g in Combination)
            {
                CombinationsAnim.SetBool(g.name, true);
            }
            EndDialogue.OnEventCheck();
            EndDialogue.OnEvent = false;
            if(EndDialogue.dialogue.DialogueMode == Dialogue.DialogueState.Finished)
            {
                //HM.SendToHub();
                HM.SetGameStage(3);
            }

        }
    }

    // this helper function returns the closest Combination Object
    public GameObject ReturnNearestCombination()
    {
        GameObject NearestCombination = null;
        foreach(GameObject g in Combination)
        {
            CombinationsAnim.SetBool(g.name, false);
            if (NearestCombination == null || Dist(player,g) < Dist(player,NearestCombination))
            {
                NearestCombination = g;
            }
        }
        return NearestCombination;

    }

    // this helper function returns a distance float from two game objects
    public float Dist(GameObject Player, GameObject other)
    {
        return Vector3.Distance(player.transform.position, other.transform.position);
 
    }


}
