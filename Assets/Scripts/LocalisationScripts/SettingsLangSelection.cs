using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsLangSelection : MonoBehaviour, NumSelectable
{
    public void OnNumSelected(int numSelected)
    {
        LocalisationSystem.SetLang(numSelected);
        LocalisationSystem.Init();

        SaveData saveData = SaveSystem.Load();
        saveData.settingsData.langId = numSelected;
        SaveSystem.Save(MenuDataManager.saveData);


        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
