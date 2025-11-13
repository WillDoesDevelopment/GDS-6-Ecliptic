using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PiscesManager : MonoBehaviour
{
    [Header("Whale Shark")]
    public GameObject whaleSharkObj;
    public Vector3[] wsTransforms;
    public Vector3[] wsRotations;
    public float[] lerpDuration;
    public GameObject particleObject;
    public ParticleSystem system;
    public Material[] glowAmt;
    public float shaderValue = 0;
    public bool isHit = false;
    public bool AtoBStart = false;
    public bool go = false;
    public float hitLength = 0.5f;
    public GameObject SparklesFx;

    public float lerpTimer;
    public float slerpTimer;

    [Header("Player")]
    public GameObject player;

    [Header("Net")]
    public GameObject[] net;

    [Header("Angler")]
    public GameObject[] ang;

    [Header("Dialogue")]
    public Dialogue[] dia;

    // Start is called before the first frame update
    void Start()
    {
        ang[0].SetActive(false);
        ang[1].SetActive(false);
        //sets red material emissive on whaleshark for hit
        glowAmt[0].SetFloat("_Glow_Amount", 0);
        glowAmt[1].SetFloat("_Glow_Amount", 0);
        //set lerp timer to 0f.
        lerpTimer = 0f;
        slerpTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHit == true) //checks if whale is hit
        {
            StartCoroutine(Hit());
        }

        if (dia[0].DialogueMode == Dialogue.DialogueState.InProgress)
        {
            
            MidLerpToPosition();
        }

    }

    public void StartLerpToPosition() //controls the first half of the whales movement
    {
        lerpTimer += Time.deltaTime;//timepassed variable += time it is now
        net[0].SetActive(false);
        SparklesFx.SetActive(false);
        ang[0].SetActive(true);
        AtoBStart = true;
        player.transform.parent = whaleSharkObj.transform;
        whaleSharkObj.transform.position = Vector3.Lerp(wsTransforms[0], wsTransforms[1], lerpTimer / lerpDuration[0]);

    }

    void MidLerpToPosition() //controls the second half of the whale movement from the middle platform to end
    {
        SparklesFx.SetActive(true);
        player.transform.parent = null;
        whaleSharkObj.SetActive(false);
        whaleSharkObj.transform.position = wsTransforms[2];
        whaleSharkObj.transform.localEulerAngles = wsRotations[1];
        whaleSharkObj.SetActive(true);
        go = true;
    }

    public void EndLerpToPosition()
    {
        slerpTimer += Time.deltaTime;
        net[1].SetActive(false);
        SparklesFx.SetActive(false);
        player.transform.parent = whaleSharkObj.transform;
        whaleSharkObj.transform.position = Vector3.Lerp(wsTransforms[2], wsTransforms[3], slerpTimer / lerpDuration[2]);
        ang[1].SetActive(true);
    }

   
    public IEnumerator sharkHit(GameObject obj)
    {
        
        yield return new WaitForSeconds(1.5f);
        isHit = true;
        particleObject.transform.position = transform.position;
        system.Emit(10);
        player.GetComponent<PlayerController>().health -= 1;
        if (player.GetComponent<PlayerController>().health == 0)
        {
            player.GetComponent<PlayerController>().Respawn();
            //Restart or faint or something
        }
        obj.SetActive(false);
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
