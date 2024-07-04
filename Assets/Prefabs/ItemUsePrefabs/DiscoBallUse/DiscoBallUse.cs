using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoBallUse : MonoBehaviour
{
    public float itemUseTime;
    private MainManager mainManager;

    private void Start()
    {
        mainManager = MainManager.thisMainManager;
        mainManager.ItemAttractionBoost(5, itemUseTime);
        mainManager.bonusesAndMultiplers.scoreHitType1Bonus += 0.5f;
        mainManager.bonusesAndMultiplers.scoreHitType2Bonus += 1f;
        mainManager.bonusesAndMultiplers.scoreHitType3Bonus += 2f;
        mainManager.bonusesAndMultiplers.attractionToScoreMultiplier += 0.3f;
        StartCoroutine(Use());
    }

    private IEnumerator Use()
    {
        yield return new WaitForSeconds(itemUseTime);
        mainManager.bonusesAndMultiplers.scoreHitType1Bonus -= 0.5f;
        mainManager.bonusesAndMultiplers.scoreHitType2Bonus -= 1f;
        mainManager.bonusesAndMultiplers.scoreHitType3Bonus -= 2f;
        mainManager.bonusesAndMultiplers.attractionToScoreMultiplier -= 0.3f;
        Destroy(gameObject);
    }
}
