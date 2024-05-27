using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccessAdjust : MonoBehaviour
{

    //This handles the sfx and music controls and canvas

    [Header("Floats")]
    public float masterVol;
    public float sfxVol;

    [Header("Audio Ref")]
    public GameObject[] BackGAud;
    public GameObject[] sfxAud;

    [Header("Music Canvas")]
    public TextMeshProUGUI[] myTextMV;
    public Slider[] mySliderMV;

    [Header("Effects Canvas")]
    public TextMeshProUGUI[] myTextSFX;
    public Slider[] mySliderSFX;

    private void Start()
    {
        masterVol = 0.5f;
        sfxVol = 0.5f;

    }
    

    private void Update()
    {
        //updating the volumes based on the master and sfx
        for(var i = 0; i < BackGAud.Length; i++)
        {
            if(BackGAud[i] != null)
            {
                BackGAud[i].GetComponent<AudioSource>().volume = masterVol;
                BackGAud[i].GetComponent<GameObject>();

            }
        }

        for (var i = 0; i < sfxAud.Length; i++)
        {
            if (sfxAud[i] != null)
            {
                sfxAud[i].GetComponent<GameObject>();
                sfxAud[i].GetComponent<AudioSource>().volume = sfxVol;

            }
        }

    }

    public void AdjMasVol(float newMV)
    {
        //creates a new mv value depending on where the slider is 

        for (var i = 0; i < mySliderMV.Length; i++)
        {
            newMV = mySliderMV[i].value * 100;

            for (var j = 0; j < myTextMV.Length; j++)
            {
                myTextMV[j].text = newMV.ToString("0");
            }

            masterVol = mySliderMV[i].value;
        }

        
    }

    public void AdjSfx(float newSFX)
    {
        //creates a new sfx value depending on where the slider is

        for (var i = 0; i < mySliderSFX.Length; i++)
        {
            newSFX = mySliderSFX[1].value * 100;

            for (var j = 0; j < myTextSFX.Length; j++)
            {
                myTextSFX[1].text = newSFX.ToString("0");
            }

            sfxVol = mySliderSFX[1].value;
        }

    }
}
