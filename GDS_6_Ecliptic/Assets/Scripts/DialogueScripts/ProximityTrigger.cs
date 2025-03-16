using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityTrigger : MonoBehaviour
{
    public GameObject player;
    public bool setPlayerState = true;
    public PlayerState newPlayerState = PlayerState.Dialogue;
    public DialogueTrigger dialogueTrigger;
    public float width = 1f;
    public float length = 1f;
    public bool autowalk = false;
    public Vector3 autowalkPos;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, new Vector3(width, 1.0f, length)/2,transform.rotation); //Hitbox for player
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject == player)
            {
                if (setPlayerState)
                {
                    playerController.playerState = newPlayerState;
                }

                if (dialogueTrigger != null)
                {
                    dialogueTrigger.OnEvent = false;
                    dialogueTrigger.TriggerDialogue();
                }

                if (autowalk)
                {
                    playerController.AutoWalkDestination = autowalkPos;
                    playerController.playerState = PlayerState.Autowalk;
                }

                gameObject.SetActive(false);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        
        var p1 = transform.position + transform.forward * length / 2 + transform.right * width / 2;
        var p2 = transform.position - transform.forward * length / 2 + transform.right * width / 2;
        var p3 = transform.position - transform.forward * length / 2 - transform.right * width / 2;
        var p4 = transform.position + transform.forward * length / 2 - transform.right * width / 2;
      
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);
    }
}
