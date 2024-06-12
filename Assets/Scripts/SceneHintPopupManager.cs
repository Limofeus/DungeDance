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
    private Dictionary<string, HintPopupPerTagInfo> hintPopupDict = new Dictionary<string, HintPopupPerTagInfo> ();
    private SaveData saveData;
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
    }

    public void TryShowPopup(string popupTag)
    {
        if (!saveData.progressTags.ContainsTag(popupTag))
        {
            saveData.progressTags.AddTag(popupTag);

            HelpPopupVisual newPopup = CreatePopupVisual(popupCanvasTransform);
            newPopup.InitializeHint(popupTag, hintPopupDict, true);


            ApplySaveData();
            UpdateHintHolder();
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
