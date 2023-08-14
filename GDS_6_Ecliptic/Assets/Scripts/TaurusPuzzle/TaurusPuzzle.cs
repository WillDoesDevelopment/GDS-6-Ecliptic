 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaurusPuzzle : MonoBehaviour
{
    public int CollectedItems;

    public GameObject Player;

    public HubManager HB;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckWinCondition();
    }
    public void CheckWinCondition()
    {
        if (CollectedItems == 4)
        {
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
