using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

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
    public ProgressTags progressTags;
    [SerializeField]
    public FishData[] fishDatas;
    [SerializeField]
    public StorageChestData storageChestData;
    [SerializeField]
    public ShopData shopData;
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
        progressTags = saveData.progressTags;
        fishDatas = saveData.fishDatas;
        storageChestData = saveData.storageChestData;
        shopData = saveData.shopData;
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
public class ProgressTags
{
    [SerializeField]
    private string[] tags = new string[0];

    public bool ContainsTag(string tag)
    {
        bool contains = false;
        foreach(string tagToCheck in tags)
        {
            if(tagToCheck == tag) contains = true;
        }
        return contains;
    }
    public void AddTag(string tag)
    {
        List<string> tagsList = tags.ToList();
        tagsList.Add(tag);
        tags = tagsList.ToArray();
    }
    public void RemoveTag(string tag)
    {
        List<string> tagsList = tags.ToList();
        tagsList.Remove(tag);
        tags = tagsList.ToArray();
    }
    public ProgressTags(ProgressTags progressTags)
    {
        tags = progressTags.tags;
    }
}
[System.Serializable]
public class FishData
{
    [SerializeField]
    public int fishUnlockment;
    [SerializeField]
    public float maxWeightCaught;

    public void UpdateUnlockment(int newUnlockment)
    {
        fishUnlockment = Math.Max(fishUnlockment, newUnlockment);
    }
    public void UpdateFishSize(float newSize)
    {
        maxWeightCaught = Mathf.Max(maxWeightCaught, newSize);
    }
    public FishData(FishData fishData)
    {
        fishUnlockment = fishData.fishUnlockment;
        maxWeightCaught = fishData.maxWeightCaught;
    }
}

[System.Serializable]
public class SettingsData
{
    [SerializeField]
    public float soundVolume;
    public float musicVolume;
    public float offsetValue;

    public SettingsData(SettingsData settingsData)
    {
        soundVolume = settingsData.soundVolume;
        musicVolume = settingsData.musicVolume;
        offsetValue = settingsData.offsetValue;
    }
}

[Serializable]
public class ShopData
{
    public int shopLevel;
    public ShopItemData[] shopItemDatas;

    public ShopData(ShopData shopData)
    {
        shopLevel = shopData.shopLevel;
        shopItemDatas = shopData.shopItemDatas;
    }
}

[Serializable]
public class ShopItemData
{
    public int shopItemId; //-1 - restocking; -2 - slot locked; -3 - no suitable items!!!
    public float timeTillItemRestock; //In seconds

    public ShopItemData(ShopItemData shopItemData)
    {
        shopItemId = shopItemData.shopItemId;
        timeTillItemRestock = shopItemData.timeTillItemRestock;
    }
}