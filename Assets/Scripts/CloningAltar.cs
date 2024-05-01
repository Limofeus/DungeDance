using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;
using TMPro;

public class CloningAltar : MonoBehaviour
{
    [SerializeField] private GeneralizedItemIcon onAltarItem;
    [SerializeField] private GeneralizedItemIcon[] playerItemIcons;
    [SerializeField] private CloneAltarButton cloneAltarButton;
    [SerializeField] private TextMeshPro playerMoneyLabel;
    [SerializeField] private TextMeshPro cloneHintText;
    private int selectedItemSlot = 0; // (STARTS FROM 1!!) 0 - none, 1 - 1, 2 - 2, etc.
    private int selectedItemId = -1;
    public SaveData saveData;
    void Start()
    {
        if (MenuDataManager.saveData != null)
            saveData = new SaveData(MenuDataManager.saveData);
        else
            saveData = SaveSystem.Load();

        onAltarItem.HideShowItem(false);
        UpdateItemsFromSaveData();
        UpdatePlayerMoneyLabel();

        playerItemIcons[0].UpdateItem(saveData.item1Id, 0, "À Ã - ‚˚·‡Ú¸");
    }
    public void TestMethod(int id)
    {
        Debug.Log("Recieved event");
        playerItemIcons[id].HideShowItem(!playerItemIcons[id].isShown);
        cloneAltarButton.UpdateCloneButton(saveData.item1Id, "222");
        cloneAltarButton.ChangeMode(CloneAltarButton.InteractMode.Normal);
    }
    public void TrySelectItem(int itemSlot)
    {
        if (selectedItemSlot == 0)
        {
            SelectItem(itemSlot);
        }
        else
        {
            if(selectedItemSlot == itemSlot)
            {
                TryDeselectItem();
            }
            else
            {
                TryDeselectItem();
                SelectItem(itemSlot);
            }
        }
        UpdateCloneButton();
    }
    private void SelectItem(int itemSlot)
    {
        switch (itemSlot)
        {
            case 1:
                selectedItemId = saveData.item1Id;
                break;
            case 2:
                selectedItemId = saveData.item2Id;
                break;
            case 3:
                selectedItemId = saveData.item3Id;
                break;
        }

        if (selectedItemId == -1) return;

        onAltarItem.UpdateItem(selectedItemId);
        onAltarItem.HideShowItem();
        playerItemIcons[itemSlot - 1].HideShowItem(false);
        selectedItemSlot = itemSlot;
    }
    private void UpdateCloneButton()
    {
        if(selectedItemId >= 0)
        {
            int clonePrice = CalcClonePrice(ItemSpriteDictionary.itemRarity[selectedItemId]);

            cloneAltarButton.UpdateCloneButton(selectedItemId, clonePrice.ToString());

            if(clonePrice <= saveData.moneyAmount && CheckForStorage() >= 0)
            {
                cloneHintText.text = " ÀŒÕ»–Œ¬¿“‹ œ–≈ƒÃ≈“??!!!?71717!??!?";
                cloneAltarButton.ChangeMode(CloneAltarButton.InteractMode.Normal);
            }
            else
            {
                if(CheckForStorage() >= 0)
                    cloneHintText.text = "Õ≈“ ƒ≈Õﬂ ";
                else
                    cloneHintText.text = "Õ≈“ Ã≈—“¿ ¬ —”Õƒ” ≈";
                cloneAltarButton.ChangeMode(CloneAltarButton.InteractMode.Locked);
            }
        }
        else
        {
            cloneAltarButton.ChangeMode(CloneAltarButton.InteractMode.Hidden);
        }
    }
    public void TryDeselectItem()
    {
        if(selectedItemSlot != 0)
        {
            onAltarItem.HideShowItem(false);
            playerItemIcons[selectedItemSlot - 1].HideShowItem();
            selectedItemSlot = 0;
            selectedItemId = -1;

            cloneHintText.text = "";
        }
        UpdateCloneButton();
    }

    public void TryCloneItem()
    {
        int clonePrice = CalcClonePrice(ItemSpriteDictionary.itemRarity[selectedItemId]);

        if (clonePrice <= saveData.moneyAmount && CheckForStorage() >= 0)
        {
            saveData.moneyAmount -= clonePrice;
            saveData.storageChestData.storageItemIds[CheckForStorage()] = selectedItemId;
            Debug.Log("Item cloned");
            UpdateCloneButton();
            UpdatePlayerMoneyLabel();
            ApplySaveData();
        }
    }
    private int CalcClonePrice(int itemRarity)
    {
        //CHANGE PRICE FUNTION
        return 200 * (itemRarity + 1);
    }
    private int CheckForStorage()
    {
        for(int i = 0; i < (5 * (2 + saveData.storageChestData.storageChestLevel)); i++)
        {
            Debug.Log($"Checking slot {i}, id: {saveData.storageChestData.storageItemIds[i]}");
            if(saveData.storageChestData.storageItemIds[i] < 0)
            {
                return i;
            }
        }
        return -1;
    }
    private void ApplySaveData()
    {
        MenuDataManager.saveData = saveData;
        SaveSystem.Save(MenuDataManager.saveData);
    }
    private void UpdateItemsFromSaveData()
    {
        if (saveData.item1Id >= 0)
            playerItemIcons[0].UpdateItem(saveData.item1Id, 0, "À Ã - ‚˚·‡Ú¸");
        else
            playerItemIcons[0].gameObject.SetActive(false);

        if (saveData.item2Id >= 0)
            playerItemIcons[1].UpdateItem(saveData.item2Id, 1, "À Ã - ‚˚·‡Ú¸");
        else
            playerItemIcons[1].gameObject.SetActive(false);

        if (saveData.item3Id >= 0)
            playerItemIcons[2].UpdateItem(saveData.item3Id, 2, "À Ã - ‚˚·‡Ú¸");
        else
            playerItemIcons[2].gameObject.SetActive(false);
    }
    private void UpdatePlayerMoneyLabel()
    {
        playerMoneyLabel.text = saveData.moneyAmount.ToString();
    }
}
