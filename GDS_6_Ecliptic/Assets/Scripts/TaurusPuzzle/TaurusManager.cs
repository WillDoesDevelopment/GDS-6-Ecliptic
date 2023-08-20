 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaurusManager : MonoBehaviour
{
    public int CollectedItems;

    public GameObject Player;

    public HubManager HB;

    public AINavMesh ANM;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckWinCondition();
        GiftPickUpCheck();
    }

    public void GiftPickUpCheck()
    {
        GameObject HeldObj = Player.GetComponent<PickUpScript>().HoldingObj;
        if (HeldObj != null)
        {
            HeldObj.GetComponent<DialogueTrigger>().OnEventCheck();
            HeldObj.GetComponent<DialogueTrigger>().OnEvent = false;
            ANM.NavMeshPause = true;
            if (HeldObj.GetComponent<DialogueTrigger>().dialogue.DialogueMode == Dialogue.DialogueState.Finished)
            {
                ANM.NavMeshPause = false;
            }
        }
    }
    public void CheckWinCondition()
    {
        if (CollectedItems == 4)
        {
            // here we will check if dialogue is done
            HB.AddOneToLevel();
            HB.SendToHub();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Artifact>() != null)
        {
            CollectedItems += 1;
            Player.GetComponent<PickUpScript>().holding = false;
            collision.gameObject.SetActive(false);
        }
    }
    public void Reset()
    {
        Player.transform.position = new Vector3(0f,0.5f,0f);
    }
}
