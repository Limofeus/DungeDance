using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingController : MonoBehaviour, FourDirectionInputTarget
{
    [SerializeField] private FishingModel fishingModel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryHookFish();
        }
    }

    public void StartFishingPressed()
    {
        fishingModel.StartFishing();
    }

    public void TryHookFish()
    {
        fishingModel.TryHookFish();
    }

    public void OnFourDirInput(FourDirectionInputTarget.FourArrowDir direction)
    {
        fishingModel.OnFourDirInput(direction);
    }
}
