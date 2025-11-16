using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerManager : MonoBehaviour
{
    [Header("Movement")]
    public PiscesManager pm;
    public GameObject sharkTarget;
    public float speed = 1.0f;

    [Header("Animations")]
    public bool spin = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(spin == true)
        {
            StartCoroutine(anglerSpin());
        }

        if(pm.AtoBStart == true)
        {
            Wave();
        }

    }

    IEnumerator anglerSpin()
    {
        this.gameObject.GetComponent<SphereCollider>().enabled = false;
        this.gameObject.GetComponent<Animator>().SetTrigger("Spin");
        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);
        spin = false;
    }

    public void Wave()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        this.transform.position = Vector3.MoveTowards(this.transform.position, sharkTarget.transform.position, step);
    }

}
