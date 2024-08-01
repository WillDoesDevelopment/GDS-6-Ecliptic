using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthUIManager : MonoBehaviour
{
    public PlayerController PC;
    private int tempHealth;
    public GameObject[] HealthUIList;
    // Start is called before the first frame update
    void Awake()
    {
        PC = GameObject.FindObjectOfType<PlayerController>();
        HealthUIDisplay();
        tempHealth = PC.health;
    }

    // Update is called once per frame
    void Update()
    {
        if(tempHealth!= PC.health)
        {
            HealthUIDisplay();
            Debug.Log(PC.health);
            tempHealth = PC.health;
        }
    }

    public void HealthUIDisplay()
    {
        Debug.Log("running");
        foreach(GameObject g in HealthUIList)
        {
            g.SetActive(false);
        }
        for (int i = 0; i < PC.health; i++)
        {
            HealthUIList[i].SetActive(true);
            //Debug.Log(i);

        }
    }
}
