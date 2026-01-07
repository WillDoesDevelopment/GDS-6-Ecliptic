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

    public GameObject cube;

    public DrinkHopper DH;

    void Start()
    {
        recipeTrig.dialogue = recipeDia[0];
    }

    private void Update()
    {
        if (recipeDia[0].DialogueMode == Dialogue.DialogueState.InProgress)
        {
            recPanel[0].SetActive(true);
        }
        else
        {
            recPanel[0].SetActive(false);
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


    public void recipeStart(int rNum)
    {
        if(recipeDia[rNum].DialogueMode == Dialogue.DialogueState.InProgress)
        {
            recPanel[rNum].SetActive(true);
        }
        else
        {
            recPanel[rNum].SetActive(false);
        }

    }

    public void IntoDrink(GameObject obj)
    {
        drinklist.Add(obj.GetComponent<IngredientData>().ing);

        if(drinklist.Count > 3)
        {
            if(DH.CheckDrinkContents(drinklist, recObj[0].ingredients))
            {
                completeDrink();
            }

            ClearQueue();
        }

        obj.SetActive(false);
    }
    

    public void completeDrink()
    {
        t += Time.fixedDeltaTime;

        if (drinkisCorrect)
        {
            jarMat.SetFloat("_Fill", Mathf.Lerp(0, 1, t));
        }
    }

}

public enum IngredientChoice
{
    Heart,
    Crab,
    Left,
    Check
}
