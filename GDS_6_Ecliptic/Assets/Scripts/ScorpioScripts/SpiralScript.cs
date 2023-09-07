using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralScript : MonoBehaviour
{
    public Animator anim;
    public Transform centerPivot;
    public GameObject player;
    [Range(0, 0.98f)] public float animPercent;       
    float playerAngle;
    float speed = 0.2f;    
    Vector3 xz = new Vector3(1, 0, 1);

    void Start()
    {
        //anim = gameObject.GetComponent<Animation>();
        animPercent = 0.98f;
    }

    // Update is called once per frame
    void Update()
    {
        //Check left or right
        {
            Vector3 targetDir = Vector3.Scale(player.transform.position - centerPivot.position, xz);
            Vector3 forward = Vector3.Scale(transform.position - centerPivot.position, xz);
            playerAngle = Vector3.SignedAngle(targetDir, forward, Vector3.up);
            if (playerAngle >= 1.0F)
            {
                //Turn Right
                animPercent += Time.deltaTime * speed;
            }
            else if (playerAngle <= -1.0F)
            {
                //Turn Left
                animPercent -= Time.deltaTime * speed;
            }
        }
        animPercent = Mathf.Clamp(animPercent, 0, 0.98f);       
        
        anim.SetFloat("MotionPercent", animPercent);
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
    }
}
