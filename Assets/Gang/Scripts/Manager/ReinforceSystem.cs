using System.IO;
using UnityEngine;

public static class ReinforceSystem
{
	private static string SavePath => Application.persistentDataPath + "/";

	public static void Save(Reinforce saveData, string saveFileName)
	{
		if (!Directory.Exists(SavePath))
		{
			Directory.CreateDirectory(SavePath);
		}

		string saveJson = JsonUtility.ToJson(saveData);

		string saveFilePath = SavePath + saveFileName + ".json";
		File.WriteAllText(saveFilePath, saveJson);
		Debug.Log("Save Success: " + saveFilePath);
	}

	public static Reinforce Load(string saveFileName)
	{
		string saveFilePath = SavePath + saveFileName + ".json";

		if (!File.Exists(saveFilePath))
		{
			Debug.LogError("No such saveFile exists");
			return null;
		}

		string saveFile = File.ReadAllText(saveFilePath);
		Reinforce saveData = JsonUtility.FromJson<Reinforce>(saveFile);
		return saveData;
	}
}
