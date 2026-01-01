using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class aquaManager : MonoBehaviour
{
    public Dialogue[] recipeDia;
    public DialogueTrigger recipeTrig;

    public GameObject[] recipePanel;

    public List<string> recipe = new List<string>();
    public List<string> ingredients = new List<string>();
    public Queue<int> leverCombo = new Queue<int>(3);

    private IngredientChoice ingre;
    public Vector3 spawnPos;
    private Quaternion spawnRot;

    public bool drinkisCorrect = false;

    public Material jarMat;
    public GameObject sparkleFx;

    private float t = 0.0f;

    public GameObject cube;

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

    public void CheckQueue(int leverNum)
    {
        if (leverCombo.Count < 2)
        {
            AddQueue(leverNum);
        } else
        {
            Instantiate(cube, spawnPos, spawnRot);
            print("Que que que que que");
            //Steps to follow:
            //CHeck the queue against ingrediants. Is there a match?
            //If there is a match, spawn the ingredient.
            //If ther is no match, just clear the queue.
            //
            //SpawnIngre(leverNum);
            ClearQueue();
        }
    }

    public void AddQueue(int leverNum)
    {
        leverCombo.Enqueue(leverNum);
        print(leverNum +"In Queue");
    }

    public void ClearQueue()
    {
        if(leverCombo.Count >= 3)
        {
            leverCombo.Clear();
            print("Queue Cleared");
        }

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
        //Instantiate(ingreObj[ingNum], spawnPos, spawnRot);
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
    

    public void completeDrink()
    {
        t += Time.fixedDeltaTime;

        if (drinkisCorrect)
        {
            jarMat.SetFloat("_Fill", Mathf.Lerp(0, 1, t));
        }
    }

}
