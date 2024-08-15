using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LangSelection : MonoBehaviour
{
    [SerializeField] private bool skipSceneIfSavedataFound;
    //[SerializeField] private int nextSceneId;
    [SerializeField] QuickSceneChange sceneChanger;
    public static int setLangId = 0;
    private bool langSelected = false;
    [SerializeField] private GameObject langButtonHolder;
    void Start()
    {
        if(SaveSystem.Load() != null && skipSceneIfSavedataFound)
        {
            langButtonHolder.SetActive(false);
            langSelected = true;
            sceneChanger.InstaChange();
        }
    }

    public void SetLang(int langId)
    {
        if (langSelected) return;
        setLangId = langId;
        langSelected = true;
        sceneChanger.ChangeScene();
    }
}
