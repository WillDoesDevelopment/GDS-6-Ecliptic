using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CancerOrb : MonoBehaviour
{
    public Animator orbAnim;
    public Animator parentAnim;
    private bool isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) ^ Input.GetKeyUp(KeyCode.JoystickButton1) && isTriggered == false)
        {
           
            
            StartCoroutine(WalkOrb1());
            isTriggered = true;
        }
       
    }

    IEnumerator WalkOrb1()
    {
        print("Started");
        yield return new WaitForSeconds(2.0f);
        parentAnim.SetTrigger("Start");
        orbAnim.SetTrigger("Walk");

        yield return new WaitForSeconds(10.0f);
        orbAnim.SetTrigger("Idle");

        print("Walking");
        yield return new WaitForSeconds(10.0f);
        orbAnim.SetTrigger("Walk");
        parentAnim.SetTrigger("Creb");

        print("Finished");
        yield return new WaitForSeconds(13.0f);
        orbAnim.SetTrigger("Idle");
    }


}
