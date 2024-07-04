using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public GameObject item1HolderGameObject;
    public GameObject item2HolderGameObject;
    public GameObject item3HolderGameObject;
    public GameObject[] itemUsePrefabs;
    [SerializeField] private int[] itemIdToPrefabMap;
    public MainManager mainManager;
    public int item1Id;
    public int item2Id;
    public int item3Id;
    public float cooldownTime;
    public bool noAutosave;
    private Animator item1HolderAnimator;
    private Animator item2HolderAnimator;
    private Animator item3HolderAnimator;
    private GameObject item1GameObject;
    private GameObject item2GameObject;
    private GameObject item3GameObject;
    private SpriteRenderer item1SpriteRenderer;
    private SpriteRenderer item2SpriteRenderer;
    private SpriteRenderer item3SpriteRenderer;
    public GameObject inventoryFullWarningPrefab;
    public Vector3 inventoryWarningSpawnPosition;
    private GameObject curretInventoryFullWarning;
    private bool inventoryFullWarningActive;
    private bool underColldown;
    private bool initialized;

    private void Start()
    {
        if (!initialized)
        {
            StuffInitialization();
        }
    }

    private void StuffInitialization()
    {
        item1HolderAnimator = item1HolderGameObject.GetComponent<Animator>();
        item1GameObject = item1HolderGameObject.transform.GetChild(0).gameObject;
        item1SpriteRenderer = item1GameObject.GetComponent<SpriteRenderer>();
        item2HolderAnimator = item2HolderGameObject.GetComponent<Animator>();
        item2GameObject = item2HolderGameObject.transform.GetChild(0).gameObject;
        item2SpriteRenderer = item2GameObject.GetComponent<SpriteRenderer>();
        item3HolderAnimator = item3HolderGameObject.GetComponent<Animator>();
        item3GameObject = item3HolderGameObject.transform.GetChild(0).gameObject;
        item3SpriteRenderer = item3GameObject.GetComponent<SpriteRenderer>();
    }
    public bool CheckForSpace()
    {
        if(item3Id == -1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SpawnInventoryWarning()
    {
        inventoryFullWarningActive = true;
        curretInventoryFullWarning = Instantiate(inventoryFullWarningPrefab, inventoryWarningSpawnPosition, Quaternion.identity);
    }
    public void ClearInventoryWarning()
    {
        inventoryFullWarningActive = false;
        curretInventoryFullWarning.GetComponent<Animator>().SetTrigger("Disappear");
        StartCoroutine(WaitForInventoryWarningAnimation());
    }

    public void AddItem(int itemId)
    {
        if (!initialized)
            StuffInitialization();
        //Debug.Log("Adding item with Id of " + itemId.ToString());
        if (item1Id == -1)
        {
            item1Id = itemId;
            //!!item1SpriteRenderer.sprite = mainManager.assignItemSprites[itemId];
            item1SpriteRenderer.sprite = ItemSpriteDictionary.itemSprites[itemId];
            item1HolderAnimator.SetTrigger("ShowItem");
        }
        else
        {
            if(item2Id == -1)
            {
                item2Id = itemId;
                item2SpriteRenderer.sprite = ItemSpriteDictionary.itemSprites[itemId];
                item2HolderAnimator.SetTrigger("ShowItem");
            }
            else
            {
                if (item3Id == -1)
                {
                    item3Id = itemId;
                    item3SpriteRenderer.sprite = ItemSpriteDictionary.itemSprites[itemId];
                    item3HolderAnimator.SetTrigger("ShowItem");
                }
                else
                {
                    Debug.Log("Not enough места"); //remakr this
                }
            }
        }
        if (!noAutosave && MenuDataManager.saveData.itemUnlockDatas[itemId] < 2)
        {
            MenuDataManager.saveData.itemUnlockDatas[itemId] = 2;
        }
        if(!noAutosave)
            SaveToMenu();
    }
    public void UseItem(int itemSlot)
    {
        if (!underColldown)
        {
            switch (itemSlot)
            {
                case 1:
                    if (item1Id != -1)
                    {
                        Debug.Log("UsingItem1");
                        ItemAction(item1Id);
                        item1HolderAnimator.SetTrigger("UseItem");
                        if (item2Id != -1)
                            item2HolderAnimator.SetTrigger("ClearItem");
                        if (item3Id != -1)
                            item3HolderAnimator.SetTrigger("ClearItem");
                        item1Id = item2Id;
                        item2Id = item3Id;
                        item3Id = -1;
                        StartCoroutine(AfterItemUse());
                    }
                    break;
                case 2:
                    if (item2Id != -1)
                    {
                        Debug.Log("UsingItem2");
                        ItemAction(item2Id);
                        item2HolderAnimator.SetTrigger("UseItem");
                        item1HolderAnimator.SetTrigger("ClearItem");
                        if (item3Id != -1)
                            item3HolderAnimator.SetTrigger("ClearItem");
                        item2Id = item3Id;
                        item3Id = -1;
                        StartCoroutine(AfterItemUse());
                    }
                    break;
                case 3:
                    if (item3Id != -1)
                    {
                        Debug.Log("UsingItem3");
                        ItemAction(item3Id);
                        item3HolderAnimator.SetTrigger("UseItem");
                        item1HolderAnimator.SetTrigger("ClearItem");
                        item2HolderAnimator.SetTrigger("ClearItem");
                        item3Id = -1;
                        StartCoroutine(AfterItemUse());
                    }
                    break;
                default:
                    break;
            }
            if(!noAutosave)
                SaveToMenu();
            if (inventoryFullWarningActive)
                ClearInventoryWarning();
        }
    }
    public void DropItem(int itemSlot)
    {
        if (!underColldown)
        {
            switch (itemSlot)
            {
                case 1:
                    if (item1Id != -1)
                    {
                        Debug.Log("DroppingItem 1");
                        item1HolderAnimator.SetTrigger("DropItem");
                        if (item2Id != -1)
                            item2HolderAnimator.SetTrigger("ClearItem");
                        if (item3Id != -1)
                            item3HolderAnimator.SetTrigger("ClearItem");
                        item1Id = item2Id;
                        item2Id = item3Id;
                        item3Id = -1;
                        StartCoroutine(AfterItemUse());
                    }
                    break;
                case 2:
                    if (item2Id != -1)
                    {
                        Debug.Log("DroppingItem 2");
                        item2HolderAnimator.SetTrigger("DropItem");
                        item1HolderAnimator.SetTrigger("ClearItem");
                        if (item3Id != -1)
                            item3HolderAnimator.SetTrigger("ClearItem");
                        item2Id = item3Id;
                        item3Id = -1;
                        StartCoroutine(AfterItemUse());
                    }
                    break;
                case 3:
                    if (item3Id != -1)
                    {
                        Debug.Log("DroppingItem 3");
                        item3HolderAnimator.SetTrigger("DropItem");
                        item1HolderAnimator.SetTrigger("ClearItem");
                        item2HolderAnimator.SetTrigger("ClearItem");
                        item3Id = -1;
                        StartCoroutine(AfterItemUse());
                    }
                    break;
                default:
                    break;
            }
            if (!noAutosave)
                SaveToMenu();
            if (inventoryFullWarningActive)
                ClearInventoryWarning();
        }
    }
    public void SaveToMenu()
    {
        MenuDataManager.saveData.item1Id = item1Id;
        MenuDataManager.saveData.item2Id = item2Id;
        MenuDataManager.saveData.item3Id = item3Id;
    }
    public IEnumerator AfterItemUse()
    {
        underColldown = true;
        yield return new WaitForSeconds(cooldownTime - 0.4f);
        if (item1Id != -1)
        {
            item1HolderAnimator.SetTrigger("ShowItem");
            item1SpriteRenderer.sprite = ItemSpriteDictionary.itemSprites[item1Id];
        }
        yield return new WaitForSeconds(0.3f);
        if (item2Id != -1)
        {
            item2HolderAnimator.SetTrigger("ShowItem");
            item2SpriteRenderer.sprite = ItemSpriteDictionary.itemSprites[item2Id];
        }
        yield return new WaitForSeconds(0.1f);
        underColldown = false;

    }
    public IEnumerator WaitForInventoryWarningAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(curretInventoryFullWarning); // This delay can cause some bugs, so maybe I will need to come back to it. (Although if scene change takes 2s while this thing only 0.5s everything should be fine)
    }
    private void ItemAction(int itemId)
    {
        if(itemId < itemIdToPrefabMap.Length)
        {
            int prefabId = itemIdToPrefabMap[itemId];
            if(prefabId >= 0)
            {
                Instantiate(itemUsePrefabs[prefabId], Vector3.zero, Quaternion.identity);
            }
            else
            {
                Debug.Log("Item use not implemented");
            }
        }
        else
        {
            Debug.Log("Id to Prefab map does not support this item");
        }
    }
}
