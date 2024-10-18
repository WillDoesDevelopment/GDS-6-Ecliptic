using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class DialogueManager : MonoBehaviour
{
    public static bool InDialogue = false;
    public EventSystem eventSystem;

    // image and text components necessary
    public Sprite OtherDialogueBoxSprite;
    public Image OtherDialogueBox;
    public GameObject EnterText;
    public TextMeshProUGUI DialogueText;

    public TextMeshProUGUI[] DecisionTexts;

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


    public bool proximityBool = false;

    public bool CoroutineRunning = false;
    private bool skip = false;


                                                        // two queues (first in first out) of sentences and types of sentences
    /*public Queue<string> Sentences;
    public Queue<Dialogue.DialogueType> SentenceType;
    public Queue<AudioClip> CharacterSFX;*/
                                                        // for the new list method we will need a dialogue index tracker so we can keep track of where we are in the dialogue, this tracker will need to be set back to zero at the 
                                                        // beginning and end of each dialogue. It will have to be incrimented where we origonally called the dequeue() function 
    public int DialogueIndexTracker = 0;
    public List<int> decisionIndexList = new List<int>();

    public GameObject player;


    public AudioSource TextSnd;
    public AudioSource CharacterSND;
    private bool sndIsPlaying = false;
    void Awake()
    {
        OtherDialogueBox.sprite = OtherDialogueBoxSprite;
/*        Sentences = new Queue<string>();
        SentenceType = new Queue<Dialogue.DialogueType>();
        CharacterSFX = new Queue<AudioClip>();*/
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
                                                        // we set this bool back to false so that if the dialogue trigger next frame sets it to true again we know that we are in proximity
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

    public List<int> DetectingDecisions(Dialogue dialogue)
    {
                                                                                          // just created a list for debugging
        List<Dialogue.DialogueLine> TempDecisionGroup = new List<Dialogue.DialogueLine>();

        List<int> decisionIndex = new List<int>();                                       // go through every dialogue line
        if(dialogue.IndentVals.Length == DialogueIndexTracker)
        {
            return decisionIndex;
        }
                                                                                        // check if the amount a sentence is indented is an odd number as it means that said dailogue line is a decision
        if(dialogue.IndentVals[DialogueIndexTracker] % 2 != 0)
        {
            TempDecisionGroup.Add(dialogue.line[DialogueIndexTracker]);
            decisionIndex.Add(DialogueIndexTracker);
                                                                                        //from the i'th position loop through the rest of the dialogue list and see if anything is indented at the same level
            for (int i = DialogueIndexTracker+1; i<dialogue.line.Length; i++)
            { 
                                                                                        // here we see if i has the same indent val as v in order to see if its a decision that belongs to the same choice
                if (dialogue.IndentVals[DialogueIndexTracker] == dialogue.IndentVals[i])
                {
                    //Debug.Log("same indent val");
                                                                                        // here we check if v or i is in the list already 
                                                                                        //in this case it is part of the same decision
                    TempDecisionGroup.Add(dialogue.line[i]);
                    decisionIndex.Add(i);
                }

                                                                                        // if there is text that is less indented as I that means we have left this decision branch
                else if(dialogue.IndentVals[DialogueIndexTracker] > dialogue.IndentVals[i])
                {
                                                                                        //in this case the dialogue branch has ended 
                    break;
                }
            }
            //Debug.Log(TempDecisionGroup.Count);
        }
        return decisionIndex;
        
        //Debug.Log(Decisions.Count);
    }
                                                            // Start dialogue, is given a chunk of data by a dialogue trigger (ie, dialogue trigger sees if its activated and if it is it calls somthing like "dialogueManager.StartDialogue(dialogue))
                                                            //it then attaches all the data to the appropriate UI, such as images, text and so on
    public void StartDialogue(Dialogue dialogue)
    {
                                                            //calls this static (global) function in hub manager
        HubManager.freezePlayerActions(player);


        dialogue.DialogueMode = Dialogue.DialogueState.InProgress;
        player.GetComponent<PlayerController>().playerState = PlayerState.Dialogue;
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
                                                            // after the prep and saving to local variables we call the next dialogue
        NextDialogue(dialogue);
    }

                                                            //this animates the text, controlls when the dialogue is skipped and sets the next sentence to be animated
                                                            //it also shows who is speaking
    public void NextDialogue(Dialogue dialogue)
    {

        if(dialogue.DialogueMode == Dialogue.DialogueState.Finished)
        {
            return;
        }
        if (DialogueIndexTracker != 0 && DialogueIndexTracker < dialogue.IndentVals.Length)
        {
            if (dialogue.IndentVals[DialogueIndexTracker] < dialogue.IndentVals[DialogueIndexTracker - 1])
            {
                for (int i = DialogueIndexTracker; i < dialogue.IndentVals.Length; i++)
                {
                    if (dialogue.IndentVals[i] == 0)
                    {
                        DialogueIndexTracker = i;
                        break;
                    }
                }
            }

        }
        DeactivateDecisions(dialogue);
        //List<int> TempDecisionIndex = new List<int>();
        decisionIndexList.Clear();                                                    // this is for debugging purposes
        decisionIndexList = DetectingDecisions(dialogue);
        if (CoroutineRunning)
        {
            //Debug.Log("skipping");
            skip = true;
            return;
        }
                                                            // this will work with the new method
        if(DialogueIndexTracker > dialogue.line.Length)
        {
            TextAnim.SetBool("PlayerImgAnimate", false);
            TextAnim.SetBool("OtherImgAnimation", false);
            return; 
        }

                                                            // intead we will need to do this

        if(dialogue.line[DialogueIndexTracker].AS != null)
        {
            CharacterSND.clip = dialogue.line[DialogueIndexTracker].AS;
            CharacterSND.Play();
        }

                                                            // instead we do this
        Dialogue.DialogueType TempType = dialogue.line[DialogueIndexTracker].dialogueType;


                                                            // all of this is fine tho  
        if(TempType == Dialogue.DialogueType.OtherDialogue)
        {
            TextAnim.SetBool("OtherImgAnimation", true);
            TextAnim.SetBool("PlayerImgAnimate", false);

        }
        else if(TempType == Dialogue.DialogueType.Monologue)
        {
            //Debug.Log("PlayerAnimBeingSetToOn");
            TextAnim.SetBool("OtherImgAnimation", false);
            TextAnim.SetBool("PlayerImgAnimate", true);
        }
        else
        {
            TextAnim.SetBool("OtherImgAnimation", false);
            TextAnim.SetBool("PlayerImgAnimate", false);
        }

        // will need to replace all these deque calls with index refrences then adding one to the index


        // instead we do this
        string tempSentence = dialogue.line[DialogueIndexTracker].sentence;
        DialogueIndexTracker++;
                                                             // incase we are mid way through typeText coroutine
        StopAllCoroutines();

        if (decisionIndexList.Count == 0) 
        {
            StartCoroutine(TypeText(tempSentence, DialogueText));
        }
        else
        {
            ActivateDecisions(dialogue);
        }
        //types each character

    }
    public void ActivateDecisions(Dialogue dialogue)
    {
        eventSystem.SetSelectedGameObject(DecisionTexts[0].transform.parent.gameObject);
        //DialogueText.text = "";
        TextAnim.SetBool("DecisionUIAnimate", true);
        for (int i = 0; i < decisionIndexList.Count; i++)
        {
            DecisionTexts[i].transform.parent.gameObject.SetActive(true);
            //DialogueText.text = "";

            Debug.Log(i.ToString() + "is itterable val");

    //        Debug.Log(decisionIndexList[i]);
            DecisionTexts[i].text = dialogue.line[decisionIndexList[i]].sentence;
            DecisionTexts[i].color = new Vector4(1,1,1,0) ;
        }
    }
    public void DeactivateDecisions(Dialogue dialogue)
    {
        TextAnim.SetBool("DecisionUIAnimate", false);
        for (int i = 0; i < decisionIndexList.Count; i++)
        {
            DecisionTexts[i].transform.parent.gameObject.SetActive(false);
            DecisionTexts[i].text = "";
        }
    }
    public void SendToDialogueIndex(int index)
    {
        DialogueIndexTracker = decisionIndexList[index]+1;
        Debug.Log(DialogueIndexTracker);
        Debug.Log(decisionIndexList.Count);

    }
                                                            // animates text
    public IEnumerator TypeText(string sentence, TextMeshProUGUI TxtElement)
    {
        sndIsPlaying = false;
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
                if(sndIsPlaying == false)
                {
                    sndIsPlaying = true;
                    StartCoroutine(typingSnd());
                }
                yield return new WaitForSeconds(.02f);
            }
            
        }
        skip = false;
        CoroutineRunning = false;

    }
    public IEnumerator typingSnd()
    {
        TextSnd.pitch = Random.Range(.2f,1);
        TextSnd.Play();
        yield return new WaitForSeconds(.07f);
        sndIsPlaying = false;
    }

                                                            // exit dialogue anims
    public void EndDialogueCheck(Dialogue dialogue)
    {
                                                                // this will not work with the new method, instead
        //if (Sentences.Count == 0)
        if (DialogueIndexTracker >= dialogue.line.Length)
        {
            HubManager.UnfreezePlayerActions(player);

            player.GetComponent<PlayerController>().playerState = PlayerState.Walk;
            //Debug.Log("EndOfDialogue");
            TextAnim.SetBool("PopUp", false);
            
            
            TextAnim.SetBool("PlayerImgAnimate", false);
            //Debug.Log(TextAnim.GetBool("PlayerImgAnimate"));
            TextAnim.SetBool("OtherImgAnimation", false);
            dialogue.DialogueMode = Dialogue.DialogueState.Finished;

            InDialogue = false;
            DialogueIndexTracker = 0;                                           // calls this static (global) function in the hub manager
            //DialogueIndexTracker = 0;
            return;
        }
    }
}
