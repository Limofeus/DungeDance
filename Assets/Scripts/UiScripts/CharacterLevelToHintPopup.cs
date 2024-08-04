using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CharacterLevelToHintPopup : MonoBehaviour
{
    [SerializeField]
    [System.Serializable]
    private class CharLevToPopup
    {
        [SerializeField]
        public int charLevelToCheck;
        [SerializeField]
        public string[] popupTags;
    }

    [SerializeField]
    private CharLevToPopup[] popups;

    public void CheckLevelsAndSendTags(SceneHintPopupManager sceneHintPopupManager) 
    {
        int charLevel = MenuDataManager.saveData.playerLevel;
        for(int i = 0; i < popups.Length; i++)
        {
            if(charLevel > popups[i].charLevelToCheck)
            {
                foreach(string popupTag in popups[i].popupTags)
                {
                    TryFirePopup(popupTag, sceneHintPopupManager);
                }
            }
        }
    }

    private void TryFirePopup(string popupTag, SceneHintPopupManager sceneHintPopupManager)
    {
        if (!MenuDataManager.saveData.progressTags.ContainsTag(popupTag))
        {
            sceneHintPopupManager.TryShowPopup(popupTag);
        }
    }
}
