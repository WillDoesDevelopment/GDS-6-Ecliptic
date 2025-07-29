using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameIEnums : MonoBehaviour
{
    public static IEnumerator ScreenshotIEnum()
    {
        yield return new WaitForEndOfFrame();
        string m_Path = Application.dataPath;
        string screenshotFile = m_Path + "/Screenshots/" + "Ecliptic_Screenshot" + System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)") + ".png";
        ScreenCapture.CaptureScreenshot(screenshotFile,4);
        Debug.Log("A screenshot was taken!"+ "\n" + screenshotFile);
        //StartCoroutine(screenshotScript.ScreenshotIEnum());
    }

}
