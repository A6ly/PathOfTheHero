using System;
using System.IO;
using UnityEngine;
using static Define;

[Serializable]
public class UserData
{
    public UserData(int _userLevel, int _userExp, int _userMaxExp, int _currentStage, int _maxStage, float _effectVolume, float _bgmVolume)
    {
        userLevel = _userLevel;
        userExp = _userExp;
        userMaxExp = _userMaxExp;
        currentStage = _currentStage;
        maxStage = _maxStage;
        effectVolume = _effectVolume;
        bgmVolume = _bgmVolume;
    }

    public int userLevel;
    public int userExp;
    public int userMaxExp;
    public int currentStage;
    public int maxStage;
    public float effectVolume;
    public float bgmVolume;

    public int UserLevel { get { return userLevel; } }
    public int UserExp { get { return userExp; } }
    public int UserMaxExp { get { return userMaxExp; } }
    public int CurrentStage { get { return currentStage; } }
    public int MaxStage { get { return currentStage; } }
    public float EffectVolume { get { return effectVolume; } }
    public float BgmVolume { get { return bgmVolume; } }
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
            UserData = new UserData(1, 0, 500, 1, 10, 1.0f, 1.0f);
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

        if (UserData.userExp >= UserData.userMaxExp)
        {
            UserData.userExp = 0;
            UserData.userLevel++;
            UserData.userMaxExp += 500;

            return true;
        }

        return false;
    }

    public void SetEffectVolume(float volume)
    {
        UserData.effectVolume = volume;
    }

    public void SetBgmVolume(float volume)
    {
        UserData.bgmVolume = volume;
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
}