using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FishingDrop
{
    public bool isItem;
    [SerializeField] private int fishOrItemId;

    [SerializeField] private float fishArrowCycleTime; //Time for full arrow cycle (time it takes for fish to create fishArrowsInCycle arrows)
    [SerializeField] private int fishArrowsInCycle; //Ammount of arrows in 1 cycle
    [SerializeField] private float perCycleDispersion; //0 - instant per cycle, all time after cycles | 1 - same time between cycles and reloading
    [SerializeField] private float arrowDirChangeChance; // 0-1 chance that next cycle arrow points in different direction
    [SerializeField] private int arrowsToOverflow; //Amount of arrows at which overflow starts having effect
    [SerializeField] private float constLooseRate; // How fast looseGauge moves towards 0
    [SerializeField] private float looseRateDuringOverflow; // How fast looseGauge moves towards 0 during overflow (added to constLooseRate)
    [SerializeField] private float winAddition; //Value added to winGauge on correct click
    [SerializeField] private float winSubtraction; //Value subtracted off of winGauge on incorrect click

    public float GetLooseRate(int currentArrows)
    {
        return constLooseRate + ((currentArrows >= arrowsToOverflow) ? looseRateDuringOverflow : 0f);
    }

    public float GetWinRateOnClick(bool correctClick)
    {
        return correctClick ? winAddition : -winSubtraction;
    }
}
public class FishingModel : MonoBehaviour, FourDirectionInputTarget
{
    public enum FishArrowDir { Up, Right, Down, Left } //Idk, I'm feeling sick rn, should do smth about this later..........

    [SerializeField] private bool canDropFish;
    [SerializeField] private bool canDropItem;
    [SerializeField] private float itemToFishRatio;
    [SerializeField] private FishingDrop[] fishingDrops;
    [SerializeField] private Vector2 minMaxFishingTime;
    [SerializeField] private float fishEscapeTime;

    private float looseGauge = 1f;
    private float winGauge = 0f;

    private bool isFishing = false;
    private float fishAppearTime;
    private FishingDrop currentFishingDrop;
    private List<FishArrowDir> fishArrowQueue = new List<FishArrowDir>();

    private void Update()
    {
        FishingUpdate(Time.deltaTime);
    }
    public void StartFishing()
    {
        if (isFishing) return;
        isFishing = true;
        currentFishingDrop = ChooseFish();
        looseGauge = 1f;
        winGauge = 0f;
        fishAppearTime = Time.time + Random.Range(minMaxFishingTime.x, minMaxFishingTime.y);
    }
    private void FishingUpdate(float deltaTime)
    {
        looseGauge -= currentFishingDrop.GetLooseRate(fishArrowQueue.Count) * deltaTime;
    }
    private FishingDrop ChooseFish()
    {
        List<FishingDrop> fishDrops = new List<FishingDrop>();
        List<FishingDrop> itemDrops = new List<FishingDrop>();
        foreach(FishingDrop fishingDrop in fishingDrops)
        {
            if (fishingDrop.isItem)
                itemDrops.Add(fishingDrop);
            else
                fishDrops.Add(fishingDrop);
        }
        List<FishingDrop> possibleDrops = new List<FishingDrop>();
        if(Random.Range(0f, 1f) < itemToFishRatio && itemDrops.Count > 0)
        {
            possibleDrops = itemDrops; //Update this thing later so that it accounts for item unlockment;
        }
        else
        {
            possibleDrops = fishDrops;
        }
        return possibleDrops[Random.Range(0, possibleDrops.Count)];
    }

    public void OnFourDirInput(FourDirectionInputTarget.FourArrowDir direction)
    {
        throw new System.NotImplementedException();
    }
}
