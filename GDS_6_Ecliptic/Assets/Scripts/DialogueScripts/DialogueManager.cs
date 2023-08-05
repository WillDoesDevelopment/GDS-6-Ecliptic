using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // image and text components necessary
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
    

    public Queue<string> Sentences;
    public Queue<Dialogue.DialogueType> SentenceType;

    private bool DialogueMode = false;
    public float radius;
    // Start is called before the first frame update
    void Awake()
    {
        Sentences = new Queue<string>();
        SentenceType = new Queue<Dialogue.DialogueType>();
    }


    
    public void EnterPrompt()
    {
        // toggles on the enter script
        Animator EnterAnim = EnterText.GetComponent<Animator>();
        EnterAnim.SetBool("FadeIn", true);

    }
    public void EnterAnimExit()
    {
        //toggles off the enter script
        Animator EnterAnim = EnterText.GetComponent<Animator>();
        EnterAnim.SetBool("FadeIn", false);

    }

    public void StartDialogue(Dialogue dialogue)
    {
        // animates our text ui to pop up
        TextAnim.SetBool("PopUp", true);

        // add image and name from our dialogue package that is passed through


        
        SpriteUI.GetComponent<Image>().sprite = dialogue.DialogueImage;
        NameText.text = dialogue.name;

        PlayerNameText.text = dialogue.monologueName;
        PlayerSpriteUI.GetComponent<Image>().sprite = dialogue.MonologueImage;

        //sets them all to inactive to start
        SpriteUI.gameObject.SetActive(false);
        NameText.transform.parent.gameObject.SetActive(false);

        PlayerNameText.transform.parent.gameObject.SetActive(false);
        PlayerSpriteUI.gameObject.SetActive(false);

        Sentences.Clear();

        foreach (Dialogue.DialogueLine info in dialogue.line)
        {
            // add the sentances in our dialogue package to the local sentance queue
            Sentences.Enqueue(info.sentence);
            SentenceType.Enqueue(info.dialogueType);

        }

        // after the prep and saving to local variables we call the next dialogue
        NextDialogue();
    }
    public void NextDialogue()
    {
        // check if we are out of dialogue
        Debug.Log(Sentences.Count);
        if (Sentences.Count == 0)
        {
            endDialogue();
            return;
        }

        Dialogue.DialogueType TempType = SentenceType.Dequeue();

        if(TempType == Dialogue.DialogueType.OtherDialogue)
        {
            SpriteUI.gameObject.SetActive(true);
            NameText.transform.parent.gameObject.SetActive(true);

            PlayerNameText.transform.parent.gameObject.SetActive(false);
            PlayerSpriteUI.gameObject.SetActive(false);

        }
        else if(TempType == Dialogue.DialogueType.Monologue)
        {
            SpriteUI.gameObject.SetActive(false);
            NameText.transform.parent.gameObject.SetActive(false);

            PlayerNameText.transform.parent.gameObject.SetActive(true);
            PlayerSpriteUI.gameObject.SetActive(true);
        }
        else
        {
            PlayerNameText.transform.parent.gameObject.SetActive(false);
            PlayerSpriteUI.gameObject.SetActive(false);

            PlayerNameText.transform.parent.gameObject.SetActive(false);
            PlayerSpriteUI.gameObject.SetActive(false);
        }

        // dequeue sentences as the same time as saving them to a temp variable
        string tempSentence = Sentences.Dequeue();
        // incase we are mid way through typeText coroutine
        StopAllCoroutines();
        //types each character
        StartCoroutine(TypeText(tempSentence, DialogueText));
    }
    public IEnumerator TypeText(string sentence, TextMeshProUGUI TxtElement)
    {
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            TxtElement.text += letter;
            yield return new WaitForSeconds(.05f);
        }
    }

    // exit dialogue anim
    public void endDialogue()
    {
        TextAnim.SetBool("PopUp", false);
    }


}
