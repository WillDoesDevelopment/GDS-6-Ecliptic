using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class aquaManager : MonoBehaviour
{
    public Dialogue[] recipeDia;
    public DialogueTrigger recipeTrig;

    public GameObject[] recipePanel;

    public List<string> recipe = new List<string>();
    public List<string> ingredients = new List<string>();

    public IngredientChoice ingre;
    public GameObject[] ingreObj;
    public Vector3 spawnPos;
    public Quaternion spawnRot;

    public bool drinkisCorrect = false;

    public enum IngredientChoice
    {
        heart,
        crab,
        left,
        check
    }


    void Start()
    {
        recipeTrig.dialogue = recipeDia[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void recipeStart(Dialogue dia, int rNum)
    {
        if(dia.DialogueMode == Dialogue.DialogueState.InProgress)
        {
            recipePanel[rNum].SetActive(true);
        }
        else
        {
            recipePanel[rNum].SetActive(false);
        }

        switch(ingre)
        {
            case IngredientChoice.heart:
                recipe.Add("Heart");
                break;
            case IngredientChoice.crab:
                recipe.Add("Crab");
                break; 
            case IngredientChoice.left:
                recipe.Add("Left");
                break;
            case IngredientChoice.check:
                recipe.Add("Check");
                break;
            default:
                break;
        }
    }

    public void SpawnIngre(int ingNum)
    {
        Instantiate(ingreObj[ingNum], spawnPos, spawnRot);
    }

    public void IntoDrink(GameObject obj)
    {
            switch (ingre)
            {
                case IngredientChoice.heart:
                    ingredients.Add("Heart");
                    break;
                case IngredientChoice.crab:
                    ingredients.Add("Crab");
                    break;
                case IngredientChoice.left:
                    ingredients.Add("Left");
                    break;
                case IngredientChoice.check:
                    ingredients.Add("Check");
                    break;
                default:
                    break;
            }

        obj.SetActive(false);
    }

    public void CheckDrinkContents(List<string> A, List<string> B)
    {
        if(A.Count != B.Count)
        {
            drinkisCorrect = false;
            return;        
        }

        List<string> ASort = A.ToList();
        ASort.Sort();
        List<string> BSort = B.ToList();
        BSort.Sort();

        for (int i = 0; i < A.Count; i++) 
        {
            if (ASort[i] != BSort[i])
            {
                drinkisCorrect = false;
                return;
            }
            else
            {
                drinkisCorrect = true;
            }
        }

    }
    

    

}
