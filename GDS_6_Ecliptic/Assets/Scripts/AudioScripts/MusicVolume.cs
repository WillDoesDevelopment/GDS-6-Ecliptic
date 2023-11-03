using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MusicVolume : MonoBehaviour
{
    public static float MUSICVOLUME = 0.5f;
    public static float SFXVOLUME = 0.5f;

    public Slider MusicVol;
    public Slider SFXVol;

    public AudioSource BackgroundAudio;
  

    public void Awake()
    {
        /*AllAudio = GameObject.FindObjectsOfType<AudioSource>();

        foreach (AudioSource AS in AllAudio)
        {
            if(AS == BackgroundAudio)
            {
                //AS.clip = null;
            }
        }

        Debug.Log(AllAudio.Length);*/
    }

    void Start()
    {
        MusicVol.value = MUSICVOLUME;
        SFXVol.value = SFXVOLUME;
    }

    // Update is called once per frame
    void Update()
    {
        /*        foreach( AudioSource AS in AllAudio)
                {
                    AS.volume = SFXVol.value;
                }
                BackgroundAudio.volume = MusicVol.value;*/

        MUSICVOLUME = MusicVol.value;
        SFXVOLUME = SFXVol.value;
    }
}
