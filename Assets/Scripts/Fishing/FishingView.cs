using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingView : MonoBehaviour
{
    [SerializeField] private Transform fishArrowsHolder;
    [SerializeField] private Transform winGaugeTransform;
    [SerializeField] private Transform looseGaugeTransform;
    [SerializeField] private GameObject fishArrowPrefab;
    [SerializeField] private float distBetweenArrows;
    [SerializeField] private float posLerpPow;
    [SerializeField] private float winGaugeLerp;
    private List<FishingArrowVisual> fishArrowQueue = new List<FishingArrowVisual>();

    public float winValue = 0f;
    public float looseValue = 0f;

    private void Update()
    {
        UpdateFishArrowQueue();
        UpdateGauges();
    }
    public void AddArrowTofishQueue(FishingModel.FishArrowDir fishArrowDir)
    {
        FishingArrowVisual newArrow = Instantiate(fishArrowPrefab, fishArrowsHolder).GetComponent<FishingArrowVisual>();
        newArrow.SetArrowRotation((int)fishArrowDir);
        newArrow.transform.localPosition = Vector3.down * distBetweenArrows * fishArrowQueue.Count;
        fishArrowQueue.Add(newArrow);
    }

    public void ClickOnLastArrow(bool isCorrectClick)
    {
        fishArrowQueue[0].DeleteArrow(isCorrectClick);
        fishArrowQueue.RemoveAt(0);
    }
    private void UpdateFishArrowQueue()
    {
        if(fishArrowQueue.Count > 0)
        {
            for(int i = 0; i < fishArrowQueue.Count; i++)
            {
                fishArrowQueue[i].transform.localPosition = Vector3.Lerp(fishArrowQueue[i].transform.localPosition, Vector3.down * distBetweenArrows * i, Time.deltaTime * posLerpPow);
            }
        }
    }

    private void UpdateGauges()
    {
        winGaugeTransform.localScale = Vector3.Lerp(winGaugeTransform.localScale, new Vector3(1f, winValue, 1f), Time.deltaTime * winGaugeLerp);
        looseGaugeTransform.localScale = new Vector3(1f, looseValue, 1f);
    }
}
