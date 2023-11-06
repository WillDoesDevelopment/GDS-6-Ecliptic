using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolScript : MonoBehaviour
{
    public AudioSource[] thisAudio;
    public MusicVolume MV;
    // Start is called before the first frame update
    void Start()
    {
        thisAudio = this.GetComponents<AudioSource>();
        MV = GameObject.FindObjectOfType<MusicVolume>();
        Debug.Log("OnStart");
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(AudioSource AS in thisAudio)
        {
            if(MV!= null)
            {
                if (AS.clip != MV.BackgroundAudio.clip)
                {
                    AS.volume = MusicVolume.SFXVOLUME;
                }
                else
                {
                    //Debug.Log("Running");
                    AS.volume = MusicVolume.MUSICVOLUME;
                }

            }
            else
            {
                //Debug.Log("Running");
                AS.volume = MusicVolume.MUSICVOLUME;
            }
        }
    }
}
