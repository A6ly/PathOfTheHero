using System;
using System.IO;
using UnityEngine;
using UnityEngine.Localization.Settings;

[Serializable]
public class UserData
{
    public UserData(int _userLevel, int _userExp, int _userMaxExp, int _currentStage, int _maxStage, int _currentLanguage, float _effectVolume, float _bgmVolume)
    {
        userLevel = _userLevel;
        userExp = _userExp;
        userMaxExp = _userMaxExp;
        currentStage = _currentStage;
        maxStage = _maxStage;
        currentLanguage = _currentLanguage;
        effectVolume = _effectVolume;
        bgmVolume = _bgmVolume;
    }

    public int userLevel;
    public int userExp;
    public int userMaxExp;
    public int currentStage;
    public int maxStage;
    public int currentLanguage;
    public float effectVolume;
    public float bgmVolume;

    public int UserLevel { get { return userLevel; } }
    public int UserExp { get { return userExp; } }
    public int UserMaxExp { get { return userMaxExp; } }
    public int CurrentStage { get { return currentStage; } }
    public int MaxStage { get { return maxStage; } }
    public int CurrentLanguage { get { return currentLanguage; } }
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
            UserData = new UserData(1, 0, 500, 1, 15, 0, 0.75f, 0.5f);
        }

        LocalizationSettings.InitializationOperation.WaitForCompletion();
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[UserData.CurrentLanguage];
    }

    public UserData Load()
    {
        string saveFilePath = $"{savePath}userData.json";

        if (!File.Exists(saveFilePath))
        {
            // Debug.LogError("No such saveFile exists");

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

    public void SetLocalization(int index)
    {
        UserData.currentLanguage = index;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
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
        // Debug.Log("Save Success: " + saveFilePath);
    }
}