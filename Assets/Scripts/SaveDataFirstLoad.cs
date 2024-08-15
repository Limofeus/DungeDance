using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public class SaveDataFirstLoad : MonoBehaviour
{
    public SaveData startingData;
    public TextMeshPro textMeshPro;
    public MainMenuScene mainMenuScene;
    private void Awake()
    {
        Debug.Log($"Is lang init? {LocalisationSystem.IsInit()}");
        string dataPath = Application.persistentDataPath + "/DungeDanceData.aboba"; //<- TMP - TEMPERORY!!! (change later) (ALREADY DID) IN TWO SCRIPTS!!! (3/3)
        if (!File.Exists(dataPath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(dataPath, FileMode.Create);
            SaveData data = startingData;

            LocalisationSystem.SetLang(LangSelection.setLangId);
            data.settingsData.langId = LangSelection.setLangId;

            binaryFormatter.Serialize(fileStream, data);
            fileStream.Close();
            textMeshPro.text = LocalisationSystem.GetLocalizedValue("ui_mainmenu_button_newgame");
            mainMenuScene.firstTimeOpen = true;
        }
        else
        {
            SaveData saveData = SaveSystem.Load();
            LocalisationSystem.SetLang(saveData.settingsData.langId);
            if (saveData.playerName == "")
            {
                textMeshPro.text = LocalisationSystem.GetLocalizedValue("ui_mainmenu_button_newgame");
                mainMenuScene.firstTimeOpen = true;
            }
            else
            {
                textMeshPro.text = LocalisationSystem.GetLocalizedValue("ui_mainmenu_button_continue");
                mainMenuScene.firstTimeOpen = false;
            }
        }
    }
    public void EraseData()
    {
        SettingsData settingsData = SaveSystem.Load().settingsData;
        SaveData saveData = startingData;
        saveData.settingsData = settingsData;
        MenuDataManager.saveData = saveData;
        SaveSystem.Save(MenuDataManager.saveData);
    }
}
