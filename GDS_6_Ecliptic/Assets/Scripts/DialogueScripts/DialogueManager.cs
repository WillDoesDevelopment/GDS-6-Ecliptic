using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static bool InDialogue = false;
    // image and text components necessary
    //public PlayerScript PS;
    public Sprite OtherDialogueBoxSprite;
    public Image OtherDialogueBox;

    public GameObject EnterText;

    public TextMeshProUGUI DialogueText;

    [Header("Other Player Attributes")]
    public TextMeshProUGUI NameText;
    public Image SpriteUI;

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
    //public bool DialogueMode = false;

    public Queue<string> Sentences;
    public Queue<Dialogue.DialogueType> SentenceType;

    //private bool DialogueMode = false;

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

    public void EnterPromptCheck()
    {
        EnterAnimExit();

        if (proximityBool == true)
        {
            EnterPrompt();
            proximityBool = false;
        }
    }

    public void EnterPrompt()
    {

        EnterAnim.SetBool("FadeIn", true);

    }
    public void EnterAnimExit()
    {
        EnterAnim.SetBool("FadeIn", false);

    }

    public void StartDialogue(Dialogue dialogue)
    {

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
        //Debug.Log("it run");
    }
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
