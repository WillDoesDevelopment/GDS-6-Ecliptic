using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public GameObject ItemIndicator;

    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetArtifact()
    {
        Debug.Log("Working");
        player.GetComponent<PickUpScript>().PutDown(this.gameObject);
        player.GetComponent<PickUpScript>().HoldingObj = player.GetComponent<PickUpScript>().PickUpPos.gameObject;
        anim.SetBool("Bob", true);
        ItemIndicator.SetActive(false);
        this.transform.position = startPos;
    }

}
