using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveManager : MonoBehaviour
{
	[SerializeField] private SaveData sceneNum;
    private string saveFileName;

    // Start is called before the first frame update
    void Start()
    {
        sceneNum = new SaveData();
        sceneNum.sceneNum = SceneManager.GetActiveScene().buildIndex;

        saveFileName = string.Concat(Application.persistentDataPath, "/save.ecls");
    }

    public void Save()
    {
		Debug.Log(Application.persistentDataPath);
		CheckFile();

        File.WriteAllText(saveFileName, ConvertToJson());
    }

    private void CheckFile()
    {
        if (!Directory.Exists(Application.persistentDataPath))
        {
			Directory.CreateDirectory(Application.persistentDataPath);
		}
    }

    private string ConvertToJson()
    {
        string saveData = JsonUtility.ToJson(sceneNum);

        return saveData;
    }

	public void Load()
    {
        if (File.Exists(saveFileName))
        {
			SaveData load = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveFileName));

			Debug.LogError(load.sceneNum);

			sceneNum.sceneNum = load.sceneNum;

			SceneManager.LoadScene(sceneNum.sceneNum);
		}
    }
}

[System.Serializable]
public class SaveData
{
    public int sceneNum;
}
