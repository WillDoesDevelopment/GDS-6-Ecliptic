using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAppear2 : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

        foreach (Transform child in transform)
        {
            if (child.gameObject.TryGetComponent<MeshRenderer>(out MeshRenderer renderer))
            {
                foreach (Material mat in renderer.materials)
                {

                    mat.SetFloat("_AlphaOverride", 0f);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.TryGetComponent<MeshRenderer>(out MeshRenderer renderer))
            {
                foreach (Material mat in renderer.materials)
                {
                    mat.SetVector("_PlayerPos", player.transform.position);
                }
            }
        }
    }
       

    private void OnDrawGizmos()
    {
        // Set the color with custom alpha
        Gizmos.color = Color.cyan;
        //Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawWireCube(transform.position,new Vector3(6f,0.6f,6f));
    }
}
