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

    void Start()
    {
        StartCoroutine(panCam());
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
        yield return new WaitForSeconds(6f);
        anim.SetTrigger("End");
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(levelName);


    }

}
