using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreJoyAdditions
{
    //BONUSES AND MULTIPLIERS
    //Score bonuses
    public float scoreMonsterBonus = 0f; //This one is for when the monster gives player score, not tied to follows
    public float scoreHitType0Bonus = 0f;
    public float scoreHitType1Bonus = 0f;
    public float scoreHitType2Bonus = 0f;
    public float scoreHitType3Bonus = 0f;
    public float scoreAllHitBonus = 0f;
    //Score multipliers
    public float scoreMonsterMultiplier = 0f; //Same as above
    public float scoreHitType0Multiplier = 0f;
    public float scoreHitType1Multiplier = 0f;
    public float scoreHitType2Multiplier = 0f;
    public float scoreHitType3Multiplier = 0f;
    public float scoreAllHitMultiplier = 0f;
    //Joy bonuses
    public float joyHitType0Bonus = 0f;
    public float joyHitType1Bonus = 0f;
    public float joyHitType2Bonus = 0f;
    public float joyHitType3Bonus = 0f;
    public float joyAllHitBonus = 0f;
    //Joy multipliers
    public float joyHitType0Multiplier = 0f;
    public float joyHitType1Multiplier = 0f;
    public float joyHitType2Multiplier = 0f;
    public float joyHitType3Multiplier = 0f;
    public float joyAllHitMultiplier = 0f;
    public float joyDynamicHitMultiplier = 0f;
    //Some other stat changes
    public float followMonsterToScoreMultiplier = 0f;
    public float followMonsterToJoyMultiplier = 0f;
    public float attractionToScoreMultiplier = 0f;
    public float attractionToJoyMultiplier = 0f;
}
public class BuffDebuffItemEffectTrigger : ItemEffectTrigger
{
    [SerializeField] private bool destroyWhenDone;
    [SerializeField] private float duration;
    [SerializeField] private int itemAttrBoostPower;
    [SerializeField] private float itemAttrBoostTime;
    [SerializeField] private ScoreJoyAdditions bonusMultAdd;
    private MainManager mainManager;
    private void Start()
    {
        mainManager = MainManager.thisMainManager;
    }
    public override void OnTrigger()
    {
        TriggerAnim();
        if(itemAttrBoostPower > 0)
        {
            mainManager.ItemAttractionBoost(itemAttrBoostPower, itemAttrBoostTime);
        }
        StartCoroutine(TriggerEffects());
    }

    private IEnumerator TriggerEffects()
    {
        // Applying bonuses and multipliers
        mainManager.bonusesAndMultiplers.scoreMonsterBonus += bonusMultAdd.scoreMonsterBonus;
        mainManager.bonusesAndMultiplers.scoreHitType0Bonus += bonusMultAdd.scoreHitType0Bonus;
        mainManager.bonusesAndMultiplers.scoreHitType1Bonus += bonusMultAdd.scoreHitType1Bonus;
        mainManager.bonusesAndMultiplers.scoreHitType2Bonus += bonusMultAdd.scoreHitType2Bonus;
        mainManager.bonusesAndMultiplers.scoreHitType3Bonus += bonusMultAdd.scoreHitType3Bonus;
        mainManager.bonusesAndMultiplers.scoreAllHitBonus += bonusMultAdd.scoreAllHitBonus;

        mainManager.bonusesAndMultiplers.scoreMonsterMultiplier += bonusMultAdd.scoreMonsterMultiplier;
        mainManager.bonusesAndMultiplers.scoreHitType0Multiplier += bonusMultAdd.scoreHitType0Multiplier;
        mainManager.bonusesAndMultiplers.scoreHitType1Multiplier += bonusMultAdd.scoreHitType1Multiplier;
        mainManager.bonusesAndMultiplers.scoreHitType2Multiplier += bonusMultAdd.scoreHitType2Multiplier;
        mainManager.bonusesAndMultiplers.scoreHitType3Multiplier += bonusMultAdd.scoreHitType3Multiplier;
        mainManager.bonusesAndMultiplers.scoreAllHitMultiplier += bonusMultAdd.scoreAllHitMultiplier;

        mainManager.bonusesAndMultiplers.joyHitType0Bonus += bonusMultAdd.joyHitType0Bonus;
        mainManager.bonusesAndMultiplers.joyHitType1Bonus += bonusMultAdd.joyHitType1Bonus;
        mainManager.bonusesAndMultiplers.joyHitType2Bonus += bonusMultAdd.joyHitType2Bonus;
        mainManager.bonusesAndMultiplers.joyHitType3Bonus += bonusMultAdd.joyHitType3Bonus;
        mainManager.bonusesAndMultiplers.joyAllHitBonus += bonusMultAdd.joyAllHitBonus;

        mainManager.bonusesAndMultiplers.joyHitType0Multiplier += bonusMultAdd.joyHitType0Multiplier;
        mainManager.bonusesAndMultiplers.joyHitType1Multiplier += bonusMultAdd.joyHitType1Multiplier;
        mainManager.bonusesAndMultiplers.joyHitType2Multiplier += bonusMultAdd.joyHitType2Multiplier;
        mainManager.bonusesAndMultiplers.joyHitType3Multiplier += bonusMultAdd.joyHitType3Multiplier;
        mainManager.bonusesAndMultiplers.joyAllHitMultiplier += bonusMultAdd.joyAllHitMultiplier;
        mainManager.bonusesAndMultiplers.joyDynamicHitMultiplier += bonusMultAdd.joyDynamicHitMultiplier;

        mainManager.bonusesAndMultiplers.followMonsterToScoreMultiplier += bonusMultAdd.followMonsterToScoreMultiplier;
        mainManager.bonusesAndMultiplers.followMonsterToJoyMultiplier += bonusMultAdd.followMonsterToJoyMultiplier;
        mainManager.bonusesAndMultiplers.attractionToScoreMultiplier += bonusMultAdd.attractionToScoreMultiplier;
        mainManager.bonusesAndMultiplers.attractionToJoyMultiplier += bonusMultAdd.attractionToJoyMultiplier;

        yield return new WaitForSeconds(duration);

        // Removing bonuses and multipliers
        mainManager.bonusesAndMultiplers.scoreMonsterBonus -= bonusMultAdd.scoreMonsterBonus;
        mainManager.bonusesAndMultiplers.scoreHitType0Bonus -= bonusMultAdd.scoreHitType0Bonus;
        mainManager.bonusesAndMultiplers.scoreHitType1Bonus -= bonusMultAdd.scoreHitType1Bonus;
        mainManager.bonusesAndMultiplers.scoreHitType2Bonus -= bonusMultAdd.scoreHitType2Bonus;
        mainManager.bonusesAndMultiplers.scoreHitType3Bonus -= bonusMultAdd.scoreHitType3Bonus;
        mainManager.bonusesAndMultiplers.scoreAllHitBonus -= bonusMultAdd.scoreAllHitBonus;

        mainManager.bonusesAndMultiplers.scoreMonsterMultiplier -= bonusMultAdd.scoreMonsterMultiplier;
        mainManager.bonusesAndMultiplers.scoreHitType0Multiplier -= bonusMultAdd.scoreHitType0Multiplier;
        mainManager.bonusesAndMultiplers.scoreHitType1Multiplier -= bonusMultAdd.scoreHitType1Multiplier;
        mainManager.bonusesAndMultiplers.scoreHitType2Multiplier -= bonusMultAdd.scoreHitType2Multiplier;
        mainManager.bonusesAndMultiplers.scoreHitType3Multiplier -= bonusMultAdd.scoreHitType3Multiplier;
        mainManager.bonusesAndMultiplers.scoreAllHitMultiplier -= bonusMultAdd.scoreAllHitMultiplier;

        mainManager.bonusesAndMultiplers.joyHitType0Bonus -= bonusMultAdd.joyHitType0Bonus;
        mainManager.bonusesAndMultiplers.joyHitType1Bonus -= bonusMultAdd.joyHitType1Bonus;
        mainManager.bonusesAndMultiplers.joyHitType2Bonus -= bonusMultAdd.joyHitType2Bonus;
        mainManager.bonusesAndMultiplers.joyHitType3Bonus -= bonusMultAdd.joyHitType3Bonus;
        mainManager.bonusesAndMultiplers.joyAllHitBonus -= bonusMultAdd.joyAllHitBonus;

        mainManager.bonusesAndMultiplers.joyHitType0Multiplier -= bonusMultAdd.joyHitType0Multiplier;
        mainManager.bonusesAndMultiplers.joyHitType1Multiplier -= bonusMultAdd.joyHitType1Multiplier;
        mainManager.bonusesAndMultiplers.joyHitType2Multiplier -= bonusMultAdd.joyHitType2Multiplier;
        mainManager.bonusesAndMultiplers.joyHitType3Multiplier -= bonusMultAdd.joyHitType3Multiplier;
        mainManager.bonusesAndMultiplers.joyAllHitMultiplier -= bonusMultAdd.joyAllHitMultiplier;
        mainManager.bonusesAndMultiplers.joyDynamicHitMultiplier -= bonusMultAdd.joyDynamicHitMultiplier;

        mainManager.bonusesAndMultiplers.followMonsterToScoreMultiplier -= bonusMultAdd.followMonsterToScoreMultiplier;
        mainManager.bonusesAndMultiplers.followMonsterToJoyMultiplier -= bonusMultAdd.followMonsterToJoyMultiplier;
        mainManager.bonusesAndMultiplers.attractionToScoreMultiplier -= bonusMultAdd.attractionToScoreMultiplier;
        mainManager.bonusesAndMultiplers.attractionToJoyMultiplier -= bonusMultAdd.attractionToJoyMultiplier;

        yield return new WaitForSeconds(nextTriggerDelay);

        nextTrigger?.OnTrigger();
        if (destroyWhenDone)
        {
            Destroy(gameObject);
        }
    }
}
