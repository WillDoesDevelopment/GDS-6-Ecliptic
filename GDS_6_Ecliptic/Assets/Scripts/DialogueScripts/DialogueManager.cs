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

    private bool DialogueMode = false;
    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        Sentences = new Queue<string>();
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
        // add image and from our dialogue package that is passed through
        SpriteUI.GetComponent<Image>().sprite = dialogue.DialogueImage;
        NameText.text = dialogue.name;
        TextAnim.SetBool("PopUp", true);

        PlayerNameText.text = dialogue.monologueName;
        PlayerSpriteUI.GetComponent<Image>().sprite = dialogue.MonologueImage;
        Sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            // add the sentances in our dialogue package to the local sentance queue
            Sentences.Enqueue(sentence);

        }
        // after the prep and saving to local variables we call the next dialogue
        NextDialogue();
    }
    public void NextDialogue()
    {
        // check if we are out of dialogue
        if (Sentences.Count == 0)
        {
            endDialogue();
            return;
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
