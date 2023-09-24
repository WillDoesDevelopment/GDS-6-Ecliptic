using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsScript : MonoBehaviour
{
    public Animator InstructionsAnim;
    // Start is called before the first frame update
    void Start()
    {
        InstructionsAnim = this.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {

        fadeOut();
    }

    public void fadeOut()
    {
        if(Input.GetKeyDown(KeyCode.Return) ^ Input.GetKeyUp(KeyCode.JoystickButton1))
        {
            Time.timeScale = 1f;
            InstructionsAnim.SetTrigger("Animate");
        }
    }
    public void timeScale()
    {
        Time.timeScale = 0f;
    }
}
