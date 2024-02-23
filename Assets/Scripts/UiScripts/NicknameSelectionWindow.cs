using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NicknameSelectionWindow : MonoBehaviour
{
    public bool enabledd;
    public TextMeshPro nameText;
    public TextMeshPro errorText;
    public Transform shakeSmoother;
    public MainMenuScene mainMenuScene;
    public string nickname;
    public Color defaultCol;
    public Color defaultAlphaCol;
    public Color symbolAddedCol;
    public Color invalidSymbolCol;
    public Color charLimitCol;
    public Color symboldeleteCol;
    private Array keyCodes = Enum.GetValues(typeof(KeyCode));
    void Start()
    {
        keyCodes = Enum.GetValues(typeof(KeyCode));
        Debug.Log(Application.persistentDataPath);
    }

    void Update()
    {
        if (Input.anyKeyDown && enabledd)
        {
            Debug.Log("Hmmmm");
            foreach (KeyCode keyCode in keyCodes) //Now. This is stupid as hell, BUT... I mean... if it works - it works
            {
                if (keyCode != KeyCode.Mouse0 && keyCode != KeyCode.Mouse1 && Input.GetKeyDown(keyCode))
                {
                    if(keyCode != KeyCode.Backspace && keyCode != KeyCode.Delete)
                    {
                        string keyCodeStr = keyCode.ToString();
                        if(keyCodeStr.Length == 1)
                        {
                            if(nickname.Length < 13)
                            {
                                if (nickname.Length < 1)
                                {
                                    nickname += keyCodeStr;
                                }
                                else
                                {
                                    nickname += keyCodeStr.ToLower();
                                }
                                //Debug.Log("Character added");
                                nameText.color = symbolAddedCol;
                                //errorText.color = symbolAddedCol;
                                RandomPush(0.4f);
                            }
                            else
                            {
                                //Debug.Log("Character limit");
                                nameText.color = charLimitCol;
                                errorText.color = charLimitCol;
                                errorText.text = LocalisationSystem.GetLocalizedValue("ui_mainmenu_choose_name_character_limit");
                                RandomPush(1.2f);
                            }
                        }
                        else
                        {
                            //Debug.Log("Invalid symbol");
                            nameText.color = invalidSymbolCol;
                            errorText.color = invalidSymbolCol;
                            errorText.text = LocalisationSystem.GetLocalizedValue("ui_mainmenu_choose_name_invalid_symbol");
                            RandomPush(0.7f);
                        }
                    }
                    else
                    {
                        if(nickname.Length > 0)
                        {
                            //Debug.Log("Removing letter");
                            nickname = nickname.Remove(nickname.Length - 1);
                            nameText.color = symboldeleteCol;
                            //errorText.color = symboldeleteCol;
                            RandomPush(1f);
                        }
                    }
                    nameText.text = nickname;
                    break;
                }
            }
        }

        nameText.color = Color.Lerp(nameText.color, defaultCol, 2f * Time.deltaTime);
        errorText.color = Color.Lerp(errorText.color, defaultAlphaCol, Time.deltaTime);
        shakeSmoother.localPosition = Vector3.Lerp(shakeSmoother.localPosition, Vector3.zero, 15f * Time.deltaTime);
        shakeSmoother.localRotation = Quaternion.Lerp(shakeSmoother.localRotation, Quaternion.identity, 15f * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, shakeSmoother.position, 10f * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, shakeSmoother.rotation, 10f * Time.deltaTime);
    }

    public void StartG()
    {
        if (enabledd)
        {
            if (nickname.Length > 0)
            {
                SaveData saveData = SaveSystem.Load();
                saveData.playerName = nickname;
                MenuDataManager.saveData = saveData;
                SaveSystem.Save(MenuDataManager.saveData);
                mainMenuScene.NewGameStart();
            }
            else
            {
                SaveData saveData = SaveSystem.Load();
                saveData.playerName = "Mr.Noname";
                MenuDataManager.saveData = saveData;
                SaveSystem.Save(MenuDataManager.saveData);
                mainMenuScene.NewGameStart();
            }
        }
    }
    private void RandomPush(float pushStrength)
    {
        shakeSmoother.localPosition = new Vector3(UnityEngine.Random.Range(-0.4f, 0.4f) * pushStrength, UnityEngine.Random.Range(-0.4f, 0.4f) * pushStrength, 0f);
        shakeSmoother.localRotation = Quaternion.Euler(Vector3.forward * UnityEngine.Random.Range(-10f, 10f) * pushStrength);
    }
}
