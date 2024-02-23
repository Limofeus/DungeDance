using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    [SerializeField]
    public string playerName; // I'll use this to figure out whether the character is created or not
    [SerializeField]
    public int playerLevel;
    [SerializeField]
    public int playerXp;
    [SerializeField]
    public int item1Id;
    [SerializeField]
    public int item2Id;
    [SerializeField]
    public int item3Id;
    [SerializeField]
    public int[] itemUnlockDatas; // 0 - Locked, 1 - Unlocked unknown, 2 - Unlocked known
    [SerializeField]
    public int moneyAmount;
    [SerializeField]
    public LevelData[] levelDatas;
    [SerializeField]
    public StorageChestData storageChestData;
    [SerializeField]
    public SettingsData settingsData;

    public SaveData(SaveData saveData)
    {
        playerName = saveData.playerName;
        playerLevel = saveData.playerLevel;
        playerXp = saveData.playerXp;
        item1Id = saveData.item1Id;
        item2Id = saveData.item2Id;
        item3Id = saveData.item3Id;
        itemUnlockDatas = saveData.itemUnlockDatas;
        moneyAmount = saveData.moneyAmount;
        levelDatas = saveData.levelDatas;
        storageChestData = saveData.storageChestData;
        settingsData = saveData.settingsData;
    }
}

[System.Serializable]
public class LevelData
{
    [SerializeField]
    public bool completed;
    [SerializeField]
    public int maxScore;

    public LevelData(LevelData levelData)
    {
        completed = levelData.completed;
        maxScore = levelData.maxScore;
    }
}
[System.Serializable]
public class StorageChestData
{
    [SerializeField]
    public int storageChestLevel;
    [SerializeField]
    public int[] storageItemIds;

    public StorageChestData(StorageChestData storageChestData)
    {
        storageChestLevel = storageChestData.storageChestLevel;
        storageItemIds = storageChestData.storageItemIds;
    }
}

[System.Serializable]
public class SettingsData
{
    [SerializeField]
    public float soundVolume;
    public float musicVolume;

    public SettingsData(SettingsData settingsData)
    {
        soundVolume = settingsData.soundVolume;
        musicVolume = settingsData.musicVolume;
    }
}
