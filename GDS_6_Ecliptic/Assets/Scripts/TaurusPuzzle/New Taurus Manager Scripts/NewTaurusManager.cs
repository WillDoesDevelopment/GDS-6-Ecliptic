using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;


public class NewTaurusManager : MonoBehaviour
{
    public TaurusStage[] TaurusStages;
    public int taurusStageCounter;

    public GameObject Player;
    public GameObject particleObject;


    private void Start()
    {
        
    }

    public void Update()
    {
        
        TaurusStages[taurusStageCounter].ResetCheck(Player);
        if (TaurusStages[taurusStageCounter].ArtifactCheck(Player, particleObject, taurusStageCounter))
        {
            if (taurusStageCounter < 3)
            {
                taurusStageCounter += 1;

            }
            else
            {
                return;
            }
        }

    }

    
    public void ResetCheck()
    {
        //this function needs to check the players location, bulls location and 
    }

}
