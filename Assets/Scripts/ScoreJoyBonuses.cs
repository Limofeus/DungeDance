using UnityEngine;

public class ScoreJoyBonuses
{
    //BONUSES AND MULTIPLIERS
    //Score bonuses
    [HideInInspector] public float scoreMonsterBonus = 0f; //This one is for when the monster gives player score, not tied to follows
    [HideInInspector] public float scoreHitType0Bonus = 0f;
    [HideInInspector] public float scoreHitType1Bonus = 0f;
    [HideInInspector] public float scoreHitType2Bonus = 0f;
    [HideInInspector] public float scoreHitType3Bonus = 0f;
    [HideInInspector] public float scoreAllHitBonus = 0f;
    //Score multipliers
    [HideInInspector] public float scoreMonsterMultiplier = 1f; //Same as above
    [HideInInspector] public float scoreHitType0Multiplier = 1f;
    [HideInInspector] public float scoreHitType1Multiplier = 1f;
    [HideInInspector] public float scoreHitType2Multiplier = 1f;
    [HideInInspector] public float scoreHitType3Multiplier = 1f;
    [HideInInspector] public float scoreAllHitMultiplier = 1f;
    //Joy bonuses
    [HideInInspector] public float joyHitType0Bonus = 0f;
    [HideInInspector] public float joyHitType1Bonus = 0f;
    [HideInInspector] public float joyHitType2Bonus = 0f;
    [HideInInspector] public float joyHitType3Bonus = 0f;
    [HideInInspector] public float joyAllHitBonus = 0f;
    //Joy multipliers
    [HideInInspector] public float joyHitType0Multiplier = 1f;
    [HideInInspector] public float joyHitType1Multiplier = 1f;
    [HideInInspector] public float joyHitType2Multiplier = 1f;
    [HideInInspector] public float joyHitType3Multiplier = 1f;
    [HideInInspector] public float joyAllHitMultiplier = 1f;
    [HideInInspector] public float joyDynamicHitMultiplier = 1f;
    //Some other stat changes
    [HideInInspector] public float followMonsterToScoreMultiplier = 1f;
    [HideInInspector] public float followMonsterToJoyMultiplier = 1f;
    [HideInInspector] public float attractionToScoreMultiplier = 1f;
    [HideInInspector] public float attractionToJoyMultiplier = 1f;
}
