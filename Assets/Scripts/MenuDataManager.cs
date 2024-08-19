using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuDataManager : MonoBehaviour
{
    public TextMeshPro playerLevelText;
    public TextMeshPro playerNameText;
    public Transform levelbarMaskTransform;
    public Transform itemInfoTab;
    public Animator itemInfoHolderAnimator;
    public ItemInfoIcon itemInfoIcon1;
    public ItemInfoIcon itemInfoIcon2;
    public ItemInfoIcon itemInfoIcon3;
    public TextMeshPro moneyCountText;
    public LevelSelector[] levelSelectors; // MAKE SURE IDS ARE SAME AS IN SAVE FILE YOU BAKA!!!!!
    public SpecialLevelSelector[] specialLevelSelectors; //ids could be whatever here (I think..)
    public int[] assignRankXps;
    public static int[] rankXps;
    [SerializeField] private MapMover mapMover;
    public bool debugSave;
    public bool debugUpdateData;
    public bool debugUpdateVisuals;
    public SaveData debugSaveData;
    public static SaveData saveData; // I SHOULD NOT FORGET TO USE THIS ONE
    public static bool dataLoaded;
    public static bool rankXpsLoaded;
    public static float uiLockValue = 0f;
    private void Awake()
    {
        if(!rankXpsLoaded)
        {
            rankXps = assignRankXps;
            rankXpsLoaded = true;
        }
    }
    private void Start()
    {
        if(!dataLoaded)
        {
            SaveData data = SaveSystem.Load();
            dataLoaded = true;
            if (data == null)
                Debug.Log("NoAboba :(");
            else
                saveData = data;
            //Debug.Log(saveData.playerLevel);
        }

        itemInfoIcon1.menuDataManager = this;
        itemInfoIcon2.menuDataManager = this;
        itemInfoIcon3.menuDataManager = this;
        mapMover.SetMapMoverPageToSaveDataPage();
        UpdateCharacter();
        UpdateItems();
        UpdateLevelSelectors();
        if (DiscordIntegrator.working)
            DiscordIntegrator.UpdateActivity("Selecting a challenge","Level: " + (saveData.playerLevel + 1) + " | Money: " + saveData.moneyAmount, "dismap", "On the map");
    }

    private void Update()
    {
        if(debugSave)
        {
            saveData = new SaveData(debugSaveData);
            debugSave = false;
            SaveSystem.Save(saveData);
        }
        if (debugUpdateData)
        {
            debugUpdateData = false;
            debugSaveData = new SaveData(saveData);
        }
        if (debugUpdateVisuals)
        {
            debugUpdateVisuals = false;
            UpdateCharacter();
            UpdateItems();
            UpdateLevelSelectors();
        }
    }
    public void UpdateCharacter()
    {
        playerLevelText.text = (saveData.playerLevel + 1).ToString();
        playerNameText.text = saveData.playerName;
        levelbarMaskTransform.localScale = new Vector3((float)(saveData.playerXp - rankXps[saveData.playerLevel])/(rankXps[saveData.playerLevel + 1] - rankXps[saveData.playerLevel]), 1f, 1f);
    }
    public void DropItemBySlotId(int slotIdToDrop)
    {
        if(saveData.item3Id == -1)
        {
            if(saveData.item2Id == -1)
            {
                saveData.item1Id = -1;
            }
            else
            {
                if (slotIdToDrop == 3)
                {
                    saveData.item2Id = -1;
                }
                if (slotIdToDrop == 2)
                {
                    saveData.item1Id = saveData.item2Id;
                    saveData.item2Id = -1;
                }
            }
        }
        else
        {
            if(slotIdToDrop == 3)
            {
                saveData.item3Id = -1;
            }
            if(slotIdToDrop == 2)
            {
                saveData.item2Id = saveData.item3Id;
                saveData.item3Id = -1;
            }
            if (slotIdToDrop == 1)
            {
                saveData.item1Id = saveData.item2Id;
                saveData.item2Id = saveData.item3Id;
                saveData.item3Id = -1;
            }
        }
        SaveSystem.Save(saveData);
        StartCoroutine(AnimatedItemUpdate());
    }
    public void UpdateItems()
    {
        if(saveData.item3Id == -1)
        {
            if(saveData.item2Id == -1)
            {
                itemInfoIcon1.LoadItemId(-1, 3);
                itemInfoIcon2.LoadItemId(-1, 2);
                itemInfoIcon3.LoadItemId(saveData.item1Id, 1);
                if (saveData.item1Id == -1)
                {
                    itemInfoTab.localPosition = new Vector3(-15f, itemInfoTab.localPosition.y, itemInfoTab.localPosition.z);
                }
                else
                {
                    itemInfoTab.localPosition = new Vector3(-10f, itemInfoTab.localPosition.y, itemInfoTab.localPosition.z);
                }
            }
            else
            {
                itemInfoTab.localPosition = new Vector3(-9f, itemInfoTab.localPosition.y, itemInfoTab.localPosition.z);
                itemInfoIcon1.LoadItemId(-1, 3);
                itemInfoIcon2.LoadItemId(saveData.item1Id, 1);
                itemInfoIcon3.LoadItemId(saveData.item2Id, 2);
            }
        }
        else
        {
            itemInfoTab.localPosition = new Vector3(-8f, itemInfoTab.localPosition.y, itemInfoTab.localPosition.z);
            itemInfoIcon1.LoadItemId(saveData.item1Id, 1);
            itemInfoIcon2.LoadItemId(saveData.item2Id, 2);
            itemInfoIcon3.LoadItemId(saveData.item3Id, 3);
        }
        moneyCountText.text = saveData.moneyAmount.ToString();
    }
    public void UpdateLevelSelectors()
    {
        for(int i = 0; i < levelSelectors.Length; i++)
        {
            levelSelectors[i].UpdateLevelData(saveData.levelDatas[i]);
            levelSelectors[i].UpdateItemUnlockment(saveData.itemUnlockDatas);
        }
        for(int i = 0; i < specialLevelSelectors.Length; i++)
        {
            specialLevelSelectors[i].UpdateLevelSelector(saveData);
        }
    }
    public IEnumerator AnimatedItemUpdate()
    {
        ItemInfoIcon.animating = true;
        ItemInfoIcon.animatingStart = true;
        itemInfoHolderAnimator.SetTrigger("HideShow");
        yield return new WaitForSeconds(0.5f);
        UpdateItems();
        ItemInfoIcon.animatingStart = false;
        yield return new WaitForSeconds(0.6f);
        ItemInfoIcon.animating = false;
    }
}
