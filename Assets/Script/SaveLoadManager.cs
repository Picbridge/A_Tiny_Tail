using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class PlayerData
{
    public bool isCharacterSaved;
    public bool isStarSaved;
    public CharacterSaver characterSaver;
    public RankSaver starSaver;
}

public class SaveLoadManager : MonoBehaviour {

    #region Singleton

    public static SaveLoadManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of SaveLoadManager found!");
            return;
        }
        instance = this;
    }

    #endregion

    [HideInInspector] public PlayerData dataSavedByOtherClasses;

    public void Save()
    {
        // Creates a BinaryFormatter & a File
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        // Creates a Object to save the data to
        PlayerData data = new PlayerData();
        data = dataSavedByOtherClasses;

        // Writes the Object to the file & Closes it
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            dataSavedByOtherClasses = data;
        }
        else
        {
            Debug.Log("File does not exist");
        }
    }
    
    public void Delete()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
            File.Delete(Application.persistentDataPath + "/playerInfo.dat");
    }
}
