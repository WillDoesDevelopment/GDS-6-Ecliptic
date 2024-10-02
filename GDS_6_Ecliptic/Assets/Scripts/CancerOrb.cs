using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancerOrb : MonoBehaviour
{
    public Animator orbAnim;
    public Animator parentAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator WalkOrb1()
    {
        yield return new WaitForSeconds(1.0f);
        orbAnim.SetTrigger("Walk");
        parentAnim.SetTrigger("Creb");
    }


}
