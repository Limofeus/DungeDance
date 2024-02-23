using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StorageChestManager : MonoBehaviour
{
    public int[] upgradeCosts;
    public GameObject[] upgradeAnimators;
    public int slotsUnlocked; // 1 - 3 (slots on player)
    public int chestLevel; // 0 - Lvl1, 1 - Lvl2, etc. (Chest rows of slots)
    public Animator sceneBlackScreen;
    public Transform playerItems;
    public Transform storageItems;
    public Transform storageBGPivot;
    public Transform upgradeStorage;
    public GameObject playerItemPrefab;
    public GameObject storageItemPrefab;
    public GameObject playerSlotLockedPrefab;
    public GameObject[] rarityTexts;
    public TextMeshPro itemNameText;
    public TextMeshPro itemDescText;
    public TextMeshPro moneyCountText;
    public TextMeshPro upgradeCostText;
    public SaveData saveData;
    private StoragePlayerItem storagePlayerItem1;
    private StoragePlayerItem storagePlayerItem2;
    private StoragePlayerItem storagePlayerItem3;
    [SerializeField]
    private StorageStoredItem[] storageStoredItems;
    public float verticalRowDist;
    private bool exiting = false;
    private string itemName = ":)";
    private string itemDesc = "[o_o]";
    private float itemNameLen = 0f;
    private float itemDescLen = 0f;
    private AsyncOperation asyncLoad;
    void Start()
    {
        if (MenuDataManager.saveData != null)
            saveData = new SaveData(MenuDataManager.saveData);
        else
            saveData = SaveSystem.Load();
        moneyCountText.text = saveData.moneyAmount.ToString();
        chestLevel = saveData.storageChestData.storageChestLevel;
        if (chestLevel < upgradeCosts.Length)
            upgradeCostText.text = upgradeCosts[chestLevel].ToString();
        UpdateSlotCount();
        InitialPlayerItemUpdate();
        InitialStorageItemUpdate();
        asyncLoad = SceneManager.LoadSceneAsync(1); // Fuck it all, I'll just preload the scene and then activate it when the time comes
        asyncLoad.allowSceneActivation = false;
    }
    private void UpdateSlotCount()
    {
        if (slotsUnlocked > 0) //Im too tired and too lazy to ake normal code, okay?
        {
            storagePlayerItem1 = Instantiate(playerItemPrefab, new Vector3(-2.1f, 0f, 0f) + playerItems.position, Quaternion.identity, playerItems).GetComponent<StoragePlayerItem>();
            if (slotsUnlocked > 1)
            {
                storagePlayerItem2 = Instantiate(playerItemPrefab, new Vector3(0f, 0f, 0f) + playerItems.position, Quaternion.identity, playerItems).GetComponent<StoragePlayerItem>();
                if (slotsUnlocked > 2)
                {
                    storagePlayerItem3 = Instantiate(playerItemPrefab, new Vector3(2.1f, 0f, 0f) + playerItems.position, Quaternion.identity, playerItems).GetComponent<StoragePlayerItem>();
                }
                else
                {
                    Instantiate(playerSlotLockedPrefab, new Vector3(2.1f, 0f, 0f) + playerItems.position, Quaternion.identity, playerItems);
                }
            }
            else
            {
                Instantiate(playerSlotLockedPrefab, new Vector3(0f, 0f, 0f) + playerItems.position, Quaternion.identity, playerItems);
                Instantiate(playerSlotLockedPrefab, new Vector3(2.1f, 0f, 0f) + playerItems.position, Quaternion.identity, playerItems);
            }
        }
        else
        {
            Instantiate(playerSlotLockedPrefab, new Vector3(-2.1f, 0f, 0f) + playerItems.position, Quaternion.identity, playerItems);
            Instantiate(playerSlotLockedPrefab, new Vector3(0f, 0f, 0f) + playerItems.position, Quaternion.identity, playerItems);
            Instantiate(playerSlotLockedPrefab, new Vector3(2.1f, 0f, 0f) + playerItems.position, Quaternion.identity, playerItems);
        }
    }//Just thought of сварачивать functuions in VScode, this is acctually INCREDIBLY useful
    private void InitialPlayerItemUpdate()
    {
        if (storagePlayerItem1 != null)
        {
            storagePlayerItem1.UpdateItem(saveData.item1Id);
            storagePlayerItem1.playerSlotId = 1;
            storagePlayerItem1.storageChestManager = this;
        }
        if (storagePlayerItem2 != null)
        {
            storagePlayerItem2.UpdateItem(saveData.item2Id);
            storagePlayerItem2.playerSlotId = 2;
            storagePlayerItem2.storageChestManager = this;
        }
        if (storagePlayerItem3 != null)
        {
            storagePlayerItem3.UpdateItem(saveData.item3Id);
            storagePlayerItem3.playerSlotId = 3;
            storagePlayerItem3.storageChestManager = this;
        }
    }
    private void InitialStorageItemUpdate()
    {
        storageStoredItems = new StorageStoredItem[(chestLevel + 2) * 5];
        for(int  i = 0; i < (chestLevel + 2) * 5; i++)
        {
            GameObject storageItem = Instantiate(storageItemPrefab,storageItems.position + new Vector3( 0.75f + ((i%5) * 1.25f), - 0.75f - ((i/5) * verticalRowDist), 0f), Quaternion.identity, storageItems);
            StorageStoredItem storageItemComp = storageItem.GetComponent<StorageStoredItem>();
            storageItemComp.UpdateItem(saveData.storageChestData.storageItemIds[i]);
            storageItemComp.storageChestManager = this;
            storageItemComp.storageSlotId = i;
            storageStoredItems[i] = storageItemComp;
        }
        storageBGPivot.localScale = new Vector3(1f, 1f + (chestLevel * 0.475f), 1f);
        if(chestLevel < 3)
        {
            upgradeStorage.transform.localPosition = Vector3.down * chestLevel * 1.35f;
        }
        else
        {
            upgradeStorage.gameObject.SetActive(false);
        }
    }
    public void UpgradeChest()
    {
        Debug.Log("Upgradin'");
        chestLevel = saveData.storageChestData.storageChestLevel;
        if (saveData.moneyAmount >= upgradeCosts[chestLevel])
        {
            saveData.moneyAmount -= upgradeCosts[chestLevel];
            saveData.storageChestData.storageChestLevel += 1;
            chestLevel += 1;
            MenuDataManager.saveData = saveData;
            SaveSystem.Save(MenuDataManager.saveData);
            moneyCountText.text = saveData.moneyAmount.ToString();
            Instantiate(upgradeAnimators[chestLevel - 1], upgradeStorage.position, Quaternion.identity);
            WipeItemSlots();
            InitialStorageItemUpdate();
            if(chestLevel < upgradeCosts.Length)
                upgradeCostText.text = upgradeCosts[chestLevel].ToString();
        }
    }
    public void UpdateItemTexts(string newItemName, string newItemDesc, int newItemRarity)
    {
        if(itemName != newItemName || itemDesc != newItemDesc)
        {
            itemName = newItemName;
            itemDesc = newItemDesc;
            itemNameLen = 0f;
            itemDescLen = 0f;
        }
        foreach(GameObject rarityText in rarityTexts)
        {
            rarityText.SetActive(false);
        }
        rarityTexts[newItemRarity].SetActive(true);
    }
    public void WipeItemSlots()
    {
        foreach(Transform chestItemSlot in storageItems)
        {
            Destroy(chestItemSlot.gameObject);
        }
        storageStoredItems = new StorageStoredItem[0];
    }
    public void StoreItem(int playerSlotId)
    {
        //bool itemStored = false;
        for(int i =0; i < (chestLevel + 2) * 5; i++)
        {
            if(saveData.storageChestData.storageItemIds[i] == -1)
            {
                switch (playerSlotId)
                {
                    case 1:
                        saveData.storageChestData.storageItemIds[i] = saveData.item1Id;
                        if (saveData.item2Id != -1)
                        {
                            if(saveData.item3Id != -1)
                            {
                                storagePlayerItem1.SpawnEndObj();
                                storagePlayerItem2.SpawnEndObj();
                                storagePlayerItem3.SpawnEndObj();
                                saveData.item1Id = saveData.item2Id; //Should make this better (maybe another function) when making Animations (Working on it rn(It'll NOT only check for visuals))
                                saveData.item2Id = saveData.item3Id;
                                saveData.item3Id = -1;
                                storagePlayerItem1.UpdateItem(saveData.item1Id);
                                storagePlayerItem2.UpdateItem(saveData.item2Id);
                                storagePlayerItem3.UpdateItem(saveData.item3Id);
                                storagePlayerItem1.SetAppearing();
                                storagePlayerItem2.SetAppearing();
                            }
                            else
                            {
                                storagePlayerItem1.SpawnEndObj();
                                storagePlayerItem2.SpawnEndObj();
                                saveData.item1Id = saveData.item2Id;
                                saveData.item2Id = -1;
                                storagePlayerItem1.UpdateItem(saveData.item1Id);
                                storagePlayerItem2.UpdateItem(saveData.item2Id);
                                storagePlayerItem1.SetAppearing();
                            }
                        }
                        else
                        {
                            storagePlayerItem1.SpawnEndObj();
                            saveData.item1Id = -1;
                            storagePlayerItem1.UpdateItem(saveData.item1Id);
                        }
                        storageStoredItems[i].UpdateItem(saveData.storageChestData.storageItemIds[i]);
                        storageStoredItems[i].SetAppearing();
                        break;
                    case 2:
                        saveData.storageChestData.storageItemIds[i] = saveData.item2Id;
                        if (saveData.item3Id != -1)
                        {
                            storagePlayerItem2.SpawnEndObj();
                            storagePlayerItem3.SpawnEndObj();
                            saveData.item2Id = saveData.item3Id; //And this one too
                            saveData.item3Id = -1;
                            storagePlayerItem2.UpdateItem(saveData.item2Id);
                            storagePlayerItem3.UpdateItem(saveData.item3Id);
                            storagePlayerItem2.SetAppearing();
                        }
                        else
                        {
                            storagePlayerItem2.SpawnEndObj();
                            saveData.item2Id = -1;
                            storagePlayerItem2.UpdateItem(saveData.item2Id);
                        }
                        storageStoredItems[i].UpdateItem(saveData.storageChestData.storageItemIds[i]);
                        storageStoredItems[i].SetAppearing();
                        break;
                    case 3:
                        storagePlayerItem3.SpawnEndObj();
                        saveData.storageChestData.storageItemIds[i] = saveData.item3Id;
                        saveData.item3Id = -1;
                        storagePlayerItem3.UpdateItem(saveData.item3Id);
                        storageStoredItems[i].UpdateItem(saveData.storageChestData.storageItemIds[i]);
                        storageStoredItems[i].SetAppearing();
                        break;
                }
                //itemStored = true;
                break;
            }
        }
        MenuDataManager.saveData = saveData;
        SaveSystem.Save(MenuDataManager.saveData);
    }
    public void TakeItem(int storageSlotId)
    {
        if(saveData.item1Id == -1)
        {
            storageStoredItems[storageSlotId].SpawnEndObj();
            saveData.item1Id = saveData.storageChestData.storageItemIds[storageSlotId];
            saveData.storageChestData.storageItemIds[storageSlotId] = -1;
            storagePlayerItem1.UpdateItem(saveData.item1Id);
            storagePlayerItem1.SetAppearing();
            storageStoredItems[storageSlotId].UpdateItem(saveData.storageChestData.storageItemIds[storageSlotId]);
        }
        else
        {
            if(saveData.item2Id == -1)
            {
                storageStoredItems[storageSlotId].SpawnEndObj();
                saveData.item2Id = saveData.storageChestData.storageItemIds[storageSlotId];
                saveData.storageChestData.storageItemIds[storageSlotId] = -1;
                storagePlayerItem2.UpdateItem(saveData.item2Id);
                storagePlayerItem2.SetAppearing();
                storageStoredItems[storageSlotId].UpdateItem(saveData.storageChestData.storageItemIds[storageSlotId]);
            }
            else
            {
                if(saveData.item3Id == -1)
                {
                    storageStoredItems[storageSlotId].SpawnEndObj();
                    saveData.item3Id = saveData.storageChestData.storageItemIds[storageSlotId];
                    saveData.storageChestData.storageItemIds[storageSlotId] = -1;
                    storagePlayerItem3.UpdateItem(saveData.item3Id);
                    storagePlayerItem3.SetAppearing();
                    storageStoredItems[storageSlotId].UpdateItem(saveData.storageChestData.storageItemIds[storageSlotId]);
                }
            }
        }
        MenuDataManager.saveData = saveData;
        SaveSystem.Save(MenuDataManager.saveData);
    }
    public void RemovePItem(int playerSlotId)
    {
        switch (playerSlotId)
        {
            case 1:
                if (saveData.item2Id != -1)
                {
                    if (saveData.item3Id != -1)
                    {
                        storagePlayerItem1.SpawnRemObj();
                        storagePlayerItem2.SpawnEndObj();
                        storagePlayerItem3.SpawnEndObj();
                        saveData.item1Id = saveData.item2Id; //Should make this better (maybe another function) when making Animations (Working on it rn(It'll NOT only check for visuals))
                        saveData.item2Id = saveData.item3Id;
                        saveData.item3Id = -1;
                        storagePlayerItem1.UpdateItem(saveData.item1Id);
                        storagePlayerItem2.UpdateItem(saveData.item2Id);
                        storagePlayerItem3.UpdateItem(saveData.item3Id);
                        storagePlayerItem1.SetAppearing();
                        storagePlayerItem2.SetAppearing();
                    }
                    else
                    {
                        storagePlayerItem1.SpawnRemObj();
                        storagePlayerItem2.SpawnEndObj();
                        saveData.item1Id = saveData.item2Id;
                        saveData.item2Id = -1;
                        storagePlayerItem1.UpdateItem(saveData.item1Id);
                        storagePlayerItem2.UpdateItem(saveData.item2Id);
                        storagePlayerItem1.SetAppearing();
                    }
                }
                else
                {
                    storagePlayerItem1.SpawnRemObj();
                    saveData.item1Id = -1;
                    storagePlayerItem1.UpdateItem(saveData.item1Id);
                }
                break;
            case 2:
                if (saveData.item3Id != -1)
                {
                    storagePlayerItem2.SpawnRemObj();
                    storagePlayerItem3.SpawnEndObj();
                    saveData.item2Id = saveData.item3Id; //And this one too
                    saveData.item3Id = -1;
                    storagePlayerItem2.UpdateItem(saveData.item2Id);
                    storagePlayerItem3.UpdateItem(saveData.item3Id);
                    storagePlayerItem2.SetAppearing();
                }
                else
                {
                    storagePlayerItem2.SpawnRemObj();
                    saveData.item2Id = -1;
                    storagePlayerItem2.UpdateItem(saveData.item2Id);
                }
                break;
            case 3:
                storagePlayerItem3.SpawnRemObj();
                saveData.item3Id = -1;
                storagePlayerItem3.UpdateItem(saveData.item3Id);
                break;
        }
        MenuDataManager.saveData = saveData;
        SaveSystem.Save(MenuDataManager.saveData);
    }
    public void RemoveSItem(int storageSlotId)
    {
        storageStoredItems[storageSlotId].SpawnRemObj();
        saveData.storageChestData.storageItemIds[storageSlotId] = -1;
        storageStoredItems[storageSlotId].UpdateItem(saveData.storageChestData.storageItemIds[storageSlotId]);
        MenuDataManager.saveData = saveData;
        SaveSystem.Save(MenuDataManager.saveData);
    }
    public void Tomenu()
    {
        if (!exiting)
        {
            exiting = true;
            sceneBlackScreen.SetTrigger("Fade");
            StartCoroutine(AnimeAndExit());
        }
    }
    IEnumerator AnimeAndExit() //Holy shit this was supposed to say "AnimateAndExit" what does Anime has to do with this??!!!?1/?!/!?!1
    {
        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1); //Welp, lets try this thing now...
        yield return new WaitForSeconds(0.45f);
        /*
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        */
        //SceneManager.LoadScene(1); //Fuck this shit, WHY THE HELL IS IT THAT LONG... I'll try Async loading, but it'll still be kinda long tho :(
        asyncLoad.allowSceneActivation = true;
    }
    void Update()
    {
        itemNameLen = Mathf.Lerp(itemNameLen, itemName.Length, 10f * Time.deltaTime);
        itemDescLen = Mathf.Lerp(itemDescLen, itemDesc.Length, 10f * Time.deltaTime);
        itemNameText.text = itemName.Substring(0, Mathf.RoundToInt(itemNameLen));
        itemDescText.text = itemDesc.Substring(0, Mathf.RoundToInt(itemDescLen));
    }
}
