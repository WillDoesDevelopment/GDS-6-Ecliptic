using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerManager : MonoBehaviour
{
    public bool spin = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spin == true)
        {
            StartCoroutine(anglerSpin());
        }
    }

    IEnumerator anglerSpin()
    {

        this.gameObject.GetComponent<Animator>().SetTrigger("Spin");
        this.gameObject.GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
        spin = false;
    }

}
