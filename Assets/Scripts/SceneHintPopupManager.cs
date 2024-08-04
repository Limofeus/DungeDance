using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct HintPopupPerTagInfo //This shit has to be a struct, otherwise Unity wont allow to modify it in inspector (Or maybe it will idk, too lazy to check)
{
    [SerializeField]
    public string upperText;
    [SerializeField]
    public string lowerText;
    [SerializeField]
    public Sprite sprite; // if none, then use large text, else use small one
}

public class SceneHintPopupManager : MonoBehaviour
{
    [SerializeField] private string onSceneStartTag;
    [SerializeField] private string[] tagsForHintPopups;
    [SerializeField] private HintPopupPerTagInfo[] hintPopupPerTagInfos;
    [SerializeField] private GameObject hintPopupPrefab;
    [SerializeField] private Transform hintPopupHolderTransform;
    [SerializeField] private Transform popupCanvasTransform;
    [SerializeField] private bool usePopupQueue;
    private Dictionary<string, HintPopupPerTagInfo> hintPopupDict = new Dictionary<string, HintPopupPerTagInfo> ();
    private SaveData saveData;

    private List<string> popupQueue = new List<string>();
    private bool popupShown = false; //Used for popup queue;

    [SerializeField] private CharacterLevelToHintPopup characterLevelToHintPopup;
    void Start()
    {
        for(int i = 0; i < tagsForHintPopups.Length; i++)
        {
            hintPopupDict.Add(tagsForHintPopups[i], hintPopupPerTagInfos[i]);
        }
        LoadSaveData();
        UpdateHintHolder();

        if(onSceneStartTag != "")
        {
            TryShowPopup(onSceneStartTag);
        }

        characterLevelToHintPopup?.CheckLevelsAndSendTags(this);
    }

    public void TryShowPopup(string popupTag)
    {
        if (!saveData.progressTags.ContainsTag(popupTag))
        {
            saveData.progressTags.AddTag(popupTag);

            if (!usePopupQueue)
            {
                HelpPopupVisual newPopup = CreatePopupVisual(popupCanvasTransform);
                newPopup.InitializeHint(popupTag, hintPopupDict, true);
            }
            else
            {
                if(popupQueue.Count == 0 && popupShown == false)
                {
                    HelpPopupVisual newPopup = CreatePopupVisual(popupCanvasTransform);
                    newPopup.InitializeHint(popupTag, hintPopupDict, true, this);
                    popupShown = true;
                }
                else
                {
                    popupQueue.Add(popupTag);
                }
            }


            ApplySaveData();
            UpdateHintHolder();
        }
    }

    public void HintClosed()
    {
        if(popupQueue.Count > 0)
        {
            HelpPopupVisual newPopup = CreatePopupVisual(popupCanvasTransform);
            newPopup.InitializeHint(popupQueue[0], hintPopupDict, true, this);
            popupQueue.RemoveAt(0);
        }
        else
        {
            popupShown = false;
        }
    }
    private void LoadSaveData()
    {
        if (MenuDataManager.saveData == null)
        {
            MenuDataManager.saveData = SaveSystem.Load();
        }
        saveData = MenuDataManager.saveData;
    }
    private void ApplySaveData()
    {
        //MenuDataManager.saveData = saveData;
        SaveSystem.Save(MenuDataManager.saveData);
    }
    private void UpdateHintHolder()
    {
        //Hope this will work and destroy all childerrenn
        foreach(Transform hintTransform in hintPopupHolderTransform.transform)
        {
            Destroy(hintTransform.gameObject);
        }
        for(int i = 0; i < tagsForHintPopups.Length; i++)
        {
            if (saveData.progressTags.ContainsTag(tagsForHintPopups[i]))
            {
                HelpPopupVisual newPopup = CreatePopupVisual(hintPopupHolderTransform);
                newPopup.InitializeHint(tagsForHintPopups[i], hintPopupDict);
            }
        }

    }
    private HelpPopupVisual CreatePopupVisual(Transform parentTransform)
    {
        return Instantiate(hintPopupPrefab, parentTransform).GetComponent<HelpPopupVisual>();
    }
}
