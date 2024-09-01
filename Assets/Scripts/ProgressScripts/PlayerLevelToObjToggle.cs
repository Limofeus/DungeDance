using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelToObjToggle : MonoBehaviour
{
    [SerializeField] private int playerLevelAtWhichToChannge = 1;
    [SerializeField] private GameObject lowerLevelObj;
    [SerializeField] private GameObject higherLevelObj;

    void Start()
    {
        if(MenuDataManager.saveData == null)
        {
            MenuDataManager.saveData = SaveSystem.Load();
        }
        int playerLevel = MenuDataManager.saveData.playerLevel + 1;

        if(playerLevel >= playerLevelAtWhichToChannge)
        {
            lowerLevelObj.SetActive(false);
            higherLevelObj.SetActive(true);
        }
        else
        {
            higherLevelObj.SetActive(false);
            lowerLevelObj.SetActive(true);
        }
    }
}
