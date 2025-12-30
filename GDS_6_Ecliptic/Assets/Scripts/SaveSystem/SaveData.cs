using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class SaveData
{
	private int m_currLvNum;

	//private int m_completeRuns;

	public SaveData()
	{
		m_currLvNum = 1;
		//m_completeRuns = 0;
	}

	public SaveData(int completeRuns)
	{
		m_currLvNum = 1;
		//m_completeRuns = completeRuns;
	}

	public SaveData(int currLvNum, int completeRuns)
	{
		m_currLvNum = currLvNum;
		//m_completeRuns = completeRuns;
	}

	public int CurrentLevel
	{
		get { return m_currLvNum; }
		set { m_currLvNum = value; }
	}

	/*public int CompleteRuns
	{
		get { return m_completeRuns; }
	}

	public void IncrementRun()
	{
		m_completeRuns++;
	}*/
}

public enum SaveState
{
	Reset = 0,
	Save = 1,
	Load = 2
}
