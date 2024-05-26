using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Stats : MonoBehaviour
{
    public float furtherstDistance = 0;
    public float totalDistance = 0;
    public int totalfoodEaten = 0;
    public int bestRun;
    public bool InitialAD = false;
    public int[] levelcompletes;
    public bool[] Dragons;


    public void UpdateStats(int newfood, float newdistance)
    {
        if (furtherstDistance < newdistance)
            furtherstDistance = newdistance;
        if (newfood > bestRun)
        {
            bestRun = newfood;
        }
        totalDistance += newdistance;
        totalfoodEaten += newfood;
        Save();

    }
    public void LoadStats()
    {
        Load();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();
        data.furtherstDistance = furtherstDistance;
        data.totalDistance = totalDistance;
        data.totalfoodEaten = totalfoodEaten;
        data.bestRun = bestRun;
        data.playedad = InitialAD;

        
        data.levelcomps = levelcompletes;

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("SAVED!");
    }
    public void ResetData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();
        data.furtherstDistance = 0;
        data.totalDistance = 0;
        data.totalfoodEaten = 0;
        data.bestRun = 0;
        data.playedad = false;


        data.levelcomps = new int[0];

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("SAVED!");
    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            furtherstDistance = data.furtherstDistance;
            totalDistance = data.totalDistance;
            totalfoodEaten = data.totalfoodEaten;
            bestRun = data.bestRun;
            InitialAD = data.playedad;

            //levelcompletes;
            levelcompletes = new int[GameMaster.instance.Levels.Count];
            for(int i =0;i< levelcompletes.Length;i++)
            {
                if (data.levelcomps != null && i < data.levelcomps.Length)
                {
                    levelcompletes[i] = data.levelcomps[i];
                }
                else
                {
                    levelcompletes[i] = 0;
                }
            }
            //levelcompletes = data.levelcomps;
            Debug.Log("LOADED!");
        }
    }

}
[System.Serializable]
class PlayerData
{
    public float furtherstDistance;
    public float totalDistance;
    public int totalfoodEaten;
    public int bestRun;
    public bool playedad;
    public int[] levelcomps = new int[0];

}
