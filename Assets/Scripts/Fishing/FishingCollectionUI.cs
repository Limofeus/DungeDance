using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingCollectionUI : MonoBehaviour
{
    [SerializeField] private int fishesPerRow = 4;
    [SerializeField] private GameObject fishVisualPrefab;
    [SerializeField] private Transform contentScaler;
    [SerializeField] private float inBetweenFishDist;
    [SerializeField] private FishHoverOverHint hoverOverHint;
    private Animator fishCollUIAnimator;
    private bool uiOpen = false;
    private List<FishColPrefab> fishColPrefabList = new List<FishColPrefab>();

    private void Start()
    {
        fishCollUIAnimator = GetComponent<Animator>();
    }
    public void CreateAndUpdateFishicons(FishData[] fishDatas, List<int> thisLocationFishes)
    {
        Debug.Log($"FDL: {fishDatas.Length}, FDL/4: {fishDatas.Length / 4f}");
        int rowsTotal = Mathf.CeilToInt(((float)fishDatas.Length) / ((float)fishesPerRow));
        for(int i = 0; i < fishDatas.Length; i++)
        {
            int horizCoord = i % fishesPerRow;
            int verticalCoord = Mathf.FloorToInt(((float)i) / ((float)fishesPerRow));
            int fprMod = ((verticalCoord + 1 == rowsTotal) && (fishDatas.Length % fishesPerRow != 0)) ? fishDatas.Length % fishesPerRow : fishesPerRow;
            Vector3 spawnCoords = new Vector3(
                (horizCoord * inBetweenFishDist) - ((((fprMod - 1) * inBetweenFishDist) / 2f)),
                -(verticalCoord * inBetweenFishDist) + ((((rowsTotal - 1) * inBetweenFishDist) / 2f)),
                0f
                );
            Debug.Log($"HC: {horizCoord}, VC: {verticalCoord}, VEC{spawnCoords}");
            GameObject fishInstance = Instantiate(fishVisualPrefab, spawnCoords, Quaternion.identity, contentScaler);
            fishInstance.transform.localPosition = spawnCoords;
            FishColPrefab fcp = fishInstance.GetComponent<FishColPrefab>();
            fcp.hoverOverHint = hoverOverHint;
            fishColPrefabList.Add(fcp);
        }
        UpdateFishVisibilities(fishDatas, thisLocationFishes);
    }
    public void UpdateFishVisibilities(FishData[] fishDatas, List<int> thisLocationFishes)
    {
        for(int i = 0; i < fishColPrefabList.Count; i++)
        {
            fishColPrefabList[i].UpdateFishVisibility((FishColPrefab.FishVisib)fishDatas[i].fishUnlockment);
            fishColPrefabList[i].UpdateFishSprite(i);
            if(fishDatas[i].fishUnlockment >= 2)
            {
                fishColPrefabList[i].upperText = LocalisationSystem.GetLocalizedValue($"fish_name_id{i}");
                fishColPrefabList[i].lowerText = LocalisationSystem.GetLocalizedValue($"fish_desc_id{i}");
                fishColPrefabList[i].fishSizeString = LocalisationSystem.GetLocalizedValue("fishing_max_caught_size") + fishDatas[i].maxWeightCaught.ToString("G3");
            }
            else
            {
                fishColPrefabList[i].upperText = "???";
                fishColPrefabList[i].fishSizeString = "";
                if (thisLocationFishes.Contains(i))
                {
                    fishColPrefabList[i].lowerText = LocalisationSystem.GetLocalizedValue("fishing_unknown_fish_this_location");
                }
                else
                {
                    fishColPrefabList[i].lowerText = LocalisationSystem.GetLocalizedValue("fishing_unknown_fish_other_location");
                }
            }
        }
    }
    public void OpenCloseUI()
    {
        uiOpen = !uiOpen;
        if (uiOpen)
        {
            fishCollUIAnimator.SetTrigger("OpenFishUI");
        }
        else
        {
            fishCollUIAnimator.SetTrigger("CloseFishUI");
        }
    }
}
