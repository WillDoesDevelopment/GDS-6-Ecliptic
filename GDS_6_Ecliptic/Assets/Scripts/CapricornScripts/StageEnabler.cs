using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEnabler : MonoBehaviour
{
    public CapStage targetCapStage;

    public void UpdateCapStage(CapStage updateStage)
    {
        foreach (Transform child in transform)
        {
            //Debug.Log(child.name);

            if (child.gameObject.TryGetComponent<CapStageHolder>(out CapStageHolder capStageHolder))
            {
                if ((capStageHolder.capStage & updateStage) != 0)
                {
                    child.gameObject.SetActive(true);
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    [ContextMenu("Show Capricorn Stage")]
    void ShowCapStage()
    {
        foreach (Transform child in transform)
        {            
            //Debug.Log(child.name);
            
            if (child.gameObject.TryGetComponent<CapStageHolder>(out CapStageHolder capStageHolder))
            {
                if((capStageHolder.capStage & targetCapStage) != 0)
                {
                    child.gameObject.SetActive(true);
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }            
        }
    }

    [ContextMenu("Enable All Children")]
    void EnableAllChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
