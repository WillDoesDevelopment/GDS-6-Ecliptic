using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroCineManager : MonoBehaviour
{

    public GameObject[] obj;
    public DialogueTrigger dt;
    public Animator anim;
    public GameObject[] vcam;
  

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(letsgo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator letsgo()
    {
        yield return new WaitForSeconds(5f);
        obj[0].SetActive(true);
        anim.SetTrigger("go");
        yield return new WaitForSeconds(3f);
        vcam[0].SetActive(false);
        vcam[1].SetActive(false);
        obj[1].SetActive(false); //player
        yield return new WaitForSeconds(10f);
        obj[2].SetActive(true); //2d canvas
        obj[3].SetActive(false); //ophistar
        yield return new WaitForSeconds(5f);
        obj[2].SetActive(false);
        dt.OnEventCheck();

    }

}
