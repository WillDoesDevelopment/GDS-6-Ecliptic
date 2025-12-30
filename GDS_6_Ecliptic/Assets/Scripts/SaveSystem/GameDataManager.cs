using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameDataManager : MonoBehaviour
{
	[SerializeField] private string savePath = "/save.ecl";

	private void CheckPath()
	{
		Debug.Log("Checking path...");
		if (!Directory.Exists(Application.persistentDataPath))
		{
			Debug.Log("Path not found.");
			Directory.CreateDirectory(Application.persistentDataPath);
		}
		Debug.Log("Checked path.");
	}

	private void CheckFile()
	{
		Debug.Log("Checking file...");
		if (!File.Exists(Application.persistentDataPath + savePath))
		{
			File.Create(Application.persistentDataPath + savePath).Dispose();
			Debug.Log("File not found.");
		}
		Debug.Log("Checked file.");
	}

	private bool CheckLevel(int lvNum)
	{
		if (lvNum > 1 && lvNum < 13)
			return false;
		else
			return true;
	}

	private SaveData CreateSave(int lv)
	{
		SaveData data;

		if (CheckLevel(lv))
			data = new SaveData(lv);
		else
			data = new SaveData();

		return data;
	}

	private SaveData ReadFile()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.OpenRead(Application.persistentDataPath + savePath);
		//try catch
		SaveData data = (SaveData)bf.Deserialize(file);
		file.Close();
		Debug.Log("Save File Lv: " + data.CurrentLevel);
		return data;
	}

	private void WriteFile(SaveData data)
	{
		Debug.Log("Writing file...");
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(Application.persistentDataPath + savePath, FileMode.OpenOrCreate);
		bf.Serialize(file, data);
		file.Close();
		Debug.Log("File written");
	}

	public void Save()
	{
		CheckPath();
		CheckFile();

		int lv = SceneManager.GetActiveScene().buildIndex;
		Debug.Log("Scene Number: " + lv);
		SaveData data = CreateSave(lv);

		WriteFile(data);
	}

	public void Load()
	{
		SaveData data = ReadFile();

		Debug.Log("Current Level: " + data.CurrentLevel);

		if (CheckLevel(data.CurrentLevel))
		{
			SceneManager.LoadScene(data.CurrentLevel);
		}
		else
		{
			SceneManager.LoadScene(1);
		}

		Time.timeScale = 1;
	}

	public void Reset()
	{
		CheckPath();
		CheckFile();

		SaveData data = CreateSave(0);

		WriteFile(data);
	}
}
