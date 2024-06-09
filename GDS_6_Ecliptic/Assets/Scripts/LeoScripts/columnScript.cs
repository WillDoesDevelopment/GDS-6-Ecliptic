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

    float timer = 0f;

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

        if (Input.GetKeyDown(KeyCode.P))
        {
            Destroy(gameObject, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            fall = true;
        }
    }

    public void FallUpdate()
    {
        timer += Time.deltaTime;

        if (stage1Lock == false)
        {
            BreakStage1();
            stage1Lock = true;
        }

        if (timer > 5 && stage2Lock == false)
        {
            BreakStage2();
            stage2Lock = true;
        }

        if (timer > 10 && stage3Lock == false)
        {
            BreakStage3();
            stage3Lock = true;
        }
    }

    void BreakStage1()
    {
        brokenColumn.SetActive(true);
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }

    void BreakStage2()
    {
        brokenColumn.SetActive(false);
    }

    void BreakStage3()
    {
        barrierObject.GetComponent<barrierSegment>().move = true;
        Destroy(gameObject, 0.0f);
    }
}
