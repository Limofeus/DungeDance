using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SpecialLevelSelector : MonoBehaviour
{
    [SerializeField] private MapLevelManager mapLevelManager;
    [SerializeField] private int specialSceneid;
    [SerializeField] private string specialKnownTag;
    [SerializeField] private bool addTagOnclick;
    [SerializeField] private string actualLevelname;
    [SerializeField] private string actualLevelDesc;
    [SerializeField] private TextMeshPro nameTextMeshPro;
    [SerializeField] private TextMeshPro descTextMeshPro;
    [SerializeField] private SpriteRenderer leveliconSr;
    [SerializeField] private SpriteRenderer QuestionMarkSr;
    [SerializeField] private Color levelUnknownColor;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip mapPlayerMusic;
    [SerializeField] private float mapMusicPlayerTime;
    private bool selected = false;
    private bool levelKnown = false;
    [SerializeField] private float uiLockValueCheck = 0f;

    private void Update()
    {
        if ((Input.GetMouseButtonDown(0)/* || (Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Began)*/) && selected)
        {
            int[] itemUnlockDatas = MenuDataManager.saveData.itemUnlockDatas; //Welp, this was the problem all along

            if (addTagOnclick)
            {
                if (!MenuDataManager.saveData.progressTags.ContainsTag(specialKnownTag))
                {
                    MenuDataManager.saveData.progressTags.AddTag(specialKnownTag);
                    SaveSystem.Save(MenuDataManager.saveData);
                }
            }
            //Discord integration here (I SHOULD MAKE THIS WORK LATER BECAUSE THIS WILL BE COOL THEN AHHAHSGHGASGDHHASDAHGS)
            /*
            if (DiscordIntegrator.working)
                DiscordIntegrator.UpdateActivity("DANCEtroing those monsters!", "On " + levelName + " stage!", "disbg" + backgroundId.ToString(), "DUNGE DACE!", "dif" + Mathf.Floor(difficulty).ToString(), "STAGE DIFFICULTY! O_O");
            */
            mapLevelManager.LoadLevel(specialSceneid);
        }
    }
    public void UpdateLevelSelector(SaveData saveData)
    {
        if(specialKnownTag == "" || saveData.progressTags.ContainsTag(specialKnownTag))
        {
            levelKnown = true;
            nameTextMeshPro.text = LocalisationSystem.GetLocalizedValue(actualLevelname);
            descTextMeshPro.text = LocalisationSystem.GetLocalizedValue(actualLevelDesc);
            QuestionMarkSr.gameObject.SetActive(false);
        }
        else
        {
            nameTextMeshPro.text = "???";
            descTextMeshPro.text = "???";
            leveliconSr.color = levelUnknownColor;
        }
    }
    private void OnMouseEnter()
    {
        if (MenuDataManager.uiLockValue > uiLockValueCheck) return;
        //Debug.Log($"MDMuiLock: {MenuDataManager.uiLockValue}, CurrSLS: {gameObject.name}, ButnLock: {uiLockValueCheck}");
        animator.SetBool("MouseOver", true);
        if(mapPlayerMusic != null && levelKnown)
            MapMusicPlayer.mapMusicPlayer.MouseOverTrack(true, mapPlayerMusic, mapMusicPlayerTime);
        selected = true;
    }
    private void OnMouseExit()
    {
        if (MenuDataManager.uiLockValue > uiLockValueCheck) return;
        animator.SetBool("MouseOver", false);
        if (mapPlayerMusic != null && levelKnown)
            MapMusicPlayer.mapMusicPlayer.MouseOverTrack(false, mapPlayerMusic, mapMusicPlayerTime);
        selected = false;
    }
}
