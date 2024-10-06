using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PAXStartIntro : MonoBehaviour
{

    public string levelName;
    public AudioSource snd;
    public AudioSource snd2;
    public Material[] Mats;

    // Start is called before the first frame update
    void Start()
    {
        Mats[0].DisableKeyword("_EMISSION");
        Mats[1].DisableKeyword("_EMISSION");
        StartCoroutine(outro());
    }


    IEnumerator outro()
    {

        //outro function, this plays the final animation of ophie 

        //OPHIE DISCOVERS HE HAS HANDS
        yield return new WaitForSeconds(2f);
        snd2.Play();

        yield return new WaitForSeconds(5f);
        DoorsOn();

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(2); // GOES TO HUB AFTER THAT SOUNDS DONE

    }

    public void DoorsOn()
    {
        Mats[0].EnableKeyword("_EMISSION");
        Mats[1].EnableKeyword("_EMISSION");
    }
}
