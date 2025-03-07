using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class columnScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool fall = false;
    public GameObject floorSegment;
    public GameObject barrierObject;
    public GameObject brokenColumn;
    public GameObject lion;
    public GameObject Shield;

    Renderer[] allChildren;

    float timer = 0f;
    public float stage2Time = 6.0f;
    public float stage3Time = 8.0f;

    bool stage1Lock = false;
    bool stage2Lock = false;
    bool stage3Lock = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fall == true)
        {
            FallUpdate();            
        }

        if (fall == false && lion.GetComponent<lionAI>().stage == 2)
        {
            Shield.SetActive(true);
        }

        /*
        if (Input.GetKeyDown(KeyCode.P))
        {
            Destroy(gameObject, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            fall = true;
        }
        */
    }

    public void FallUpdate()
    {
        timer += Time.deltaTime;

        if (stage1Lock == false)
        {
            BreakStage1();
            stage1Lock = true;            
        }

        if (timer > stage2Time)
        {
            BreakStage2();            
        }

        if (timer > stage3Time && stage3Lock == false)
        {
            BreakStage3();
            stage3Lock = true;
        }
    }

    void BreakStage1()
    {
        Shield.SetActive(false);
        brokenColumn.SetActive(true);
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        lion.GetComponent<lionAI>().columnCount -= 1;

        allChildren = GetComponentsInChildren<Renderer>();
    }

    void BreakStage2()
    {
        foreach (Renderer rend in allChildren)
        {
            rend.material.SetFloat("_Clip_Amt", Mathf.InverseLerp(stage2Time, stage3Time, timer));//Shader VFX  
        }
    }

    void BreakStage3()
    {
        brokenColumn.SetActive(false);
        barrierObject.GetComponent<barrierSegment>().move = true;
        gameObject.SetActive(false);
    }
}
