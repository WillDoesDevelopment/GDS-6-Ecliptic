using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutManager : MonoBehaviour
{
    public Sprite FadeInImage;
    public Sprite FadeOutImage;

    public Sprite[] FadeOutSprites;
    public bool inHub = false;

    // Start is called before the first frame update
    void Start()
    {
        SetFadeOutImage();
        if (FadeInImage != null)
        {
            this.transform.GetChild(1).GetComponent<Image>().sprite = FadeInImage;
        }
    }
    public void SetFadeOutImage()
    {
        if (FadeOutSprites.Length > HubManager.LevelNumber && inHub == true)
        {
            this.FadeOutImage = FadeOutSprites[HubManager.LevelNumber];

        }
    }
    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void fadeOutImageChange()
    {
        if (FadeOutImage != null)
        {
            this.transform.GetChild(1).GetComponent<Image>().sprite = FadeOutImage;
        }
    }
}
