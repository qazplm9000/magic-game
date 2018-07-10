using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameData : MonoBehaviour {

	public static GameData gameData;
	public String filename = "player.dat";

	public int health;
	public int fireLevel;
	public int lightningLevel;


	// Use this for initialization
	void Awake(){
		if(gameData == null){
			DontDestroyOnLoad(gameObject);
			gameData = this;
		}else if(gameData != this){
			Destroy(gameObject);
		}
	}
	

	void OnGUI(){
		
	}



	public void Save(){
		String filepath = Application.persistentDataPath + "/" + filename;

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(filepath);
		PlayerData data = new PlayerData();
		data.health = health;
		data.fireLevel = fireLevel;
		data.lightningLevel = lightningLevel;

		bf.Serialize(file, data);
		file.Close();
	}

	public void Load(){
		String filepath = Application.persistentDataPath + "/" + filename;

		if(File.Exists(filepath)){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(filepath, FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();
		}
	}


}

[Serializable]
class PlayerData{
	public int health;
	public int fireLevel;
	public int lightningLevel;
}


