using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingsWindow : MonoBehaviour
{
    public MainMenuScene mainMenuScene;
    public SaveDataFirstLoad saveDataFirstLoad;
    public Sliderd musicSlider;
    public Sliderd soundSlider;
    [SerializeField] private TextMeshPro offsetValueTmp;

    // Update is called once per frame
    void Start()
    {
        SaveData saveData = SaveSystem.Load();
        SpectrumManager.volumeDemultiplier = 1f / saveData.settingsData.musicVolume;
        musicSlider.UnmapAndUpdate(saveData.settingsData.musicVolume);
        soundSlider.UnmapAndUpdate(saveData.settingsData.soundVolume);
        UpdateOffsetTMP(saveData.settingsData.offsetValue);
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
    public void OffsetChangeR1()
    {
        ChangeOffset(5f);
    }
    public void OffsetChangeR2()
    {
        ChangeOffset(50f);
    }
    public void OffsetChangeL1()
    {
        ChangeOffset(-5f);
    }
    public void OffsetChangeL2()
    {
        ChangeOffset(-50f);
    }
    private void ChangeOffset(float changeValue)
    {
        SaveData saveData = SaveSystem.Load();
        saveData.settingsData.offsetValue = Mathf.Clamp(saveData.settingsData.offsetValue + changeValue, -800f, 800f);
        MenuDataManager.saveData = saveData;
        SaveSystem.Save(MenuDataManager.saveData);
        UpdateOffsetTMP(saveData.settingsData.offsetValue);
    }
    private void UpdateOffsetTMP(float tmpOffset)
    {
        offsetValueTmp.text = ((int)tmpOffset).ToString();
    }
    public void EraseAllData()
    {
        saveDataFirstLoad.EraseData();
        mainMenuScene.ActuallyQuit();
    }
}
