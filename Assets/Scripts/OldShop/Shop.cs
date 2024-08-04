using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private ShopCell[] _itemCells;
        [SerializeField] private ItemsUpdateButton _updateButton;
        [SerializeField] private ItemDescription _itemDescription;
        [SerializeField, Range(1, 5)] private int _openCellsCount = 3;
        private SaveData _saveData;
        private int _updatePrice = 1000;
        private bool _isDiscount = true;
        public event Action ItemBought;
        public event Action<int> MoneyChanged;

        private void OnEnable()
        {
            UpdateSaveData();
            _updateButton.OnClick += TryUpdateItemList;
            _updateButton.OnSelect += UpdateButtonSelected;

            foreach (var item in _itemCells)
            {
                item.OnSelect += ItemSelected;
                item.OnClick += TryBuyItem;
            }
        }

        private void OnDisable()
        {
            _updateButton.OnClick -= TryUpdateItemList;
            foreach (var item in _itemCells)
            {
                item.OnSelect -= ItemSelected;
                item.OnClick -= TryBuyItem;
            }
            Save();
        }
        private void Start()
        {
            // OLD SHOP!!!! DEPRICATED!!!!
            /*
            if (_saveData.shopMustBeChanged)
            {
                UpdateItemList();
            }
            MoneyChanged?.Invoke(_saveData.moneyAmount);
            */
        }

        private void ItemSelected(int cellId)
        {
            ChangeDescription(cellId);
        }        
        
        private void UpdateButtonSelected()
        {
            ChangeDescription();
        }

        private void ChangeDescription(int cellId)
        {
            var id = _itemCells[cellId].ItemID;
            _itemDescription.Init(id);
        }

        private void ChangeDescription()
        {
            _itemDescription.Init("reroll_name", "reroll_desk");
        }


        private void TryBuyItem(int cellId)
        {
            var price = _itemCells[cellId].Price;

            if (_saveData.moneyAmount < price)
            {
                var message = LocalisationSystem.GetLocalizedValue("shop_warning_noMoney");
                DisplayMessage(message);
                Debug.Log("noMoney");
                return;
            }

            if (CheckForStorage() < 0 && _saveData.item3Id >= 0 && _saveData.item2Id >= 0 && _saveData.item1Id >= 0)
            {
                var message = LocalisationSystem.GetLocalizedValue("shop_warning_noSpace");
                DisplayMessage(message);
                Debug.Log("NoSpace");
                return;
            }

            if (_saveData.item1Id < -1)
                _saveData.storageChestData.storageItemIds[_saveData.item1Id] = _itemCells[cellId].ItemID;
            else if (_saveData.item2Id < -1)
                _saveData.storageChestData.storageItemIds[_saveData.item2Id] = _itemCells[cellId].ItemID;
            else if (_saveData.item3Id < -1)
                _saveData.storageChestData.storageItemIds[_saveData.item3Id] = _itemCells[cellId].ItemID;
            else
                _saveData.storageChestData.storageItemIds[CheckForStorage()] = _itemCells[cellId].ItemID;

            _saveData.moneyAmount -= price;
            _itemCells[cellId].IsSold();
            Save();
            ItemBought?.Invoke();
            MoneyChanged?.Invoke(_saveData.moneyAmount);
        }

        private void UpdateSaveData()
        {
            if (MenuDataManager.saveData == null)
            {
                MenuDataManager.saveData = SaveSystem.Load();
            }
            _saveData = MenuDataManager.saveData;
        }
        private void Save()
        {
            //MenuDataManager.saveData = saveData;
            SaveSystem.Save(MenuDataManager.saveData);
        }

        private void TryUpdateItemList()
        {
            if (_saveData.moneyAmount < _updatePrice)
                return;
            UpdateItemList();
        }

        private void UpdateItemList()
        {
            var itemIDs = GetsAvailableItems();

            var itemID = GetRandomItemFrom(itemIDs);
            _itemCells[0].Init(itemID, _isDiscount);
            itemIDs.Remove(itemID);

            for (int i = 1; i < _openCellsCount; i++)
            {
                itemID = GetRandomItemFrom(itemIDs);
                _itemCells[i].Init(itemID);
                itemIDs.Remove(itemID);
            }
            for (int i = _openCellsCount; i < _itemCells.Length; i++)
            {
                _itemCells[i].IsClosed();
            }
        }

        private List<int> GetsAvailableItems()
        {
            var itemIDs = new List<int>();
            for (int i = 0; i < _saveData.itemUnlockDatas.Length; i++)
            {
                var itemUnlockStatus = _saveData.itemUnlockDatas[i];
                switch (itemUnlockStatus)
                {
                    case 0:
                        break;
                    case 1:
                        itemIDs.Add(i);
                        itemIDs.Add(i);
                        break;
                    case 2:
                        itemIDs.Add(i);
                        break;
                    default:
                        break;
                }
            }
            return itemIDs;
        }

        private int CheckForStorage()
        {
            for (int i = 0; i < (5 * (2 + _saveData.storageChestData.storageChestLevel)); i++)
            {
                Debug.Log($"Checking slot {i}, id: {_saveData.storageChestData.storageItemIds[i]}");
                if (_saveData.storageChestData.storageItemIds[i] < 0)
                {
                    return i;
                }
            }
            return -1;
        }

        private T GetRandomItemFrom<T>(List<T> list)
        {
            var id = UnityEngine.Random.Range(0, list.Count);
            return list[id];
        }

        private void DisplayMessage(string message)
        {

        }
    }
}