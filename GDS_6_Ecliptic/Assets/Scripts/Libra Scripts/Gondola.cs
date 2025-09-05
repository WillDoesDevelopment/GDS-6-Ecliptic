using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Gondola : MonoBehaviour
{
    public GameObject player;
    public GameObject gondolaPlatformTop;
    public GameObject gondolaPlatformFloor;
    public Transform startTransform;
    public Transform endTransform;
    public GameObject startWalls;
    public GameObject endWalls;
    public MeshRenderer unlockMatRenderer;
    public Material unlockedMat;

    public float moveTime = 3f;
    float time = 0f;
    public float radius = 2f;

    public AnimationCurve movementCurve;
    public AnimationCurve angleCurve;

    public GondolaState state = GondolaState.Locked;

    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {        
        playerController = player.GetComponent<PlayerController>();
        MoveGondola(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(state == GondolaState.Start)
        {
            //Check for player
            Collider[] hitColliders = Physics.OverlapSphere(gondolaPlatformFloor.transform.position, radius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject == player)
                {
                    //freeze player, make child
                    //move walls
                    playerController.playerState = PlayerState.Freeze;
                    startWalls.SetActive(false);
                    endWalls.SetActive(false);
                    player.transform.SetParent(gondolaPlatformTop.transform,true);
                    state = GondolaState.Moving;
                }
            }

        }
        if(state == GondolaState.Moving)
        {
            time += Time.deltaTime;
            float t = time / moveTime;
            t = Mathf.Clamp01(t);

            MoveGondola(t);

            if(t == 1)
            {
                //move walls
                //release player, un child
                playerController.playerState = PlayerState.Walk;
                startWalls.SetActive(true);
                endWalls.SetActive(true);
                player.transform.SetParent(null, true);
                state = GondolaState.End;
            }

        }
        if(state == GondolaState.End)
        {

        }
    }

    void MoveGondola(float t)
    {
        gondolaPlatformTop.transform.position = Vector3.Lerp(startTransform.position, endTransform.position, movementCurve.Evaluate(t));
        gondolaPlatformTop.transform.eulerAngles = new Vector3(180 * angleCurve.Evaluate(t), gondolaPlatformTop.transform.eulerAngles.y, gondolaPlatformTop.transform.eulerAngles.z);
    }

    public void UnlockGondola()
    {
        state = GondolaState.Start;
        unlockMatRenderer.material = unlockedMat;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(startTransform.position, endTransform.position);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(startTransform.position, 1);
        Gizmos.DrawWireSphere(endTransform.position, 1);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(gondolaPlatformFloor.transform.position, radius);
 
    }
}
