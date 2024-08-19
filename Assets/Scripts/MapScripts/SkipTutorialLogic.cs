using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipTutorialLogic : MonoBehaviour
{
    [SerializeField] private int[] tutorialLevelIds;
    [SerializeField] private int[] itemIdsToUnlock;
    [SerializeField] private MapContentAnimAppear[] mapContentAnimAppears;
    public void SkipTutoral()
    {
        foreach(var levelId in tutorialLevelIds)
        {
            MenuDataManager.saveData.levelDatas[levelId].completed = true;
        }
        foreach(int itemId in itemIdsToUnlock)
        {
            MenuDataManager.saveData.itemUnlockDatas[itemId] = System.Math.Max(1, MenuDataManager.saveData.itemUnlockDatas[itemId]);
        }
        SaveSystem.Save(MenuDataManager.saveData);
        foreach(var mcA in mapContentAnimAppears)
        {
            mcA.CheckAndAnimate();
        }
    }
}
