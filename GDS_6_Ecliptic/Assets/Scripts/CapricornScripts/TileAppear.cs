using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAppear : MonoBehaviour
{
    public GameObject player;
    bool visible = false;
    float range = 10f;
    // Start is called before the first frame update
    void Start()
    {
        visible = false;
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            if (!visible)
            {
                visible = true;
                Appear();
            }
        }
        else
        {
            if (visible)
            {
                visible = false;
                Hide();
            }
        }
    }

    void Appear()
    {
        if (gameObject.TryGetComponent<MeshRenderer>(out MeshRenderer pRenderer))
        {
            pRenderer.enabled = true;
        }
        foreach (Transform child in transform)
        {
            if (child.gameObject.TryGetComponent<MeshRenderer>(out MeshRenderer renderer))
            {
                renderer.enabled = true;
            }
        }
    }

    void Hide()
    {
        if (gameObject.TryGetComponent<MeshRenderer>(out MeshRenderer pRenderer))
        {
            pRenderer.enabled = false;
        }

        foreach (Transform child in transform)
        {
            if (child.gameObject.TryGetComponent<MeshRenderer>(out MeshRenderer renderer))
            {
                renderer.enabled = false;
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
