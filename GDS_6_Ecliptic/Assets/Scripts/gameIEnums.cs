using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameIEnums : MonoBehaviour
{
    public static IEnumerator ScreenshotIEnum()
    {
        yield return new WaitForEndOfFrame();
        string m_Path = Application.dataPath +"/Screenshots";        

        bool exists = System.IO.Directory.Exists(m_Path);

        if (!exists)
            System.IO.Directory.CreateDirectory(m_Path);

        string screenshotFile = m_Path + "/Ecliptic_Screenshot" + System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)") + ".png";
        int imageSizeMultiplier = 4;

        ScreenCapture.CaptureScreenshot(screenshotFile, imageSizeMultiplier);

        Debug.Log("A screenshot was taken!"+ "\n" + screenshotFile);

        //StartCoroutine(screenshotScript.ScreenshotIEnum());
    }

}
