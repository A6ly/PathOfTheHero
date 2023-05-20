using System;
using System.IO;
using UnityEngine;

[Serializable]
public class UserData
{
    public UserData(int _userLevel, int _userExp, int _currentStage, int _maxStage)
    {
        userLevel = _userLevel;
        userExp = _userExp;
        currentStage = _currentStage;
        maxStage = _maxStage;
    }

    public int userLevel;
    public int userExp;
    public int currentStage;
    public int maxStage;

    public int UserLevel { get { return userLevel; } }
    public int UserExp { get { return userExp; } }
    public int CurrentStage { get { return currentStage; } }
    public int MaxStage { get { return currentStage; } }
}

public class DataManager
{
    public UserData UserData;
    string savePath => $"{Application.persistentDataPath}/Save/";

    public void Init()
    {
        UserData = Load();

        if (UserData == null)
        {
            UserData = new UserData(1, 0, 1, 10);
        }
    }

    public UserData Load()
    {
        string saveFilePath = $"{savePath}userData.json";

        if (!File.Exists(saveFilePath))
        {
            Debug.LogError("No such saveFile exists");

            return null;
        }

        string saveFile = File.ReadAllText(saveFilePath);
        UserData saveData = JsonUtility.FromJson<UserData>(saveFile);

        return saveData;
    }

    public int CalculateBonusStat()
    {
        int bonusStat = 10 * UserData.UserLevel;

        return bonusStat;
    }

    public void ClearStage(int stageNum)
    {
        if (UserData.currentStage == stageNum && UserData.currentStage < UserData.maxStage)
        {
            UserData.currentStage++;
        }
    }

    public bool AddExp(int exp)
    {
        UserData.userExp += exp;

        if (UserData.userExp >= 500 * UserData.userLevel)
        {
            UserData.userExp = 0;
            UserData.userLevel++;

            return true;
        }

        return false;
    }

    public void Save()
    {
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        string saveJson = JsonUtility.ToJson(UserData);

        string saveFilePath = $"{savePath}userData.json";
        File.WriteAllText(saveFilePath, saveJson);
        Debug.Log("Save Success: " + saveFilePath);
    }

    //int userLevel;
    //int userExp;
    //int currentStage;
    //int maxStage;

    //public int UserLevel { get { return userLevel; } }
    //public int UserExp { get { return userExp; } }
    //public int CurrentStage { get { return currentStage; } }
    //public int MaxStage { get { return currentStage; } }

    //public void Init()
    //{
    //    userLevel = PlayerPrefs.GetInt("UserLevel", 1);
    //    userExp = PlayerPrefs.GetInt("UserExp", 0);
    //    currentStage = PlayerPrefs.GetInt("CurrentStage", 1);
    //    maxStage = PlayerPrefs.GetInt("MaxStage", 10);
    //}

    //public int CalculateBonusStat()
    //{
    //    int bonusStat = 10 * userLevel;

    //    return bonusStat;
    //}

    //public void ClearStage(int stageNum)
    //{
    //    if (currentStage == stageNum && currentStage < maxStage)
    //    {
    //        currentStage++;
    //    }
    //}

    //public bool AddExp(int exp)
    //{
    //    userExp += exp;

    //    if (userExp >= 500 * userLevel)
    //    {
    //        userExp = 0;
    //        userLevel++;

    //        return true;
    //    }

    //    return false;
    //}

    //public void Save()
    //{
    //    PlayerPrefs.SetInt("UserLevel", userLevel);
    //    PlayerPrefs.SetInt("UserExp", userExp);
    //    PlayerPrefs.SetInt("CurrentStage", currentStage);
    //}
}