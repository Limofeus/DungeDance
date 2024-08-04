using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemTimeoutTracker : MonoBehaviour
{
    private static ShopItemTimeoutTracker Singleton;
    private ShopItemData[] shopItemDatas;
    void Awake()
    {
        if(Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
            if(MenuDataManager.saveData == null)
            {
                SaveData loadedData = SaveSystem.Load();
                if(loadedData == null || loadedData.shopData.shopLevel == 0)
                {
                    //SL == 0, no need in timeout tracker;
                    Singleton = null;
                    Debug.Log("Deleting this thing");
                    Destroy(this.gameObject);
                    return;
                }
                MenuDataManager.saveData = loadedData;
            }
            shopItemDatas = MenuDataManager.saveData.shopData.shopItemDatas;
        }
        else
        {
            Singleton.SaveCountedTimeouts();
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        TickItemTimer(Time.deltaTime);
    }
    void OnApplicationQuit()
    {
        if (this != Singleton) return;
        Debug.Log("Application ending after " + Time.time + " seconds, saving shop item restock times");
        SaveCountedTimeouts();
        SaveSystem.Save(MenuDataManager.saveData);
    }
    private void TickItemTimer(float delta)
    {
        foreach(var itemDat in shopItemDatas)
        {
            if(itemDat.shopItemId == -1)
            {
                itemDat.timeTillItemRestock -= delta;
            }
        }
    }
    private void SaveCountedTimeouts()
    {
        Debug.Log("Saving SITT");
        foreach (var itemDat in shopItemDatas)
        {
            Debug.Log($"itId: {itemDat.shopItemId}, ttRS: {itemDat.timeTillItemRestock}");
        }
        MenuDataManager.saveData.shopData.shopItemDatas = shopItemDatas;
    }

    public static void LoadCountedTimeouts()
    {
        Debug.Log("Loading up SITT");
        Singleton.shopItemDatas = MenuDataManager.saveData.shopData.shopItemDatas;
        foreach (var itemDat in Singleton.shopItemDatas)
        {
            Debug.Log($"itId: {itemDat.shopItemId}, ttRS: {itemDat.timeTillItemRestock}");
        }
    }
}
