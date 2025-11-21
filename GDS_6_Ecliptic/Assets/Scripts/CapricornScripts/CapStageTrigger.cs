using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapStageTrigger : MonoBehaviour
{
    public GameObject player;
    public StageEnabler parentStageEnabler;
    public CapStage nextCapStage;
    public float range = 5f;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            parentStageEnabler.UpdateCapStage(nextCapStage);
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        // Set the color with custom alpha
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
