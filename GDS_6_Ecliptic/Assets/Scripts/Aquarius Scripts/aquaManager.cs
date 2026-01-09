using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class aquaManager : MonoBehaviour
{
    public Dialogue[] recipeDia;
    public DialogueTrigger recipeTrig;
    public GameObject[] recPanel;

    public List<Recipe> recObj = new List<Recipe>();
    public List<IngredientChoice> drinklist = new List<IngredientChoice>();

    public List<int> leverCombo = new List<int>(3);
    public List<IngredientObj> iObj = new List<IngredientObj>(); 

    private IngredientChoice ingre;
    public Vector3 spawnPos;
    private Quaternion spawnRot;

    public bool drinkisCorrect = false;

    public Material jarMat;
    public GameObject sparkleFx;

    private float t = 0.0f;

    private int recNum;

    public DoorScript ds;

    public GameEventDia ge;

    void Start()
    {
        recipeTrig.dialogue = recipeDia[0];
    }

    private void Update()
    {
        if (drinkisCorrect)
        {
            t += Time.fixedDeltaTime;
            jarMat.SetFloat("_Fill", Mathf.Lerp(0, 1, t / 2));
            sparkleFx.SetActive(true);
        }
    }

    public void CheckLeverList(int leverNum)
    {
        AddQueue(leverNum);

        if(leverCombo.Count > 2)
        {
            foreach (IngredientObj i in iObj)
            {
                if (i.CompareList(leverCombo))
                {
                    Instantiate(i.IngreObjPrefab, spawnPos, spawnRot);
                    break;
                }
            }

            ClearQueue();
        }
    }

    public void AddQueue(int leverNum)
    {
        leverCombo.Add(leverNum);
        print(leverNum +"In List");
    }

    public void ClearQueue()
    {
        if(leverCombo.Count >= 3)
        {
            leverCombo.Clear();
            print("List Cleared");
        }

        if(drinklist.Count >= 4)
        {
            drinklist.Clear();
        }

    }


    public void recipeStart()
    {
        if(recipeDia[recNum].DialogueMode == Dialogue.DialogueState.InProgress)
        {
            recPanel[recNum].SetActive(true);
        }
        else
        {
            recPanel[recNum].SetActive(false);
        }

    }

    public void IntoDrink(GameObject obj)
    {
        drinklist.Add(obj.GetComponent<IngredientData>().ing);

        if(drinklist.Count > 3)
        {
            if(CheckDrinkContents(drinklist, recObj[0].ingredients))
            {
                print("Drink Contents Checked");
                drinkisCorrect = true;
                completeDrink();
            }

            ClearQueue();
        }

        obj.SetActive(false);
    }
    

    public void completeDrink()
    {
        recNum++;
        ge.TriggerEvent(recipeDia[recNum]);

        if(recNum <= 4)
        {
            ds.GetComponent<DoorStatus>().IsOpen = true;
        }
    }

    public bool CheckDrinkContents(List<IngredientChoice> A, List<IngredientChoice> B)
    {
        if (A.Count != B.Count)
        {
            return false;
        }

        List<IngredientChoice> ASort = A.ToList();
        ASort.Sort();
        List<IngredientChoice> BSort = B.ToList();
        BSort.Sort();

        for (int i = 0; i < A.Count; i++)
        {
            if (ASort[i] != BSort[i])
            {
                return false;
            }
        }

        return true;

    }

}

public enum IngredientChoice
{
    Heart,
    Crab,
    Left,
    Check
}
