using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolScript : MonoBehaviour
{
    public AudioSource[] thisAudio;
    public MusicVolume MV;

    public bool IsBackgroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        thisAudio = this.GetComponents<AudioSource>();
        MV = GameObject.FindObjectOfType<MusicVolume>();
        //Debug.Log("OnStart");
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (AudioSource AS in thisAudio)
        {
            /*            if (MV != null)
                        {
                            if (AS.clip != MV.BackgroundAudio.clip)
                            {
                                Debug.Log("Working");
                                AS.volume = MusicVolume.SFXVOLUME;
                            }
                            else
                            {
                                //Debug.Log("Running");
                                AS.volume = MusicVolume.MUSICVOLUME;
                            }

                        }
                        else if (MV == null)
                        {
                            //Debug.Log("Running");
                            AS.volume = MusicVolume.SFXVOLUME;
                        }*/
            AS.volume = MusicVolume.SFXVOLUME;
        }
        if (IsBackgroundMusic)
        {
            thisAudio[0].volume = MusicVolume.MUSICVOLUME;
        }
    }
}
