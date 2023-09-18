using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introCineCam : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject star;
    public Animator anim;
    public string levelname;

    void Start()
    {
        StartCoroutine(PanCam());
    }

    IEnumerator PanCam()
    {
        yield return new WaitForSeconds(1.5f);
        anim.SetTrigger("Pan");
        StartCoroutine(OutroCam());
    }

    IEnumerator OutroCam()
    {
        yield return new WaitForSeconds(6f);
        player.SetActive(true);
        star.SetActive(false);
        yield return new WaitForSeconds(2f);
        anim.SetTrigger("Outro");
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(levelname);
    }
}
