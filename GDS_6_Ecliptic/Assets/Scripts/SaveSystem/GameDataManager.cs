using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Text;

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
			return true;
		else
			return false;
	}

	private SaveData CreateSave(int lv)
	{
		if (CheckLevel(lv))
		{
			SaveData data = new SaveData(lv);
			Debug.Log("New Save Data Created: " + lv);
			return new SaveData(lv);
		}
		else
			return new SaveData();
	}

	private SaveData ReadFile()
	{
		Debug.Log("Reading file...");
		
		try
		{
			FileStream file = File.OpenRead(Application.persistentDataPath + savePath);
			StreamReader stream = new StreamReader(file, Encoding.UTF8);

			SaveData data;
			data = JsonUtility.FromJson<SaveData>(stream.ReadToEnd());
			
			file.Close();
			Debug.Log("Save File Lv: " + data.CurrentLevel);
			return data;
		}
		catch (Exception e)
		{
			Debug.LogError("Could not read save file: " + e);
			return null;
		}
	}

	private void WriteFile(SaveData data)
	{
		Debug.Log("Writing file...");
		Debug.Log("Data: " + data.CurrentLevel);
		string js = JsonUtility.ToJson(data);

		Debug.Log("Json Data: " + js);
		
		try
		{
			FileStream file = File.Open(Application.persistentDataPath + savePath, FileMode.Create);
			file.Write(Encoding.UTF8.GetBytes(js));
			file.Close();
		}
		catch (Exception e)
		{
			Debug.LogError("Could not write save file: " + e);
		}
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
