using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAppear : MonoBehaviour
{
    public GameObject player;

    float range = 10f;
    float clipAmount = 1f; //initial visibility
    float prevClipAmount = -1f;
    float dissolveSpeed = 0.6f;


    // Start is called before the first frame update
    void Start()
    {
        //visible = false;
        SetClip();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            clipAmount = Mathf.MoveTowards(clipAmount,0f,Time.deltaTime * dissolveSpeed);
        }
        else
        {
            clipAmount = Mathf.MoveTowards(clipAmount, 1f, Time.deltaTime * dissolveSpeed);
        }

        if(clipAmount != prevClipAmount)
        {
            SetClip();
        } 

        prevClipAmount = clipAmount;
    }


    void SetClip()
    {
        /*
        if (gameObject.TryGetComponent<MeshRenderer>(out MeshRenderer pRenderer))
        {
            pRenderer.enabled = false;
        }
        */

        foreach (Transform child in transform)
        {
            if (child.gameObject.TryGetComponent<MeshRenderer>(out MeshRenderer renderer))
            {                
                //renderer.enabled = false;

                foreach (Material mat in renderer.materials)
                {
                    mat.SetFloat("_Clip_Amt", clipAmount);//Shader VFX 
                }
                //renderer.material.SetFloat("_Clip_Amt", clipAmount);//Shader VFX 
                
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Set the color with custom alpha
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
