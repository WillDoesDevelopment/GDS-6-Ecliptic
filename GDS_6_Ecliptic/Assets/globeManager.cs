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
        if(HubManager.LevelNumber >= 1 && HubManager.LevelNumber <= 3)
        {
            Act1();
        }
        if(HubManager.LevelNumber >= 4)
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
    }

    public void Act3()
    {
        rend.material.EnableKeyword("_EMISSION");
    }
        
 }
