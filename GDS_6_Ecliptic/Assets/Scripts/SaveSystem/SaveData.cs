using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class SaveData
{
	[SerializeField] private int m_currLvNum;

	public SaveData()
	{
		m_currLvNum = 1;
	}

	public SaveData(int currLvNum)
	{
		m_currLvNum = currLvNum;
	}

	public int CurrentLevel
	{
		get { return m_currLvNum; }
		set { m_currLvNum = value; }
	}
}

public enum SaveState
{
	Reset = 0,
	Save = 1,
	Load = 2
}
