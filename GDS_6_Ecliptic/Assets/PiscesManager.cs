using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiscesManager : MonoBehaviour
{
    [Header("Whale Shark")]

    public GameObject whaleSharkObj;
    public Vector3[] wsTransforms;
    public Vector3[] wsRotations;
    public float lerpDuration;
    public GameObject particleObject;
    public ParticleSystem system;
    public Material[] glowAmt;
    public float shaderValue = 0;
    public bool isHit = false;
    public float hitLength = 0.5f;
    public GameObject SparklesFx;

    private float lerpTimer;

    [Header("Player")]
    public GameObject player;

    [Header("Net")]
    public GameObject net;

    // Start is called before the first frame update
    void Start()
    {
        //sets red material emissive on whaleshark for hit
        glowAmt[0].SetFloat("_Glow_Amount", 0);
        glowAmt[1].SetFloat("_Glow_Amount", 0);


        //set lerp timer to 0f.
        lerpTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHit == true) //checks if whale is hit
        {
            StartCoroutine(Hit());
        }
    }

    public void StartLerpToPosition() //controls the first half of the whales movement
    {
        lerpTimer += Time.deltaTime;//timepassed variable += time it is now
        net.SetActive(false);
        SparklesFx.SetActive(false);
        player.transform.parent = whaleSharkObj.transform;
        whaleSharkObj.transform.position = Vector3.Lerp(wsTransforms[0], wsTransforms[4], lerpTimer/lerpDuration);

    }








    void EndLerpToPosition() //controls the second half of the whale movement from the middle platform to end
    {
        player.transform.parent = whaleSharkObj.transform;
        //whaleSharkObj.transform.rotation = Quaternion.Slerp(whaleSharkObj.transform.rotation, Quaternion.Euler(wsRotations[1]), Time.deltaTime * lerpSpeed);
        //whaleSharkObj.transform.position = Vector3.Lerp(whaleSharkObj.transform.position, wsTransforms[3], lerpSpeed);
    }

    public void sharkHit() //when shark is it
    {
        print("sharkHit");
        var colliders = Physics.OverlapSphere(transform.position, .5f);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.tag == "Angler")
            {
                print("Hit Hit");
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
