using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class OrbMovementAries : MonoBehaviour
{
    public Animator Anim;
    public Transform[] Points;
    public float speed;
    public int pointsIndex;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Points[pointsIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(pointsIndex <= Points.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, Points[pointsIndex].transform.position, speed * Time.deltaTime);
            
            if(transform.position == Points[pointsIndex].transform.position )
            {
                pointsIndex++;
                Anim.SetBool("isMoving", true);
            }

            if(pointsIndex == Points.Length)
            {
                pointsIndex = 0;
                Anim.SetBool("isMoving", false);
            }
        }
    }
}
