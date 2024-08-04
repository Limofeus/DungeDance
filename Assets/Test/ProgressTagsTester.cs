using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTagsTester : MonoBehaviour
{
    public string tagName;
    [SerializeField] private SceneHintPopupManager sceneHintPopupManager;
    [SerializeField] private bool activateHintPopup;
    public bool addTag;
    public bool removeTag;
    public bool checkForTag;
    public bool checkForTagResult;
    public bool loadsData;
    public bool savesData;
    public SaveData saveData;
    void Update()
    {
        if (loadsData) { LoadSaveData(); loadsData = false; }
        if (savesData) { ApplySaveData(); savesData = false; }
        if (addTag) { saveData.progressTags.AddTag(tagName); addTag = false;  }
        if (removeTag) { saveData.progressTags.RemoveTag(tagName); removeTag = false; }
        if (checkForTag) { checkForTagResult = saveData.progressTags.ContainsTag(tagName); checkForTag = false; }
        if (activateHintPopup) { sceneHintPopupManager.TryShowPopup(tagName); activateHintPopup = false; }
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
}
