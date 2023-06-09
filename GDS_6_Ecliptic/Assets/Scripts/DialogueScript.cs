using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public GameObject EnterText;
    public TextMeshProUGUI DialogueText;
   
    public GameObject player;

    public string[] Dialogue;

    private bool DialogueMode = false;
    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DialogueMode == false)
        {
            Proximity();
        }
        else
        {
            DialogueFunc();
        }
    }

    public void Proximity()
    {
        Animator EnterAnim = EnterText.GetComponent<Animator>();
        float playerDistZ = player.transform.position.z - transform.position.z;
        float playerDistX = player.transform.position.x - transform.position.x;
        if (Mathf.Sqrt(Mathf.Pow(playerDistX, 2) + Mathf.Pow(playerDistZ, 2)) < radius)
        {
            EnterAnim.SetBool("FadeIn", true);
            if (Input.GetKey(KeyCode.Return))
            {
                EnterAnim.SetBool("FadeIn", false);
                DialogueMode = true;
            }
        }
        else
        {
            EnterAnim.SetBool("FadeIn", false);
        }
    }

    public void DialogueFunc()
    {
        DialogueText.text = Dialogue[0];
    }
}
