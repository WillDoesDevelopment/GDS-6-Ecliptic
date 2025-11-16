using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

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
    public CinemachineVirtualCamera vCam1;
    public CinemachineVirtualCamera vCam2;

    [Header("Net")]
    public GameObject[] net;

    [Header("Angler")]
    public GameObject[] ang;

    [Header("Dialogue")]
    public Dialogue[] dia;
    public GameObject exitDoor;

    // Start is called before the first frame update
    void Start()
    {
        vCam1.Priority = 10;
        vCam2.Priority = 0;
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

        if (dia[1].DialogueMode == Dialogue.DialogueState.Finished)
        {
            exitDoor.GetComponent<DoorScript>().DS.IsOpen = true;

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
        player.GetComponent<PlayerController>().health = 3;
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
        vCam1.Priority = 0;
        vCam2.Priority = 10;
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
            PiscesReset();
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


    public void PiscesReset()
    {
        lerpTimer = 0f;
        slerpTimer = 0f;

        player.transform.parent = null;
        whaleSharkObj.transform.position = wsTransforms[0];
        whaleSharkObj.transform.localEulerAngles = wsRotations[0];
        net[0].SetActive(true);
        net[1].SetActive(true);

        for (int i = 0; i < ang[0].transform.childCount; i++)
        {
            ang[0].transform.GetChild(i).gameObject.SetActive(true);
        }
        for (int i = 0; i < ang[1].transform.childCount; i++)
        {
            ang[1].transform.GetChild(i).gameObject.SetActive(true);
        }

        dia[0].DialogueMode = Dialogue.DialogueState.NotStarted;
        dia[1].DialogueMode = Dialogue.DialogueState.NotStarted;

        ang[0].SetActive(false);
        ang[1].SetActive(false);

        exitDoor.GetComponent<DoorScript>().DS.IsOpen = false;

        vCam1.Priority = 10;
        vCam2.Priority = 0;

        AtoBStart = false;
        go = false;

    }

}
