using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FishingDrop
{
    public bool isItem;
    [SerializeField] private int fishOrItemId;

    [SerializeField] public float fishArrowCycleTime; //Time for full arrow cycle (time it takes for fish to create fishArrowsInCycle arrows)
    [SerializeField] public int fishArrowsInCycle; //Ammount of arrows in 1 cycle
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

    public float GetTimeBetweenArrows()
    {
        return (fishArrowCycleTime / (float)fishArrowsInCycle) * Mathf.Clamp01(perCycleDispersion);
    }

    public FishingModel.FishArrowDir GetNextArrowDir(FishingModel.FishArrowDir currentFisharrowDir)
    {
        if(Random.Range(0f, 1f) <= arrowDirChangeChance)
        {
            List<int> possibleDirChanges = new List<int>() { 0, 1, 2, 3 };
            possibleDirChanges.Remove((int)currentFisharrowDir);
            return (FishingModel.FishArrowDir)possibleDirChanges[Random.Range(0, 3)];
        }
        else
        {
            return currentFisharrowDir;
        }
    }
}
public class FishingModel : MonoBehaviour
{
    public enum FishArrowDir { Up = 0, Right = 1, Down = 2, Left = 3 } //Idk, I'm feeling sick rn, should do smth about this later..........

    [SerializeField] private FishingView fishView;

    [SerializeField] private bool canDropFish;
    [SerializeField] private bool canDropItem;
    [SerializeField] private float itemToFishRatio;
    [SerializeField] private FishingDrop[] fishingDrops;
    [SerializeField] private Vector2 minMaxFishingTime;
    [SerializeField] private float fishEscapeTime;
    [SerializeField] private int maxArrowsInQueue;

    private float looseGauge = 1f;
    private float winGauge = 0f;

    private bool isFishing = false;
    private bool canHookFish = false;
    private bool fishHooked = false;
    private float fishAppearTime;
    private FishingDrop currentFishingDrop;
    private List<FishArrowDir> fishArrowQueue = new List<FishArrowDir>();

    private float fishArrowCycleTimer;
    private IEnumerator fishCycleCoroutine;

    private FishArrowDir currentFishArrowDir;

    private void Start()
    {
        currentFishArrowDir = (FishArrowDir)Random.Range(0, 4);
    }

    private void Update()
    {
        FishingUpdate(Time.deltaTime);
    }
    public void StartFishing()
    {
        Debug.Log("Starting fishing");
        if (isFishing) return;
        isFishing = true;
        fishHooked = false;
        canHookFish = false;
        currentFishingDrop = ChooseFish();
        looseGauge = 1f;
        winGauge = 0f;
        fishAppearTime = Time.time + Random.Range(minMaxFishingTime.x, minMaxFishingTime.y);
    }
    public void TryHookFish()
    {
        if (!fishHooked)
        {
            if (canHookFish)
            {
                HookFish();
            }
            else
            {
                FalseHook();
            }
        }
    }
    public void OnFourDirInput(FourDirectionInputTarget.FourArrowDir direction)
    {
        if (fishHooked && fishArrowQueue.Count > 0)
        {
            bool isCorrectClick = ((int)direction == (int)fishArrowQueue[0]);
            winGauge = Mathf.Clamp01(winGauge + currentFishingDrop.GetWinRateOnClick(isCorrectClick));
            fishArrowQueue.RemoveAt(0);
            fishView.ClickOnLastArrow(isCorrectClick);
            fishView.winValue = winGauge;
        }
    }
    private void FishingUpdate(float deltaTime)
    {
        if (isFishing)
        {
            if (fishHooked)
            {
                looseGauge -= currentFishingDrop.GetLooseRate(fishArrowQueue.Count) * deltaTime;
                fishView.looseValue = looseGauge;
                fishArrowCycleTimer -= Time.deltaTime;
                if(fishArrowCycleTimer <= 0f)
                {
                    StartFishCycle();
                }
            }
            else
            {
                if (Time.time > fishAppearTime)
                {
                    if (Time.time > fishAppearTime + fishEscapeTime)
                    {
                        FishEscaped();
                    }
                    else
                    {
                        if (!canHookFish)
                            FishHookable();
                    }
                }
            }
        }
    }
    private void HookFish()
    {
        currentFishArrowDir = (FishArrowDir)Random.Range(0, 4);
        canHookFish = false;
        fishHooked = true;
        StartFishCycle();
    }
    private void FalseHook()
    {
        Debug.Log("–€¡€ Õ≈“ ¿À®!");
        FishEscaped();
    }
    private void FishEscaped()
    {
        isFishing = false;
        canHookFish = false;
        Debug.Log("–˚·‡ ÛÎÂÚÂÎ‡");
    }
    private void FishHookable()
    {
        canHookFish = true;
        Debug.Log("œŒƒ—≈ ¿…!!");
    }
    private FishingDrop ChooseFish()
    {
        List<FishingDrop> fishDrops = new List<FishingDrop>();
        List<FishingDrop> itemDrops = new List<FishingDrop>();
        foreach (FishingDrop fishingDrop in fishingDrops)
        {
            if (fishingDrop.isItem)
                itemDrops.Add(fishingDrop);
            else
                fishDrops.Add(fishingDrop);
        }
        List<FishingDrop> possibleDrops = new List<FishingDrop>();
        if (Random.Range(0f, 1f) < itemToFishRatio && itemDrops.Count > 0)
        {
            possibleDrops = itemDrops; //Update this thing later so that it accounts for item unlockment;
        }
        else
        {
            possibleDrops = fishDrops;
        }
        return possibleDrops[Random.Range(0, possibleDrops.Count)];
    }

    private void AddFishingArrow()
    {
        if(fishArrowQueue.Count < maxArrowsInQueue)
        {
            Debug.Log("FishArrowAdded");
            currentFishArrowDir = currentFishingDrop.GetNextArrowDir(currentFishArrowDir);
            Debug.Log($"New FAD: {currentFishArrowDir}");
            fishArrowQueue.Add(currentFishArrowDir);
            fishView.AddArrowTofishQueue(currentFishArrowDir);
        }
    }
    private void StartFishCycle()
    {
        Debug.Log("FishCycle started");
        fishArrowCycleTimer = currentFishingDrop.fishArrowCycleTime;
        if(fishCycleCoroutine != null)
            StopCoroutine(fishCycleCoroutine);
        fishCycleCoroutine = FishCycleCoroutine();
        StartCoroutine(fishCycleCoroutine);
    }

    private IEnumerator FishCycleCoroutine()
    {
        int fishArrowsLeft = currentFishingDrop.fishArrowsInCycle;
        while(fishArrowsLeft > 0)
        {
            fishArrowsLeft--;
            AddFishingArrow();
            yield return new WaitForSeconds(currentFishingDrop.GetTimeBetweenArrows());
        }
    }
}
