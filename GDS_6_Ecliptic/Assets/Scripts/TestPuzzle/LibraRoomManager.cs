using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraRoomManager : MonoBehaviour
{
    //what needs to happen in the Libra room??? 
    //and what components do we need to know about???
    //want these combinations to rotate 360/9  degrees when the player presses "return" infront of the combination
    public GameObject[] Combination;

    public GameObject player;

    public Animator CombinationsAnim;
    void Start()
    {
        
    }

    void Update()
    {
        RotateCombination(ReturnNearestCombination());
    }

    public void RotateCombination(GameObject Combination)
    {
        if (Dist(player, Combination) < 2)
        {
            CombinationsAnim.SetBool(Combination.name, true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Combination.transform.Rotate(0, 0, 360 / 9);
            }
        }
        else
        {
            CombinationsAnim.SetBool(Combination.name, false);
        }
    }
    public GameObject ReturnNearestCombination()
    {
        GameObject NearestCombination = null;
        foreach(GameObject g in Combination)
        {
            if(NearestCombination == null || Dist(player,g) < Dist(player,NearestCombination))
            {
                NearestCombination = g;
            }
        }
        return NearestCombination;

    }
    public float Dist(GameObject Player, GameObject other)
    {
        return Vector3.Distance(player.transform.position, other.transform.position);
 
    }
}
