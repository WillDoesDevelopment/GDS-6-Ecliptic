using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject EnterText;
    public TextMeshProUGUI DialogueText;

    public Animator TextAnim;
    //public GameObject player;

    public Queue<string> Sentences;

    private bool DialogueMode = false;
    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        Sentences = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
    public void EnterPrompt()
    {
        Animator EnterAnim = EnterText.GetComponent<Animator>();
        EnterAnim.SetBool("FadeIn", true);

    }
    public void EnterAnimExit()
    {
        Animator EnterAnim = EnterText.GetComponent<Animator>();
        EnterAnim.SetBool("FadeIn", false);

    }

    public void StartDialogue(Dialogue dialogue)
    {
        TextAnim.SetBool("PopUp", true);
        Debug.Log("Working");
        Sentences.Clear();

        foreach (string sentence in dialogue.sentances)
        {
            Sentences.Enqueue(sentence);

        }
        NextDialogue();
    }
    public void NextDialogue()
    {
        if (Sentences.Count == 0)
        {
            endDialogue();
            return;
        }

        string tempSentence = Sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeText(tempSentence));
    }
    public IEnumerator TypeText(string sentence)
    {
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(.1f);
        }
    }

    public void endDialogue()
    {
        TextAnim.SetBool("PopUp", false);
    }
}
