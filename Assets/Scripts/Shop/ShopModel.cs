using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopModel : MonoBehaviour
{
    [SerializeField] private ShopSlotButton[] shopSlotButtons;
    [SerializeField] private ShopKeepVisual shopKeepVisual;
    [SerializeField] private ItemInfoVisual itemInfoVisual;
    [SerializeField] private GeneralizedItemIcon[] playerInventoryItemIcons;
    [SerializeField] private TextMeshPro moneyTmpro;
    [SerializeField] private TextMeshPro investPriceLabel;
    [SerializeField] private RerollButtonVisual rerollButtonVisual;
    [SerializeField] private LerpScaleVisibilityToggle noInvSpaceWarning;

    [SerializeField] private int[] upgradePrices; //0 - to Lv2., 1 - to Lv3., 2 - to Lv4., 3 - to Lv5., etc.

    [SerializeField] private float itemRestockTime;
    [SerializeField] private int rerollPrice;

    //[SerializeField] private ShopItemData[] shopItems;

    private SaveData saveData;
    void Start()
    {
        if (MenuDataManager.saveData == null)
        {
            MenuDataManager.saveData = SaveSystem.Load();
        }
        saveData = MenuDataManager.saveData;
        if(saveData.shopData.shopLevel == 0)
        {
            Debug.Log("FIRST TIME IN THE SHOP!!!");
            saveData.shopData.shopLevel = 1;
            RerollAllShopItems();
            for(int i = 3; i < 5; i++)
            {
                saveData.shopData.shopItemDatas[i].shopItemId = -2;
            }
            ApplySaveData();
        }
        UpdatePlayerInventoryItems();
        UpdateMoneyLabel();
        ShopLevelBasedUpdate();
    }
    void Update()
    {
        
    }
    private void UpdatePlayerInventoryItems()
    {
        if (saveData.item1Id >= 0)
        {
            playerInventoryItemIcons[0].UpdateItem(saveData.item1Id, 0);
            playerInventoryItemIcons[0].HideShowItem(true);
        }
        else
            playerInventoryItemIcons[0].HideShowItem(false);
        if (saveData.item2Id >= 0)
        {
            playerInventoryItemIcons[1].UpdateItem(saveData.item2Id, 1);
            playerInventoryItemIcons[1].HideShowItem(true);
        }
        else
            playerInventoryItemIcons[1].HideShowItem(false);
        if (saveData.item3Id >= 0)
        {
            playerInventoryItemIcons[2].UpdateItem(saveData.item3Id, 2);
            playerInventoryItemIcons[2].HideShowItem(true);
        }
        else
            playerInventoryItemIcons[2].HideShowItem(false);
    }
    private void UpdateMoneyLabel()
    {
        moneyTmpro.text = saveData.moneyAmount.ToString();
    }
    private void ShopLevelBasedUpdate()
    {
        noInvSpaceWarning.SetVisibility(!PlayerHasFreeSpace());
        int slotsToUnlock = 3;
        if(saveData.shopData.shopLevel >= 2) //Hardcoding aint calld HARDcoding for a reason.. What a mess.......
        {
            rerollButtonVisual.SetButtonUnlockment(saveData.shopData.shopLevel >= 3);
            slotsToUnlock++;
            if(saveData.shopData.shopLevel >= 4)
            {
                slotsToUnlock++;
            }
        }

        if(saveData.shopData.shopLevel - 1 < upgradePrices.Length)
        {
            investPriceLabel.text = upgradePrices[saveData.shopData.shopLevel - 1].ToString();
        }
        else
        {
            investPriceLabel.text = LocalisationSystem.GetLocalizedValue("shop_invest_max_level");
        }

        //Updating each slot
        bool itemIdsChangedFlag = false;
        for(int i = 0; i < shopSlotButtons.Length; i++)
        {
            if(i < slotsToUnlock)
            {
                int itemId = saveData.shopData.shopItemDatas[i].shopItemId;
                //Reroll slot if it was visited with no unlocked items!
                if(itemId == -3 || itemId == -2)
                {
                    itemId = GetRandomShopItem();
                    saveData.shopData.shopItemDatas[i].shopItemId = itemId;
                    itemIdsChangedFlag = true;
                }
                UpdateItemSlotButton(i, itemId, saveData.shopData.shopItemDatas[i].timeTillItemRestock);
            }
            else
            {
                shopSlotButtons[i].SetSlotState(ShopSlotButton.SlotState.Locked);
                shopSlotButtons[i].SetSelectability(false);
            }
        }
        if (itemIdsChangedFlag)
            ApplySaveData();
    }
    private void UpdateItemSlotButton(int buttonId, int itemId, float itemRestockTime = -1f)
    {
        if(itemId >= 0)
        { //NormalUpdate
            shopSlotButtons[buttonId].SetSlotState(ShopSlotButton.SlotState.ItemAvailable);
            Debug.Log($"TryingToCalculate price of item {itemId}");
            int itemPrice = ItemPriceCalculator.CalculateItemPrice(itemId, saveData);
            bool canBuyItem = saveData.moneyAmount >= itemPrice;

            shopSlotButtons[buttonId].SetItemData(itemId, itemPrice, canBuyItem);
            shopSlotButtons[buttonId].SetSelectability(true);
        }
        else if(itemId == -1)   
        {
            if(itemRestockTime >= 0f)
                shopSlotButtons[buttonId].restockTime = itemRestockTime;
            shopSlotButtons[buttonId].SetSlotState(ShopSlotButton.SlotState.ItemTimeout);
            shopSlotButtons[buttonId].SetSelectability(false);
        }
    }
    private void ApplySaveData()
    {
        MenuDataManager.saveData = saveData;
        SaveSystem.Save(MenuDataManager.saveData);
    }

    private void RerollAllShopItems()
    {
        List<int> possibleShopItemIds = new List<int>();
        for(int i = 0; i < saveData.itemUnlockDatas.Length; i++)
        {
            if(saveData.itemUnlockDatas[i] == 1)
            {
                possibleShopItemIds.Add(i);
                possibleShopItemIds.Add(i);
            }
            else if(saveData.itemUnlockDatas[i] == 2)
            {
                possibleShopItemIds.Add(i);
            }
        }
        List<int> rerolledItems = new List<int>();
        for (int i = 0; i < saveData.shopData.shopItemDatas.Length; i++)
        {
            if (possibleShopItemIds.Count > 0)
                rerolledItems.Add(possibleShopItemIds[Random.Range(0, possibleShopItemIds.Count)]);
            else
                rerolledItems.Add(-3);
        }
        for(int i = 0; i < saveData.shopData.shopItemDatas.Length; i++)
        {
            if(saveData.shopData.shopItemDatas[i].shopItemId != -2)
                saveData.shopData.shopItemDatas[i].shopItemId = rerolledItems[i];
        }
    }

    private int GetRandomShopItem()
    {
        List<int> possibleShopItemIds = new List<int>();
        for (int i = 0; i < saveData.itemUnlockDatas.Length; i++)
        {
            if (saveData.itemUnlockDatas[i] == 1)
            {
                possibleShopItemIds.Add(i);
                possibleShopItemIds.Add(i);
            }
            else if (saveData.itemUnlockDatas[i] == 2)
            {
                possibleShopItemIds.Add(i);
            }
        }
        if (possibleShopItemIds.Count > 0)
            return possibleShopItemIds[Random.Range(0, possibleShopItemIds.Count)];
        else
            return -3;
    }
    private int CheckForStorage() //HEY, I know that I just copied this thing from the cloning altar scene while I was supposed to make a utility class or something and use it to get nex unocuppied slot in chest...
    {
        for (int i = 0; i < (5 * (2 + saveData.storageChestData.storageChestLevel)); i++)
        {
            Debug.Log($"Checking slot {i}, id: {saveData.storageChestData.storageItemIds[i]}");
            if (saveData.storageChestData.storageItemIds[i] < 0)
            {
                return i;
            }
        }
        return -1;
    }
    private bool PlayerHasFreeSpace()
    {
        return (saveData.item1Id == -1 || saveData.item2Id == -1 || saveData.item2Id == -1 || CheckForStorage() != -1);
    }
    private void BuyItem(int itemButId)
    {
        int boughtItemId = saveData.shopData.shopItemDatas[itemButId].shopItemId;
        //ADD ITEM TO INV/CHEST!
        if (saveData.item1Id == -1)
        {
            saveData.item1Id = boughtItemId;
        }
        else if(saveData.item2Id == -1)
        {
            saveData.item2Id = boughtItemId;
        }
        else if(saveData.item3Id == -1)
        {
            saveData.item3Id = boughtItemId;
        }
        else if(CheckForStorage() != -1)
        {
            saveData.storageChestData.storageItemIds[CheckForStorage()] = boughtItemId;
        }
        else
        {
            //мер леярю!!!!!
        }
        //Subtract form player money!!!
        saveData.moneyAmount -= ItemPriceCalculator.CalculateItemPrice(boughtItemId, saveData); //That's also NOT THE BEST way of doing it since price might change with players progress... idk, maybe I wont even implement such a feature
        //Account for Unlock datas
        saveData.itemUnlockDatas[boughtItemId] = 2;
        for (int i = 0; i < shopSlotButtons.Length; i++)
        {
            if (i == itemButId)
                UpdateItemSlotButton(itemButId, -1, itemRestockTime);
            else
                UpdateItemSlotButton(i, saveData.shopData.shopItemDatas[i].shopItemId);
        }
        saveData.shopData.shopItemDatas[itemButId].shopItemId = -1;
        saveData.shopData.shopItemDatas[itemButId].timeTillItemRestock = itemRestockTime;
        ApplySaveData();
        UpdateMoneyLabel();
        UpdatePlayerInventoryItems();
        noInvSpaceWarning.SetVisibility(!PlayerHasFreeSpace());
    }
    public void TryRerollItems()
    {
        if(saveData.moneyAmount >= rerollPrice)
        {
            saveData.moneyAmount -= rerollPrice;
            for (int i = 0; i < shopSlotButtons.Length; i++)
            {
                saveData.shopData.shopItemDatas[i].timeTillItemRestock = 0f;
            }
            RerollAllShopItems();
            ApplySaveData();
            UpdateMoneyLabel();
            ShopLevelBasedUpdate();
        }
    }
    public void TryUpgradeShop()
    {
        if(upgradePrices.Length >= saveData.shopData.shopLevel)
        {
            if(saveData.moneyAmount >= upgradePrices[saveData.shopData.shopLevel - 1])
            {
                Debug.Log("UPGRADING SHOP");
                saveData.moneyAmount -= upgradePrices[saveData.shopData.shopLevel - 1];
                saveData.shopData.shopLevel++;
                for (int i = 0; i < shopSlotButtons.Length; i++)
                {
                    saveData.shopData.shopItemDatas[i].timeTillItemRestock = shopSlotButtons[i].restockTime;
                }
                ShopLevelBasedUpdate();
                ApplySaveData();
                UpdateMoneyLabel();
            }
        }
    }
    public void OnItemButtonSelect(int buttonId)
    {
        shopKeepVisual.UpdateSelectedItem(buttonId);
        if(saveData.shopData.shopItemDatas[buttonId].shopItemId >= 0)
            itemInfoVisual.SetItemInfo(saveData.shopData.shopItemDatas[buttonId].shopItemId);
    }

    public void OnItemButtonDeselected(int buttonId)
    {
        shopKeepVisual.UpdateSelectedItem(-1);
    }

    public void OnItemButtonClick(int buttonId)
    {
        int itemPrice = ItemPriceCalculator.CalculateItemPrice(saveData.shopData.shopItemDatas[buttonId].shopItemId, saveData);
        if (itemPrice <= saveData.moneyAmount && PlayerHasFreeSpace())
        {
            BuyItem(buttonId);
            shopKeepVisual.UpdateSelectedItem(-2);
        }
    }
    public void RestockItem(int buttonId)
    {
        int newItemId = GetRandomShopItem();
        saveData.shopData.shopItemDatas[buttonId].shopItemId = newItemId;
        saveData.shopData.shopItemDatas[buttonId].timeTillItemRestock = 0f;
        UpdateItemSlotButton(buttonId, newItemId);
        ApplySaveData();
    }

    public void StartOffSceneRestockCounter()
    {
        for(int i = 0; i < shopSlotButtons.Length; i++)
        {
            saveData.shopData.shopItemDatas[i].timeTillItemRestock = shopSlotButtons[i].restockTime;
        }
        ApplySaveData();
        ShopItemTimeoutTracker.LoadCountedTimeouts();
    }
}
