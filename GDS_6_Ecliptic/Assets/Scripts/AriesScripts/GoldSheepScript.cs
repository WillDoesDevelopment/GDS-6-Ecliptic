using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSheepScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void triggerAnim()
    {
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Animate2");
    }
}
