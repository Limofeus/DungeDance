using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsWindow : MonoBehaviour
{
    public MainMenuScene mainMenuScene;
    public SaveDataFirstLoad saveDataFirstLoad;
    public Sliderd musicSlider;
    public Sliderd soundSlider;

    // Update is called once per frame
    void Start()
    {
        SaveData saveData = SaveSystem.Load();
        SpectrumManager.volumeDemultiplier = 1f / saveData.settingsData.musicVolume;
        musicSlider.UnmapAndUpdate(saveData.settingsData.musicVolume);
        soundSlider.UnmapAndUpdate(saveData.settingsData.soundVolume);
        mainMenuScene.UpdateVolume();
    }
    public void UpdateMusicVolume(float volume)
    {
        SaveData saveData = SaveSystem.Load();
        saveData.settingsData.musicVolume = volume;
        SpectrumManager.volumeDemultiplier = 1f / volume;
        MenuDataManager.saveData = saveData;
        SaveSystem.Save(MenuDataManager.saveData);
        mainMenuScene.UpdateVolume();
    }

    public void UpdateSoundVolume(float volume)
    {
        SaveData saveData = SaveSystem.Load();
        saveData.settingsData.soundVolume = volume;
        MenuDataManager.saveData = saveData;
        SaveSystem.Save(MenuDataManager.saveData);
        mainMenuScene.UpdateVolume();
    }
    public void EraseAllData()
    {
        saveDataFirstLoad.EraseData();
        mainMenuScene.ActuallyQuit();
    }
}
