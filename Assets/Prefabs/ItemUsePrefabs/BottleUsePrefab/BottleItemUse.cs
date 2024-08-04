using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleItemUse : MonoBehaviour
{
    [SerializeField] private float timeBeforeActivation;
    [SerializeField] private float timeTillDeactivation;
    [SerializeField] private float timeTillDeletion;
    private MainManager mainManager;
    void Start()
    {
        mainManager = MainManager.thisMainManager;
        StartCoroutine(UseItem());
    }

    private IEnumerator UseItem()
    {
        yield return new WaitForSeconds(timeBeforeActivation);
        mainManager.ItemAttractionBoost(12, 15f);
        mainManager.bonusesAndMultiplers.attractionToJoyMultiplier += 0.3f;
        mainManager.bonusesAndMultiplers.joyHitType0Bonus += 0.3f;
        mainManager.bonusesAndMultiplers.joyHitType1Bonus += 0.2f;
        mainManager.bonusesAndMultiplers.joyHitType2Bonus += 0.1f;
        mainManager.bonusesAndMultiplers.joyHitType3Bonus += 0.1f;
        yield return new WaitForSeconds(timeTillDeactivation);
        mainManager.bonusesAndMultiplers.attractionToJoyMultiplier -= 0.3f;
        mainManager.bonusesAndMultiplers.joyHitType0Bonus -= 0.3f;
        mainManager.bonusesAndMultiplers.joyHitType1Bonus -= 0.2f;
        mainManager.bonusesAndMultiplers.joyHitType2Bonus -= 0.1f;
        mainManager.bonusesAndMultiplers.joyHitType3Bonus -= 0.1f;
        yield return new WaitForSeconds(timeTillDeletion);
        Destroy(gameObject);
    }
}
