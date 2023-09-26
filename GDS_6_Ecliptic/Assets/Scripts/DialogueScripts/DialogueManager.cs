using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static bool InDialogue = false;


    // image and text components necessary
    public Sprite OtherDialogueBoxSprite;
    public Image OtherDialogueBox;
    public GameObject EnterText;
    public TextMeshProUGUI DialogueText;

    // UI components for the right side of the dialogue
    [Header("Other Player Attributes")]
    public TextMeshProUGUI NameText;
    public Image SpriteUI;

    // UI components for the left side of the dialogue
    [Header("Main Player Attributes")]
    public TextMeshProUGUI PlayerNameText;
    public Image PlayerSpriteUI;

    // animator for dialogue
    public Animator TextAnim;
    public Animator EnterAnim;

   /* public Animator PlayerImgAnim;
    public Animator OtherImgAnim;*/


    public bool proximityBool = false;

    public bool CoroutineRunning = false;
    private bool skip = false;


    // two queues (first in first out) of sentences and types of sentences
    public Queue<string> Sentences;
    public Queue<Dialogue.DialogueType> SentenceType;


    // Start is called before the first frame update

    public GameObject player;
    void Awake()
    {
        OtherDialogueBox.sprite = OtherDialogueBoxSprite;
        Sentences = new Queue<string>();
        SentenceType = new Queue<Dialogue.DialogueType>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        EnterPromptCheck();

    }
    // check to see if the enter prompt should appear
    public void EnterPromptCheck()
    {
        //by default we exit the animation
        EnterAnimExit();

        // if however proximity is true (This gets set by a dialogue trigger) we enter the animation instead
        if (proximityBool == true)
        {
            EnterPrompt();
            // we set this bool back to false so that if the dialogue trigger next frame sets it to true again we know that we are in proximity
            proximityBool = false;
        }
    }

    // sets the prompt animation to true
    public void EnterPrompt()
    {

        EnterAnim.SetBool("FadeIn", true);

    }
    // sets the prompt animation to false
    public void EnterAnimExit()
    {
        EnterAnim.SetBool("FadeIn", false);

    }

    // Start dialogue, is given a chunk of data by a dialogue trigger (ie, dialogue trigger sees if its activated and if it is it calls somthing like "dialogueManager.StartDialogue(dialogue))
    //it then attaches all the data to the appropriate UI, such as images, text and so on
    public void StartDialogue(Dialogue dialogue)
    {
        //calls this static (global) function in hub manager
        HubManager.freezePlayerActions(player);


        dialogue.DialogueMode = Dialogue.DialogueState.InProgress;
        player.GetComponent<PlayerController>().canWalk = false;
        // animates our text ui to pop up
        TextAnim.SetBool("PopUp", true);

        // add image and name from our dialogue package that is passed through

        SpriteUI.GetComponent<Image>().sprite = dialogue.DialogueImage;
        SpriteUI.rectTransform.sizeDelta = new Vector2 (dialogue.DialogueImage.rect.width, dialogue.DialogueImage.rect.height);
        NameText.text = dialogue.name;

        PlayerSpriteUI.GetComponent<Image>().sprite = dialogue.MonologueImage;
        PlayerSpriteUI.rectTransform.sizeDelta = new Vector2(dialogue.MonologueImage.rect.width, dialogue.MonologueImage.rect.height);
        PlayerNameText.text = dialogue.monologueName;

        // if there was a previous dialogue we have to make sure the queues are empty before filling them
        Sentences.Clear();
        SentenceType.Clear();

        foreach (Dialogue.DialogueLine info in dialogue.line)
        {
            // add the sentances in our dialogue package to the local sentance queue
            Sentences.Enqueue(info.sentence);
            SentenceType.Enqueue(info.dialogueType);
        }


        // after the prep and saving to local variables we call the next dialogue
        NextDialogue(dialogue);
    }

    //this animates the text, controlls when the dialogue is skipped and sets the next sentence to be animated
    //it also shows who is speaking
    public void NextDialogue(Dialogue dialogue)
    {
        if (CoroutineRunning)
        {
            skip = true;
            return;
        }
        // check if we are out of dialogue and if so,  dont run the dialogue
        if (Sentences.Count == 0)
        {
            return;
        }

        // grab the dialogue type from the queue and check what it is, depending on this we will set the approptiate animations
        Dialogue.DialogueType TempType = SentenceType.Dequeue();

        if(TempType == Dialogue.DialogueType.OtherDialogue)
        {
            TextAnim.SetBool("OtherImgAnimation", true);
            TextAnim.SetBool("PlayerImgAnimate", false);

        }
        else if(TempType == Dialogue.DialogueType.Monologue)
        {
            TextAnim.SetBool("OtherImgAnimation", false);
            TextAnim.SetBool("PlayerImgAnimate", true);
        }
        else
        {
            TextAnim.SetBool("OtherImgAnimation", false);
            TextAnim.SetBool("PlayerImgAnimate", false);
        }

        // dequeue sentences as the same time as saving them to a temp variable
        string tempSentence = Sentences.Dequeue();

        // incase we are mid way through typeText coroutine
        StopAllCoroutines();

        //types each character
        StartCoroutine(TypeText(tempSentence, DialogueText));

    }

    // animates text
    public IEnumerator TypeText(string sentence, TextMeshProUGUI TxtElement)
    {
        CoroutineRunning = true;
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            // if you put a slash in text it will do a carrage return
            if(letter == '/')
            {
                TxtElement.text += "<br>";
            }
            else
            {
                TxtElement.text += letter;
            }
            if(skip == false)
            {
                yield return new WaitForSeconds(.02f);

            }
        }
        //yield return new WaitForSeconds(0);
        skip = false;
        CoroutineRunning = false;
    }

    // exit dialogue anims
    public void EndDialogueCheck(Dialogue dialogue)
    {
        
        if (Sentences.Count == 0)
        {
            // calls this static (global) function in the hub manager
            HubManager.UnfreezePlayerActions(player);

            player.GetComponent<PlayerController>().canWalk = true;

            TextAnim.SetBool("PopUp", false);
            
            
            TextAnim.SetBool("PlayerImgAnimate", false);
            TextAnim.SetBool("OtherImgAnimation", false);
            dialogue.DialogueMode = Dialogue.DialogueState.Finished;
            //TextAnim.SetBool("PopUp", false);

            InDialogue = false;
            return;
        }
    }
}
