using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class inputLog : MonoBehaviour
{
    Ecliptic02 TestController;
    // Start is called before the first frame update
    void Start()
    {
        TestController = new Ecliptic02();
        TestController.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        //float p = Gamepad.current.leftStick.x.ReadValue();
        Debug.Log(TestController.Player.Move.ReadValue<Vector2>());
        //Debug.Log("X: " + Input.GetAxis("Horizontal") + "  Y: " + Input.GetAxis("Vertical"));
    }
}
