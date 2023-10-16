using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINavMesh : MonoBehaviour
{
    public GameObject Player;
    public int visionDist;

    public NavMeshAgent NMA;
    private Vector3 CurrentNavPos;

    public Queue<Transform> positionQueue = new Queue<Transform>();
    public Transform[] positions;

    public DialogueTrigger StartDialogue;
    public bool NavMeshPause = false;

    public TaurusManager TM;

    public GameObject[] bullSnds;
    private bool CoolDownBool = false;

    bool canStep = true;
    [Range(0f, 1f)] public float stepVolume = 0.8f;
    [Range(0f, 0.8f)] public float stepPitchVariance = 0.5f;
    public AudioSource audioSource;
    public AudioClip[] stepArray;
    bool playDaftPunk = false;

    public Animator PlayerAnim;
    private void Awake()
    {
        foreach (Transform t in positions)
        {
            positionQueue.Enqueue(t);
        }
        //Debug.Log(positionQueue.Count);
    }
    void Update()
    {
        var h = PlayerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        h = h - Mathf.Floor(h);
        if (h > 0.3f && h < 0.4f && canStep == true)
        {
            Footstep();

        }
        if (h > 0.6f && h < 0.7f)
        {
            canStep = true;
        }
        if (h > 0.8f && h < 0.9f && canStep == true)
        {
            Footstep();

        }
        if (h > 0.9f || h < 0.2f)
        {
            canStep = true;
        }

        playSnd();
        CollisionCheck();
        //if(StartDialogue.dialogue.DialogueMode == Dialogue.DialogueState.Finished && NavMeshPause == false)
        if(DialogueManager.InDialogue == false && NavMeshPause == false)
        {
            this.GetComponentInChildren<Animator>().speed = 1;
            GetComponent<Renderer>().material.color = Color.red;
            CurrentNavPos = positionQueue.Peek().position;
            if(Proximity(Player.transform.position, visionDist))
            {
                CurrentNavPos = Player.transform.position;
                GetComponent<Renderer>().material.color = Color.black;
            }
            else if (Proximity(CurrentNavPos, 1))
            {
                Transform temp = positionQueue.Dequeue();
                positionQueue.Enqueue(temp);
            
            }
            NMA.SetDestination(CurrentNavPos);
        }
        else
        {
            // the bull does not move
            this.GetComponentInChildren<Animator>().speed = 0;
            NMA.SetDestination(this.transform.position);
        }
    }

    public bool Proximity( Vector3 NavPos, float radius)
    {
        float playerDistZ = NavPos.z - transform.position.z;
        float playerDistX = NavPos.x - transform.position.x;
        if (Mathf.Sqrt(Mathf.Pow(playerDistX, 2) + Mathf.Pow(playerDistZ, 2)) < radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CollisionCheck()
    {
        Collider[] Collisions = Physics.OverlapSphere(this.gameObject.transform.localPosition, 1f);
        //Debug.Log(Collisions.Length);
        foreach(Collider c in Collisions)
        {
            if (c.gameObject == Player)
            {
              
                TM.Resetting();
            }
        }
    }

    public void playSnd()
    {
        if(Vector3.Distance(Player.transform.position, this.transform.position) < 5)
        {
            if(CoolDownBool == false)
            {
                CoolDownBool = true;
                bullSnds[Random.Range(0,bullSnds.Length)].SetActive(true);
                StartCoroutine(CoolDown());
            }
        }
    }
    public IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(10);
        CoolDownBool = false;
        foreach(GameObject g in bullSnds)
        {
            g.SetActive(false);
        }
    }

    void Footstep()
    {
        //Debug.Log("Step");
        canStep = false;
        if (audioSource != null && stepArray != null)
        {
            audioSource.clip = stepArray[Random.Range(0, stepArray.Length)];
            audioSource.pitch = 1 + Random.Range(-stepPitchVariance, stepPitchVariance);
            audioSource.volume = stepVolume;
            audioSource.PlayOneShot(audioSource.clip);

            //step.Play();
        }
    }

}
