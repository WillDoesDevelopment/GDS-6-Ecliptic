using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiscesManager : MonoBehaviour
{
    [Header("Whale Shark")]

    public GameObject whaleSharkObj;
    public Vector3[] wsTransforms;
    public Vector3[] wsRotations;
    public float lerpSpeed;
    public GameObject particleObject;
    public ParticleSystem system;
    public Material[] glowAmt;
    public float shaderValue = 0;
    public bool isHit = false;
    public float hitLength = 0.5f;

    [Header("Player")]
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //sets red material emissive on whaleshark for hit
        glowAmt[0].SetFloat("_Glow_Amount", 0);
        glowAmt[1].SetFloat("_Glow_Amount", 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (isHit == true) //checks if whale is hit
        {
            StartCoroutine(Hit());
        }
    }

    private void OnTriggerEnter(Collider other) //first time
    {
        if(other.gameObject == player)
        {
            StartLerpToPosition();
        }
    }

    void StartLerpToPosition() //controls the first half of the whales movement
    {
        player.transform.parent = whaleSharkObj.transform;
        transform.position = Vector3.Lerp(this.transform.position, wsTransforms[0], lerpSpeed);
    }

    void EndLerpToPosition() //controls the second half of the whale movement from the middle platform to end
    {
        player.transform.parent = whaleSharkObj.transform;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(wsRotations[1]), Time.deltaTime * lerpSpeed);
        transform.position = Vector3.Lerp(transform.position, wsTransforms[3], lerpSpeed);
    }

    void sharkHit() //when shark is it
    {
        var colliders = Physics.OverlapSphere(transform.position, .5f);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.tag == "Spike")
            {
                isHit = true;
                particleObject.transform.position = transform.position;
                player.GetComponent<PlayerController>().Damage();

                system.Emit(2);
                break;
            }

        }
    }

    IEnumerator Hit() //actual changing of materials
    {
        shaderValue = 1;
        glowAmt[0].SetFloat("_Glow_Amount", shaderValue);
        glowAmt[1].SetFloat("_Glow_Amount", shaderValue);

        yield return new WaitForSeconds(hitLength);

        shaderValue = 0;
        glowAmt[0].SetFloat("_Glow_Amount", shaderValue);
        glowAmt[1].SetFloat("_Glow_Amount", shaderValue);

        yield return new WaitForSeconds(hitLength);

        shaderValue = 1;
        glowAmt[0].SetFloat("_Glow_Amount", shaderValue);
        glowAmt[1].SetFloat("_Glow_Amount", shaderValue);

        yield return new WaitForSeconds(hitLength);

        shaderValue = 0;
        glowAmt[0].SetFloat("_Glow_Amount", shaderValue);
        glowAmt[1].SetFloat("_Glow_Amount", shaderValue);

        yield return new WaitForSeconds(hitLength);

        shaderValue = 1;
        glowAmt[0].SetFloat("_Glow_Amount", shaderValue);
        glowAmt[1].SetFloat("_Glow_Amount", shaderValue);

        yield return new WaitForSeconds(hitLength);

        shaderValue = 0;
        glowAmt[0].SetFloat("_Glow_Amount", shaderValue);
        glowAmt[1].SetFloat("_Glow_Amount", shaderValue);

        yield return new WaitForSeconds(hitLength);
        isHit = false;

    }


}
