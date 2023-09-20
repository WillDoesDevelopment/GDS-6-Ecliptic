using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introCineCamScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator anim;
    public GameObject plyer;
    public GameObject star;
    public string levelName;
    public Material[] glowAmt;
    public float amtStart = 1;

    private float shaderValue = 1f;
    private float change = 0.01f;

    void Start()
    {
        glowAmt[0].GetFloat("_Glow_Amount");
        glowAmt[1].GetFloat("_Glow_Amount");
        StartCoroutine(panCam());
        amtStart = 2;

        InvokeRepeating("AmtGo", 0.01f, 0.15f);
    }

    private void Update()
    {
        //amt = Mathf.Lerp(amtStart, amtEnd, Time.deltaTime * 6.0f);
        //AmtGo();
        
    }

    IEnumerator panCam()
    {
        yield return new WaitForSeconds(6f);
        anim.SetTrigger("Pan");
        plyer.SetActive(true);
        star.SetActive(false);

        StartCoroutine(outro());
    }

    IEnumerator outro()
    {
        yield return new WaitForSeconds(10f);
        anim.SetTrigger("End");
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(levelName);


    }

    public void AmtGo()
    {

            shaderValue -= change;
            glowAmt[0].SetFloat("_Glow_Amount", shaderValue);
            glowAmt[1].SetFloat("_Glow_Amount", shaderValue);


    }

}
