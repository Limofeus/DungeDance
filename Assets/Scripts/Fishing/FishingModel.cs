using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FishingDrop
{
    public bool isItem;
    [SerializeField] public int fishOrItemId;

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
    [SerializeField] private FishingCollectionUI fishCollectionUI;

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

    private List<int> thisLevelFishIds = new List<int>();

    private float fishArrowCycleTimer;
    private IEnumerator fishCycleCoroutine;

    private FishArrowDir currentFishArrowDir;
    [SerializeField] private SaveData saveData;

    private void Start()
    {
        currentFishArrowDir = (FishArrowDir)Random.Range(0, 4);
        LoadSaveData();
        if (canDropFish)
        {
            foreach(FishingDrop fishingDrop in fishingDrops)
            {
                if (!fishingDrop.isItem)
                {
                    thisLevelFishIds.Add(fishingDrop.fishOrItemId);
                    saveData.fishDatas[fishingDrop.fishOrItemId].UpdateUnlockment(1);
                }
            }
        }
        ApplySaveData();
        fishCollectionUI.CreateAndUpdateFishicons(saveData.fishDatas, thisLevelFishIds);
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
        fishView.SendAnimatorTrigger("Casat");
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

            if(winGauge >= 1f)
            {
                FishCought();
            }

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

                if (looseGauge <= 0f)
                {
                    FishEscaped();
                }
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
        fishView.SendAnimatorTrigger("HookFish");
        currentFishArrowDir = (FishArrowDir)Random.Range(0, 4);
        canHookFish = false;
        fishHooked = true;
        StartFishCycle();
    }
    private void FalseHook()
    {
        Debug.Log("РЫБЫ НЕТ АЛЁ!");
        FishEscaped();
    }
    private void FishCought()
    {
        ResetState();
        if (!currentFishingDrop.isItem)
        {
            saveData.fishDatas[currentFishingDrop.fishOrItemId].UpdateUnlockment(2);
            ApplySaveData();
            fishCollectionUI.UpdateFishVisibilities(saveData.fishDatas, thisLevelFishIds);
        }
        else
        {
            //Рофлянычи с предметами нужгны (Типа чтоб новые открывалсь и выпадались при получении)
        }
        Debug.Log("Рыба поймана");
        fishView.SendAnimatorTrigger("FishCaught");
        fishView.UpdateItemCaughtSR(currentFishingDrop.isItem, currentFishingDrop.fishOrItemId);
    }
    private void ResetState()
    {
        isFishing = false;
        canHookFish = false;
        fishArrowQueue = new List<FishArrowDir>();
        if (fishCycleCoroutine != null)
            StopCoroutine(fishCycleCoroutine);
        looseGauge = 1f;
        winGauge = 0f;
        fishView.looseValue = 0f;
        fishView.winValue = 0f;
        fishView.ResetVisual();
    }
    private void FishEscaped()
    {
        ResetState();
        Debug.Log(fishView.looseValue);
        Debug.Log("Рыба улетела");
        fishView.SendAnimatorTrigger("FishLost");
        Debug.Log(fishView.looseValue);
    }
    private void FishHookable()
    {
        canHookFish = true;
        fishView.SendAnimatorTrigger("FishKlyn");
        Debug.Log("ПОДСЕКАЙ!!");
    }
    private void LoadSaveData()
    {
        if (MenuDataManager.saveData != null)
            saveData = new SaveData(MenuDataManager.saveData);
        else
            saveData = SaveSystem.Load();
    }
    private void ApplySaveData()
    {
        MenuDataManager.saveData = saveData;
        SaveSystem.Save(MenuDataManager.saveData);
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
