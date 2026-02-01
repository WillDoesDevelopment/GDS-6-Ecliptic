using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class globeManager : MonoBehaviour
{
    public Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend.material.DisableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        if(HubManager.LevelNumber >= 0 && HubManager.LevelNumber <= 3)
        {
            Act1();
        }
        if(HubManager.LevelNumber >= 4 && HubManager.LevelNumber <= 7)
        {
            Act2();
        }
        if (HubManager.LevelNumber >= 7)
        {
            return;
        }
    }

    public void Act1()
    {
        rend.material.EnableKeyword("_EMISSION");
        this.GetComponent<Animator>().SetTrigger("Act1Rot");
    }

    public void Act2()
    {
        rend.material.EnableKeyword("_EMISSION");
        this.GetComponent<Animator>().SetTrigger("Act3Bob");
    }

    public void Act3()
    {
        rend.material.EnableKeyword("_EMISSION");
    }
        
 }
