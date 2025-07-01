using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class mazeTrigger2 : MonoBehaviour
{
    public NewTaurusManager NTM;

    public MazeState Enter;
    public MazeState Exit;

    private bool swapToggle = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (swapToggle)
            NTM.MazeStateChange(Enter, Exit);
        else
            NTM.MazeStateChange(Exit, Enter);

        swapToggle = swapToggle ? false : true;
    }
}
