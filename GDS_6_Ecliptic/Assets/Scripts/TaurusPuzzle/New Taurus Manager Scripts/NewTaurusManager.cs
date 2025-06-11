using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;


public class NewTaurusManager : MonoBehaviour
{
    public TaurusStage[] TaurusStages;
    public int taurusStageCounter;

    public GameObject Player;
    public GameObject particleObject;

    public GameObject DT;

    private void Start()
    {
        
    }

    private void Awake()
    {
        MazeStateChange(MazeState.StartMaze, MazeState.CentreMaze);
    }

    public void Update()
    {
        
        TaurusStages[taurusStageCounter].ResetCheck(Player);
        if (TaurusStages[taurusStageCounter].ArtifactCheck(Player, particleObject, taurusStageCounter, TaurusStages[taurusStageCounter].GetTargetObject()))
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

        /*if (DT.GetComponent<DialogueTrigger>().dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            MazeStateChange(MazeState.StartMaze, MazeState.CentreMaze);
            //TaurusStages[0].MazeActiveToggle(true);
            print("Yeehaw");
        }*/

    }

    
    public void ResetCheck()
    {
        //this function needs to check the players location, bulls location and 
    }

    public void MazeStateChange(MazeState mazeEnter, MazeState mazeExit)
    {
        TaurusStage stageEnter = Array.Find(TaurusStages, p => p.maze == mazeEnter);
        TaurusStage stageExit = Array.Find(TaurusStages, p => p.maze == mazeExit);

        stageEnter.MazeActiveToggle(false);
        stageExit.MazeActiveToggle(true);
    }

}

public enum MazeState
{
    StartMaze,
    CentreMaze,
    TreeMaze,
    FinalMaze
}
